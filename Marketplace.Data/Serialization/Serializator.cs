using Marketplace.Core.Interfaces;
using System.Text.Json;

namespace Marketplace.Data.Serialization;

public class Serializator : ISerializator
{
    public T Deserialize<T>(string path)
    {
        string json;

        using (var reader = new StreamReader(path))
        {
            json = reader.ReadToEnd();
        }

        return JsonSerializer.Deserialize<T>(json)!;
    }

    public T Deserialize<T>(Stream stream)
    {
        string json;

        using (var reader = new StreamReader(stream))
        {
            json = reader.ReadToEnd();
        }

        return JsonSerializer.Deserialize<T>(json)!;
    }
}
