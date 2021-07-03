using ESE.Auth.API.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NetDevPack.Security.JwtSigningCredentials;
using NetDevPack.Security.JwtSigningCredentials.Store.EntityFrameworkCore;

namespace ESE.Auth.API.Data
{
    public class AuthDbContext: IdentityDbContext, ISecurityKeyContext
    {
        public DbSet<SecurityKeyWithPrivate> SecurityKeys { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options) { }
    }
}