using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace BTG.CasePricing.Domain.Interfaces.Tenant;

public class AddRequiredHeaderParameter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        operation.Parameters ??= new List<OpenApiParameter>();

        var dicCanalVenda = new Dictionary<string, OpenApiExample>();
        dicCanalVenda.Add("Linguagem", new OpenApiExample
        {
            Summary = "Linguagem de Programação",
            Description = "Neste campo deverá ser informado a Linguagem de Programação. Linguagem de Programação: 'C#', 'JavaScript', 'Pyton', 'Ruby','VB.NET'."
        });
        dicCanalVenda.Add("C#", new OpenApiExample
        {
            Summary = "C#",
            Description = "Linguagem de Programação - C#.",
            Value = new OpenApiString("C#")
        });
        dicCanalVenda.Add("JavaScript", new OpenApiExample
        {
            Summary = "JavaScript",
            Description = "Linguagem de Programação - JavaScript.",
            Value = new OpenApiString("JavaScript")
        });
        dicCanalVenda.Add("Pyton", new OpenApiExample
        {
            Summary = "Pyton",
            Description = "Linguagem de Programação - Pyton.",
            Value = new OpenApiString("Pyton")
        });

        dicCanalVenda.Add("Ruby", new OpenApiExample
        {
            Summary = "Ruby",
            Description = "Linguagem de Programação - Ruby.",
            Value = new OpenApiString("Ruby")
        });

        dicCanalVenda.Add("VB.NET", new OpenApiExample
        {
            Summary = "VB.NET",
            Description = "Linguagem de Programação - VB.NET.",
            Value = new OpenApiString("VB.NET")
        });

        operation.Parameters.Add(new OpenApiParameter
        {
            Name = "linguagem",
            In = ParameterLocation.Query,
            Description = "Linguagem de Programação",
            Schema = new OpenApiSchema() { Type = "string" },
            Required = true,
            Examples = dicCanalVenda,
        });
    }
}
