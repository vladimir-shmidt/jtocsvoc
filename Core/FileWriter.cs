using System;
using System.IO;
using System.Threading.Tasks;

namespace Core
{
    public class FileWriter : IWriter
    {
        private Stream _file;

        public FileWriter(Stream file)
        {
            _file = file;
        }

        private async Task Write(string data, Stream file)
        {
            using (var stream = new StreamWriter(file))
            {
                await stream.WriteAsync(data);
            }
        }

        /// <summary>
        /// Write data to a given file
        /// </summary>
        /// <param name="data">
        /// string data to write to a file
        /// </param>
        /// <exception cref="Core.FileWriteDataConverterException">Thrown when can not write data to a file</exception>
        /// <returns>Converted CSV data</returns>
        public async Task Write(string data)
        {
            Func<Task> write = async () => await Write(data, _file);
            await write.Catch<UnauthorizedAccessException, PathTooLongException, DirectoryNotFoundException, NotSupportedException>(_ => throw new FileWriteDataConverterException($"Could not write file {_file}", _));
        }
    }
}
