namespace CinemaApp.Data.Common
{
    public static class EntityConstants
    {
        public static class Movie
        {
            /// <summary>
            /// Movie Title should be able to store text with length up to 150
            /// </summary>
            public const int TitleMaxLength = 150;

            /// <summary>
            /// Movie Genre should be able to store text with length up to 30
            /// </summary>
            public const int GenreMaxLength = 30;

            /// <summary>
            /// Movie Director should be able to store text with length up to 150
            /// </summary>
            public const int DirectorMaxLength = 150;

            /// <summary>
            /// Movie Description should be able to store text with length up to 1024
            /// </summary>
            public const int DescriptionMaxLength = 1024;

            /// <summary>
            /// Movie ImageUrl should be able to store text with length up to 2048 (refer URI RFC)
            /// </summary>
            public const int ImageUrlMaxLength = 2048;
        }
    }
}
