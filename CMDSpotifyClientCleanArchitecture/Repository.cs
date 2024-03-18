using CMDSpotifyClient.InterfaceAdapters.Interfaces;
using CMDSpotifyClient.Responses;
using CMDSpotifyClient.Entities;
using CMDSpotifyClient.Repository.Interfaces;
using Newtonsoft.Json;

namespace CMDSpotifyClient.Repository
{
    public class TrackRepository : ITrackRepository
    {
        private readonly ISpotifySearchAdapter _spotifySearchAdapter;

        public TrackRepository(ISpotifySearchAdapter spotifySearchAdapter)
        {
            _spotifySearchAdapter = spotifySearchAdapter;
        }

        public async Task<List<Track>> SearchTracksAsync(string trackName)
        {
            var jsonResult = await _spotifySearchAdapter.SearchTrackAsync(trackName);
            var response = JsonConvert.DeserializeObject<JSONResponses.SearchForItem.Rootobject>(jsonResult);
            var tracks = new List<Track>();
            foreach (var trackItem in response.tracks.items)
            {
                var track = new Track
                {
                    Id = trackItem.id,
                    Name = trackItem.name,
                    Artists = trackItem.artists.Select(artist => new Artist
                    {
                        Id = artist.id,
                        Name = artist.name
                    }).ToList(),
                    //DurationMs = trackItem.duration_ms,
                    //Explicit = trackItem._explicit,
                    //Album = new Album
                    //{
                    //    Id = trackItem.album.id,
                    //    Name = trackItem.album.name,
                    //},
                    //PreviewUrl = trackItem.preview_url,
                    //Popularity = trackItem.popularity,
                };

                tracks.Add(track);
            }

            return tracks;
        }
    }

    // Implementierung weiterer Repositories analog
}
