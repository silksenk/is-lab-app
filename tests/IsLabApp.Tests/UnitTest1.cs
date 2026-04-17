using Xunit;

namespace IsLabApp.Tests;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        Assert.True(true);
    }
    
    [Fact]
    public void Test2()
    {
        int result = 2 + 2;
        Assert.Equal(4, result);
    }
}
