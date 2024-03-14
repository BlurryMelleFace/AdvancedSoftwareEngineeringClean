using CMDSpotifyClient.Entities;
using CMDSpotifyClient.UseCases.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public Controller(
            ISearchTrackUseCase searchTrackUseCase, 
            ISearchArtistUseCase searchArtistUseCase, 
            ISearchAlbumUseCase searchAlbumUseCase, 
            ISearchGenrePlaylistUseCase searchGenrePlaylistUseCase, 
            IGetTrackUseCase getTrackUseCase
            )
        {
            _searchTrackUseCase = searchTrackUseCase;
            _searchArtistUseCase = searchArtistUseCase;
            _searchAlbumUseCase = searchAlbumUseCase;
            _searchGenrePlaylistUseCase = searchGenrePlaylistUseCase;
            _getTrackUseCase = getTrackUseCase;
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
        public Task<List<Playlist>> SearchGenrePlalyist(string genreName)
        {
            return _searchGenrePlaylistUseCase.ExecuteAsync(genreName);
        }
        // Get Use Cases 
        public Task<List<Track>> GetTrack(string trackName)
        {
            return _getTrackUseCase.ExecuteAsync(trackName);
        }
    }
}
