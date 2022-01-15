using System;
using System.Collections;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Bogus;
using FluentAssertions;
using JobBoardDemoApi.Controllers;
using JobBoardDemoApi.Controllers.Interface;
using JobBoardRepository;
using JobBoardRepository.Domain;
using JobBoardRepository.Interface;
using JobBoardServices.Interface;
using JobBoardServices.Profile;
using JobBoardServices.View;
using Moq;
using Xunit;

namespace JobBoardDemoTests.Repository;

public class JobRepositoryTests
{
    public Mock<IDapperWrapper> _dw;
    public Mock<ISqlReader> _sqlreader;
    public JobRepository _jr;
    public Faker<JobDTO> faker;
    
    public JobRepositoryTests()
    {
        _dw = new Mock<IDapperWrapper>();
        _sqlreader = new Mock<ISqlReader>();
        _jr = new JobRepository(_dw.Object,_sqlreader.Object);
        faker = new Faker<JobDTO>()
            .RuleFor(property: x => x.ProductId, c => c.IndexFaker)
            .RuleFor(property: x => x.Title, setter: c => c.Company + " " + c.Commerce)
            .RuleFor(property: x => x.Description, setter: c => c.Lorem.Word())
            .RuleFor(property: x => x.SubTitle, setter: c => c.Lorem.Word())
            .RuleFor(property: x => x.MaxPay, setter: c => c.Finance.Random.Int())
            .RuleFor(property: x => x.MinPay, setter: (c, usr) =>
            {
                int val;
                do
                {
                    val = c.Finance.Random.Int();
                } while (val > usr.MaxPay);

                return val;
            });    }
    [Fact]
    public async void OnGet_ValidValue_ShouldReturnObject()
    {
        //Arrange
        var data = faker.Generate(20).ToArray();
        _dw.Setup(x => x.QueryAsync<JobDTO>(It.IsAny<string>()))
            .ReturnsAsync(data);;
        _sqlreader.Setup(x => x.ReadFile(It.IsAny<string>()))
            .Returns<string>(async x => "abc");
        //Act   
        var result = await _jr.Get();
        //Assert
        result.Should().BeEquivalentTo(data);
    }
    [Fact]
    public async void OnGet_EmptyValue_ShouldReturnEmptyArray()
    {
        //Arrange
        _dw.Setup(x => x.QueryAsync<JobDTO>(It.IsAny<string>()))
            .ReturnsAsync(Array.Empty<JobDTO>());
        _sqlreader.Setup(x => x.ReadFile(It.IsAny<string>()))
            .ReturnsAsync("abc");
        //Act   
        var result = await _jr.Get();
        //Assert
        result.Should().BeEmpty();
    }
    [Fact]
    public async void OnPost_ValidValue_ShouldReturnTrue()
    {
        //Arrange
        var data = faker.Generate(1).First();
        _dw.Setup(x => x.ExecuteParamsAsync(It.IsAny<string>(), It.IsAny<object>()));
        _sqlreader.Setup(x => x.ReadFile(It.IsAny<string>()))
            .ReturnsAsync("abc");
        //Act   
        var result = await _jr.Post(data);
        //Assert
        result.Should().BeTrue();
        
    }
    [Fact]
    public async void OnPost_InvalidValue_ShouldReturnFalse()
    {
        //Arrange
        _dw.Setup(x => x.ExecuteParamsAsync<JobDTO>(It.IsAny<string>(),It.IsAny<JobDTO>()))
            .Throws<Exception>();
        _sqlreader.Setup(x => x.ReadFile(It.IsAny<string>()))
            .ReturnsAsync("abc");
        //Act   
        bool result = await _jr.Post(null);
        //Assert
        Assert.False(result);
    }

    [Fact]
    public async void OnPost_ValidValue_ShouldReturnMappedObject()
    {
        //Arrange
        var data = faker.Generate(1)[0];
        _dw.Setup(x => x.ExecuteParamsAsync<JobDTO>(It.IsAny<string>(), It.IsAny<JobDTO>()));
        var result = await _jr.Post(data);
        //Assert
        Assert.True(result);
    }

    [Fact]
    public async void OnGetId_ValidId_ShouldReturnMappedObject()
    {
        //Arrange
        var data = faker.Generate(1);
        _dw.Setup(x => x.QueryAsyncParams<JobDTO>(It.IsAny<string>(),It.IsAny<object>()))
            .ReturnsAsync(data);
        _sqlreader.Setup(x => x.ReadFile(It.IsAny<string>()))
            .Returns<string>(async x => "abc");
        //Act   
        JobDTO result = await _jr.GetId(1);
        //Assert
        result.Should().BeEquivalentTo(data.ToList()[0]);
    }

    [Fact]
    public async void OnGetId_InvalidId_ShouldThrowError()
    {
        //Arrange
        _dw.Setup(x => x.QueryAsyncParams<JobDTO>(It.IsAny<string>(), It.IsAny<object>()))
            .ReturnsAsync(Array.Empty<JobDTO>());
        _sqlreader.Setup(x => x.ReadFile(It.IsAny<string>()));
        //Act   
        //Assert
        await Assert.ThrowsAsync<Exception>(() => _jr.GetId(1));
    }
    [Fact]
    public async void OnPut_ValidValue_ShouldReturnTrue()
    {
        //Arrange
        var data = faker.Generate(1).First();
        _dw.Setup(x => x.ExecuteParamsAsync(It.IsAny<string>(), It.IsAny<object>()));
        _sqlreader.Setup(x => x.ReadFile(It.IsAny<string>()))
            .ReturnsAsync("abc");
        //Act   
        var result = await _jr.Put(data);
        //Assert
        result.Should().BeTrue();
    }
    [Fact]
    public async void OnPut_InvalidValue_ShouldReturnFalse()
    {
        //Arrange
        _dw.Setup(x => x.ExecuteParamsAsync<JobDTO>(It.IsAny<string>(), It.IsAny<JobDTO>()))
            .Throws<Exception>();
        _sqlreader.Setup(x => x.ReadFile(It.IsAny<string>()))
            .Returns<string>(async x => "abc");
        //Act   
        bool result = await _jr.Put(null);
        //Assert
        Assert.False(result);
    }
    [Fact]
    public async void OnDelete_ValidValue_ShouldReturnTrue()
    {
        //Arrange
        _dw.Setup(x => x.ExecuteParamsAsync(It.IsAny<string>(), It.IsAny<object>()));
        _sqlreader.Setup(x => x.ReadFile(It.IsAny<string>()))
            .ReturnsAsync("abc");
        //Act   
        var result = await _jr.Delete(1);
        //Assert
        result.Should().BeTrue();
    }
    [Fact]
    public async void OnDelete_InvalidValue_ShouldReturnFalse()
    {
        //Arrange
        _dw.Setup(x => x.ExecuteParamsAsync(It.IsAny<string>(), It.IsAny<object>()))
            .Throws<Exception>();
        _sqlreader.Setup(x => x.ReadFile(It.IsAny<string>()))
            .ReturnsAsync("abc");
        //Act   
        bool result = await _jr.Delete(1);
        //Assert
        Assert.False(result);
    }
   


}