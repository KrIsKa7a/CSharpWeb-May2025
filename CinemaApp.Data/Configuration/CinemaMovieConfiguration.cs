namespace CinemaApp.Data.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Models;
    using static Common.EntityConstants.CinemaMovie;

    public class CinemaMovieConfiguration : IEntityTypeConfiguration<CinemaMovie>
    {
        public void Configure(EntityTypeBuilder<CinemaMovie> entity)
        {
            entity
                .HasKey(cm => cm.Id);

            // Define composite pseudo-PK
            entity
                .HasIndex(cm => new { cm.MovieId, cm.CinemaId, cm.Showtime })
                .IsUnique(true);

            entity
                .Property(cm => cm.IsDeleted)
                .HasDefaultValue(false);

            entity
                .Property(cm => cm.AvailableTickets)
                .HasDefaultValue(AvailableTicketsDefaultValue);

            entity
                .Property(cm => cm.Showtime)
                .IsRequired(true)
                .HasMaxLength(ShowtimeMaxLength);

            entity
                .HasQueryFilter(cm => cm.IsDeleted == false &&
                                                cm.Movie.IsDeleted == false &&
                                                cm.Cinema.IsDeleted == false);

            entity
                .HasOne(cm => cm.Movie)
                .WithMany(m => m.MovieProjections)
                .HasForeignKey(cm => cm.MovieId)
                .OnDelete(DeleteBehavior.Restrict);

            entity
                .HasOne(cm => cm.Cinema)
                .WithMany(c => c.CinemaMovies)
                .HasForeignKey(cm => cm.CinemaId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
