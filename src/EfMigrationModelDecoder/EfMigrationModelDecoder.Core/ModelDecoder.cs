using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations.History;
using System.Data.SqlClient;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace EfMigrationModelDecoder.Core
{
    public class ModelDecoder
    {
        private readonly string _connectionString;

        public ModelDecoder(string connectionString)
        {
            _connectionString = connectionString;
        }

        public ModelDecodeResult GetModelByMigrationNumber(int num)
        {
            var migrationsIds = GetMigrationIds();
            if ((num >= 0 && num > migrationsIds.Length - 1)
                || num < 0 && num < - migrationsIds.Length)
            {
                return new ModelDecodeResult
                {
                    ErrorText = string.Format("Cannot get migration #{0}, there are only {1} migrations in the database", num.ToString(), migrationsIds.Length.ToString()),
                };
            }

            var effectiveNum = num >= 0 ? num : num + migrationsIds.Length;

            var migrationId = migrationsIds[effectiveNum];
            var model = GetModel(migrationId);
            return new ModelDecodeResult
            {
                Edmx = Decode(model),
                MigrationId = migrationId,
            };
        }

        public ModelDecodeResult GetModelByMigrationId(string migrationId)
        {
            var model = GetModel(migrationId);
            if (model == null)
            {
                return new ModelDecodeResult
                {
                    ErrorText = string.Format("Migration with id {0} not found.", migrationId),
                };
            }
            return new ModelDecodeResult
            {
                Edmx = Decode(model),
                MigrationId = migrationId,
            };
        }

        private byte[] GetModel(string migrationId)
        {
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                using (var historyContext = new HistoryContext(sqlConnection, "dbo"))
                {
                    var migration = historyContext.History.SingleOrDefault(m => m.MigrationId == migrationId)                   //try full name
                                 ?? historyContext.History.SingleOrDefault(m => m.MigrationId.Substring(16) == migrationId);    //try name without timestamp
                    return migration == null ? null : migration.Model;
                }
            }
        }

        private string[] GetMigrationIds()
        {
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                using (var historyContext = new HistoryContext(sqlConnection, "dbo"))
                {
                    return historyContext.History.Select(m => m.MigrationId).ToArray();
                }
            }
        }

        private XDocument Decode(byte[] model)
        {
            using (var memoryStream = new MemoryStream(model))
            {
                using (var gzipStream = new GZipStream(memoryStream, CompressionMode.Decompress))
                {
                    return XDocument.Load(gzipStream);
                }
            }
        }

    }
}
