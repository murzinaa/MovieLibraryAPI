using System.Threading.Tasks;
using FilmsLibrary.Application;
using FilmsLibrary.Infrastructure;
using FilmsLibrary.Models.Domain;
using Moq;
using NUnit.Framework;

namespace FilmsLibrary.Tests;

public class Tests
{
    private Mock<IMovieRepository> _repositoryMock;
    private ActorService _actorService;

    [SetUp]
    public void Setup()
    {
        _repositoryMock = new Mock<IMovieRepository>();
        _actorService = new ActorService(_repositoryMock.Object);
    }

    [Test]
    public async Task AddActor_WhenActorDoesNotExist_ShouldReturnAddedActorId()
    {
        // Arrange
        var actor = new Actor { Name = "John", Surname = "Doe" };
        _repositoryMock.Setup(repo => repo.GetActorByName(actor.Name, actor.Surname))
            .ReturnsAsync((Actor)null);
        _repositoryMock.Setup(repo => repo.AddActor(actor))
            .ReturnsAsync(1); // Assuming the added actor has Id = 1

        // Act
        var result = await _actorService.AddActor(actor);

        // Assert
        Assert.That(result, Is.EqualTo(1));
        _repositoryMock.Verify(repo => repo.GetActorByName(actor.Name, actor.Surname), Times.Once);
        _repositoryMock.Verify(repo => repo.AddActor(actor), Times.Once);
    }

    [Test]
    public async Task AddActor_WhenActorExists_ShouldReturnExistingActorId()
    {
        // Arrange
        var actor = new Actor { Name = "John", Surname = "Doe" };
        var existingActor = new Actor { Id = 2 };
        _repositoryMock.Setup(repo => repo.GetActorByName(actor.Name, actor.Surname))
            .ReturnsAsync(existingActor);

        // Act
        var result = await _actorService.AddActor(actor);

        // Assert
        Assert.That(result, Is.EqualTo(existingActor.Id));
        _repositoryMock.Verify(repo => repo.GetActorByName(actor.Name, actor.Surname), Times.Once);
        _repositoryMock.Verify(repo => repo.AddActor(actor), Times.Never);
    }
}