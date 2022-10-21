using BTG.CasePricing.Domain.Result;
using BTG.CasePricing.WebAPI.Model;

namespace BTG.CasePricing.WebAPI.Extensions
{
    public static class ModelExtensions
    {
        public static FailureResultModel ToFailureResultModel(this FailureResult failureResult)
        {
            return new FailureResultModel()
            {
                Messages = failureResult
                    .GetFailureDetails()
                    .Select(s => s.Message).ToArray()
            };

        }
    }
}
