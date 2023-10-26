using System.Security.Claims;

namespace fast_currencies_be;

public class SessionService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    public SessionService(IHttpContextAccessor httpContextAccessor) {
        _httpContextAccessor = httpContextAccessor;
    }

    public int GetUserId() {
        Claim? userIdClaim = _httpContextAccessor.HttpContext.User.FindFirst("userId");

        if(userIdClaim == null) {
            throw new Exception("No se pudo validar la sesión.");
        } else {
            return int.Parse(userIdClaim.Value);
        }
    }

    public Role GetUserRol() {
        Claim? userRolClaim = _httpContextAccessor.HttpContext.User.FindFirst("rol");

        if(userRolClaim == null) {
            throw new Exception("No se pudo validar el rol del usuario.");
        } else {
            return (Role)Enum.Parse(typeof(Role), userRolClaim.Value);
        }
    }
}
