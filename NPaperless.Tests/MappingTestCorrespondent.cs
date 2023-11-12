using AutoMapper;
using NPaperless.Services;
using NUnit.Framework;
using Entity = NPaperless.DataAccess.Entities;
using Dto = NPaperless.Services.DTOs;

namespace NPaperless.Tests;

[TestFixture]
public class MappingTestCorrespondent
{
    private readonly IMapper _mapper;

    public MappingTestCorrespondent()
    {
        var configuration = new MapperConfiguration(cfg =>
            cfg.AddProfile<MappingProfile>());
        _mapper = configuration.CreateMapper();
    }

    [SetUp]
    public void Setup()
    {
        // Any setup before each test can go here
    }

    [Test]
    public void ConfigurationIsValid()
    {
        var configuration = new MapperConfiguration(cfg =>
            cfg.AddProfile<MappingProfile>());

        configuration.AssertConfigurationIsValid();
    }

    [Test]
    public void ShouldMapCorrespondentToCorrespondentDTO()
    {
        // Arrange
        var correspondent = new Entity.Correspondent
        {
            Id = 1,
            Name = "Test Name",
            Match = "Test Match",
            MatchingAlgorithm = 2,
            IsInsensitive = true,
            DocumentCount = 10,
            LastCorrespondence = DateTime.UtcNow
        };

        // Act
        var dto = _mapper.Map<Dto.CorrespondentDTO>(correspondent);

        // Assert
        Assert.AreEqual(correspondent.Id, dto.Id);
        Assert.AreEqual(correspondent.Name, dto.Name);
        Assert.AreEqual(correspondent.Match, dto.Match);
        Assert.AreEqual(correspondent.MatchingAlgorithm, dto.MatchingAlgorithm);
        Assert.AreEqual(correspondent.IsInsensitive, dto.IsInsensitive);
        Assert.AreEqual(correspondent.DocumentCount, dto.DocumentCount);
        Assert.AreEqual(correspondent.LastCorrespondence, dto.LastCorrespondence);
    }

    // Additional tests for other mappings and reverse mapping if necessary
}