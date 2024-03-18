using CMDSpotifyClient.Entities;
using CMDSpotifyClient.InterfaceAdapters.Interfaces;
using CMDSpotifyClient.UseCases.Interfaces;
using CMDSpotifyClient.Responses;
using Newtonsoft.Json;
using System.Threading.Tasks;
using CMDSpotifyClient.Repository.Interfaces;

namespace CMDSpotifyClient.UseCases
{
    //Search Use Cases
    public class SearchTrackUseCase : ISearchTrackUseCase
    {
        private readonly ISearchRepository _searchTrackRepository;
        public SearchTrackUseCase(ISearchRepository searchTrackRepository)
        {
            _searchTrackRepository = searchTrackRepository;
        }
        public async Task<List<Track>> ExecuteAsync(string trackName)
        {
            return await _searchTrackRepository.SearchTracksAsync(trackName);
        }
    }
    public class SearchArtistUseCase : ISearchArtistUseCase
    {
        private readonly ISearchRepository _searchArtistRepository;
        public SearchArtistUseCase(ISearchRepository searchArtistRepository)
        {
            _searchArtistRepository = searchArtistRepository;
        }
        public async Task<List<Artist>> ExecuteAsync(string artistName)
        {
            return await _searchArtistRepository.SearchArtistsAsync(artistName);
        }
    }
    public class SearchAlbumUseCase : ISearchAlbumUseCase
    {
        private readonly ISearchRepository _searchAlbumRepository;
        public SearchAlbumUseCase(ISearchRepository searchAlbumRepository)
        {
            _searchAlbumRepository = searchAlbumRepository;
        }
        public async Task<List<Album>> ExecuteAsync(string albumName)
        {
            return await _searchAlbumRepository.SearchAlbumsAsync(albumName);
        }
    }
    public class SearchGenrePlaylistUseCase : ISearchGenrePlaylistUseCase
    {
        private readonly ISearchRepository _searchGenrePlalyistRepository;
        public SearchGenrePlaylistUseCase(ISearchRepository searchGenrePlaylistRepository)
        {
            _searchGenrePlalyistRepository = searchGenrePlaylistRepository;
        }
        public async Task<List<Playlist>> ExecuteAsync(string genreName)
        {
            return await _searchGenrePlalyistRepository.SearchGenrePlaylistsAsync(genreName);
        }
    }
    // Get Use Cases
    public class GetTrackUseCase : IGetTrackUseCase
    {
        private readonly IRetrievalRepository _getTrackUseCase;
        public GetTrackUseCase(IRetrievalRepository getTrackUseCase)
        {
            _getTrackUseCase = getTrackUseCase;
        }
        public async Task<List<Track>> ExecuteAsync(string trackId)
        {
            return await _getTrackUseCase.GetTrackAsync(trackId);
        }
    }
    public class GetArtistUseCase : IGetArtistUseCase
    {
        private readonly IRetrievalRepository _getArtistUseCase;
        public GetArtistUseCase(IRetrievalRepository getArtistUseCase)
        {
            _getArtistUseCase = getArtistUseCase;
        }
        public async Task<List<Artist>> ExecuteAsync(string artistId)
        {
            return await _getArtistUseCase.GetArtistAsync(artistId);
        }
    }
    public class GetAlbumUseCase : IGetAlbumUseCase
    {
        private readonly IRetrievalRepository _getAlbumUseCase;
        public GetAlbumUseCase(IRetrievalRepository getAlbumUseCase)
        {
            _getAlbumUseCase = getAlbumUseCase;
        }
        public async Task<List<Album>> ExecuteAsync(string albumId)
        {
            return await _getAlbumUseCase.GetAlbumAsync(albumId);
        }
    }

    public class GetGenrePlaylistUseCase : IGetGenrePlaylistUseCase
    {
        private readonly IRetrievalRepository _getGenrePlaylistUseCase;
        public GetGenrePlaylistUseCase(IRetrievalRepository getGenrePlaylistUseCase)
        {
            _getGenrePlaylistUseCase = getGenrePlaylistUseCase;
        }
        public async Task<List<Playlist>> ExecuteAsync(string playlistId)
        {
            return await _getGenrePlaylistUseCase.GetGenrePlaylistAsync(playlistId);
        }
    }
}

