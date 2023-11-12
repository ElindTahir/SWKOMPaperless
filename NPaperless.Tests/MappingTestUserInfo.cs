using AutoMapper;
using NUnit.Framework;
using NPaperless.Services;
using Entity = NPaperless.DataAccess.Entities;
using Dto = NPaperless.Services.DTOs;

namespace NPaperless.Tests;

[TestFixture]
public class MappingTestUserInfo
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
    public void ShouldMapUserInfoToUserInfoDTO()
    {
        // Arrange
        var userInfo = new Entity.UserInfo
        {
            Id = 1,
            Username = "testuser",
            Password = "password"
        };

        // Act
        var dto = _mapper.Map<Dto.UserInfoDTO>(userInfo);

        // Assert
        Assert.AreEqual(userInfo.Username, dto.Username);
        Assert.AreEqual(userInfo.Password, dto.Password);
    }

    // Additional tests for other mappings and reverse mapping if necessary
}