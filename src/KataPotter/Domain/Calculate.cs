using KataPotter.Infrastructure;

namespace KataPotter.Domain;

public class Calculate(IDiscount discount, IBookOrganiser bookOrganiser) : ICalculate
{
    private const decimal BasePrice = 8;
    
    public decimal TotalBooksPrice(IEnumerable<Book> books)
    {
        var bookIds = books.Select(BookId).ToArray();
        
        if (bookIds.Length == 0) return 0;
        
        var allBookSets = bookOrganiser.GetBooksSets(bookIds);

        var total = GetOptimalPrice(allBookSets);
        
        return total;
    }

    private static int BookId(Book book) => (int)book;
    
    private decimal GetOptimalPrice(IEnumerable<List<HashSet<int>>> allBookSets) => allBookSets.Min(GetSetTotalPrice);
    
    private decimal GetSetTotalPrice(List<HashSet<int>> sets) => 
        sets.Sum(set => set.Count * BasePrice * (decimal)discount.Get(set.Count));
}
