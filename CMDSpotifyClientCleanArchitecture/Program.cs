using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using CMDSpotifyClient.InterfaceAdapters;
using CMDSpotifyClient.InterfaceAdapters.Interfaces;
using CMDSpotifyClient.UseCases;
using CMDSpotifyClient.UseCases.Interfaces;
using CMDSpotifyClient.Presentation;
using CMDSpotifyClient.Infrastructure;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Http;
using CMDSpotifyClientCleanArchitecture.Controller;
using CMDSpotifyClient.Repository.Interfaces;
using CMDSpotifyClient.Repository;

//Rechtsklicken Sie auf Ihr Projekt im Solution Explorer, wählen Sie "Manage NuGet Packages...", und suchen Sie dann nach den oben genannten Paketen, um sie zu installieren.
//package Microsoft.Extensions.Hosting
//package Microsoft.Extensions.DependencyInjection
//package Microsoft.Extensions.Http

var builder = Host.CreateDefaultBuilder(args);

builder.ConfigureServices((hostContext, services) =>
{
    // Registriere HTTP Client für die SpotifyAdapter
    services.AddHttpClient<ISpotifySearchAdapter, SpotifyAdapter>();
    services.AddHttpClient<ISpotifyDataRetrievalAdapter, SpotifyAdapter>();
    // Registrierte Repository-Klassen
    services.AddScoped<ISearchRepository, SearchRepository>();
    services.AddScoped<IRetrievalRepository, RetrievalRepository>();

    // Registrieren Sie HttpClient für SpotifyCredentials
    services.AddHttpClient<SpotifyCredentials>().ConfigureHttpClient(client =>
    {
        // Konfig?
    });

    // SpotifyCredentials als Singleton 
    services.AddSingleton(provider =>
    {
        var httpClientFactory = provider.GetRequiredService<IHttpClientFactory>();
        var httpClient = httpClientFactory.CreateClient();
        var clientId = "65030c7ddddc4cbe822c46c4277fe265";
        var clientSecret = "f8500ebc8b434ec98ebb0f525a02a55c";
        return new SpotifyCredentials(httpClient, clientId, clientSecret);
    });

    // Registrierte Search Use Cases
    services.AddTransient<ISearchTrackUseCase, SearchTrackUseCase>();
    services.AddTransient<ISearchArtistUseCase, SearchArtistUseCase>();
    services.AddTransient<ISearchAlbumUseCase, SearchAlbumUseCase>();
    services.AddTransient<ISearchGenrePlaylistUseCase, SearchGenrePlaylistUseCase>();
    // Registrierte Get Use Cases
    services.AddTransient<IGetTrackUseCase, GetTrackUseCase>();
    services.AddTransient<IGetArtistUseCase, GetArtistUseCase>();
    services.AddTransient<IGetAlbumUseCase, GetAlbumUseCase>();
    services.AddTransient<IGetGenrePlaylistUseCase, GetGenrePlaylistUseCase>();
    services.AddTransient<Controller>();

    // Registrierte Menu Präsentationsschicht
    services.AddTransient<MainMenuPage>();
    // Registrierte Search Präsentationsschichten
    services.AddTransient<SearchTrackScreen>();
    services.AddTransient<SearchArtistScreen>();
    services.AddTransient<SearchAlbumScreen>();
    services.AddTransient<SearchGenrePlaylistScreen>();
    // Registrierte Get Präsentationsschichten
    services.AddTransient<GetTrackScreen>();
    //services.AddTransient<GetArtistScreen>();
    //services.AddTransient<GetAlbumScreen>();
    //services.AddTransient<GetGenrePlaylistScreen>();

    //Delete CMD MEssages
    services.RemoveAll<IHttpMessageHandlerBuilderFilter>();
});

var app = builder.Build();

// Hier beginnt die Ausführung deiner Anwendung
await RunApplicationAsync(app.Services);

async Task RunApplicationAsync(IServiceProvider services)
{
    using var scope = services.CreateScope();
    var serviceProvider = scope.ServiceProvider;

    try
    {
        var mainMenuPage = serviceProvider.GetRequiredService<MainMenuPage>();
        await mainMenuPage.ShowAsync();
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Ein unerwarteter Fehler ist aufgetreten: {ex.Message}");
    }
}

await app.RunAsync();
