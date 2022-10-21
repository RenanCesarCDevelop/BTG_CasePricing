namespace BTG.CasePricing.WebAPI.Model;
public class FailureResultModel
{

    public FailureResultModel()
    {

    }

    public FailureResultModel(string message)
    {
        Messages = new string[] { message };
    }

    public string[] Messages { get; set; }
}

