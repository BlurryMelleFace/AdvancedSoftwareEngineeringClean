using CMDSpotifyClient.Entities;

namespace CMDSpotifyClient.Repository.Interfaces
{
    // Search Interfaces
    public interface ISearchRepository
    {
        Task<List<Track>> SearchTracksAsync(string trackName);
        Task<List<Artist>> SearchArtistsAsync(string artistName);
        Task<List<Album>> SearchAlbumAsync(string artistName);
        Task<List<Playlist>> SearchGenrePlaylistsAsync(string genreName);
    }

    // Get Interfaces
    public interface IRetrievalRepository
    {
        Task<Track> GetTrackAsync(string trackId);
        Task<Artist> GetArtistAsync(string artistId);
        Task<Album> GetAlbumAsync(string albumId);
        Task<Playlist> GetGenrePlaylistAsync(string playlistId);
    }
}
