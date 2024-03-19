using CMDSpotifyClient.UseCases.Interfaces;
using CMDSpotifyClientCleanArchitecture.Controller;
using System.Diagnostics;
using NAudio.Wave;

namespace CMDSpotifyClient.Presentation
{
    //Search Pages
    public class SearchTrackScreen
    {
        private Controller _controller;
        private List<string> _listOfTrackIds;
        public SearchTrackScreen(Controller controller)
        {
            _controller = controller;
            _listOfTrackIds = new List<string>();
        }
        public async Task ShowAsync()
        {
            Console.Clear();
            Console.WriteLine("Enter the name of the track you want to search for: ");
            var trackName = Console.ReadLine();
            try
            {
                Console.Clear();
                Console.WriteLine("Suggestion based on your search query");
                Console.WriteLine("\n");
                var tracks = await _controller.SearchTrack(trackName);
                foreach (var track in tracks)
                {
                    Console.WriteLine($"Found Track: {track.Name} - {string.Join(", ", track.Artists.Select(a => a.Name))}");
                    Console.WriteLine($"SpotifyID: {track.Id}");
                    Console.WriteLine("\n");

                    _listOfTrackIds.Add(track.Id);
                }
            }
            catch (Exception ex)
            {
                Console.Clear();
                Console.WriteLine($"There has been an Error: {ex.Message}");
            }

            Console.WriteLine("\n");
            Console.WriteLine("1. Show me the Track");
            Console.WriteLine("2. <--");
            Console.WriteLine("\n");
            var option = Console.ReadLine();

            try
            {
                switch (option)
                {
                    case "1":
                        var GetTrackScreen = new GetTrackScreen(_controller, _listOfTrackIds);
                        await GetTrackScreen.ShowAsync();
                        break;
                    case "2":
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.Clear();
                Console.WriteLine($"There has been an Error: {ex.Message}");
            }
        }
    }
    public class SearchArtistScreen
    {
        private Controller _controller;
        private List<string> _listOfArtistIds;
        public SearchArtistScreen(Controller controller)
        {
            _controller = controller;
            _listOfArtistIds = new List<string>();  
        }
        public async Task ShowAsync()
        {
            Console.Clear();
            Console.WriteLine("Enter the name of the artist you want to search for:");
            var artistName = Console.ReadLine();

            try
            {
                Console.Clear();
                Console.WriteLine("Suggestion based on your search query");
                Console.WriteLine("\n");
                var artists = await _controller.SearchArtist(artistName);
                foreach (var artist in artists)
                {
                    Console.WriteLine($"Gefunden: {artist.Name}");
                    Console.WriteLine($"SpotifyID: {artist.Id}");
                    Console.WriteLine("\n");
                    _listOfArtistIds.Add(artist.Id);
                }
            }
            catch (Exception ex)
            {
                Console.Clear();
                Console.WriteLine($"There has been an Error: {ex.Message}");
            }

            Console.WriteLine("\n");
            Console.WriteLine("1. Show me the Artist");
            Console.WriteLine("2. <--");
            Console.WriteLine("\n");
            var option = Console.ReadLine();

            try
            {
                switch (option)
                {
                    case "1":
                        var GetArtistScreen = new GetArtistScreen(_controller, _listOfArtistIds);
                        await GetArtistScreen.ShowAsync();
                        break;
                    case "2":
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.Clear();
                Console.WriteLine($"There has been an Error: {ex.Message}");
            }
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
            Console.WriteLine("Enter the name of the album you want to search for:");
            var albumName = Console.ReadLine();

            try
            {
                var albums = await _controller.SearchAlbum(albumName);
                foreach (var album in albums)
                {
                    Console.WriteLine($"Gefunden: {album.Name} -  {string.Join(", ", album.Artists.Select(a => a.Name))}");
                    Console.WriteLine($"SpotifyID: {album.Id}");
                    Console.WriteLine("\n");
                }
            }
            catch (Exception ex)
            {
                Console.Clear();
                Console.WriteLine($"There has been an Error: {ex.Message}");
            }

            Console.WriteLine("Press any button to continue...");
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
            Console.WriteLine("Enter a genre for which you would like to listen to a playlist:");
            var genreName = Console.ReadLine();

            try
            {
                var playlists = await _controller.SearchGenrePlayist(genreName);
                foreach (var playlist in playlists)
                {
                    Console.WriteLine($"Found: {playlist.Name}");
                    Console.WriteLine($"SpotifyID: {playlist.Id}");
                    Console.WriteLine("\n");
                }
            }
            catch (Exception ex)
            {
                Console.Clear();
                Console.WriteLine($"There has been an Error: {ex.Message}");
            }

            Console.WriteLine("Press any button to continue...");
            Console.ReadKey();
        }
    }
    //Get Pages

    public class GetTrackScreen
    {
        private Controller _controller;
        private readonly List<string> _listOfTrackIds;
        private string _previewUrl;

