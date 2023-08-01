using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ZimskaLiga.Models
{
    public class LoginDbcontext : DbContext
    {
        public LoginDbcontext(DbContextOptions<LoginDbcontext> options)
            : base(options)
        {

        }

        public DbSet<LogInModel> UserLogin { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<LogInModel>(entity => {
                entity.HasKey(k => k.id);
            });
        }
    }
}