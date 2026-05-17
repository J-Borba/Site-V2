using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace projects_api.Data.Configurations.Auth.User;

public class UserDbContextMapping : IEntityTypeConfiguration<Models.User>
{
    public void Configure(EntityTypeBuilder<Models.User> builder)
    {
        builder.ToTable("AspNetUsers", "auth");
    }
}
