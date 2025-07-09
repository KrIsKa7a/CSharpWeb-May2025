namespace CinemaApp.Web.ViewModels.Cinema
{
    public class CinemaProgramViewModel
    {
        public string CinemaId { get; set; } = null!;

        public string CinemaName { get; set; } = null!;

        public string CinemaData { get; set; } = null!;

        public IEnumerable<CinemaProgramMovieViewModel> Movies { get; set; } = null!;
    }
}
