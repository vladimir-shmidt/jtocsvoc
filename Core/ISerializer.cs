using System;
using System.Threading.Tasks;

namespace Core
{
    public interface ISerializer
    {
        Task<T> Deserialize<T>(string obj);
        Task<string> Serialize<T>(T obj);
    }
}
