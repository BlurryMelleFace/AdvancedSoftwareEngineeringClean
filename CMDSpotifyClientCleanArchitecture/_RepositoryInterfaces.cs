using CMDSpotifyClient.Entities;

namespace CMDSpotifyClient.Repository.Interfaces
{
    // Search Interfaces
    public interface ISearchRepository
    {
        Task<List<Track>> SearchTracksAsync(string trackName);
        Task<List<Artist>> SearchArtistsAsync(string artistName);
        Task<List<Album>> SearchAlbumsAsync(string albumName);
        Task<List<Playlist>> SearchGenrePlaylistsAsync(string genreName);
    }

    // Get Interfaces
    public interface IRetrievalRepository
    {
        Task<List<Track>> GetTrackAsync(string trackId);
        Task<List<Artist>> GetArtistAsync(string artistId);
        Task<List<Album>> GetAlbumAsync(string albumId);
        Task<List<Playlist>> GetGenrePlaylistAsync(string playlistId);
    }
}
