using CMDSpotifyClient.InterfaceAdapters.Interfaces;
using CMDSpotifyClient.Responses;
using CMDSpotifyClient.Entities;
using CMDSpotifyClient.Repository.Interfaces;
using Newtonsoft.Json;

namespace CMDSpotifyClient.Repository
{
    public class SearchRepository : ISearchRepository
    {
        private readonly ISpotifySearchAdapter _spotifySearchAdapter;

        public SearchRepository(ISpotifySearchAdapter spotifySearchAdapter)
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
                };

                tracks.Add(track);
            }

            return tracks;
        }
        public async Task<List<Artist>> SearchArtistsAsync(string artistName)
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
        public async Task<List<Album>> SearchAlbumsAsync(string albumName)
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
        public async Task<List<Playlist>> SearchGenrePlaylistsAsync(string genreName)
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

    public class RetrievalRepository : IRetrievalRepository
    {
        private readonly ISpotifyDataRetrievalAdapter _spotifyDataRetrievalAdapter;

        public RetrievalRepository(ISpotifyDataRetrievalAdapter spotifyDataRetrievalAdapter)
        {
            _spotifyDataRetrievalAdapter = spotifyDataRetrievalAdapter;
        }
        private string ConvertMsToMinSec(int ms)
        {
            int totalSeconds = ms / 1000;
            int minutes = totalSeconds / 60;
            int seconds = totalSeconds % 60;
            return $"{minutes}:{seconds:D2}";
        }
        public async Task<List<Track>> GetTrackAsync(string trackId)
        {
            var jsonResult = await _spotifyDataRetrievalAdapter.GetTrackAsync(trackId);
            var response = JsonConvert.DeserializeObject<JSONResponses.GetTrack.Rootobject>(jsonResult);
            var tracks = new List<Track>();

            var track = new Track
            {
                Id = response.id,
                Name = response.name,
                DurationMin = ConvertMsToMinSec(response.duration_ms),
                Artists = response.artists.Select(artist => new Artist
                {
                    Id = artist.id,
                    Name = artist.name
                }).ToList(),
                Popularity = response.popularity,
                Explicit = response._explicit,
                Album = new Album
                {
                    Id = response.album.id,
                    Name = response.album.name,
                    TotalTracks = response.album.total_tracks,
                    ReleaseDate = response.album.release_date,
                },
                TrackNumber = response.track_number,
                PreviewUrl = response.preview_url,
                Type = response.type,
            };

            tracks.Add(track);

            return tracks;
        }
        public async Task<List<Artist>> GetArtistAsync(string artistId)
        {
            var jsonResult = await _spotifyDataRetrievalAdapter.GetArtistAsync(artistId);
            var response = JsonConvert.DeserializeObject<JSONResponses.GetArtists.Rootobject>(jsonResult);
            var artists = new List<Artist>();

            foreach (var artist in response.artists)
            {
                var artist1 = new Artist
                {
                    Id = artist.id,
                    Name = artist.name,
                    Followers = artist.followers.total,
                    Popularity = artist.popularity,
                    Genre = artist.genres.ToList(),
                };

                artists.Add(artist1);
            }

            return artists;
        }
        public async Task<List<Album>> GetAlbumAsync(string albumId)
        {
            var jsonResult = await _spotifyDataRetrievalAdapter.GetAlbumAsync(albumId);
            var response = JsonConvert.DeserializeObject<JSONResponses.GetAlbum.Rootobject>(jsonResult);
            var albums = new List<Album>();

            var album = new Album
            {
                Id = response.id,
                Name = response.name,
                TotalTracks = response.total_tracks,
                Popularity = response.popularity,
                ReleaseDate = response.release_date,
                Label = response.label,
                Artists = response.artists.Select(artist => new Artist
                {
                    Id = artist.id,
                    Name = artist.name
                }).ToList(),
                Tracks = response.tracks.items.Select(tracks => new Track
                {
                    Id = tracks.id,
                    Name = tracks.name
                }).ToList(),

            };

            albums.Add(album);

            return albums;
        }
        public async Task<List<Playlist>> GetGenrePlaylistAsync(string playlistId)
        {
            var jsonResult = await _spotifyDataRetrievalAdapter.GetGenrePlaylistAsync(playlistId);
            var response = JsonConvert.DeserializeObject<JSONResponses.GetPlaylist.Rootobject>(jsonResult);
            var playlists = new List<Playlist>();

            var playlist = new Playlist
            {
                Id = response.id,
                Name = response.name,
                Description = response.description,
                Followers = response.followers.total,
                Tracks = response.tracks.items.Select(tracks => new Track
                {
                    Id = tracks.track.id,
                    Name = tracks.track.name
                }).ToList(),

            };

            playlists.Add(playlist);

            return playlists;
        }
    }
}
