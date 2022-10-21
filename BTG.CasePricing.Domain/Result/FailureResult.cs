
namespace BTG.CasePricing.Domain.Result;

    public class FailureResult<T> : FailureResult, IResult<T>
{
    public FailureResult(IEnumerable<FailureDetail> failureDetails)
        : base(failureDetails)
    {
    }

    public FailureResult(string message)
        : base(message)
    {
    }

    public T Value { get; }
}

public class FailureResult : IResult
{
    private readonly IEnumerable<FailureDetail> _failureDetails;

    public FailureResult()
    {
        HasSucceeded = false;
    }

    public FailureResult(IEnumerable<FailureDetail> failureDetails)
        : this()
    {
        _failureDetails = failureDetails;
    }

    public FailureResult(string message)
    {
        _failureDetails = new[] { new FailureDetail(message) };
    }

    public bool HasSucceeded { get; }

    public IEnumerable<FailureDetail> GetFailureDetails()
    {
        return _failureDetails;
    }
}
