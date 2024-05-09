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
                throw new HttpRequestException($"Spotify API returned error: {response.StatusCode}");
            }
            return await response.Content.ReadAsStringAsync();
        }

        private Task<string> SearchSpotifyAsync (string searchType, string query) =>
            GetAuthorizedContentAsync($"{BaseSpotifyUrl}/search?q={Uri.EscapeDataString(query)}&type={searchType}&limit=1");


        public Task<string> SearchTrackAsync (string trackName) => SearchSpotifyAsync("track", trackName);
        public Task<string> SearchAlbumAsync (string albumName) => SearchSpotifyAsync("album", albumName);
        public Task<string> SearchArtistAsync (string artistName) => SearchSpotifyAsync("artist", artistName);
        public Task<string> SearchGenrePlaylistAsync (string genreName) => SearchSpotifyAsync("playlist", genreName);


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
