using KataPotter.Domain;
using KataPotter.Infrastructure;

namespace KataPotterTests.Domain;

public class PriceTests
{
    private readonly Price _sut = new(new InMemoryClient());

    [Theory]
    [InlineData(new Book[]{}, 0)]
    public void NoBooks(Book[] books, decimal expected) => 
        RunTest(books, expected);
    
    
    [Theory]
    [InlineData(new[]{Book.First}, 8)]
    [InlineData(new[]{Book.Second}, 8)]
    [InlineData(new[]{Book.Third}, 8)]
    [InlineData(new[]{Book.Fourth}, 8)]
    [InlineData(new[]{Book.Fifth}, 8)]
    public void SingleBook(Book[] books, decimal expected) => 
        RunTest(books, expected);
    
    
    [Theory]
    [InlineData(new[]{Book.First, Book.First}, 16)]
    [InlineData(new[]{Book.Second, Book.Second, Book.Second}, 24)]
    public void DuplicateBooks(Book[] books, decimal expected) => 
        RunTest(books, expected);
    
    
    [Theory]
    [InlineData(new[]{Book.First, Book.Second}, 8 * 2 * 0.95)]
    [InlineData(new[]{Book.First, Book.Third, Book.Fifth}, 8 * 3 * 0.9)]
    [InlineData(new[]{Book.First, Book.Second, Book.Third, Book.Fifth}, 8 * 4 * 0.8)]
    [InlineData(new[]{Book.First, Book.Second, Book.Third, Book.Fourth, Book.Fifth}, 8 * 5 * 0.75)]
    public void TestSimpleDiscounts(Book[] books, decimal expected) => 
        RunTest(books, expected);
    
    
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
    public void TestSeveralDiscounts(Book[] books, decimal expected) => 
        RunTest(books, expected);
    
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
    public void TestEdgeCasesDiscountCombinations(Book[] books, decimal expected) => 
        RunTest(books, expected);
    
    private void RunTest(IEnumerable<Book> books, decimal expected)
    {
        // Act
        var result = _sut.GetTotal(books);
        
        // Assert
        Assert.Equal(expected, result);
    }
}