using Avalonia.Platform;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Mendelings.Core.Services
{
    public static class FileService
    {
        static private readonly Random _random = new();
        private const string  PATH_MALE= "avares://Mendelings/Assets/Text/NameMale.json";
        private const string  PATH_FEMALE= "avares://Mendelings/Assets/Text/NameFemale.json";

        public static string FindPath(PetSex sex)
        {
            if (sex == PetSex.Female) return PATH_FEMALE;
            else return PATH_MALE;
        }


        public static async Task<List<string>> DeserializeAsync(string filePath)
        {
            var uri = new Uri(filePath);

            await using var stream = AssetLoader.Open(uri);

            var items = await JsonSerializer.DeserializeAsync<List<string>>(stream);

            return items ?? new List<string>();
        }

        public static async Task<string> GetRandomStringAsync(string filePath)
        {
            var items = await DeserializeAsync(filePath);

            if (items.Count == 0)
                throw new InvalidOperationException("JSON-файл не содержит строк.");

            return items[_random.Next(items.Count)];
        }


    }
}
