using Microsoft.AspNetCore.Mvc;
using MultiTenantApi.APIs;
using MultiTenantApi.APIs.Common;
using MultiTenantApi.APIs.Dtos;
using MultiTenantApi.APIs.Errors;

namespace MultiTenantApi.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class UserTokensControllerBase : ControllerBase
{
    protected readonly IUserTokensService _service;

    public UserTokensControllerBase(IUserTokensService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one UserToken
    /// </summary>
    [HttpPost()]
    public async Task<ActionResult<UserToken>> CreateUserToken(UserTokenCreateInput input)
    {
        var userToken = await _service.CreateUserToken(input);

        return CreatedAtAction(nameof(UserToken), new { id = userToken.Id }, userToken);
    }

    /// <summary>
    /// Delete one UserToken
    /// </summary>
    [HttpDelete("{Id}")]
    public async Task<ActionResult> DeleteUserToken(
        [FromRoute()] UserTokenWhereUniqueInput uniqueId
    )
    {
        try
        {
            await _service.DeleteUserToken(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many UserTokens
    /// </summary>
    [HttpGet()]
    public async Task<ActionResult<List<UserToken>>> UserTokens(
        [FromQuery()] UserTokenFindManyArgs filter
    )
    {
        return Ok(await _service.UserTokens(filter));
    }

    /// <summary>
    /// Meta data about UserToken records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> UserTokensMeta(
        [FromQuery()] UserTokenFindManyArgs filter
    )
    {
        return Ok(await _service.UserTokensMeta(filter));
    }

    /// <summary>
    /// Get one UserToken
    /// </summary>
    [HttpGet("{Id}")]
    public async Task<ActionResult<UserToken>> UserToken(
        [FromRoute()] UserTokenWhereUniqueInput uniqueId
    )
    {
        try
        {
            return await _service.UserToken(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one UserToken
    /// </summary>
    [HttpPatch("{Id}")]
    public async Task<ActionResult> UpdateUserToken(
        [FromRoute()] UserTokenWhereUniqueInput uniqueId,
        [FromQuery()] UserTokenUpdateInput userTokenUpdateDto
    )
    {
        try
        {
            await _service.UpdateUserToken(uniqueId, userTokenUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Get a customer record for UserToken
    /// </summary>
    [HttpGet("{Id}/customer")]
    public async Task<ActionResult<List<Customer>>> GetCustomer(
        [FromRoute()] UserTokenWhereUniqueInput uniqueId
    )
    {
        var customer = await _service.GetCustomer(uniqueId);
        return Ok(customer);
    }
}
