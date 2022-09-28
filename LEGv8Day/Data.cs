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
        /// <summary>
        /// The directory in which data is saved.
        /// </summary>
        private static string SaveDirectory => Application.UserAppDataPath;

        /// <summary>
        /// Saves the given object as a json text file at the given file path.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <param name="obj"></param>
        public static void Save<T>(string path, T? obj)
        {
            string fullPath = Path.Combine(SaveDirectory, path);

            string json = JsonConvert.SerializeObject(obj);

            File.WriteAllText(fullPath, json);
        }

        /// <summary>
        /// Loads the json data from the given file path.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <returns>Null if there is no file, or no text in the file.</returns>
        public static T? Load<T>(string path)
        {
            string fullPath = Path.Combine(SaveDirectory, path);

            if (!File.Exists(fullPath))
            {
                return default(T);
            }

            string json = File.ReadAllText(fullPath);

            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
