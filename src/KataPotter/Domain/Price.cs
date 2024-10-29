using KataPotter.Infrastructure;

namespace KataPotter.Domain;

public class Price(IClient client) : IPrice
{
    private const decimal BasePrice = 8;
    
    public decimal GetTotal(IEnumerable<Book> books)
    {
        var bookIds = ExtractBookIds(books);
        
        var booksSets = BookOrganiser.GetSets(bookIds);

        var total = GetOptimalPrice(booksSets);
        
        return total;
    }

    private static int[] ExtractBookIds(IEnumerable<Book> books) => 
        books.Select(BookId).ToArray();

    private static int BookId(Book book) => 
        (int)book;
    
    private decimal GetOptimalPrice(IEnumerable<Set> bookSets) => 
        bookSets.Min(GetCurrentSetTotalPrice);
    
    private decimal GetCurrentSetTotalPrice(Set set) => 
        set.Collections.Sum(GetCollectionPrice);

    private decimal GetCollectionPrice(HashSet<int> collection) =>
        collection.Count * BasePrice * (decimal)client.Get(collection.Count);
}
