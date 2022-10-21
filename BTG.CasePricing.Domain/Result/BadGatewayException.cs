namespace BTG.CasePricing.Domain.Result;
public class BadGatewayException : Exception
{
    public BadGatewayException()
    {
    }

    public BadGatewayException(string message)
        : base(message)
    {
    }
}