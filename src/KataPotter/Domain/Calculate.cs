using KataPotter.Infrastructure;

namespace KataPotter.Domain;

public class Calculate(IDiscount discount, IBookOrganiser bookOrganiser) : ICalculate
{
    private const decimal BasePrice = 8;
    
    public decimal TotalBooksPrice(IEnumerable<Book> books)
    {
        var bookIds = ExtractBookIds(books);
        
        if (bookIds.Length == 0) return 0;
        
        var booksSets = bookOrganiser.GetBooksSets(bookIds);

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
        collection.Count * BasePrice * (decimal)discount.Get(collection.Count);
}
