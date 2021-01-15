using IdentityServer4.EntityFramework.Options;
using Majunga.Server.Models;
using Majunga.Shared.ViewModels;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Majunga.Server.Data
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        public ApplicationDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<File>()
                .HasOne(e => e.Link)
                .WithOne(e => e.File)
                .HasForeignKey<Link>(e => e.FileId)
                .IsRequired(false);

            base.OnModelCreating(builder);
        }

        public DbSet<File> Files { get; set; }
        public DbSet<Link> Links { get; set; }
    }
}
