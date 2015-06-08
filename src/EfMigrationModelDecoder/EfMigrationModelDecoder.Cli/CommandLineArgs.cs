using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CmdLine;

namespace EfMigrationModelDecoder.Cli
{
    [CommandLineArguments(Program = "EfMigrationModelDecoder", Title = "EF Migration Model Decoder sample:", Description = "")]
    public class CommandLineArgs
    {
        [CommandLineParameter(Command = "?", Default = false, Description = "Show Help", Name = "Help", IsHelp = true)]
        public bool Help { get; set; }

        [CommandLineParameter(Name = "connectionString", ParameterIndex = 1, Required = true, Description = "Specifies connection string for the database.")]
        public string ConnectionString { get; set; }

        [CommandLineParameter(Command = "outFile", ParameterIndex = 2, Description = "Specifies the output filename.", Default = "Model.edmx")]
        public string OutFileName { get; set; }

        [CommandLineParameter(Command = "migration", ParameterIndex = 3, Description = "Specifies migrationId or migration number (supports negative values to count from the end).", Default = "-1")]
        public string Migration { get; set; }


    } 
}
