using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.IRepositories
{
    public interface IDistributedCacheService
    {
        Task<T> Get<T>(string key);
        Task Remove(string key);
        Task Set<T>(string key, T value);
    }
}
