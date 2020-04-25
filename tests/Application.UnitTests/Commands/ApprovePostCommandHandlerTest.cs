using Application.Common.Exceptions;
using Application.Posts.Commands.UpdatePost;
using Application.Repositories;
using Domain;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Application.UnitTests.Commands
{
    public class ApprovePostCommandHandlerTest
    {
        private readonly ApprovePostCommandHandler _sut;
        private readonly Mock<IPostsRepository> _postsRepositoryMock = new Mock<IPostsRepository>();

        public ApprovePostCommandHandlerTest()
        {
            _sut = new ApprovePostCommandHandler(_postsRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldUpdateThePostStatusAsApproved_WhenPostExists()
        {
            // Arrange
            var command = new ApprovePostCommand { PostId = 1 };
            Post post = new Post
            {
                Id = command.PostId
            };
            _postsRepositoryMock.Setup(mock => mock.GetAsync(command.PostId)).ReturnsAsync(post);
            _postsRepositoryMock.Setup(mock => mock.UpdateAsync(command.PostId, It.IsAny<Post>()));

            // Act

            await _sut.Handle(command, new CancellationToken(false));

            // Assert
            _postsRepositoryMock.Verify(mock => mock.UpdateAsync(command.PostId, It.IsAny<Post>()), Times.Once());
        }

        [Fact]
        public async Task Handle_ShouldThrowNotFoundException_WhenPostDoesNotExists()
        {
            // Arrange
            var command = new ApprovePostCommand { PostId = 1 };
            _postsRepositoryMock.Setup(mock => mock.GetAsync(command.PostId)).ReturnsAsync(() => null);
            _postsRepositoryMock.Setup(mock => mock.UpdateAsync(command.PostId, It.IsAny<Post>()));

            // Act
            await Assert.ThrowsAsync<NotFoundException>(() => _sut.Handle(command, new CancellationToken(false)));

            // Assert
            _postsRepositoryMock.Verify(mock => mock.UpdateAsync(command.PostId, It.IsAny<Post>()), Times.Never());
        }
    }
}
