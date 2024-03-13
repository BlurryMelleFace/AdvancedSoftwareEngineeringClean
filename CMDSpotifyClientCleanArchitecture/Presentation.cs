using CMDSpotifyClient.UseCases;
using CMDSpotifyClient.UseCases.Interfaces;

namespace CMDSpotifyClient.Presentation
{
    public class SearchTrackScreen
    {
        private readonly ISearchTrackUseCase _searchTrackUseCase;

        public SearchTrackScreen(ISearchTrackUseCase searchTrackUseCase)
        {
            _searchTrackUseCase = searchTrackUseCase;
        }

        public async Task ShowAsync()
        {
            Console.WriteLine("Geben Sie den Namen des Tracks ein, den Sie suchen möchten:");
            var trackName = Console.ReadLine();

            try
            {
                var tracks = await _searchTrackUseCase.ExecuteAsync(trackName);
                foreach (var track in tracks) 
                {
                    Console.WriteLine($"Gefunden: {track.Name} von {string.Join(", ", track.Artists.Select(a => a.Name))}");
                }
            }
            catch (Exception ex)
            {
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
            Console.WriteLine("Geben Sie den Namen des Artisten ein, den Sie suchen möchten:");
            var artistName = Console.ReadLine();

            try
            {
                var artists = await _searchArtistUseCase.ExecuteAsync(artistName);
                foreach (var artist in artists)
                {
                    Console.WriteLine($"Gefunden: {artist.Name}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ein Fehler ist aufgetreten: {ex.Message}");
            }

            Console.WriteLine("Drücke eine beliebige Taste, um fortzufahren...");
            Console.ReadKey();
        }


    }
    public class MainMenuPage
    {
        private readonly ISearchTrackUseCase _searchTrackUseCase;
        private readonly ISearchArtistUseCase _searchArtistUseCase;
        private readonly ISearchAlbumUseCase _searchAlbumUseCase;
        // Fügen Sie weitere Use Cases hinzu, je nachdem, welche Funktionen Sie anbieten möchten

        public MainMenuPage(
            ISearchTrackUseCase searchTrackUseCase,
            ISearchArtistUseCase searchArtistUseCase
            )
        {
            _searchTrackUseCase = searchTrackUseCase;
            _searchArtistUseCase = searchArtistUseCase;
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
                Console.WriteLine("3. Beenden");
                Console.Write("Wählen Sie eine Option: ");

                var option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        var searchTrackScreen = new SearchTrackScreen(_searchTrackUseCase);
                        await searchTrackScreen.ShowAsync();
                        break;
                    case "2":
                        var searchArtistScreen = new SearchArtistScreen(_searchArtistUseCase); 
                        await searchArtistScreen.ShowAsync();
                        break;
                    case "3":
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
