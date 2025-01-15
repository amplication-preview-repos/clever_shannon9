using Microsoft.AspNetCore.Mvc;
using MultiTenantApi.APIs.Common;
using MultiTenantApi.Infrastructure.Models;

namespace MultiTenantApi.APIs.Dtos;

[BindProperties(SupportsGet = true)]
public class UserTokenFindManyArgs : FindManyInput<UserToken, UserTokenWhereInput> { }
