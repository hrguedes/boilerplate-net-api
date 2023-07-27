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

public class RemoveCompanyTest
{
   private readonly Mock<ICompanyRepository> _companyRepository = new Mock<ICompanyRepository>();
    private readonly ICompanyService _companyService;

    public RemoveCompanyTest()
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
    [Description("Remove Company and return failed Response because not exists Company (400)")]
    public async Task RemoveCompany_ValidScenario_ShouldReturnNotExistsCompanyResponse()
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
        var result = await _companyService.RemoveCompany(request.Id);

        // Assert
        Assert.False(result.Ok);
        Assert.Equal(400, result.StatusCode);
        Assert.Contains("Not found Company", result.Messages);
    }
    
    [Fact]
    [Description("Remove Company and return Success Response (200)")]
    public async Task RemoveCompany_ValidScenario_ShouldReturnSuccessResponse()
    {
        // Arrange
        var id = Guid.NewGuid();
        var request = new EditCompanyRequest()
        {
            Id = id,
            FullName = "Company One Limited",
            SocialName = "Company One",
            Document = "123.123.123/000123"
        };
        
        // mock
        _companyRepository.Setup(repo => repo.LoadRecordByIdAsync(request.Id)).ReturnsAsync(new Company()
        {
            Id = id.ToString(),
            FullName = "Company One Limited",
            SocialName = "Company One",
            Document = "123.123.123/000123"
        });

        // Act
        var result = await _companyService.RemoveCompany(id);

        // Assert
        Assert.IsType<CompanyResponse>(result.Data);
        Assert.True(result.Ok);
        Assert.Equal(200, result.StatusCode);
        Assert.Contains("Company removed", result.Messages);
    }
}