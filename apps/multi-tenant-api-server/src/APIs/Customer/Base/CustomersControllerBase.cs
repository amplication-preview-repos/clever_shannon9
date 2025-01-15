using Microsoft.AspNetCore.Mvc;
using MultiTenantApi.APIs;
using MultiTenantApi.APIs.Common;
using MultiTenantApi.APIs.Dtos;
using MultiTenantApi.APIs.Errors;

namespace MultiTenantApi.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class CustomersControllerBase : ControllerBase
{
    protected readonly ICustomersService _service;

    public CustomersControllerBase(ICustomersService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Customer
    /// </summary>
    [HttpPost()]
    public async Task<ActionResult<Customer>> CreateCustomer(CustomerCreateInput input)
    {
        var customer = await _service.CreateCustomer(input);

        return CreatedAtAction(nameof(Customer), new { id = customer.Id }, customer);
    }

    /// <summary>
    /// Delete one Customer
    /// </summary>
    [HttpDelete("{Id}")]
    public async Task<ActionResult> DeleteCustomer([FromRoute()] CustomerWhereUniqueInput uniqueId)
    {
        try
        {
            await _service.DeleteCustomer(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Customers
    /// </summary>
    [HttpGet()]
    public async Task<ActionResult<List<Customer>>> Customers(
        [FromQuery()] CustomerFindManyArgs filter
    )
    {
        return Ok(await _service.Customers(filter));
    }

    /// <summary>
    /// Meta data about Customer records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> CustomersMeta(
        [FromQuery()] CustomerFindManyArgs filter
    )
    {
        return Ok(await _service.CustomersMeta(filter));
    }

    /// <summary>
    /// Get one Customer
    /// </summary>
    [HttpGet("{Id}")]
    public async Task<ActionResult<Customer>> Customer(
        [FromRoute()] CustomerWhereUniqueInput uniqueId
    )
    {
        try
        {
            return await _service.Customer(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Customer
    /// </summary>
    [HttpPatch("{Id}")]
    public async Task<ActionResult> UpdateCustomer(
        [FromRoute()] CustomerWhereUniqueInput uniqueId,
        [FromQuery()] CustomerUpdateInput customerUpdateDto
    )
    {
        try
        {
            await _service.UpdateCustomer(uniqueId, customerUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Connect multiple ConnectionStrings records to Customer
    /// </summary>
    [HttpPost("{Id}/connectionStrings")]
    public async Task<ActionResult> ConnectConnectionStrings(
        [FromRoute()] CustomerWhereUniqueInput uniqueId,
        [FromQuery()] ConnectionStringWhereUniqueInput[] connectionStringsId
    )
    {
        try
        {
            await _service.ConnectConnectionStrings(uniqueId, connectionStringsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Disconnect multiple ConnectionStrings records from Customer
    /// </summary>
    [HttpDelete("{Id}/connectionStrings")]
    public async Task<ActionResult> DisconnectConnectionStrings(
        [FromRoute()] CustomerWhereUniqueInput uniqueId,
        [FromBody()] ConnectionStringWhereUniqueInput[] connectionStringsId
    )
    {
        try
        {
            await _service.DisconnectConnectionStrings(uniqueId, connectionStringsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find multiple ConnectionStrings records for Customer
    /// </summary>
    [HttpGet("{Id}/connectionStrings")]
    public async Task<ActionResult<List<ConnectionString>>> FindConnectionStrings(
        [FromRoute()] CustomerWhereUniqueInput uniqueId,
        [FromQuery()] ConnectionStringFindManyArgs filter
    )
    {
        try
        {
            return Ok(await _service.FindConnectionStrings(uniqueId, filter));
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update multiple ConnectionStrings records for Customer
    /// </summary>
    [HttpPatch("{Id}/connectionStrings")]
    public async Task<ActionResult> UpdateConnectionStrings(
        [FromRoute()] CustomerWhereUniqueInput uniqueId,
        [FromBody()] ConnectionStringWhereUniqueInput[] connectionStringsId
    )
    {
        try
        {
            await _service.UpdateConnectionStrings(uniqueId, connectionStringsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Connect multiple UserTokens records to Customer
    /// </summary>
    [HttpPost("{Id}/userTokens")]
    public async Task<ActionResult> ConnectUserTokens(
        [FromRoute()] CustomerWhereUniqueInput uniqueId,
        [FromQuery()] UserTokenWhereUniqueInput[] userTokensId
    )
    {
        try
        {
            await _service.ConnectUserTokens(uniqueId, userTokensId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Disconnect multiple UserTokens records from Customer
    /// </summary>
    [HttpDelete("{Id}/userTokens")]
    public async Task<ActionResult> DisconnectUserTokens(
        [FromRoute()] CustomerWhereUniqueInput uniqueId,
        [FromBody()] UserTokenWhereUniqueInput[] userTokensId
    )
    {
        try
        {
            await _service.DisconnectUserTokens(uniqueId, userTokensId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find multiple UserTokens records for Customer
    /// </summary>
    [HttpGet("{Id}/userTokens")]
    public async Task<ActionResult<List<UserToken>>> FindUserTokens(
        [FromRoute()] CustomerWhereUniqueInput uniqueId,
        [FromQuery()] UserTokenFindManyArgs filter
    )
    {
        try
        {
            return Ok(await _service.FindUserTokens(uniqueId, filter));
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update multiple UserTokens records for Customer
    /// </summary>
    [HttpPatch("{Id}/userTokens")]
    public async Task<ActionResult> UpdateUserTokens(
        [FromRoute()] CustomerWhereUniqueInput uniqueId,
        [FromBody()] UserTokenWhereUniqueInput[] userTokensId
    )
    {
        try
        {
            await _service.UpdateUserTokens(uniqueId, userTokensId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