        public GetTrackScreen(Controller controller, List<string> listOfTrackIds)
        {
            _controller = controller;
            _listOfTrackIds = listOfTrackIds;
            _previewUrl = string.Empty; 
        }
        public async Task ShowAsync()
        {
            Console.Clear();
           
            try
            {
                foreach (var trackId in _listOfTrackIds)
                {
                    var tracks = await _controller.GetTrack(trackId);
                    foreach (var track in tracks)
                    {
                        Console.WriteLine($"Name of The Track: {track.Name}");
                        int i = 1;
                        foreach (var artist in track.Artists)
                        {
                            Console.WriteLine($"Artist {i}: {artist.Name}");
                            i++;
                        }
                        Console.WriteLine("\n");
                        Console.WriteLine($"SpotifyID: {track.Id}");
                        Console.WriteLine($"Duration in Min: {track.DurationMin}");
                        Console.WriteLine($"Popularity: {track.Popularity}");
                        Console.WriteLine($"Explicit: {track.Explicit}");
                        Console.WriteLine($"Name Of the Corresponding Album: {track.Album.Name}");
                        Console.WriteLine($"Album Release date: {track.Album.ReleaseDate}");
                        Console.WriteLine($"Total Tracks in Album: {track.Album.TotalTracks}");
                        Console.WriteLine($"Track Position in Album: {track.TrackNumber}");
                        Console.WriteLine($"Type: {track.Type}");
                        _previewUrl = track.PreviewUrl;
                        Console.WriteLine("\n");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Clear();
                Console.WriteLine($"There has been an Error: {ex.Message}");
            }

            Console.WriteLine("\n");
            Console.WriteLine("1. Preview Track");
            Console.WriteLine("2. <--");
            Console.WriteLine("\n");
            var option = Console.ReadLine();

            try
            {
                switch (option)
                {
                    case "1":
                        var PlayTrackScreen = new PlayTrackScreen(_controller,_previewUrl);
                        PlayTrackScreen.ShowAsync();
                        break;
                    case "2":
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.Clear();
                Console.WriteLine($"There has been an Error: {ex.Message}");
            }
        }
    }
    public class GetArtistScreen
    {
        private Controller _controller;
        private readonly List<string> _listOfArtistIds;

        public GetArtistScreen(Controller controller, List<string> listOfArtistIds)
        {
            _controller = controller;
            _listOfArtistIds = listOfArtistIds;
        }
        public async Task ShowAsync()
        {
            Console.Clear();

            try
            {
                foreach (var artistId in _listOfArtistIds)
                {
                    var artists = await _controller.GetArtist(artistId);
                    foreach (var artist in artists)
                    {
                        Console.WriteLine($"Name of The Artist: {artist.Name}");
                        Console.WriteLine("\n");
                        Console.WriteLine($"SpotifyID: {artist.Id}");
                        Console.WriteLine($"Followers: {artist.Followers}");
                        Console.WriteLine($"Popularity: {artist.Popularity}");
                        int i = 1;  
                        foreach (var genre in artist.Genre)
                        {
                            Console.WriteLine($"Genre {i}: {genre}");
                            i++;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Clear();
                Console.WriteLine($"There has been an Error: {ex.Message}");
            }

            Console.WriteLine("\n");
            Console.WriteLine("1. Preview Trac");
            Console.WriteLine("2. <--");
            Console.WriteLine("\n");
            var option = Console.ReadLine();

            try
            {
                switch (option)
                {
                    case "1":
                        break;
                    case "2":
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.Clear();
                Console.WriteLine($"There has been an Error: {ex.Message}");
            }
        }
    }
    // Play Track Screen
    public class PlayTrackScreen
    {
        private Controller _controller;
        private string _previewUrl;
        public PlayTrackScreen(Controller controller, string previewUrl)
        {
            _controller = controller;
            _previewUrl = previewUrl;
        }
        public void ShowAsync()
        {
            string audioUrl = _previewUrl;
            try
            {
                if (audioUrl == null)
                {
                    Console.WriteLine("Audio URL is null.");
                    Console.ReadLine();
                }
                using (var webStream = new MediaFoundationReader(audioUrl))
                using (var outputDevice = new WaveOutEvent())
                {
                    outputDevice.Init(webStream);
                    outputDevice.Play();

                    Console.WriteLine("Press any key to stop playback or go back to Track Search");
                    Console.ReadKey();

                    outputDevice.Stop();

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
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
                Console.WriteLine("Main Menu");
                Console.WriteLine("1. Search a Track");
                Console.WriteLine("2. Search an Artist");
                Console.WriteLine("3. Search an Album");
                Console.WriteLine("4. Search a Genre");
                Console.WriteLine("5. Quit");
                Console.Write("Choose an Option: ");

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
                        Console.WriteLine("Wrong Option, please try again.");
                        Console.ReadKey();
                        break;
                }
            }
        }
    }
}
