using System;
using System.IO;
using System.Text.Json;

namespace TaskManager__WPF.Services
{
    public abstract class JSONService
    {
        public static void Write<T>(string path, T content)
        {
            if (!File.Exists(path))
            {
                File.Create(path).Close();
            }
            File.WriteAllText(path, JsonSerializer.Serialize(content));

        }

        public static T? Read<T>(string path)
        {
            if (!File.Exists(path))
            {
                File.Create(path).Close();
            }
            try
            {
                return (T)JsonSerializer.Deserialize(File.ReadAllText(path), typeof(T));
            }
            catch (Exception) { throw; }
        }
    }
}
