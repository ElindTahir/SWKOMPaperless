using AutoMapper;
using NUnit.Framework;
using NPaperless.Services;
using Entity = NPaperless.DataAccess.Entities;
using Dto = NPaperless.Services.DTOs;

namespace NPaperless.Tests;

[TestFixture]
public class MappingTestDocument
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
    public void ShouldMapDocumentToDocumentDTO()
    {
        // Arrange
        var document = new Entity.Document
        {
            Id = 1,
            Correspondent = 2,
            DocumentType = 3,
            StoragePath = 4,
            Title = "Sample Title",
            Content = "Sample Content",
            Tags = new List<int> { 1, 2, 3 },
            Created = DateTime.UtcNow,
            CreatedDate = DateTime.UtcNow.AddDays(-1),
            Modified = DateTime.UtcNow.AddDays(-2),
            Added = DateTime.UtcNow.AddDays(-3),
            ArchiveSerialNumber = "SN123456",
            OriginalFileName = "original.pdf",
            ArchivedFileName = "archived.pdf"
        };

        // Act
        var dto = _mapper.Map<Dto.DocumentDTO>(document);

        // Assert
        Assert.AreEqual(document.Id, dto.Id);
        Assert.AreEqual(document.Correspondent, dto.Correspondent);
        Assert.AreEqual(document.DocumentType, dto.DocumentType);
        Assert.AreEqual(document.StoragePath, dto.StoragePath);
        Assert.AreEqual(document.Title, dto.Title);
        Assert.AreEqual(document.Content, dto.Content);
        Assert.AreEqual(document.Tags, dto.Tags);
        Assert.AreEqual(document.Created, dto.Created);
        Assert.AreEqual(document.CreatedDate, dto.CreatedDate);
        Assert.AreEqual(document.Modified, dto.Modified);
        Assert.AreEqual(document.Added, dto.Added);
        Assert.AreEqual(document.ArchiveSerialNumber, dto.ArchiveSerialNumber);
        Assert.AreEqual(document.OriginalFileName, dto.OriginalFileName);
        Assert.AreEqual(document.ArchivedFileName, dto.ArchivedFileName);
    }

    // Additional tests for other mappings and reverse mapping if necessary
}