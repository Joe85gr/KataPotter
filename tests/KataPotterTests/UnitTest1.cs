using KataPotter;

namespace KataPotterTests;

public class UnitTest1
{
    [Fact]
    public void TestBasics()
    {
        Assert.Equal(0, Calculate.Price(new int[] { }));
        
        Assert.Equal(8, Calculate.Price(new [] { 1 }));
        Assert.Equal(8, Calculate.Price(new [] { 2 }));
        Assert.Equal(8, Calculate.Price(new [] { 3 }));
        Assert.Equal(8, Calculate.Price(new [] { 4 }));
        
        Assert.Equal(8 * 3, Calculate.Price(new [] { 1, 1, 1 }));
    }
    
    [Fact]
    public void TestSimpleDiscounts()
    {
        Assert.Equal(15.2, Calculate.Price(new [] { 0, 1 }));
        Assert.Equal(21.6, Calculate.Price(new [] { 0, 2, 4 }));
        Assert.Equal(25.6, Calculate.Price(new [] { 0, 1, 2, 4 }));
        Assert.Equal(30, Calculate.Price(new [] { 0, 1, 2, 3, 4 }));
    }

    [Fact]
    public void TestSeveralDiscounts()
    {
        Assert.Equal(23.2, Calculate.Price(new[] { 0, 0, 1 }));
        Assert.Equal(30.4, Calculate.Price(new[] { 0, 0, 1, 1 }));
        Assert.Equal(40.8, Calculate.Price(new[] { 0, 0, 1, 2, 2, 3 }));
        Assert.Equal(38, Calculate.Price(new[] { 0, 1, 1, 2, 3, 4 }));
    }

    [Fact]
    public void TestEdgeCases()
    {
        Assert.Equal(51.2, Calculate.Price(new[] { 0, 0, 1, 1, 2, 2, 3, 4 }));
        Assert.Equal(141.2,
            Calculate.Price(new[]
            {
                0, 0, 0, 0, 0,
                1, 1, 1, 1, 1,
                2, 2, 2, 2,
                3, 3, 3, 3, 3,
                4, 4, 4, 4
            }));
    }
}