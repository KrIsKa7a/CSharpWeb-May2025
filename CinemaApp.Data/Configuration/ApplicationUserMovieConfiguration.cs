namespace CinemaApp.Data.Configuration
{
    using Models;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class ApplicationUserMovieConfiguration : IEntityTypeConfiguration<ApplicationUserMovie>
    {
        public void Configure(EntityTypeBuilder<ApplicationUserMovie> entity)
        {
            // Define composite Primary Key of the Mapping Entity
            entity
                .HasKey(aum => new { aum.ApplicationUserId, aum.MovieId });

            // Define required constraint for the ApplicationUserId, as it is type string
            entity
                .Property(aum => aum.ApplicationUserId)
                .IsRequired();

            // Define default value for soft-delete functionality
            entity
                .Property(aum => aum.IsDeleted)
                .HasDefaultValue(false);

            // Configure relation between ApplicationUserMovie and IdentityUser
            // The IdentityUser does not contain navigation property, as it is built-in type from the ASP.NET Core Identity
            entity
                .HasOne(aum => aum.ApplicationUser)
                .WithMany() // We do not have navigation property from the IdentityUser side
                .HasForeignKey(aum => aum.ApplicationUserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure relation between ApplicationUserMovie and Movie
            entity
                .HasOne(aum => aum.Movie)
                .WithMany(m => m.UserWatchlists)
                .HasForeignKey(aum => aum.MovieId)
                .OnDelete(DeleteBehavior.Restrict);

            // Define query filter to hide the deleted entries in the user Watchlist
            entity
                .HasQueryFilter(aum => aum.IsDeleted == false &&
                                                        aum.Movie.IsDeleted == false);
        }
    }
}
