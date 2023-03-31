namespace Marketplace.Core.Interfaces;

public interface ISerializator
{
    T Deserialize<T>(string path);

    T Deserialize<T>(Stream stream);
}
