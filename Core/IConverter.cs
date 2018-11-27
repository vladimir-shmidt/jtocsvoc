using System;
using System.Threading.Tasks;

namespace Core
{
    public interface IConverter : IDisposable
    {
        Task Convert();
    }
}
