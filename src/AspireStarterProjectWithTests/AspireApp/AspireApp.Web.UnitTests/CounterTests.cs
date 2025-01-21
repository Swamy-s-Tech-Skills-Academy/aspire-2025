using AspireApp.Web.Components.Pages;
using Bunit;

namespace AspireApp.Web.UnitTests;

public class CounterTests : TestContext
{
    [Fact]
    public void CounterStartsAtZero()
    {
        // Arrange
        var cut = RenderComponent<Counter>();

        // Assert
        cut.Find("p").MarkupMatches("<p role=\"status\">Current count: 0</p>");
    }

    [Fact]
    public void ClickingButtonIncrementsCounter()
    {
        // Arrange
        var cut = RenderComponent<Counter>();

        // Act
        cut.Find("button").Click();

        // Assert
        cut.Find("p").MarkupMatches("<p role=\"status\">Current count: 1</p>");
    }
}
