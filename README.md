# CMDSpotifyClient

CMDSpotifyClient is a console-based application designed to interact with the Spotify Web API. It enables users to search for tracks, albums, artists, and genre-specific playlists and retrieve detailed information about them. This client uses a clean architecture approach, dividing the solution into several projects, including domain entities, use cases, interface adapters, and infrastructure.

## Features

- Search for tracks, artists, albums, and playlists based on genre.
- Retrieve detailed information about a specific track, artist, album, or playlist.
- Interact with Spotify's Web API using a set of predefined commands.
- Console-based user interface for easy navigation and interaction.

## Getting Started

These instructions will get you a copy of the project up and running on your local machine for development and testing purposes.

### Prerequisites

- .NET 5.0 or higher
- A Spotify Developer account and a registered application to obtain the necessary client ID and client secret.

### Installing

1. Clone the repository:
   ```
   git clone https://github.com/yourusername/CMDSpotifyClient.git
   ```
2. Navigate to the project directory:
   ```
   cd CMDSpotifyClient
   ```
3. Restore NuGet packages:
   ```
   dotnet restore
   ```
4. Build the solution:
   ```
   dotnet build
   ```
5. Run the application:
   ```
   dotnet run
   ```

### Configuration

Before running the application, ensure you have updated the `SpotifyCredentials` class with your Spotify client ID and client secret:

```csharp
var clientId = "YOUR_CLIENT_ID";
var clientSecret = "YOUR_CLIENT_SECRET";
```

## Usage

After starting the application, you will be presented with a main menu where you can choose to search for tracks, artists, albums, or playlists. Follow the on-screen instructions to navigate through the application.

## Built With

- [.NET Core](https://dotnet.microsoft.com/) - The framework used
- [Spotify Web API](https://developer.spotify.com/documentation/web-api/) - External service for music data

## Contributing

Moritz Staudacher

## Versioning

Git is used for Versioning

## Authors

Moritz Staudacher
- Anyone whose code was used

---

Remember to replace placeholders like `yourusername`, `YOUR_CLIENT_ID`, and `YOUR_CLIENT_SECRET` with your actual data. This template should be adapted as necessary to fit the specifics of your project better.
