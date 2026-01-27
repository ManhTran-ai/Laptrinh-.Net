using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace GuhaStore.Web.Filters;

public class SessionAuthorizationAttribute : Attribute, IAuthorizationFilter
{
    private readonly string[] _allowedRoles;

    public SessionAuthorizationAttribute(params string[] allowedRoles)
    {
        _allowedRoles = allowedRoles.Length > 0 ? allowedRoles : new[] { "admin" };
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var userRole = context.HttpContext.Session.GetString("UserRole");

        if (string.IsNullOrEmpty(userRole) || !_allowedRoles.Contains(userRole))
        {
            context.Result = new RedirectToActionResult("Login", "Account", new { returnUrl = context.HttpContext.Request.Path });
        }
    }
}

