namespace CinemaApp.Services.Core.Interfaces
{
    using Web.ViewModels.Cinema;

    public interface ICinemaService
    {
        Task<IEnumerable<UsersCinemaIndexViewModel>> GetAllCinemasUserViewAsync();
    }
}
