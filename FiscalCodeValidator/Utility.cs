using FiscalCodeValidator.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Text.RegularExpressions;

namespace FiscalCodeValidator
{
    public static class Utility
    {
        public static List<Province> GetProvinces()
        {
            return LoadJsonData<List<Province>>("provinces");
        }

        public static List<Place> GetPlaces()
        {
            return LoadJsonData<List<Place>>("places");
        }

        public static List<Place> GetPlaces(string province)
        {
            return GetPlaces().Where(x => x.Province == province).ToList();
        }

        public static Place GetPlace(string code)
        {
            return GetPlaces().FirstOrDefault(x => x.Code == code);
        }

        // Extract vowels from the given string
        public static string GetVowels(string input)
        {
            return Regex.Replace(input.ToUpperInvariant(), "[B-DF-HJ-NP-TV-Z]*", "");
        }

        // Extract consonants from the given string
        public static string GetConsonants(string input)
        {
            return Regex.Replace(input.ToUpperInvariant(), "[AEIOU]*", "");
        }

        // Check if the given class has a null property
        public static bool HasNullProperty<T>(this T obj)
        {
            return typeof(T).GetProperties().Any(x => x.GetValue(obj) is null);
        }

        // Remove non alphanumeric characters and diacritics
        public static string Clean(string input)
        {
            input = new String(input.Where(char.IsLetterOrDigit).ToArray());
            input = RemoveDiacritics(input);
            return input.Trim().ToUpperInvariant();
        }

        // Remove all the diacritics from the given input
        private static string RemoveDiacritics(string input)
        {
            var normalized = input.Normalize(NormalizationForm.FormD);
            var result = new StringBuilder();

            foreach (var character in normalized)
            {
                var category = CharUnicodeInfo.GetUnicodeCategory(character);
                if (category != UnicodeCategory.NonSpacingMark)
                {
                    result.Append(character);
                }
            }
            return result.ToString().Normalize(NormalizationForm.FormC);
        }

        private static T LoadJsonData<T>(string fileName)
        {
            var path = Path.Combine("Data", $"{fileName}.json");
            using (var reader = new StreamReader(path))
            {
                var serializer = new DataContractJsonSerializer(typeof(T));
                return (T)serializer.ReadObject(reader.BaseStream);
            };
        }
    }
}
