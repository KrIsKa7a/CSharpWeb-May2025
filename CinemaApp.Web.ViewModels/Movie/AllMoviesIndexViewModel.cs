namespace CinemaApp.Web.ViewModels.Movie
{
    public class AllMoviesIndexViewModel
    {
        // ViewModel:
        // Describes all properties of the corresponding Entity that we want to visualize
        public string Id { get; set; } = null!;

        public string Title { get; set; } = null!;

        public string Genre { get; set; } = null!;

        // ReleaseDate is string -> ReleaseDate is formatted in desired ReleaseDate format
        public string ReleaseDate { get; set; } = null!;

        public string Director { get; set; } = null!;

        public string? ImageUrl { get; set; }

        public bool IsAddedToUserWatchlist { get; set; }
    }
}
