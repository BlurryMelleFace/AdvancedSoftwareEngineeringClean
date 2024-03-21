
namespace CMDSpotifyClient.Tests
{
    [TestFixture]
    public class SearchTrackUseCaseTests
    {

        private Mock<ISearchRepository> _searchRepositoryMock;
        private SearchTrackUseCase _searchTrackUseCase;

        [SetUp]
        public void SetUp ( )
        {
            _searchRepositoryMock = new Mock<ISearchRepository>();
            _searchTrackUseCase = new SearchTrackUseCase(_searchRepositoryMock.Object);
        }

        [Test]
        public async Task ExecuteAsync_CallsSearchRepository_WithCorrectParameters ( )
        {
            /// <summary>
            /// Tests the ExecuteAsync method in the SearchTrackUseCase class.
            /// Asserts that it calls the ISearchRepository's SearchTracksAsync method with the correct parameters
            /// and returns the search results accurately.
            /// Verifies the delegation of search operation to the ISearchRepository is performed correctly.
            /// </summary>

            // Arrange
            var trackName = "Hello";
            var tracks = new List<Track> { new Track { Name = "Hello", Id = "1" } };
            _searchRepositoryMock.Setup(m => m.SearchTracksAsync(trackName)).ReturnsAsync(tracks);

            // Act
            var result = await _searchTrackUseCase.ExecuteAsync(trackName);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.EqualTo(tracks));
            _searchRepositoryMock.Verify(m => m.SearchTracksAsync(trackName), Times.Once);
        }
    }
}
