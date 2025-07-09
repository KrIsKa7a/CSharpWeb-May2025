namespace CinemaApp.Web.ViewModels.Cinema
{
    public class CinemaDetailsViewModel
    {
        public string Name { get; set; } = null!;

        public string Location { get; set; } = null!;

        public IEnumerable<CinemaDetailsMovieViewModel> Movies { get; set; } = null!;
    }
}
