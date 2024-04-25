namespace KataPotter.Domain;

public interface IBookOrganiser
{
    List<List<HashSet<int>>> GetBooksSets(int[] books);
}

public class BookOrganiser : IBookOrganiser
{
    public List<List<HashSet<int>>> GetBooksSets(int[] books)
    {
        var bookSets = new List<List<HashSet<int>>>
        {
            GetBooksSets(books, SetType.Smallest),
            GetBooksSets(books, SetType.Biggest),
        };
        
        return bookSets;
    }
    
    private static List<HashSet<int>> GetBooksSets(IEnumerable<int> books, SetType setType)
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
                if(setType == SetType.Biggest) bookSets.Sort(CrescentOrder());
                
                bookSets.First(set => !set.Contains(book)).Add(book);
            }
        }

        return bookSets;
    } 

    private static Comparison<HashSet<int>> CrescentOrder() => (a,b) => a.Count - b.Count;
}

public enum SetType
{
    Smallest,
    Biggest
}