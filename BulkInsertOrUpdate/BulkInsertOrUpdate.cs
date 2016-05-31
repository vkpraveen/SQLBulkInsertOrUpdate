using Microsoft.SqlServer.Server;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace BulkInsertOrUpdate
{
    public class BulkInsertOrUpdate : IBulkInsertOrUpdate
    {
        public string ConnectionString { get; private set; }

        public BulkInsertOrUpdate(string connectionString)
        {
            ConnectionString = connectionString;
        }
        public int BulkInsertUpdate(List<MyBulkClass> recordsList)
        {
            int rowsEffected = 0;

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "dbo.USP_BulkInsertOrUpdate";
                    command.CommandType = CommandType.StoredProcedure;
                    var AdesaUpdateChromeStyleIDParameter = command.Parameters.AddWithValue("@P_MyCustomType", CreateSqlDataRecords(recordsList));
                    AdesaUpdateChromeStyleIDParameter.SqlDbType = SqlDbType.Structured;
                    AdesaUpdateChromeStyleIDParameter.TypeName = "dbo.MyCustomType";
                    command.ExecuteNonQuery();
                }
            }

            return rowsEffected;
        }

        private IEnumerable<SqlDataRecord> CreateSqlDataRecords(List<MyBulkClass> request)
        {
            SqlMetaData[] metaData = new SqlMetaData[2];
            metaData[0] = new SqlMetaData("Id", SqlDbType.Int);
            metaData[1] = new SqlMetaData("RandomString", SqlDbType.Text);

            foreach (var updateRecord in request)
            {

                SqlDataRecord record = new SqlDataRecord(metaData);
                record.SetInt32(0, updateRecord.Id);
                record.SetString(1, updateRecord.RandomString);

                yield return record;
            }
        }
    }
}