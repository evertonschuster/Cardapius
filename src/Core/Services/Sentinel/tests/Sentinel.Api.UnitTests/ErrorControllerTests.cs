using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sentinel.Api.Controllers;

public class ErrorControllerTests
{
    [Fact]
    public void Error_ReturnsView()
    {
        var controller = new ErrorController();
        var result = controller.Error() as ViewResult;
        result.Should().NotBeNull();
        result!.ViewName.Should().Be("Index");
    }

    [Fact]
    public void Error_WithStatusCode_SetsStatusCode()
    {
        var controller = new ErrorController
        {
            ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            }
        };

        var result = controller.Error(404) as ViewResult;
        result.Should().NotBeNull();
        controller.Response.StatusCode.Should().Be(404);
    }
}