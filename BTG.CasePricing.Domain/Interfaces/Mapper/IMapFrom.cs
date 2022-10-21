using AutoMapper;
using System.Diagnostics.CodeAnalysis;

namespace BTG.CasePricing.Domain.Interfaces.Mapper;

public interface IMapFrom<T>
{
    [ExcludeFromCodeCoverage]
    void Mapping(Profile profile) => profile.CreateMap(typeof(T), GetType());
}
