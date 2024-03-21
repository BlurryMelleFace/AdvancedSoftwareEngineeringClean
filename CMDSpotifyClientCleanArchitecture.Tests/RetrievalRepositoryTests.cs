namespace CMDSpotifyClient.Tests
{

    [TestFixture]
    public class RetrievalRepositoryTests
    {
        private Mock<ISpotifyDataRetrievalAdapter> _spotifyDataRetrievalAdapterMock;
        private RetrievalRepository _retrievalRepository;

        [SetUp]
        public void SetUp ( )
        {
            _spotifyDataRetrievalAdapterMock = new Mock<ISpotifyDataRetrievalAdapter>();
            _retrievalRepository = new RetrievalRepository(_spotifyDataRetrievalAdapterMock.Object);
        }

        [Test]
        public async Task GetTrackAsync_ReturnsDetailedTrackInformation ( )
        {
            /// <summary>
            /// Tests the GetTrackAsync method in the RetrievalRepository class.
            /// Verifies that it correctly requests track details from the ISpotifyDataRetrievalAdapter
            /// for a given track ID, processes the API response, and returns a Track object with the expected properties.
            /// Also checks that the ISpotifyDataRetrievalAdapter's GetTrackAsync method was called exactly once.
            /// </summary>

            // Arrange
            var trackId = "62PaSfnXSMyLshYJrlTuL3";
            var mockApiResponse = @"{
                ""id"": ""62PaSfnXSMyLshYJrlTuL3"",
                ""name"": ""Hello"",
                ""duration_ms"": 215280,
                ""explicit"": false,
                ""artists"": [
                    {
                        ""id"": ""4dpARuHxo51G3z768sgnrY"",
                        ""name"": ""Adele""
                    }
                ],
                ""popularity"": 85,
                ""album"": {
                    ""id"": ""someAlbumId"",
                    ""name"": ""Some Album Name"",
                    ""total_tracks"": 10,
                    ""release_date"": ""2024-03-21""
                },
                ""track_number"": 5,
                ""preview_url"": ""https://example.com/preview"",
                ""type"": ""track""
            }";
            _spotifyDataRetrievalAdapterMock.Setup(m => m.GetTrackAsync(It.IsAny<string>())).ReturnsAsync(mockApiResponse);

            // Act
            var result = await _retrievalRepository.GetTrackAsync(trackId);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.First().Id, Is.EqualTo(trackId));
            Assert.That(result.First().Name, Is.EqualTo("Hello"));
            Assert.That(result.First().Artists.First().Name, Is.EqualTo("Adele"));
            Assert.That(result.First().Popularity, Is.EqualTo(85));
            _spotifyDataRetrievalAdapterMock.Verify(m => m.GetTrackAsync(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public async Task GetArtistAsync_ReturnsDetailedArtistInformation ( )
        {
            /// <summary>
            /// Tests the GetArtistAsync method in the RetrievalRepository class.
            /// Verifies that it correctly requests artist details from the ISpotifyDataRetrievalAdapter
            /// for a given artist ID, processes the API response, and returns an Artist object with the expected properties.
            /// Also checks that the ISpotifyDataRetrievalAdapter's GetArtistAsync method was called exactly once.
            /// </summary>

            // Arrange
            var artistId = "4dpARuHxo51G3z768sgnrY";
            var mockApiResponse = @"{
                ""artists"": [
                    {
                        ""id"": ""4dpARuHxo51G3z768sgnrY"",
                        ""name"": ""Adele"",
                        ""followers"": { ""total"": 54212927 },
                        ""popularity"": 85,
                        ""genres"": [""pop"", ""soul""],
                    }
                ]
            }";
            _spotifyDataRetrievalAdapterMock.Setup(m => m.GetArtistAsync(It.IsAny<string>())).ReturnsAsync(mockApiResponse);

            // Act
            var result = await _retrievalRepository.GetArtistAsync(artistId);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.First().Id, Is.EqualTo(artistId));
            Assert.That(result.First().Name, Is.EqualTo("Adele"));
            Assert.That(result.First().Followers, Is.EqualTo(54212927));
            Assert.That(result.First().Popularity, Is.EqualTo(85));

            _spotifyDataRetrievalAdapterMock.Verify(m => m.GetArtistAsync(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public async Task GetAlbumAsync_ReturnsDetailedAlbumInformation ( )
        {
            /// <summary>
            /// Tests the GetAlbumAsync method in the RetrievalRepository class.
            /// Verifies that it correctly requests album details from the ISpotifyDataRetrievalAdapter
            /// for a given album ID, processes the API response, and returns an Album object with the expected properties.
            /// Also checks that the ISpotifyDataRetrievalAdapter's GetAlbumAsync method was called exactly once.
            /// </summary>

            // Arrange
            var albumId = "3AvPX1B1HiFROvYjLb5Qwi";
            var mockApiResponse = @"{
                ""id"": ""3AvPX1B1HiFROvYjLb5Qwi"",
                ""name"": ""25"",
                ""total_tracks"": 11,
                ""release_date"": ""2015-11-20"",
                ""label"": ""XL Recordings"",
                ""popularity"": 81,
                ""artists"": [
                    {
                        ""id"": ""4dpARuHxo51G3z768sgnrY"",
                        ""name"": ""Adele""
                    }
                ],
                ""tracks"": {
                    ""items"": [
                        {
                            ""name"": ""Hello"",
                        },
                    ]
                }
            }";
            _spotifyDataRetrievalAdapterMock.Setup(m => m.GetAlbumAsync(It.IsAny<string>())).ReturnsAsync(mockApiResponse);

            // Act
            var result = await _retrievalRepository.GetAlbumAsync(albumId);

            // Assert

            Assert.That(result, Is.Not.Null);
            Assert.That(result.First().Id, Is.EqualTo(albumId));
            Assert.That(result.First().Name, Is.EqualTo("25"));
            Assert.That(result.First().Artists.First().Id, Is.EqualTo("4dpARuHxo51G3z768sgnrY"));
            Assert.That(result.First().Popularity, Is.EqualTo(81));
            _spotifyDataRetrievalAdapterMock.Verify(m => m.GetAlbumAsync(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public async Task GetGenrePlaylistAsync_ReturnsDetailedPlaylistInformation ( )
        {
            /// <summary>
            /// Tests the GetGenrePlaylistAsync method in the RetrievalRepository class.
            /// Verifies that it correctly requests playlist details from the ISpotifyDataRetrievalAdapter
            /// for a given playlist ID, processes the API response, and returns a Playlist object with the expected properties.
            /// Also checks that the ISpotifyDataRetrievalAdapter's GetGenrePlaylistAsync method was called exactly once.
            /// </summary>

            // Arrange
            var playlistId = "37i9dQZF1EIhshGKK0SEkb";
            var mockApiResponse = @"{
                ""id"": ""37i9dQZF1EIhshGKK0SEkb"",
                ""name"": ""Trap Mix"",
                ""description"": "".."",
                ""followers"": { ""total"": 0 },
                ""tracks"": {
                    ""items"": [
                        {
                            ""track"": {
                                ""id"": ""3TXy6nchgKeYlVwOKNk9lE"",
                                ""name"": ""100 Shots"",
                            }
                        }
                    ]
                }
            }";
            _spotifyDataRetrievalAdapterMock.Setup(m => m.GetGenrePlaylistAsync(It.IsAny<string>())).ReturnsAsync(mockApiResponse);

            // Act
            var result = await _retrievalRepository.GetGenrePlaylistAsync(playlistId);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.First().Id, Is.EqualTo(playlistId));
            Assert.That(result.First().Name, Is.EqualTo("Trap Mix"));
            Assert.That(result.First().Tracks.First().Id.Equals("3TXy6nchgKeYlVwOKNk9lE"));
            Assert.That(result.First().Tracks.First().Name.Equals("100 Shots"));
            _spotifyDataRetrievalAdapterMock.Verify(m => m.GetGenrePlaylistAsync(It.IsAny<string>()), Times.Once);
        }

    }
}
