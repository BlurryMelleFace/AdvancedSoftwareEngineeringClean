using CMDSpotifyClient.Entities;
using CMDSpotifyClient.UseCases.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CMDSpotifyClientCleanArchitecture.Controller
{
    public class Controller
    {
        private readonly ISearchTrackUseCase _searchTrackUseCase;
        private readonly ISearchArtistUseCase _searchArtistUseCase;
        private readonly ISearchAlbumUseCase _searchAlbumUseCase;
        private readonly ISearchGenrePlaylistUseCase _searchGenrePlaylistUseCase;
        private readonly IGetTrackUseCase _getTrackUseCase;
        private readonly IGetArtistUseCase _getArtistUseCase;
        private readonly IGetAlbumUseCase _getAlbumUseCase;
        private readonly IGetGenrePlaylistUseCase _getGenrePlaylistUseCase;

        public Controller(
            ISearchTrackUseCase searchTrackUseCase,
            ISearchArtistUseCase searchArtistUseCase,
            ISearchAlbumUseCase searchAlbumUseCase,
            ISearchGenrePlaylistUseCase searchGenrePlaylistUseCase,
            IGetTrackUseCase getTrackUseCase,
            IGetArtistUseCase getArtistUseCase,
            IGetAlbumUseCase getAlbumUseCase,
            IGetGenrePlaylistUseCase getGenrePlaylistUseCase)
        {
            _searchTrackUseCase = searchTrackUseCase;
            _searchArtistUseCase = searchArtistUseCase;
            _searchAlbumUseCase = searchAlbumUseCase;
            _searchGenrePlaylistUseCase = searchGenrePlaylistUseCase;
            _getTrackUseCase = getTrackUseCase;
            _getArtistUseCase = getArtistUseCase;
            _getAlbumUseCase = getAlbumUseCase;
            _getGenrePlaylistUseCase = getGenrePlaylistUseCase;
        }
        // Search Use Cases
        public Task<List<Track>> SearchTrack(string trackName)
        {
            return _searchTrackUseCase.ExecuteAsync(trackName);
        }
        public Task<List<Artist>> SearchArtist(string artistName)
        {
            return _searchArtistUseCase.ExecuteAsync(artistName);
        }
        public Task<List<Album>> SearchAlbum(string albumName)
        {
            return _searchAlbumUseCase.ExecuteAsync(albumName);
        }
        public Task<List<Playlist>> SearchGenrePlayist(string genreName)
        {
            return _searchGenrePlaylistUseCase.ExecuteAsync(genreName);
        }
        // Get Use Cases 
        public Task<List<Track>> GetTrack(string trackId)
        {
            return _getTrackUseCase.ExecuteAsync(trackId);
        }

        public Task<List<Artist>> GetArtist(string artistId)
        {
            return _getArtistUseCase.ExecuteAsync(artistId);
        }

        public Task<List<Album>> GetAlbum(string albumId)
        {
            return _getAlbumUseCase.ExecuteAsync(albumId);
        }

        public Task<List<Playlist>> GetGenrePlaylist(string playlistId)
        {
            return _getGenrePlaylistUseCase.ExecuteAsync(playlistId);
        }
    }
}
