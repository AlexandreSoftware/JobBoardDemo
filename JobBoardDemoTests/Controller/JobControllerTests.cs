using JobBoardDemoApi.Controllers;
using JobBoardDemoApi.Controllers.Interface;
using JobBoardServices.Interface;
using JobBoardServices.View;
using Moq;
using Xunit;

namespace JobBoardDemoTests.Controller;

public class JobControllerTests
{
    public Mock<IJobService> js;
    public IJobController jc;
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
        Assert.Null(result.Value);
    }
    [Fact]
    public void OnGet_emptyArray_ShouldReturnNull()
    {
        //Arrange
        js.Setup(x => x.Get()).Returns(new Job[]{});
        //Act   
        var result = jc.Get();
        //Assert
        Assert.Null(result.Value);
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
        var obj = new Job {id = id};
        //Arrange
        js.Setup(x => x.Post(obj)).Returns(true);
        //Act   
        var result = jc.Post(obj);
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
        var obj = new Job {id = id};
        //Arrange
        js.Setup(x => x.Put(obj)).Returns(true);
        //Act   
        var result = jc.Put(obj);
        //Assert
        Assert.True(result.Value);   
    }
}