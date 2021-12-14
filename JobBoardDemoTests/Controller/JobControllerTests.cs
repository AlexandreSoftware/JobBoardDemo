using FluentAssertions;
using JobBoardDemoApi.Controllers;
using JobBoardServices;
using JobBoardServices.Interface;
using JobBoardServices.View;
using Moq;
using Serilog;
using Xunit;

namespace JobBoardDemoTests.Controller;

public class JobControllerTests
{
    public Mock<IJobService> js;
    public JobController jc;
    public JobControllerTests()
    {
        js = new Mock<IJobService>();
        jc = new JobController(js.Object);
    }
    [Fact]
    public void OnGet_NullValue_ShouldReturnNull()
    {
        //Arrange
        js.Setup(x => x.Get()).Returns((Job[])null);
        //Act   
        var result = jc.Get();
        //Assert
        Assert.Equal(null,result.Value);
    }
    [Fact]
    public void OnGet_emptyArray_ShouldReturnNull()
    {
        //Arrange
        js.Setup(x => x.Get()).Returns(new Job[]{});
        //Act   
        var result = jc.Get();
        //Assert
        Assert.Equal(null,result.Value);
    }
    [Fact]
    public void OnPost_NullValue_ShouldReturnNull()
    {
        //Arrange
        js.Setup(x => x.Post(null)).Returns(false);
        //Act   
        var result = jc.Post(null);
        //Assert
        Assert.False(result.Value);   
    }
    [Fact]
    public void OnPost_InvalidValue_ShouldReturnNull()
    {
        //Arrange
        js.Setup(x => x.Post(null)).Returns(false);
        //Act   
        var result = jc.Post(null);
        //Assert
        Assert.False(result.Value);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(5)]
    [InlineData(3)]
    [InlineData(6)]
    [InlineData(30)]
    [InlineData(12)]
    [InlineData(242)]
    public void OnPost_ValidValue_ShouldReturnTrue(int id)
    {
        //Arrange
        js.Setup(x => x.Post(new Job{id = id})).Returns(true);
        //Act   
        var result = jc.Post(new Job{id = id});
        //Assert
        Assert.True(result.Value);   
    }
    
    
    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(5)]
    [InlineData(3)]
    [InlineData(6)]
    [InlineData(30)]
    [InlineData(12)]
    [InlineData(242)]
    public void OnDelete_ValidValue_ShouldReturnTrue(int id)
    {
        //Arrange
        js.Setup(x => x.Delete(id)).Returns(true);
        //Act   
        var result = jc.Delete(id);
        //Assert
        Assert.True(result.Value);   
    }
    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(5)]
    [InlineData(3)]
    [InlineData(6)]
    [InlineData(30)]
    [InlineData(12)]
    [InlineData(242)]
    public void OnPut_ValidValue_ShouldReturnTrue(int id)
    {
        //Arrange
        js.Setup(x => x.Put(new Job{id = id})).Returns(true);
        //Act   
        var result = jc.Put(new Job{id = id});
        //Assert
        Assert.True(result.Value);   
    }
}