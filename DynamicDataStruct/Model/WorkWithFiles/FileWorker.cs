using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkWithFiles
{
    public static class FileWorker
    {
        public static List<string> GetAllDatasetLines(string path)
        {
            List<string> lines = new List<string>();
            try
            {
                lines = File.ReadAllLines(path).ToList();
            }
            catch
            {
                throw new ArgumentException($"Does not exist file with path: {path}");
            }
            return lines;
        }

        public static void WriteAllDatasetLines(List<string> dataset, string path) => File.AppendAllLines(path, dataset);
    }
}
