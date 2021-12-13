using JobBoardDemoApi.Controllers;
using JobBoardServices;
using JobBoardServices.Interface;
using JobBoardServices.View;
using Microsoft.Extensions.Logging;
using Moq;
using Serilog.Core;
using Xunit;

namespace JobBoardDemoTests.Controller;

public class JobControllerTests
{
    public Mock<IJobService> js;
    public Mock<Logger> log;
    public JobController jc;
    public JobControllerTests()
    {
        js = new Mock<IJobService>();
        log = new Mock<Logger>();
        jc = new JobController(js.Object,log.Object);
    }
    [Fact]
    public void OnGet_NullValue_ShouldReturnNull()
    {
        //Arrange
        js.Setup(x => x.Get()).Returns((Job)null);
        //Act   
        var result = jc.Get();
        //Assert
        Assert.Equal(result.Value, null);
    }
    [Fact]
    public void OnGet_InvalidValue_ShouldReturnNull()
    {
        
    }
    [Fact]
    public void OnPost_NullValue_ShouldReturnNull()
    {
        
    }
    [Fact]
    public void OnPost_InvalidValue_ShouldReturnNull()
    {
        
    }
    [Fact]
    public void OnDelete_NullValue_ShouldReturnNull()
    {
        
    }
    [Fact]
    public void OnDelete_InvalidValue_ShouldReturnNull()
    {
        
    }
    [Fact]
    public void OnPut_NullValue_ShouldReturnNull()
    {
        
    }
    [Fact]
    public void OnPut_InvalidValue_ShouldReturnNull()
    {
        
    }
    [Fact]
    public void OnOptions_NullValue_ShouldReturnNull()
    {
        
    }
    [Fact]
    public void OnOptions_InvalidValue_ShouldReturnNull()
    {
        
    }
}