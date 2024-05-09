namespace CMDSpotifyClient.Entities
{
    //Entities: Die innerste Schicht, die die Geschäftslogik und Geschäftsregeln enthält.
    public class Track
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string DurationMin { get; set; }
        public bool Explicit { get; set; }
        public string PreviewUrl { get; set; }
        public List<Artist> Artists { get; set; } = new List<Artist>();
        public Album Album { get; set; }
        public int Popularity { get; set; }
        public int TrackNumber { get; set; }
        public string Type { get; set; }

    }

    public class Artist
    {
        public string Id { get; set; }
        public string Name { get; set; }    
        public int Followers { get; set; }
        public List<string> Genre { get; set; }
        public int Popularity { get; set; }
    }

    public class Album
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int TotalTracks { get; set; }
        public string ReleaseDate { get; set; }
        public int Popularity { get; set; }
        public string Label { get; set; }
        public List<Track> Tracks { get; set; } = new List<Track>();
        public List<Artist> Artists { get; set; } = new List<Artist>();
    }
    public class Playlist
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Track> Tracks { get; set; } = new List<Track>();
        public int Followers { get; set; }
    }
}
