using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using CMDSpotifyClient.InterfaceAdapters;
using CMDSpotifyClient.InterfaceAdapters.Interfaces;
using CMDSpotifyClient.UseCases;
using CMDSpotifyClient.UseCases.Interfaces;
using CMDSpotifyClient.Presentation;
using CMDSpotifyClient.Infrastructure;

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

    // Registriere Use Cases
    services.AddTransient<ISearchTrackUseCase, SearchTrackUseCase>();
    services.AddTransient<ISearchArtistUseCase, SearchArtistUseCase>();
    // Füge weitere Use Cases hinzu, wie benötigt

    // Registriere Präsentationsschicht
    services.AddTransient<SearchTrackScreen>();
    services.AddTransient<SearchArtistScreen>();
    services.AddTransient<MainMenuPage>();
    // Füge weitere Bildschirme oder Menüs hinzu, wie benötigt

    // Optional: Wenn du eine SpotifyCredentials-Klasse hast, die Zugriffstokens verwaltet,
    // könntest du diese hier auch konfigurieren oder registrieren.
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
