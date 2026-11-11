using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

namespace MoneyWallet
{
    public class JsonFileService
    {
        private readonly string folderPath =Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),"MoneyWallet");
        public JsonFileService()
        {
            Directory.CreateDirectory(folderPath);
        }
        public List<T> Read<T>(string fileName)
        {
            string path = Path.Combine(folderPath, fileName);
            if (!File.Exists(path))
            {
                return new List<T>();
            }

            string json = File.ReadAllText(path);
            return JsonSerializer.Deserialize<List<T>>(json) ?? new List<T>();
        }

        public void Write<T>(string fileName, List<T> data)
        {
            string path = Path.Combine(folderPath, fileName);
            string json = JsonSerializer.Serialize(data,new JsonSerializerOptions { WriteIndented = true });
            string tempPath = path + ".tmp";
            File.WriteAllText(tempPath, json);
            File.Move(tempPath, path, true);
        }
    }
}
