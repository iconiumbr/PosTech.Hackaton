using Identidade.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Identidade
{
    public class AuthenticationService
    {
        public readonly SignInManager<User> SignInManager;
        public readonly UserManager<User> UserManager;
        private readonly PostechIdentityDbContext _context;


        public AuthenticationService(
            SignInManager<User> signInManager,
            UserManager<User> userManager,
            PostechIdentityDbContext context)
        {
            SignInManager = signInManager;
            UserManager = userManager;
            _context = context;
        }

        public async Task<ResponseLoginViewModel> GerarJwt(string email)
        {
            var user = await UserManager.FindByEmailAsync(email);
            var claims = await UserManager.GetClaimsAsync(user);

            var identityClaims = await ObterClaimsUsuario(claims, user);
            var encodedToken = CodificarToken(identityClaims);

            var refreshToken = await GerarRefreshToken(email);

            return ObterRespostaToken(encodedToken, user, claims, refreshToken);
        }

        private async Task<ClaimsIdentity> ObterClaimsUsuario(ICollection<Claim> claims, User user)
        {
            var userRoles = await UserManager.GetRolesAsync(user);

            claims.Add(new Claim(JwtRegisteredClaimNames.Name, user.Id));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, ToUnixEpochDate(DateTime.UtcNow).ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(),
                ClaimValueTypes.Integer64));
            foreach (var userRole in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaims(claims);

            return identityClaims;
        }

        private string CodificarToken(ClaimsIdentity identityClaims)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("80f5ab2f-55e4-4951-9414-c758b116bc09");

            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = "HACKATON",
                Audience = "*",
                Subject = identityClaims,
                Expires = DateTime.UtcNow.AddDays(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256)
            });

            return tokenHandler.WriteToken(token);
        }

        private ResponseLoginViewModel ObterRespostaToken(string encodedToken, User user,
            IEnumerable<Claim> claims, RefreshTokenViewModel refreshToken)
        {
            return new ResponseLoginViewModel
            {
                AccessToken = encodedToken,
                RefreshToken = refreshToken.Token,
                ExpiresIn = TimeSpan.FromDays(30).TotalSeconds,
                UsuarioToken = new ResponseTokenViewModel
                {
                    Id = user.Id,
                    Email = user.Email,
                    Claims = claims.Select(c => new UserClaimViewModel { Type = c.Type, Value = c.Value })
                }
            };
        }

        private static long ToUnixEpochDate(DateTime date)
            => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero))
                .TotalSeconds);

        private async Task<RefreshTokenViewModel> GerarRefreshToken(string email)
        {
            var refreshToken = new RefreshTokenViewModel
            {
                Username = email,
                ExpirationDate = DateTime.UtcNow.AddHours(180)
            };

            _context.RefreshTokens.RemoveRange(_context.RefreshTokens.Where(u => u.Username == email));
            await _context.RefreshTokens.AddAsync(refreshToken);

            await _context.SaveChangesAsync();

            return refreshToken;
        }

        public async Task<RefreshTokenViewModel> ObterRefreshToken(Guid refreshToken)
        {
            var token = await _context.RefreshTokens.AsNoTracking()
                .FirstOrDefaultAsync(u => u.Token == refreshToken);

            return token != null && token.ExpirationDate.ToLocalTime() > DateTime.Now
                ? token
                : null;
        }

        public async Task<string> ConfirmEmail(string email, string token)
        {
            var user = await UserManager.FindByEmailAsync(email);
            var result = await UserManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded) { return "E-mail confirmado com sucesso!"; }

            return result.Errors.FirstOrDefault().Description;
        }

    }

}
