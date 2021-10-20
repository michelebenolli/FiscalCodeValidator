using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Xunit.Sdk;

namespace FiscalCodeValidator.Tests.Data
{
    public class JsonDataAttribute : DataAttribute
    {
        private readonly string _path;
        private readonly string _property;

        /// <summary>
        /// Load data from a JSON file as the data source for a theory
        /// </summary>
        /// <param name="path">The absolute or relative path to the JSON file to load</param>
        public JsonDataAttribute(string path) : this(path, null) { }

        /// <summary>
        /// Load data from a JSON file as the data source for a theory
        /// </summary>
        /// <param name="path">The absolute or relative path to the JSON file to load</param>
        /// <param name="property">The name of the property on the JSON file that contains the data for the test</param>
        public JsonDataAttribute(string path, string property)
        {
            _path = path;
            _property = property;
        }

        /// <inheritDoc />
        public override IEnumerable<object[]> GetData(MethodInfo testMethod)
        {
            if (testMethod == null)
                throw new ArgumentNullException(nameof(testMethod));

            // Get the absolute path to the JSON file
            var path = Path.GetRelativePath(Directory.GetCurrentDirectory(), _path);

            if (!File.Exists(path))
                throw new ArgumentException($"Could not find file at path: {path}");

            var fileData = File.ReadAllText(_path);

            return string.IsNullOrEmpty(_property) ?
                JsonConvert.DeserializeObject<List<object[]>>(fileData) :
                JObject.Parse(fileData)[_property].ToObject<List<object[]>>();
        }
    }
}
