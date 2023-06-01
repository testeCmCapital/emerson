using Domain.Auth;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.CrossCutting.Data.Context
{
    public class IdentityContext : AuditContext
    {
        private readonly IUserLogged _userLogged;

        public IdentityContext(DbContextOptions<IdentityContext> options, IUserLogged userLogged) : base(options, userLogged) {
            _userLogged = userLogged;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<IdentityRoleClaim<int>>().ToTable("identityRoleClaims");
            builder.Entity<Role>().ToTable("identityRoles");
            builder.Entity<IdentityUserClaim<int>>().ToTable("identityClaims");
            builder.Entity<IdentityUserLogin<int>>().ToTable("identityUserLogins");
            builder.Entity<IdentityUserRole<int>>().ToTable("identityUserRoles");
            builder.Entity<User>().ToTable("identityUsers");
            builder.Entity<IdentityUserToken<int>>().ToTable("identityTokens");

            builder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique(true);

            builder.Entity<User>()
                .HasIndex(u => u.PhoneNumber)
                .IsUnique(true);

            builder.ApplyConfigurationsFromAssembly(typeof(IdentityContext).Assembly);
        }

    }
}
