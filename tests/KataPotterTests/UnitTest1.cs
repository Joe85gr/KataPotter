using KataPotter;

namespace KataPotterTests;

public class UnitTest1
{
    [Fact]
    public void TestBasics()
    {
        Assert.Equal(0, Calculate.Price([]));
        
        Assert.Equal(8, Calculate.Price([1]));
        Assert.Equal(8, Calculate.Price([2]));
        Assert.Equal(8, Calculate.Price([3]));
        Assert.Equal(8, Calculate.Price([4]));
        
        Assert.Equal(8 * 3, Calculate.Price([1, 1, 1]));
    }
    
    [Fact]
    public void TestSimpleDiscounts()
    {
        Assert.Equal(8 * 2 * 0.95, Calculate.Price([0, 1]));
        Assert.Equal(8 * 3 * 0.9, Calculate.Price([0, 2, 4]));
        Assert.Equal(8 * 4 * 0.8, Calculate.Price([0, 1, 2, 4]));
        Assert.Equal(8 * 5 * 0.75, Calculate.Price([0, 1, 2, 3, 4]));
    }

    [Fact]
    public void TestSeveralDiscounts()
    {
        Assert.Equal(8 + (8 * 2 * 0.95), Calculate.Price([0, 0, 1]));
        Assert.Equal(2 * (8 * 2 * 0.95), Calculate.Price([0, 0, 1, 1]));
        Assert.Equal((8 * 4 * 0.8) + (8 * 2 * 0.95), Calculate.Price([0, 0, 1, 2, 2, 3]));
        Assert.Equal(8 + (8 * 5 * 0.75), Calculate.Price([0, 1, 1, 2, 3, 4]));
    }

    [Fact]
    public void TestEdgeCases()
    {
        Assert.Equal(2 * (8 * 4 * 0.8), Calculate.Price([0, 0, 1, 1, 2, 2, 3, 4]));
        Assert.Equal(3 * (8 * 5 * 0.75) + 2 * (8 * 4 * 0.8),
            Calculate.Price([
                0, 0, 0, 0, 0,
                1, 1, 1, 1, 1,
                2, 2, 2, 2,
                3, 3, 3, 3, 3,
                4, 4, 4, 4
            ]));
    }
}