namespace CMDSpotifyClient.InterfaceAdapters.Interfaces
{
    public interface ISpotifySearchAdapter
    {
        Task<string> SearchTrackAsync(string trackName);
        Task<string> SearchAlbumAsync(string albumName);
        Task<string> SearchArtistAsync(string artistName);
        Task<string> SearchGenrePlaylistAsync(string playlistName);
    }

    public interface ISpotifyDataRetrievalAdapter
    {
        Task<string> GetTrackAsync(string trackId);
        Task<string> GetAlbumAsync(string albumId);
        Task<string> GetArtistAsync(string artistId);
        Task<string> GetPlaylistAsync(string playlistId);
    }
}
