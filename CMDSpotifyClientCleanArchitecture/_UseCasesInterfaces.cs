using CMDSpotifyClient.Entities;

namespace CMDSpotifyClient.UseCases.Interfaces
{
    //Search Use Case Interfaces
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
    //Get Use Case Interfaces
    public interface IGetTrackUseCase
    {
        Task<List<Track>> ExecuteAsync(string trackId);
    }
    public interface IGetArtistUseCase
    {
        Task<List<Artist>> ExecuteAsync(string artistId);
    }
    public interface IGetAlbumUseCase
    {
        Task<List<Album>> ExecuteAsync(string albumId);
    }
    public interface IGetGenrePlaylistUseCase
    {
        Task<List<Playlist>> ExecuteAsync(string playlistId);
    }

}
