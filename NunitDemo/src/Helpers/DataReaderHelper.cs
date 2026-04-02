using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Helper.DataReader;

public static class DataReader
{
    //Get Data path 
    public static string GetPath(string fileName)
    {
        var projectRoot = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory)!
            .Parent!.Parent!.Parent!.FullName;

        var path = Path.Combine(projectRoot, "src", "Data", fileName);

        if (!File.Exists(path))
        {
            throw new FileNotFoundException($"Test data file not found at path: {path}");
        }

        return path;
    }

    public static string ReadRaw(string fileName)
    {
        var path = GetPath(fileName);
        return File.ReadAllText(path);
    }

    public static T? Data<T>(string fileName, string? key = null)
    {
        var jsonData = ReadRaw(fileName);

        if (string.IsNullOrEmpty(key))
        {
            return JsonConvert.DeserializeObject<T>(jsonData);
        }

        JObject jObject = JObject.Parse(jsonData);
        JToken? token = jObject.SelectToken(key);

        if (token == null)
        {
            throw new Exception($"Key '{key}' not found in JSON file: {fileName}");
        }

        return token.ToObject<T>();
    }
}