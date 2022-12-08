using CRM.Services.Identity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.Services.Identity.Data.Configurations;

public class UserConfigurations : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(x => x.DefaultCulture).HasMaxLength(6);
		builder.Property(x => x.FirstName).HasMaxLength(255);
		builder.Property(x => x.LastName).HasMaxLength(255);

	}
}

