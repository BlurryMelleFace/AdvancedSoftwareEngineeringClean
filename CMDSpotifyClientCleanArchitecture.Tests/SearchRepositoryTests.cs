using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMDSpotifyClient.Tests
{
    [TestFixture]
    public class SearchRepositoryTests
    {

        private Mock<ISpotifySearchAdapter> _spotifySearchAdapterMock;
        private SearchRepository _searchRepository;

        [SetUp]
        public void SetUp ( )
        {
            _spotifySearchAdapterMock = new Mock<ISpotifySearchAdapter>();
            _searchRepository = new SearchRepository(_spotifySearchAdapterMock.Object);
        }

        [Test]
        public async Task SearchTrackAsync_ReturnsCorrectTracks ( )
        {
            /// <summary>
            /// Tests the SearchTrackAsync method in the SearchRepository class.
            /// Ensures it correctly interacts with the ISpotifySearchAdapter to search for a track by name,
            /// processes the API response, and returns a list of Track objects.
            /// Verifies that the ISpotifySearchAdapter's SearchTrackAsync method was called exactly once.
            /// </summary>

            // Arrange
            var trackName = "Hello";
            var mockApiResponse = @"{
                ""tracks"": {
                    ""items"": [
                        {
                            ""id"": ""62PaSfnXSMyLshYJrlTuL3"",
                            ""name"": ""Hello"",
                            ""artists"": [
                                {
                                    ""id"": ""4dpARuHxo51G3z768sgnrY"",
                                    ""name"": ""Adele""
                                }
                            ]
                        }
                    ]
                }
            }";
            _spotifySearchAdapterMock.Setup(m => m.SearchTrackAsync(It.IsAny<string>())).ReturnsAsync(mockApiResponse);

            // Act
            var result = await _searchRepository.SearchTracksAsync(trackName);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.Not.Empty);
            Assert.That(result.First().Name, Is.EqualTo("Hello"));
            Assert.That(result.First().Id, Is.EqualTo("62PaSfnXSMyLshYJrlTuL3"));
            _spotifySearchAdapterMock.Verify(m => m.SearchTrackAsync(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public async Task SearchArtistAsync_ReturnsCorrectArtists ( )
        {
            /// <summary>
            /// Tests the SearchArtistAsync method in the SearchRepository class.
            /// Ensures it correctly interacts with the ISpotifySearchAdapter to search for an artist by name,
            /// processes the API response, and returns a list of Artist objects.
            /// Verifies that the ISpotifySearchAdapter's SearchArtistAsync method was called exactly once.
            /// </summary>

            // Arrange
            var artistName = "Adele";
            var mockApiResponse = @"{
                ""artists"": {
                    ""items"": [
                        {
                            ""id"": ""4dpARuHxo51G3z768sgnrY"",
                            ""name"": ""Adele"",
                            ""popularity"": 85
                        }
                    ]
                }
            }";
            _spotifySearchAdapterMock.Setup(m => m.SearchArtistAsync(It.IsAny<string>())).ReturnsAsync(mockApiResponse);

            // Act
            var result = await _searchRepository.SearchArtistsAsync(artistName);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.Not.Empty);
            Assert.That(result.First().Name, Is.EqualTo("Adele"));
            Assert.That(result.First().Id, Is.EqualTo("4dpARuHxo51G3z768sgnrY"));
            _spotifySearchAdapterMock.Verify(m => m.SearchArtistAsync(It.IsAny<string>()), Times.Once);
        }
    }
}
