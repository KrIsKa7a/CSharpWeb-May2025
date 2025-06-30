namespace CinemaApp.Data.Models
{
    using Microsoft.EntityFrameworkCore;

    [Comment("Cinema in the system")]
    public class Cinema
    {
        [Comment("Cinema identifier")]
        public Guid Id { get; set; }

        [Comment("Cinema name")]
        public string Name { get; set; } = null!;

        [Comment("Cinema location")]
        public string Location { get; set; } = null!;

        [Comment("Shows if cinema is deleted")]
        public bool IsDeleted { get; set; }

        public virtual ICollection<CinemaMovie> CinemaMovies { get; set; }
            = new HashSet<CinemaMovie>();
    }
}
