using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Net;
using Api.Dtos;
using Application.Repositories.Interfaces;
using AutoMapper;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("")]
[Produces("application/json")]
public class EntitiesController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IEntityRepository _repository;

    public EntitiesController(
        IMapper mapper,
        IEntityRepository repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    /// <summary>
    /// Insert entity to repository
    /// </summary>
    /// <param name="insert">entityDto</param>
    /// <response code="201">Returns the newly created item</response>
    /// <remarks>
    /// Request example:
    /// 
    ///     POST
    ///     {
    ///         http://127.0.0.1:5000?insert={
    ///         "id":"cfaa0d3f-7fea-4423-9f69-ebff826e2f89",
    ///         "operationDate":"2019-04-02T13:10:20.0263632+03:00",
    ///         "amount":23.05}
    ///     }
    /// </remarks>
    /// <returns>insert result</returns>
    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.Created)]
    public async Task<IActionResult> InsertEntityAsync(
        [FromQuery]
        [Required]
        [DefaultValue("{\"id\":\"cfaa0d3f-7fea-4423-9f69-ebff826e2f89\",\"operationDate\":\"2019-04-02T13:10:20.0263632+03:00\",\"amount\":23.05}")]
        string insert)
    {
        var entity = _mapper.Map<Entity>(insert);
        var insertedEntity = await _repository.InsertEntityAsync(entity);

        return CreatedAtRoute(nameof(GetEntityByIdAsync), new { insertedEntity.Id }, insertedEntity);
    }
    
    /// <summary>
    /// Get entity from repository
    /// </summary>
    /// <param name="get">entity Id</param>
    /// <response code="200">If item is found</response>
    /// <response code="404">If the item is null</response>
    /// <remarks>
    /// Sample request:
    /// 
    ///     GET
    ///     {
    ///         http://127.0.0.1:5000?get=cfaa0d3f-7fea-4423-9f69-ebff826e2f89
    ///     }
    /// </remarks>
    /// <returns>entity</returns>
    [HttpGet(Name = nameof(GetEntityByIdAsync))]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> GetEntityByIdAsync(
        [FromQuery]
        [Required]
        [DefaultValue("cfaa0d3f-7fea-4423-9f69-ebff826e2f89")]
        Guid get)
    {
        var entity = await _repository.GetEntityByIdAsync(get);
        if (entity is null)
        {
            return NotFound();
        }

        var entityDto = _mapper.Map<EntityGetDto>(entity);
        return Ok(entityDto);
    }
}