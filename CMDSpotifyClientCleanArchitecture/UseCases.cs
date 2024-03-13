using CMDSpotifyClient.Entities;
using CMDSpotifyClient.InterfaceAdapters.Interfaces;
using CMDSpotifyClient.UseCases.Interfaces;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace CMDSpotifyClient.UseCases
{
    public class SearchTrackUseCase : ISearchTrackUseCase
    {
        private readonly ISpotifySearchAdapter _spotifySearchAdapter;

        public SearchTrackUseCase(ISpotifySearchAdapter spotifySearchAdapter)
        {
            _spotifySearchAdapter = spotifySearchAdapter;
        }

        public async Task<Track> ExecuteAsync(string trackName)
        {
            var jsonResult = await _spotifySearchAdapter.SearchTrackAsync(trackName);
            var track = JsonConvert.DeserializeObject<Track>(jsonResult);
            return track;
        }
    }

    public class GetAlbumUseCase : IGetAlbumUseCase
    {
        private readonly ISpotifyDataRetrievalAdapter _spotifyDataRetrievalAdapter;

        public GetAlbumUseCase(ISpotifyDataRetrievalAdapter spotifyDataRetrievalAdapter)
        {
            _spotifyDataRetrievalAdapter = spotifyDataRetrievalAdapter;
        }

        public async Task<Album> ExecuteAsync(string albumId)
        {
            var jsonResult = await _spotifyDataRetrievalAdapter.GetAlbumAsync(albumId);
            var album = JsonConvert.DeserializeObject<Album>(jsonResult); // Beispielhafte Deserialisierung
            return album;
        }
    }
}
