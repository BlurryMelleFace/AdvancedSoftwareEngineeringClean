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
                var track = await _searchTrackUseCase.ExecuteAsync(trackName);
                Console.WriteLine($"Gefunden: {track.Name} von {string.Join(", ", track.Artists.Select(a => a.Name))}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ein Fehler ist aufgetreten: {ex.Message}");
            }

            Console.WriteLine("Drücken Sie eine beliebige Taste, um fortzufahren...");
            Console.ReadKey();
        }
    }
    public class MainMenuPage
    {
        private readonly ISearchTrackUseCase _searchTrackUseCase;
        // Fügen Sie weitere Use Cases hinzu, je nachdem, welche Funktionen Sie anbieten möchten

        public MainMenuPage(ISearchTrackUseCase searchTrackUseCase)
        {
            _searchTrackUseCase = searchTrackUseCase;
        }

        public async Task ShowAsync()
        {
            bool exit = false;
            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("Hauptmenü");
                Console.WriteLine("1. Suche nach einem Track");
                Console.WriteLine("2. Beenden");
                Console.Write("Wählen Sie eine Option: ");

                var option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        var searchTrackScreen = new SearchTrackScreen(_searchTrackUseCase);
                        await searchTrackScreen.ShowAsync();
                        break;
                    case "2":
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
