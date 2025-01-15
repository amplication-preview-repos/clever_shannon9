using Microsoft.AspNetCore.Mvc;
using MultiTenantApi.APIs;
using MultiTenantApi.APIs.Common;
using MultiTenantApi.APIs.Dtos;
using MultiTenantApi.APIs.Errors;

namespace MultiTenantApi.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class ConnectionStringsControllerBase : ControllerBase
{
    protected readonly IConnectionStringsService _service;

    public ConnectionStringsControllerBase(IConnectionStringsService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one ConnectionString
    /// </summary>
    [HttpPost()]
    public async Task<ActionResult<ConnectionString>> CreateConnectionString(
        ConnectionStringCreateInput input
    )
    {
        var connectionString = await _service.CreateConnectionString(input);

        return CreatedAtAction(
            nameof(ConnectionString),
            new { id = connectionString.Id },
            connectionString
        );
    }

    /// <summary>
    /// Delete one ConnectionString
    /// </summary>
    [HttpDelete("{Id}")]
    public async Task<ActionResult> DeleteConnectionString(
        [FromRoute()] ConnectionStringWhereUniqueInput uniqueId
    )
    {
        try
        {
            await _service.DeleteConnectionString(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many ConnectionStrings
    /// </summary>
    [HttpGet()]
    public async Task<ActionResult<List<ConnectionString>>> ConnectionStrings(
        [FromQuery()] ConnectionStringFindManyArgs filter
    )
    {
        return Ok(await _service.ConnectionStrings(filter));
    }

    /// <summary>
    /// Meta data about ConnectionString records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> ConnectionStringsMeta(
        [FromQuery()] ConnectionStringFindManyArgs filter
    )
    {
        return Ok(await _service.ConnectionStringsMeta(filter));
    }

    /// <summary>
    /// Get one ConnectionString
    /// </summary>
    [HttpGet("{Id}")]
    public async Task<ActionResult<ConnectionString>> ConnectionString(
        [FromRoute()] ConnectionStringWhereUniqueInput uniqueId
    )
    {
        try
        {
            return await _service.ConnectionString(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one ConnectionString
    /// </summary>
    [HttpPatch("{Id}")]
    public async Task<ActionResult> UpdateConnectionString(
        [FromRoute()] ConnectionStringWhereUniqueInput uniqueId,
        [FromQuery()] ConnectionStringUpdateInput connectionStringUpdateDto
    )
    {
        try
        {
            await _service.UpdateConnectionString(uniqueId, connectionStringUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Get a customer record for ConnectionString
    /// </summary>
    [HttpGet("{Id}/customer")]
    public async Task<ActionResult<List<Customer>>> GetCustomer(
        [FromRoute()] ConnectionStringWhereUniqueInput uniqueId
    )
    {
        var customer = await _service.GetCustomer(uniqueId);
        return Ok(customer);
    }
}
