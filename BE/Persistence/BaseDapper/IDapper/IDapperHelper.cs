using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.BaseDapper.IDapper
{
    public interface IDapperHelper
    {
        Task ExecuteNotReturn(string query, DynamicParameters parameters = null, IDbTransaction dbTransaction = null);
        Task<T> ExecuteReturnScalar<T>(string query, DynamicParameters parameters = null, IDbTransaction dbTransaction = null);
        Task<IEnumerable<T>> ExecuteSqlReturnList<T>(string query, DynamicParameters parameters = null, IDbTransaction dbTransaction = null);
        Task<IEnumerable<T>> ExecuteStoreProcedureReturnList<T>(string query, DynamicParameters parameters = null, IDbTransaction dbTransaction = null);
    }
}
