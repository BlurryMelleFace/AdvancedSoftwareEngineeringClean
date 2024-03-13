using CMDSpotifyClient.Entities;
using CMDSpotifyClient.InterfaceAdapters.Interfaces;
using CMDSpotifyClient.UseCases.Interfaces;
using CMDSpotifyClient.Responses;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace CMDSpotifyClient.UseCases
{
    public class SearchTrackUseCase : ISearchTrackUseCase
    {
        private readonly ISpotifySearchAdapter _spotifySearchAdapter;
        public SearchTrackUseCase(ISpotifySearchAdapter spotifySearchAdapter)
        {
            _spotifySearchAdapter = spotifySearchAdapter;
        }

        public async Task<List<Track>> ExecuteAsync(string trackName)
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
                    DurationMs = trackItem.duration_ms,
                    Explicit = trackItem._explicit,
                    Artists = trackItem.artists.Select(artist => new Artist
                    {
                        Id = artist.id,
                        Name = artist.name
                    }).ToList(),
                    Album = new Album
                    {
                        Id = trackItem.album.id,
                        Name = trackItem.album.name,

                    },
                    PreviewUrl = trackItem.preview_url,
                    Popularity = trackItem.popularity,
                };

                tracks.Add(track);
            }

            return tracks;
        }
    }

    public class SearchArtistUseCase : ISearchArtistUseCase
    {
        private readonly ISpotifySearchAdapter _spotifySearchAdapter;
        public SearchArtistUseCase(ISpotifySearchAdapter spotifySearchAdapter)
        {
            _spotifySearchAdapter = spotifySearchAdapter;
        }
        public async Task<List<Artist>> ExecuteAsync(string artistName)
        {
            var jsonResult = await _spotifySearchAdapter.SearchArtistAsync(artistName);
            var response = JsonConvert.DeserializeObject<JSONResponses.SearchForItem.Rootobject>(jsonResult);
            var Artists = new List<Artist>();
            foreach (var artistItem in response.artists.items)
            {
                var track = new Artist
                {
                    Id = artistItem.id,
                    Name = artistItem.name,
                };

                Artists.Add(track);
            }

            return Artists;

        }
    }

    public class GetAlbumUseCase : IGetAlbumUseCase
    {
        private readonly ISpotifyDataRetrievalAdapter _spotifyDataRetrievalAdapter;

        public GetAlbumUseCase(ISpotifyDataRetrievalAdapter spotifyDataRetrievalAdapter)
        {
            _spotifyDataRetrievalAdapter = spotifyDataRetrievalAdapter;
        }

        public async Task<Album> ExecuteAsync(string albumId)
        {
            var jsonResult = await _spotifyDataRetrievalAdapter.GetAlbumAsync(albumId);
            var album = JsonConvert.DeserializeObject<Album>(jsonResult); // Beispielhafte Deserialisierung
            return album;
        }
    }
}

