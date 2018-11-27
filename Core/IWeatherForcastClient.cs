using System;
using System.Threading.Tasks;

namespace Core
{
    public interface IWeatherForcastClient : IDisposable
    {
        Task<string> GetWetherForcast();
    }
}
