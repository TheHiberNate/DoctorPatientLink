using DoctorLink.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using DoctorLink.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;

namespace DoctorLink.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        //inside options can configure use of SQL server and the connection string
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        public DbSet<Patient> Patients { get; set; } // create table of Data type Category

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
            builder.ApplyConfiguration(new ApplicationUserEntityConfiguration());
        }
        public DbSet<DoctorLink.Models.Medication> Medication { get; set; } = default!;
    }

    public class ApplicationUserEntityConfiguration : IEntityTypeConfiguration<UserInfo>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<UserInfo> builder)
        {
            builder.Property(x => x.FirstName).HasMaxLength(100);
            builder.Property(x => x.LastName).HasMaxLength(100);
        }
    }
}
