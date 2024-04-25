namespace KataPotter;

public static class Calculate
{
    private const int BasePrice = 8;
    
    public static double Price(int[] books)
    {
        if (books.Length == 0) return 0;
        
        var smallestSetsPrice = SmallestSetsPrice(books);
        var biggestSetsPrice = BiggestSetsPrice(books);
        
        var total = Math.Min(smallestSetsPrice, biggestSetsPrice);
        
        return Math.Round(total, 2);
    }
    
    private static double SmallestSetsPrice(IEnumerable<int> books)
    {
        var bookGroups = BooksSets(books);
        var total = 0f;

        while (ProcessingBooks(bookGroups))
        {
            var differentBooks = 0;

            foreach (var (book, count) in bookGroups)
            {
                if (count == 0) continue;
                
                bookGroups[book] -= 1;
                differentBooks++;
            }
            
            total += 8 * differentBooks * Discount[differentBooks];
        }
        
        return total;
    }

    private static Dictionary<int, int> BooksSets(IEnumerable<int> books) =>
        books.GroupBy(x => x).ToDictionary(t => t.Key, t => t.Count());
    
    private static bool ProcessingBooks(Dictionary<int, int> bookGroups) =>
        bookGroups.Any(x => x.Value > 0);
    private static float BiggestSetsPrice(IEnumerable<int> books)
    {
        var bookSets = new List<HashSet<int>>{ new() };
        
        foreach (var book in books)
        {
            if (bookSets.TrueForAll(bookSet => bookSet.Contains(book)))
            {
                bookSets.Add([book]);
            }
            else
            {
                bookSets.Sort(CrescentOrder());
                bookSets.First(set => !set.Contains(book)).Add(book);
            }
        }

        var total = bookSets.Sum(set => set.Count * BasePrice * Discount[set.Count]);

        return total;
    }
    
    private static Comparison<HashSet<int>> CrescentOrder() => (a,b) => a.Count - b.Count;
    
    private static readonly Dictionary<int, float> Discount = new()
    {
        { 1, 1f },
        { 2, 0.95f },
        { 3, 0.9f },
        { 4, 0.8f },
        { 5, 0.75f }
    };
}
