using CMDSpotifyClient.UseCases.Interfaces;
using CMDSpotifyClientCleanArchitecture.Controller;

namespace CMDSpotifyClient.Presentation
{
    //Search Pages
    public class SearchTrackScreen
    {
        private Controller _controller;
        public SearchTrackScreen(Controller controller)
        {
            _controller = controller;
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
                var tracks = await _controller.SearchTrack(trackName);
                foreach (var track in tracks)
                {
                    Console.WriteLine($"Gefunden: {track.Name} von {string.Join(", ", track.Artists.Select(a => a.Name))}");
                    Console.WriteLine($"SpotifyID: {track.Id}");
                    listOfTrackIds.Add(track.Id);
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
                        var GetTrackScreen = new GetTrackScreen(_controller, listOfTrackIds);
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
        private Controller _controller;

        public SearchArtistScreen(Controller controller)
        {
            _controller = controller;
        }
        public async Task ShowAsync()
        {
            Console.Clear();
            Console.WriteLine("Geben Sie den Namen des Artisten ein, den Sie suchen möchten:");
            var artistName = Console.ReadLine();

            try
            {
                var artists = await _controller.SearchArtist(artistName);
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
        private readonly Controller _controller;

        public SearchAlbumScreen(Controller controller)
        {
            _controller = controller;
        }
        public async Task ShowAsync()
        {
            Console.Clear();
            Console.WriteLine("Geben Sie den Namen des Albums ein, das Sie suchen möchten:");
            var albumName = Console.ReadLine();

            try
            {
                var albums = await _controller.SearchAlbum(albumName);
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
        private readonly Controller _controller;

        public SearchGenrePlaylistScreen(Controller controller)
        {
            _controller = controller;
        }
        public async Task ShowAsync()
        {
            Console.Clear();
            Console.WriteLine("Geben Sie einen Genre ein für das sie eine Playlist hören möchten:");
            var genreName = Console.ReadLine();

            try
            {
                var playlists = await _controller.SearchGenrePlalyist(genreName);
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
        private Controller _controller;
        private readonly List<string> _listOfTrackIds;

        public GetTrackScreen(Controller controller, List<string> listOfTrackIds)
        {
            _controller = controller;
            _listOfTrackIds = listOfTrackIds;
        }
        public async Task ShowAsync()
        {
            Console.Clear();
            try
            {
                foreach (var trackid in _listOfTrackIds)
                {
                    var tracks = await _controller.GetTrack(trackid);
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
        private Controller _controller;

        public MainMenuPage(Controller controller)

        {
            _controller = controller;
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
                        var searchTrackScreen = new SearchTrackScreen(_controller);
                        await searchTrackScreen.ShowAsync();
                        break;
                    case "2":
                        var searchArtistScreen = new SearchArtistScreen(_controller);
                        await searchArtistScreen.ShowAsync();
                        break;
                    case "3":
                        var searchAlbumScreen = new SearchAlbumScreen(_controller);
                        await searchAlbumScreen.ShowAsync();
                        break;
                    case "4":
                        var searchGenrePlaylistScreen = new SearchGenrePlaylistScreen(_controller);
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
