namespace yaml2json
{
    using System.IO;
    using System.Text.Json;
    using YamlDotNet.Serialization;

    internal class Program
    {
        private static readonly string inputYamlFilePath = @"<YAML input file path>";
        private static readonly string outputJsonFilePath = @"<JSON output file path>";

        static void Main()
        {
            StreamReader sr = new(inputYamlFilePath);
            Deserializer deserializer = new();
            object? yamlObject = deserializer.Deserialize(sr);

            Newtonsoft.Json.JsonSerializer jsonSerializer = new();
            StringWriter sw = new();
            jsonSerializer.Serialize(sw, yamlObject);
            File.WriteAllText(outputJsonFilePath, PrettyJson(sw.ToString()));
        }

        public static string PrettyJson(string unPrettyJson)
        {
            JsonSerializerOptions options = new()
            {
                WriteIndented = true
            };

            JsonElement jsonElement = JsonSerializer.Deserialize<JsonElement>(unPrettyJson);

            return JsonSerializer.Serialize(jsonElement, options);
        }
    }
}