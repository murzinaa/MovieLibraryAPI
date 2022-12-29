using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace FilmsLibraryEFCore.FileRepository
{
    public class FileHelper
    {
        private readonly ILogger<FileHelper> _logger;

        public FileHelper(ILogger<FileHelper> logger)
        {
            _logger = logger;
        }

        public async Task<List<string>> ReadDataFromFile(string path)
        {
            try
            {
                var data = (await File.ReadAllLinesAsync(path)).ToList();
                return data;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An Error occurred during reading from file");
                throw;
            }
        }

        public async Task WriteDataToFile(List<string> data, string path)
        {
            try
            {
                await File.WriteAllLinesAsync(path, data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An Error occurred during writing to file");
            }
        }
    }
}
