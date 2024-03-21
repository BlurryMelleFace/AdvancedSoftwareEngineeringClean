
namespace CMDSpotifyClient.Tests
{
    [TestFixture]
    public class ControllerTests
    {
        private Mock<ISearchTrackUseCase> _searchTrackUseCaseMock;
        private Mock<ISearchArtistUseCase> _searchArtistUseCaseMock;
        private Mock<ISearchAlbumUseCase> _searchAlbumUseCaseMock;
        private Mock<ISearchGenrePlaylistUseCase> _searchGenrePlaylistUseCaseMock;
        private Mock<IGetTrackUseCase> _getTrackUseCaseMock;
        private Mock<IGetArtistUseCase> _getArtistUseCaseMock;
        private Mock<IGetAlbumUseCase> _getAlbumUseCaseMock;
        private Mock<IGetGenrePlaylistUseCase> _getGenrePlaylistUseCaseMock;
        private ControllerProject _controller;

        [SetUp]
        public void SetUp ( )
        {
            _searchTrackUseCaseMock = new Mock<ISearchTrackUseCase>();
            _searchArtistUseCaseMock = new Mock<ISearchArtistUseCase>();
            _searchAlbumUseCaseMock = new Mock<ISearchAlbumUseCase>();
            _searchGenrePlaylistUseCaseMock = new Mock<ISearchGenrePlaylistUseCase>();
            _getTrackUseCaseMock = new Mock<IGetTrackUseCase>();
            _getArtistUseCaseMock = new Mock<IGetArtistUseCase>();
            _getAlbumUseCaseMock = new Mock<IGetAlbumUseCase>();
            _getGenrePlaylistUseCaseMock = new Mock<IGetGenrePlaylistUseCase>();

            _controller = new ControllerProject(
                _searchTrackUseCaseMock.Object,
                _searchArtistUseCaseMock.Object,
                _searchAlbumUseCaseMock.Object,
                _searchGenrePlaylistUseCaseMock.Object,
                _getTrackUseCaseMock.Object,
                _getArtistUseCaseMock.Object,
                _getAlbumUseCaseMock.Object,
                _getGenrePlaylistUseCaseMock.Object
            );
        }

        [Test]
        public async Task SearchTrack_ReturnsExpectedTracks ( )
        {
            /// <summary>
            /// Tests the SearchTrack method in the Controller class.
            /// Ensures it properly uses the ISearchTrackUseCase to perform a track search with a given query,
            /// and returns the expected track list.
            /// Confirms that the ISearchTrackUseCase's ExecuteAsync method was used correctly to conduct the search.
            /// </summary>

            // Arrange
            var trackName = "Hello";
            var tracks = new List<Track> { new Track { Name = "Hello", Id = "1" } };
            _searchTrackUseCaseMock.Setup(m => m.ExecuteAsync(trackName)).ReturnsAsync(tracks);

            // Act
            var result = await _controller.SearchTrack(trackName);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.EqualTo(tracks));
            _searchTrackUseCaseMock.Verify(m => m.ExecuteAsync(trackName), Times.Once);
        }
    }
}