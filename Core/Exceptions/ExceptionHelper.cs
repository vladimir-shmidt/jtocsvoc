using System;
using System.Threading.Tasks;

namespace Core
{
    public static class ExceptionHelper
    {
        public static async Task Catch<T1, T2, T3, T4>(this Func<Task> action, Action<Exception> handler)
        {
            try
            {
                await action();
            }
            catch (Exception ex)
            {
                if (ex is T1 || ex is T2 || ex is T3 || ex is T4) handler(ex);
            }
        }
    }
}
