using Microsoft.AspNetCore.Identity;

namespace MindBodySoul.Repositories.Interface
{
    public interface ITokenRepository
    {
        string CreateJwtToken(IdentityUser identityUser, List<string> roles);
    }
}
