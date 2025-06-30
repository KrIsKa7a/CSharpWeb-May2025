namespace CinemaApp.Data.Common
{
    public static class ExceptionMessages
    {
        public const string SoftDeleteOnNonSoftDeletableEntity =
            "Soft Delete can't be performed on an Entity which does not support it!";
    }
}
