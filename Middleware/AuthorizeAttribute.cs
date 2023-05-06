using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;


namespace TADA.Middleware;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AuthorizeAttribute : Attribute, IAuthorizationFilter
{
    private readonly string[] roles;
    public AuthorizeAttribute(params string[] roles)
    {
        this.roles = roles ?? new string[] { };
    }
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        if (!roles.Contains(context.HttpContext.Session.GetString("Role")))
        {
            context.Result = new RedirectResult("~/Error");
        }
    }
}