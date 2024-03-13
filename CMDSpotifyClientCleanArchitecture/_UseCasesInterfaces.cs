using CMDSpotifyClient.Entities;

namespace CMDSpotifyClient.UseCases.Interfaces
{
    public interface ISearchTrackUseCase
    {
        Task<List<Track>> ExecuteAsync(string trackName);
    }
    public interface ISearchArtistUseCase
    {
        Task<List<Artist>> ExecuteAsync(string artistName);
    }
    public interface ISearchAlbumUseCase
    {
        Task<List<Album>> ExecuteAsync(string albumName);
    }
    public interface ISearchGenrePlaylistUseCase
    {
        Task<List<Playlist>> ExecuteAsync(string genreName);
    }

    public interface IGetAlbumUseCase
    {
        Task<Album> ExecuteAsync(string trackId);
    }

    // Ähnliche Interfaces für Alben, Künstler, Playlists...
}
