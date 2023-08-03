using System.ComponentModel;
using Application.Company;
using Application.Company.Interfaces;
using AutoMapper;
using Dto.Company.Request;
using Entities;
using Entities.Intefaces;
using Moq;
using Resolver;

namespace DomainApplicationTest;

public class CreateNewCompanyTest
{
    private readonly Mock<ICompanyRepository> _companyRepository = new Mock<ICompanyRepository>();
    private readonly ICompanyService _companyService;

    public CreateNewCompanyTest()
    {
        //auto mapper configuration
        var mockMapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new MappingProfile());
        });
        var mapper = mockMapper.CreateMapper();
        _companyService = new CompanyService(_companyRepository.Object, mapper);
    }

    [Fact]
    [Description("Create New Company and return Success Response (200)")]
    public async Task CreateNewCompany_ValidScenario_ShouldReturnSuccessResponse()
    {
        // Arrange
        var request = new CreateNewCompanyRequest()
        {
            FullName = "Company One Limited",
            SocialName = "Company One",
            Document = "123.123.123/000123"
        };

        // Mock
        _companyRepository.Setup(repo => repo.InsertRecord(It.IsAny<Company>()));

        // Act
        var result = await _companyService.CreateNewCompany(request);

        // Assert
        Assert.True(result.Ok);
        Assert.Equal(200, result.StatusCode);
        Assert.Contains("New Company created", result.Messages);
    }

    [Fact]
    [Description("Create New Company and return failed Response (400)")]
    public async Task CreateNewCompany_ValidScenario_ShouldReturnFailedResponse()
    {
        // Arrange
        var requestFullName = new CreateNewCompanyRequest()
        {
            FullName = "",
            SocialName = "Company One",
            Document = "123.123.123/000123"
        };
        var requestSocialName = new CreateNewCompanyRequest()
        {
            FullName = "Company One LTDE",
            SocialName = "",
            Document = "123.123.123/000123"
        };
        var requestDocument = new CreateNewCompanyRequest()
        {
            FullName = "Company One LTDE",
            SocialName = "Company One",
            Document = ""
        };


        // Mock
        _companyRepository.Setup(repo => repo.InsertRecord(It.IsAny<Company>()));

        // Act
        var resultFullName = await _companyService.CreateNewCompany(requestFullName);
        var resultSocialName = await _companyService.CreateNewCompany(requestSocialName);
        var resultDocument = await _companyService.CreateNewCompany(requestDocument);

        // Assert
        Assert.True(!resultFullName.Ok);
        Assert.True(!resultSocialName.Ok);
        Assert.True(!resultDocument.Ok);
        Assert.Equal(400, resultFullName.StatusCode);
        Assert.Equal(400, resultSocialName.StatusCode);
        Assert.Equal(400, resultDocument.StatusCode);
        Assert.Contains("Please enter the required fields", resultFullName.Messages);
        Assert.Contains("Please enter the required fields", resultSocialName.Messages);
        Assert.Contains("Please enter the required fields", resultDocument.Messages);
    }

    [Fact]
    [Description("Create New Company and return failed Response because has exists Company (400)")]
    public async Task CreateNewCompany_ValidScenario_ShouldReturnHasExistsCompanyResponse()
    {
        // Arrange
        var request = new CreateNewCompanyRequest()
        {
            FullName = "Company One LTDA",
            SocialName = "Company One",
            Document = "123.123.123/000123"
        };

        // Mock
        _companyRepository.Setup(repo => repo
            .SearchCompany(request.Document, request.SocialName, request.FullName)).ReturnsAsync(new Company(Guid.NewGuid().ToString())
        {
            FullName = "Company One LTDA",
            SocialName = "Company One",
            Document = "123.123.123/000123"
        });
        _companyRepository.Setup(repo => repo.InsertRecord(It.IsAny<Company>()));

        // Act
        var resultFullName = await _companyService.CreateNewCompany(request);

        // Assert
        Assert.True(!resultFullName.Ok);
        Assert.Equal(400, resultFullName.StatusCode);
        Assert.Contains("There is already a company with the same data", resultFullName.Messages);
    }
}