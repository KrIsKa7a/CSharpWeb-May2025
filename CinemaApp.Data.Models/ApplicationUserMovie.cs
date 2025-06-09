namespace CinemaApp.Data.Models
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;

    [Comment("User Watchlist entry in the system.")]
    public class ApplicationUserMovie
    {
        [Comment("Foreign key to the referenced AspNetUser. Part of the entity composite PK.")]
        public string ApplicationUserId { get; set; } = null!;

        public virtual IdentityUser ApplicationUser { get; set; } = null!;

        [Comment("Foreign key to the referenced Movie. Part of the entity composite PK.")]
        public Guid MovieId { get; set; }

        public virtual Movie Movie { get; set; } = null!;

        [Comment("Shows if ApplicationUserMovie entry is deleted")]
        public bool IsDeleted { get; set; }
    }
}
