namespace KataPotter;

public static class Calculate
{
    public static double Price(IEnumerable<int> books)
    {
        var bookGroups = GroupDifferentBooks(books);
        var total = 0f;

        while (ProcessingBooks(bookGroups))
        {
            var differentBooks = 0;

            foreach (var (book, count) in bookGroups)
            {
                if (count == 0) continue;
                
                if(bookGroups.Count(x => x.Value > 0) >= 4 && differentBooks == 4)
                {
                    break;
                }
                
                bookGroups[book] -= 1;
                differentBooks++;
            }
            
            total += 8 * differentBooks * Discount[differentBooks];
        }
        
        return Math.Round(total, 2);
    }

    private static Dictionary<int, int> GroupDifferentBooks(IEnumerable<int> books) =>
        books.GroupBy(x => x).ToDictionary(t => t.Key, t => t.Count());
    
    private static bool ProcessingBooks(Dictionary<int, int> bookGroups) =>
        bookGroups.Any(x => x.Value > 0);
    
    private static readonly Dictionary<int, float> Discount = new()
    {
        { 1, 1f },
        { 2, 0.95f },
        { 3, 0.9f },
        { 4, 0.8f },
        { 5, 0.75f }
    };
}
