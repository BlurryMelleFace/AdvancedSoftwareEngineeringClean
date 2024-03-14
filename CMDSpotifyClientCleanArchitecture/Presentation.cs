using CMDSpotifyClient.UseCases;
using CMDSpotifyClient.UseCases.Interfaces;

namespace CMDSpotifyClient.Presentation
{
    //Search Pages
    public class SearchTrackScreen
    {
        private readonly ISearchTrackUseCase _searchTrackUseCase;
        private readonly IGetTrackUseCase _getTrackUseCase;
        public SearchTrackScreen(
            ISearchTrackUseCase searchTrackUseCase,
            IGetTrackUseCase getTrackUseCase
            )
        {
            _searchTrackUseCase = searchTrackUseCase;
            _getTrackUseCase = getTrackUseCase;
        }
        public async Task ShowAsync()
        {
            Console.Clear();
            Console.WriteLine("Geben Sie den Namen des Tracks ein, den Sie suchen möchten: ");
            var trackName = Console.ReadLine();
            List<string> listOfTrackIds = new List<string>();
            try
            {
                Console.Clear();
                Console.WriteLine("5 Vorschläge basierend auf ihrer Suchanfrage");
                Console.WriteLine("\n");
                var tracks = await _searchTrackUseCase.ExecuteAsync(trackName); 
                foreach (var track in tracks)
                {
                    Console.WriteLine($"Gefunden: {track.Name} von {string.Join(", ", track.Artists.Select(a => a.Name))}");
                    Console.WriteLine($"SpotifyID: {track.Id}");
                    listOfTrackIds.Add( track.Id );
                    Console.WriteLine("\n");
                }
            }
            catch (Exception ex)
            {
                Console.Clear();
                Console.WriteLine($"Ein Fehler ist aufgetreten: {ex.Message}");
            }

            Console.WriteLine("\n");
            Console.WriteLine("1. Zeige mir die Tracks");
            Console.WriteLine("2. <--");
            var option = Console.ReadLine();

            try
            {
                switch (option)
                {
                    case "1":
                        var GetTrackScreen = new GetTrackScreen(_getTrackUseCase,listOfTrackIds);
                        await GetTrackScreen.ShowAsync();
                        break;
                    case "2":
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.Clear();
                Console.WriteLine($"Ein Fehler ist aufgetreten: {ex.Message}");
            }

            Console.WriteLine("Drücke eine beliebige Taste, um fortzufahren...");
            Console.ReadKey();
        }
    }
    public class SearchArtistScreen
    {
        private readonly ISearchArtistUseCase _searchArtistUseCase;

        public SearchArtistScreen(ISearchArtistUseCase searchArtistUseCase)
        {
            _searchArtistUseCase = searchArtistUseCase;
        }
        public async Task ShowAsync()
        {
            Console.Clear();
            Console.WriteLine("Geben Sie den Namen des Artisten ein, den Sie suchen möchten:");
            var artistName = Console.ReadLine();

            try
            {
                var artists = await _searchArtistUseCase.ExecuteAsync(artistName);
                foreach (var artist in artists)
                {
                    Console.Clear();
                    Console.WriteLine($"Gefunden: {artist.Name}");
                    Console.WriteLine($"SpotifyID: {artist.Id}");
                    Console.WriteLine("\n");
                }
            }
            catch (Exception ex)
            {
                Console.Clear();
                Console.WriteLine($"Ein Fehler ist aufgetreten: {ex.Message}");
            }

            Console.WriteLine("Drücke eine beliebige Taste, um fortzufahren...");
            Console.ReadKey();
        }
    }
    public class SearchAlbumScreen
    {
        private readonly ISearchAlbumUseCase _searchAlbumUseCase;

        public SearchAlbumScreen(ISearchAlbumUseCase searchAlbumUseCase)
        {
            _searchAlbumUseCase = searchAlbumUseCase;
        }
        public async Task ShowAsync()
        {
            Console.Clear();
            Console.WriteLine("Geben Sie den Namen des Albums ein, das Sie suchen möchten:");
            var albumName = Console.ReadLine();

            try
            {
                var albums = await _searchAlbumUseCase.ExecuteAsync(albumName);
                foreach (var album in albums)
                {
                    Console.Clear();
                    Console.WriteLine($"Gefunden: {album.Name} von  {string.Join(", ", album.Artists.Select(a => a.Name))}");
                    Console.WriteLine($"SpotifyID: {album.Id}");
                    Console.WriteLine("\n");
                }
            }
            catch (Exception ex)
            {
                Console.Clear();
                Console.WriteLine($"Ein Fehler ist aufgetreten: {ex.Message}");
            }

            Console.WriteLine("Drücke eine beliebige Taste, um fortzufahren...");
            Console.ReadKey();
        }
    }
    public class SearchGenrePlaylistScreen
    {
        private readonly ISearchGenrePlaylistUseCase _searchGenrePlaylistUseCase;

