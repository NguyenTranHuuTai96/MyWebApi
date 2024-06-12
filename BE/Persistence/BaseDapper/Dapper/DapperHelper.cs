using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Persistence.BaseDapper.IDapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Persistence.BaseDapper.Dapper
{
    public class DapperHelper : IDapperHelper
    {
        private IDbConnection connection;
        public string strCon = string.Empty;

        public DapperHelper(IConfiguration configuration)
        {
            strCon = configuration.GetConnectionString("MySQLServerConnection") ?? "";
            connection = new SqlConnection(strCon);
        }
        public async Task ExecuteNotReturn(string query, DynamicParameters parameters = null, IDbTransaction dbTransaction = null)
        {
            using (var dbCon = new SqlConnection(strCon))
            {
                await dbCon.ExecuteAsync(query, parameters, dbTransaction, commandType: CommandType.Text);
            }
        }

        public async Task<T> ExecuteReturnScalar<T>(string query, DynamicParameters parameters = null, IDbTransaction dbTransaction = null)
        {
            using (var dbCon = new SqlConnection(strCon))
            {
                return await dbCon.ExecuteScalarAsync<T>(query, parameters, dbTransaction, commandType: CommandType.Text);

            }
        }

        public async Task<IEnumerable<T>> ExecuteSqlReturnList<T>(string query, DynamicParameters parameters = null, IDbTransaction dbTransaction = null)
        {
            using (var dbCon = new SqlConnection(strCon))
            {
                return await dbCon.QueryAsync<T>(query, parameters, dbTransaction, commandType: CommandType.Text);
            }
        }

        public async Task<IEnumerable<T>> ExecuteStoreProcedureReturnList<T>(string query, DynamicParameters parameters = null, IDbTransaction dbTransaction = null)
        {
            using (var dbCon = new SqlConnection(strCon))
            {
                return await dbCon.QueryAsync<T>(query, parameters, dbTransaction, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
