using System.Text.Json;
using System.Text.Json.Serialization;
using Api.Dtos;
using AutoMapper;
using Domain;

namespace Api.Mapping;

public class ApiMappingProfile : Profile
{
    public ApiMappingProfile()
    {
        CreateMap<string, Entity>()
            .ConvertUsing((s, _) =>
                JsonSerializer.Deserialize<Entity>(s,
                new JsonSerializerOptions
                {
                    NumberHandling = JsonNumberHandling.AllowReadingFromString,
                    PropertyNameCaseInsensitive = true,
                })!);
        CreateMap<Entity, EntityGetDto>();
    }
}