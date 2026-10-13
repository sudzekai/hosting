using System.Text.Json;

namespace Sudzekai.Hosting.Builders.Configuration.Provides
{
    internal sealed class JsonConfigurationProvider : IConfigurationProvider
    {
        private FileStream _stream;

        public JsonConfigurationProvider(string fileName)
        {
            _stream = OpenFile(fileName);
        }

        public Dictionary<string, object?> LoadConfigurationValues()
        {
            var doc = JsonDocument.Parse(_stream);
            
            _stream.Close();

            var result = Parse(doc.RootElement);


            return result;
        }

        private static FileStream OpenFile(string fileName)
            => new(fileName, FileMode.Open, FileAccess.Read, FileShare.Read, 4096);

        private static Dictionary<string, object?> Parse(JsonElement root)
        {
            var result = new Dictionary<string, object?>();
            Flatten(root, prefix: "", result);
            return result;
        }

        private static void Flatten(JsonElement element, string prefix, Dictionary<string, object?> result)
        {
            switch (element.ValueKind)
            {
                case JsonValueKind.Object:
                    foreach (var prop in element.EnumerateObject())
                    {
                        var path = prefix.Length == 0 ? prop.Name : $"{prefix}:{prop.Name}";
                        Flatten(prop.Value, path, result);
                    }
                    break;

                case JsonValueKind.Array:
                    int i = 0;
                    foreach (var item in element.EnumerateArray())
                    {
                        Flatten(item, $"{prefix}[{i++}]", result);
                    }
                    break;

                case JsonValueKind.String:
                    result[prefix] = element.GetString();
                    break;

                case JsonValueKind.Number:
                    result[prefix] = element.TryGetInt64(out var l) ? l : element.GetDouble();
                    break;

                case JsonValueKind.True:
                    result[prefix] = true;
                    break;

                case JsonValueKind.False:
                    result[prefix] = false;
                    break;

                case JsonValueKind.Null:
                    result[prefix] = null;
                    break;

                default:
                    throw new JsonException($"Unexpected kind: {element.ValueKind}");
            }
        }
    }
}