        public SearchGenrePlaylistScreen(ISearchGenrePlaylistUseCase searchGenrePlaylistUseCase)
        {
            _searchGenrePlaylistUseCase = searchGenrePlaylistUseCase;
        }
        public async Task ShowAsync()
        {
            Console.Clear();
            Console.WriteLine("Geben Sie einen Genre ein für das sie eine Playlist hören möchten:");
            var genreName = Console.ReadLine();

            try
            {
                var playlists = await _searchGenrePlaylistUseCase.ExecuteAsync(genreName);
                foreach (var playlist in playlists)
                {
                    Console.Clear();
                    Console.WriteLine($"Gefunden: {playlist.Name}");
                    Console.WriteLine($"SpotifyID: {playlist.Id}");
                    Console.WriteLine("\n");
                }
            }
            catch (Exception ex)
            {
                Console.Clear();
                Console.WriteLine($"Ein Fehler ist aufgetreten: {ex.Message}");
            }

            Console.WriteLine("Drücke eine beliebige Taste, um fortzufahren...");
            Console.ReadKey();
        }
    }
    //Get Pages

    public class GetTrackScreen
    {
        private readonly IGetTrackUseCase _getTrackUseCase;
        private readonly List<string> _listOfTrackIds;

        public GetTrackScreen(IGetTrackUseCase getTrackUseCase,List<string> listOfTrackIds)
        {
            _getTrackUseCase = getTrackUseCase;
            _listOfTrackIds = listOfTrackIds;
            
        }
        public async Task ShowAsync()
        {
            Console.Clear();
            try
            {
                foreach (var trackid in _listOfTrackIds)
                {
                    var tracks = await _getTrackUseCase.ExecuteAsync(trackid);
                    foreach (var track in tracks)
                    {
                        Console.WriteLine($"Gefunden: {track.Name}");
                        Console.WriteLine($"SpotifyID: {track.Id}");
                        Console.WriteLine($"Länge in Ms: {track.DurationMs}");
                        Console.WriteLine("\n");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Clear();
                Console.WriteLine($"Ein Fehler ist aufgetreten: {ex.Message}");
            }

            Console.WriteLine("Drücke eine beliebige Taste, um fortzufahren...");
            Console.ReadKey();
        }
    }

    // Main Menu Page
    public class MainMenuPage
    {
        private readonly ISearchTrackUseCase _searchTrackUseCase;
        private readonly ISearchArtistUseCase _searchArtistUseCase;
        private readonly ISearchAlbumUseCase _searchAlbumUseCase;
        private readonly ISearchGenrePlaylistUseCase _searchGenrePlaylistUseCase;

        private readonly IGetTrackUseCase _getTrackUseCase;


        public MainMenuPage(
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

        public async Task ShowAsync()
        {
            bool exit = false;
            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("Hauptmenü");
                Console.WriteLine("1. Suche nach einem Track");
                Console.WriteLine("2. Suche nach einem Artist");
                Console.WriteLine("3. Suche nach einem Album");
                Console.WriteLine("4. Suche nach einem Genre");
                Console.WriteLine("5. Beenden");
                Console.Write("Wählen Sie eine Option: ");

                var option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        var searchTrackScreen = new SearchTrackScreen(_searchTrackUseCase,_getTrackUseCase);
                        await searchTrackScreen.ShowAsync();
                        break;
                    case "2":
                        var searchArtistScreen = new SearchArtistScreen(_searchArtistUseCase); 
                        await searchArtistScreen.ShowAsync();
                        break;
                    case "3":
                        var searchAlbumScreen = new SearchAlbumScreen(_searchAlbumUseCase);
                        await searchAlbumScreen.ShowAsync();
                        break;
                    case "4":
                        var searchGenrePlaylistScreen = new SearchGenrePlaylistScreen(_searchGenrePlaylistUseCase);
                        await searchGenrePlaylistScreen.ShowAsync();
                        break;
                    case "5":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Ungültige Option, bitte erneut versuchen.");
                        Console.ReadKey();
                        break;
                }
            }
        }
    }
}
