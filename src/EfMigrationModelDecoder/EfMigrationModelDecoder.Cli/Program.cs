using System;
using System.IO;
using CmdLine;
using EfMigrationModelDecoder.Core;

namespace EfMigrationModelDecoder.Cli
{
    class Program
    {
        static void Main(string[] args)
        {
            CommandLineArgs cmdArgs;
            try
            {
                cmdArgs = CommandLine.Parse<CommandLineArgs>();
            }
            catch (CommandLineException exception)
            {
                Console.WriteLine(exception.ArgumentHelp.Message);
                Console.WriteLine();
                Console.WriteLine(exception.ArgumentHelp.GetHelpText(Console.BufferWidth));
                return;
            }

            Console.WriteLine();
            Console.WriteLine("Reading migration...");

            var modelDecoder = new ModelDecoder(cmdArgs.ConnectionString);
            int migrationNum;
            ModelDecodeResult result = int.TryParse(cmdArgs.Migration, out migrationNum) 
                ? modelDecoder.GetModelByMigrationNumber(migrationNum)
                : modelDecoder.GetModelByMigrationId(cmdArgs.Migration);
            
            if (!string.IsNullOrEmpty(result.ErrorText))
            {
                Console.WriteLine(result.ErrorText);
            }

            try
            {
                Console.WriteLine(string.Concat("Writing EDMX for migrationId \"", result.MigrationId, "\" to \"", Path.GetFullPath(cmdArgs.OutFileName), "\"..."));
                result.Edmx.Save(cmdArgs.OutFileName);
                Console.WriteLine("Done.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to save EDMX file: "+ ex.Message);
            }
            
        }

    }
}
