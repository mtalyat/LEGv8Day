using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace LEGv8Day
{
    /// <summary>
    /// Responsible for loading and saving data to the disc.
    /// </summary>
    internal static class Data
    {
        private static string SaveFolder => Application.UserAppDataPath;

        public static void Save<T>(string path, T? obj)
        {
            string fullPath = Path.Combine(SaveFolder, path);

            string json = JsonConvert.SerializeObject(obj);

            File.WriteAllText(fullPath, json);
        }

        public static T? Load<T>(string path)
        {
            string fullPath = Path.Combine(SaveFolder, path);

            if (!File.Exists(fullPath))
            {
                return default(T);
            }

            string json = File.ReadAllText(fullPath);

            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
