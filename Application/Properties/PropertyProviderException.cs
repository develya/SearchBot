namespace Application;

public class PropertyProviderException : Exception
{
    public int? UpstreamStatusCode { get; }

    public PropertyProviderException(string message, int? upstreamStatusCode = null) : base(message)
    {
        UpstreamStatusCode = upstreamStatusCode;
    }
}
