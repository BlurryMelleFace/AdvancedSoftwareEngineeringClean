using CMDSpotifyClient.Entities;

namespace CMDSpotifyClient.UseCases.Interfaces
{
    public interface ISearchTrackUseCase
    {
        Task<Track> ExecuteAsync(string trackName);
    }

    public interface IGetAlbumUseCase
    {
        Task<Album> ExecuteAsync(string trackId);
    }

    // Ähnliche Interfaces für Alben, Künstler, Playlists...
}
