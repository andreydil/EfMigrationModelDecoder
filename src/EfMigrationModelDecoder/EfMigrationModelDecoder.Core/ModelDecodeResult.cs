using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace EfMigrationModelDecoder.Core
{
    public class ModelDecodeResult
    {
        public XDocument Edmx { get; set; }
        public string ErrorText { get; set; }
        public string MigrationId { get; set; }
    }
}
