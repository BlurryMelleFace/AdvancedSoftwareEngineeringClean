using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using CMDSpotifyClient.Entities;
using CMDSpotifyClient.Infrastructure;
using CMDSpotifyClient.InterfaceAdapters.Interfaces;

namespace CMDSpotifyClient.InterfaceAdapters
{
    public class SpotifyAdapter : ISpotifySearchAdapter, ISpotifyDataRetrievalAdapter
    {
        private readonly HttpClient _httpClient;
        private readonly SpotifyCredentials _spotifyCredentials;
        private const string BaseSpotifyUrl = "https://api.spotify.com/v1";

        public SpotifyAdapter (HttpClient httpClient, SpotifyCredentials spotifyCredentials)
        {
            _httpClient = httpClient;
            _spotifyCredentials = spotifyCredentials;
        }

        private async Task<string> GetAuthorizedContentAsync (string uri)
        {
            var accessToken = await _spotifyCredentials.EnsureAccessTokenAsync();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var response = await _httpClient.GetAsync(uri);
            if (!response.IsSuccessStatusCode)
            {
                return $"Spotify API returned error";
            }
            return await response.Content.ReadAsStringAsync();
        }

        public Task<string> SearchTrackAsync (string trackName) =>
            GetAuthorizedContentAsync($"{BaseSpotifyUrl}/search?q={Uri.EscapeDataString(trackName)}&type=track&limit=1");

        public Task<string> SearchAlbumAsync (string albumName) =>
            GetAuthorizedContentAsync($"{BaseSpotifyUrl}/search?q={Uri.EscapeDataString(albumName)}&type=album&limit=1");

        public Task<string> SearchArtistAsync (string artistName) =>
            GetAuthorizedContentAsync($"{BaseSpotifyUrl}/search?q={Uri.EscapeDataString(artistName)}&type=artist&limit=1");

        public Task<string> SearchGenrePlaylistAsync (string genreName) =>
            GetAuthorizedContentAsync($"{BaseSpotifyUrl}/search?q={Uri.EscapeDataString(genreName)}&type=playlist&limit=1");

        public Task<string> GetTrackAsync (string trackId) =>
            GetAuthorizedContentAsync($"{BaseSpotifyUrl}/tracks/{trackId}");

        public Task<string> GetAlbumAsync (string albumId) =>
            GetAuthorizedContentAsync($"{BaseSpotifyUrl}/albums/{albumId}");

        public Task<string> GetArtistAsync (string artistId) =>
            GetAuthorizedContentAsync($"{BaseSpotifyUrl}/artists?ids={artistId}");

        public Task<string> GetGenrePlaylistAsync (string playlistId) =>
            GetAuthorizedContentAsync($"{BaseSpotifyUrl}/playlists/{playlistId}");
    }
}
