namespace CinemaApp.Data.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Models;
    using static GCommon.ApplicationConstants;

    public class TicketConfiguration : IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> entity)
        {
            entity
                .HasKey(t => t.Id);

            entity
                .Property(t => t.Price)
                .HasColumnType(PriceSqlType);

            entity
                .Property(t => t.UserId)
                .IsRequired(true);

            entity
                .HasOne(t => t.CinemaMovieProjection)
                .WithMany(cm => cm.Tickets)
                .HasForeignKey(t => t.CinemaMovieId);

            entity
                .HasOne(t => t.User)
                .WithMany()
                .HasForeignKey(t => t.UserId);

            entity
                .HasIndex(t => new { t.CinemaMovieId, t.UserId })
                .IsUnique(true);

            entity
                .HasQueryFilter(t => t.CinemaMovieProjection.IsDeleted == false);
        }
    }
}
