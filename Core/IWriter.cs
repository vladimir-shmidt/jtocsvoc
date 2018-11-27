using System.Threading.Tasks;

namespace Core
{
    public interface IWriter
    {
        Task Write(string data);
    }
}
