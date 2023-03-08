namespace Marketplace.Shared.Generic
{
    public class Result<T>
    {
        public T? Value { get; set; }

        public string ErrorMessage { get; set; } = string.Empty;
    }
}
