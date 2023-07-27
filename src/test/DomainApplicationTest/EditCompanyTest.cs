using System.ComponentModel;
using Application.Company;
using Application.Company.Interfaces;
using AutoMapper;
using Dto.Company.Request;
using Dto.Company.Response;
using Entities;
using Entities.Intefaces;
using Moq;
using Resolver;

namespace DomainApplicationTest;

public class EditCompanyTest
{
    private readonly Mock<ICompanyRepository> _companyRepository = new Mock<ICompanyRepository>();
    private readonly ICompanyService _companyService;

    public EditCompanyTest()
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
    [Description("Edit Company and return failed Response because not exists Company (400)")]
    public async Task EditCompany_ValidScenario_ShouldReturnNotExistsCompanyResponse()
    {
        // Arrange
        var request = new EditCompanyRequest()
        {
            Id = Guid.NewGuid(),
            FullName = "Company One Limited",
            SocialName = "Company One",
            Document = "123.123.123/000123"
        };

        // Act
        var result = await _companyService.EditCompany(request);

        // Assert
        Assert.False(result.Ok);
        Assert.Equal(400, result.StatusCode);
        Assert.Contains("Not found Company", result.Messages);
    }
    
    [Fact]
    [Description("Edit Company and return Success Response (200)")]
    public async Task EditCompany_ValidScenario_ShouldReturnSuccessResponse()
    {
        // Arrange
        var request = new EditCompanyRequest()
        {
            Id = Guid.NewGuid(),
            FullName = "Company One Limited",
            SocialName = "Company One",
            Document = "123.123.123/000123"
        };
        
        // mock
        _companyRepository.Setup(repo => repo.LoadRecordByIdAsync(request.Id)).ReturnsAsync(new Company()
        {
            Id = Guid.NewGuid().ToString(),
            FullName = "Company One Limited",
            SocialName = "Company One",
            Document = "123.123.123/000123"
        });

        // Act
        var result = await _companyService.EditCompany(request);

        // Assert
        Assert.IsType<CompanyResponse>(result.Data);
        Assert.True(result.Ok);
        Assert.Equal(200, result.StatusCode);
        Assert.Contains("Company updated", result.Messages);
    }
}