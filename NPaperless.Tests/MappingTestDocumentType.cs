using AutoMapper;
using NUnit.Framework;
using NPaperless.Services;
using Entity = NPaperless.DataAccess.Entities;
using Dto = NPaperless.Services.DTOs;

namespace NPaperless.Tests;

[TestFixture]
public class MappingTestDocumentType
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
    public void ShouldMapDocumentTypeToDocumentTypeDTO()
    {
        // Arrange
        var documentType = new Entity.DocumentType
        {
            Id = 1,
            Slug = "example-slug",
            Name = "Example Name",
            Match = "Example Match",
            MatchingAlgorithm = 1,
            IsInsensitive = false,
            DocumentCount = 10
        };

        // Act
        var dto = _mapper.Map<Dto.DocumentTypeDTO>(documentType);

        // Assert
        Assert.AreEqual(documentType.Id, dto.Id);
        Assert.AreEqual(documentType.Slug, dto.Slug);
        Assert.AreEqual(documentType.Name, dto.Name);
        Assert.AreEqual(documentType.Match, dto.Match);
        Assert.AreEqual(documentType.MatchingAlgorithm, dto.MatchingAlgorithm);
        Assert.AreEqual(documentType.IsInsensitive, dto.IsInsensitive);
        Assert.AreEqual(documentType.DocumentCount, dto.DocumentCount);
    }

    // Additional tests for other mappings and reverse mapping if necessary
}