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
                var artist = new Artist
                {
                    Id = artistItem.id,
                    Name = artistItem.name,
                };

                Artists.Add(artist);
            }

            return Artists;

        }
    }

    public class SearchAlbumUseCase : ISearchAlbumUseCase
    {
        private readonly ISpotifySearchAdapter _spotifySearchAdapter;
        public SearchAlbumUseCase(ISpotifySearchAdapter spotifySearchAdapter)
        {
            _spotifySearchAdapter = spotifySearchAdapter;
        }
        public async Task<List<Album>> ExecuteAsync(string albumName)
        {
            var jsonResult = await _spotifySearchAdapter.SearchAlbumAsync(albumName);
            var response = JsonConvert.DeserializeObject<JSONResponses.SearchForItem.Rootobject>(jsonResult);
            var Albums = new List<Album>();
            foreach (var albumItem in response.albums.items)
            {
                var album = new Album
                {
                    Id = albumItem.id,
                    Name = albumItem.name,
                    Artists = albumItem.artists.Select(artist => new Artist
                    {
                        Id = artist.id,
                        Name = artist.name
                    }).ToList(),
                };

                Albums.Add(album);
            }

            return Albums;

        }
    }
    public class SearchGenrePlaylistUseCase : ISearchGenrePlaylistUseCase
    {
        private readonly ISpotifySearchAdapter _spotifySearchAdapter;
        public SearchGenrePlaylistUseCase(ISpotifySearchAdapter spotifySearchAdapter)
        {
            _spotifySearchAdapter = spotifySearchAdapter;
        }
        public async Task<List<Playlist>> ExecuteAsync(string genreName)
        {
            var jsonResult = await _spotifySearchAdapter.SearchGenrePlaylistAsync(genreName);
            var response = JsonConvert.DeserializeObject<JSONResponses.SearchForItem.Rootobject>(jsonResult);
            var Playlists = new List<Playlist>();
            foreach (var playlistItem in response.playlists.items)
            {
                var playlist = new Playlist
                {
                    Id = playlistItem.id,
                    Name = playlistItem.name,
                };

                Playlists.Add(playlist);
            }

            return Playlists;

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

