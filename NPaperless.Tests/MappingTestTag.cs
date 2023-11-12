using AutoMapper;
using NUnit.Framework;
using NPaperless.Services;
using Entity = NPaperless.DataAccess.Entities;
using Dto = NPaperless.Services.DTOs;

namespace NPaperless.Tests;

[TestFixture]
public class MappingTestTag
{
    private IMapper _mapper;

    [SetUp]
    public void SetUp()
    {
        var configuration = new MapperConfiguration(cfg =>
            cfg.AddProfile<MappingProfile>());
        _mapper = configuration.CreateMapper();
    }

    [Test]
    public void ConfigurationIsValid()
    {
        var configuration = new MapperConfiguration(cfg =>
            cfg.AddProfile<MappingProfile>());

        configuration.AssertConfigurationIsValid();
    }

    [Test]
    public void ShouldMapTagToTagDTO()
    {
        // Arrange
        var tag = new Entity.Tag
        {
            Id = 1,
            Slug = "example-tag",
            Name = "Example Tag",
            Color = "#FFFFFF",
            Match = "Example Match",
            MatchingAlgorithm = 1,
            IsInsensitive = false,
            IsInboxTag = true,
            DocumentCount = 100
        };

        // Act
        var dto = _mapper.Map<Dto.TagDTO>(tag);

        // Assert
        Assert.AreEqual(tag.Id, dto.Id);
        Assert.AreEqual(tag.Slug, dto.Slug);
        Assert.AreEqual(tag.Name, dto.Name);
        Assert.AreEqual(tag.Color, dto.Color);
        Assert.AreEqual(tag.Match, dto.Match);
        Assert.AreEqual(tag.MatchingAlgorithm, dto.MatchingAlgorithm);
        Assert.AreEqual(tag.IsInsensitive, dto.IsInsensitive);
        Assert.AreEqual(tag.IsInboxTag, dto.IsInboxTag);
        Assert.AreEqual(tag.DocumentCount, dto.DocumentCount);
    }

    // Additional tests for other mappings and reverse mapping if necessary
}