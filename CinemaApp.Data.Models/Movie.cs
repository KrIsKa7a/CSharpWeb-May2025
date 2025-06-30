namespace CinemaApp.Data.Models
{
    using Microsoft.EntityFrameworkCore;

    [Comment("Movie in the system")]
    public class Movie
    {
        [Comment("Movie identifier")]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Comment("Movie title")]
        public string Title { get; set; } = null!;

        [Comment("Movie genre")]
        public string Genre { get; set; } = null!;

        [Comment("Movie release date")]
        public DateOnly ReleaseDate { get; set; }

        [Comment("Movie director")]
        public string Director { get; set; } = null!;

        [Comment("Movie duration")]
        public int Duration { get; set; }

        [Comment("Movie description")]
        public string Description { get; set; } = null!;

        [Comment("Movie image url from the image store")]
        public string? ImageUrl { get; set; }

        // TODO: Extract the property with Id to BaseDeletableModel
        [Comment("Shows if movie is deleted")]
        public bool IsDeleted { get; set; }

        public virtual ICollection<ApplicationUserMovie> UserWatchlists { get; set; }
            = new HashSet<ApplicationUserMovie>();

        public virtual ICollection<CinemaMovie> MovieProjections { get; set; }
            = new HashSet<CinemaMovie>();
    }
}
