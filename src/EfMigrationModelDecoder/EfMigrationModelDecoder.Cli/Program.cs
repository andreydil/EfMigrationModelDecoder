using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
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

            var modelDecoder = new ModelDecoder(cmdArgs.ConnectionString);

            int migrationNum;
            ModelDecodeResult result = int.TryParse(cmdArgs.MigrationId, out migrationNum) 
                ? modelDecoder.GetModelByMigrationNumber(migrationNum)
                : modelDecoder.GetModelByMigrationId(cmdArgs.MigrationId);
            
            if (!string.IsNullOrEmpty(result.ErrorText))
            {
                Console.WriteLine(result.ErrorText);
            }

            try
            {
                result.Edmx.Save(cmdArgs.OutFileName);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to save EDMX file: "+ ex.Message);
            }
            
        }

    }
}
