namespace JET_Task.Domain.Entities
{
    public class Movie
    {
        public MovieId ImdbId { get; }
        public string Title { get; }
        public string Year { get; }
        public string Poster { get; }
        public string Plot { get; }
        public string ImdbRating { get; }
        public string Director { get; }
        public string Actors { get; }
        public string Genre { get; }
        public string Runtime { get; }
        public string Type { get; }
        public DateTime? LastUpdated { get; }

        public Movie(
            MovieId imdbId,
            string title,
            string year,
            string poster = "",
            string plot = "",
            string imdbRating = "",
            string director = "",
            string actors = "",
            string genre = "",
            string runtime = "",
            string type = "",
            DateTime? lastUpdated = null)
        {
            ImdbId = imdbId ?? throw new ArgumentNullException(nameof(imdbId));
            Title = !string.IsNullOrWhiteSpace(title) ? title : throw new ArgumentException("Title cannot be empty.", nameof(title));
            Year = year;
            Poster = poster;
            Plot = plot;
            ImdbRating = imdbRating;
            Director = director;
            Actors = actors;
            Genre = genre;
            Runtime = runtime;
            Type = type;
            LastUpdated = lastUpdated;
        }
    }
} 