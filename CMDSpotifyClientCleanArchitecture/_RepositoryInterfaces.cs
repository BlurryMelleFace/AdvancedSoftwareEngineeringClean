using CMDSpotifyClient.Entities;

namespace CMDSpotifyClient.Repository.Interfaces
{
    public interface ITrackRepository
    {
        Task<List<Track>> SearchTracksAsync(string trackName);
    }

    public interface IArtistRepository
    {
        Task<List<Artist>> SearchArtistsAsync(string artistName);
    }

    // Weitere Interfaces für Alben und Playlists
}
