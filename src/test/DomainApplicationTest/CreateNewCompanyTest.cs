using System.ComponentModel;
using Application.Company;
using Application.Company.Interfaces;
using Dto.Company.Request;
using Entities;
using Entities.Intefaces;
using Moq;

namespace DomainApplicationTest;

public class CreateNewCompanyTest
{
    private readonly Mock<ICompanyRepository> _companyRepository = new Mock<ICompanyRepository>();
    private readonly ICompanyService _companyService;

    public CreateNewCompanyTest()
    {
        _companyService = new CompanyService(_companyRepository.Object);
    }

    [Fact]
    [Description ("Create New Companu and return Success Response (200)")]
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
}