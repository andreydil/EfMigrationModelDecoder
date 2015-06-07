using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EfMigrationModelDecoder.Core;

namespace EfMigrationModelDecoder.Cli
{
    class Program
    {
        static void Main(string[] args)
        {
            var modelDecoder = new ModelDecoder(@"Data Source=.\SQL2012;Initial Catalog=ModPacVMIAzure;User ID=sa;Password=sasasa;MultipleActiveResultSets=True");
            //var result = modelDecoder.GetModelByMigrationId("201505261030122_AddImportStatusToProduct");
            var result = modelDecoder.GetModelMyMigrationNumber(-1);
            if (!string.IsNullOrEmpty(result.ErrorText))
            {
                Console.WriteLine(result.ErrorText);
            }
        }

    }
}
