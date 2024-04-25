using KataPotter.Domain;
using KataPotter.Infrastructure;

namespace KataPotterTests.Domain;

public class CalculateTests
{
    private readonly Calculate _sut = new(new Discount(), new BookOrganiser());

    [Fact]
    public void NoBooks()
    {
        // Act
        var result = _sut.TotalBooksPrice(Array.Empty<Book>());
        
        // Assert
        Assert.Equal(0, result);
    }
    
    [Theory]
    [InlineData(new[]{Book.First}, 8)]
    [InlineData(new[]{Book.Second}, 8)]
    [InlineData(new[]{Book.Third}, 8)]
    [InlineData(new[]{Book.Fourth}, 8)]
    [InlineData(new[]{Book.Fifth}, 8)]
    public void SingleBook(Book[] books, decimal expected)
    {
        // Act
        var result = _sut.TotalBooksPrice(books);
        
        // Assert
        Assert.Equal(expected, result);
    }
    
    [Theory]
    [InlineData(new[]{Book.First, Book.Second}, 8 * 2 * 0.95)]
    [InlineData(new[]{Book.First, Book.Third, Book.Fifth}, 8 * 3 * 0.9)]
    [InlineData(new[]{Book.First, Book.Second, Book.Third, Book.Fifth}, 8 * 4 * 0.8)]
    [InlineData(new[]{Book.First, Book.Second, Book.Third, Book.Fourth, Book.Fifth}, 8 * 5 * 0.75)]
    public void TestSimpleDiscounts(Book[] books, decimal expected)
    {
        // Act
        var result = _sut.TotalBooksPrice(books);
        
        // Assert
        Assert.Equal(expected, result);
    }
    
    [Theory]
    [InlineData(new[]
    {
        Book.First, Book.First, 
        Book.Second
    }, 8 + (8 * 2 * 0.95))]
    [InlineData(new[]
    {
        Book.First, Book.First, 
        Book.Second, Book.Second
    }, 2 * (8 * 2 * 0.95))]
    
    [InlineData(new[]
    {
        Book.First, Book.First, 
        Book.Second,
        Book.Third, Book.Third, 
        Book.Fourth
    }, (8 * 4 * 0.8) + (8 * 2 * 0.95))]
    
    [InlineData(new[]
    {
        Book.First, 
        Book.Second,  Book.Second, 
        Book.Third, 
        Book.Fourth, 
        Book.Fifth, 
    }, 8 + (8 * 5 * 0.75))]
    public void TestSeveralDiscounts(Book[] books, decimal expected)
    {
        // Act
        var result = _sut.TotalBooksPrice(books);
        
        // Assert
        Assert.Equal(expected, result);
    }
    
    [Theory]
    [InlineData(new[]
    {
        Book.First, Book.First, 
        Book.Second, Book.Second, 
        Book.Third, Book.Third, 
        Book.Fourth, 
        Book.Fifth, 
    }, 2 * (8 * 4 * 0.8))]
    [InlineData(new[]
    {
        Book.First, Book.First, Book.First, Book.First, Book.First,
        Book.Second, Book.Second, Book.Second, Book.Second, Book.Second, 
        Book.Third, Book.Third, Book.Third, Book.Third, 
        Book.Fourth, Book.Fourth, Book.Fourth, Book.Fourth, Book.Fourth, 
        Book.Fifth, Book.Fifth, Book.Fifth, Book.Fifth, 
    }, 3 * (8 * 5 * 0.75) + 2 * (8 * 4 * 0.8))]
    public void TestEdgeCases(Book[] books, decimal expected)
    {
        // Act
        var result = _sut.TotalBooksPrice(books);
        
        // Assert
        Assert.Equal(expected, result);
    }
}