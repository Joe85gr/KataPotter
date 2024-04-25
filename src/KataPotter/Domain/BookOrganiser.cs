namespace KataPotter.Domain;

public interface IBookOrganiser
{
    List<Set> GetBooksSets(int[] books);
}

public class BookOrganiser : IBookOrganiser
{
    public List<Set> GetBooksSets(int[] books)
    {
        var bookSets = new List<Set>
        {
            GetBooksSets(books, SetType.Smallest),
            GetBooksSets(books, SetType.Biggest),
        };
        
        return bookSets;
    }
    
    private static Set GetBooksSets(IEnumerable<int> books, SetType setType)
    {
        var bookSets = new Set();
        
        foreach (var book in books)
        {
            if (bookSets.Items.TrueForAll(bookSet => bookSet.Contains(book)))
            {
                bookSets.Items.Add([book]);
            }
            else
            {
                if(setType == SetType.Biggest) bookSets.Items.Sort(CrescentOrder());
                
                bookSets.Items.First(set => !set.Contains(book)).Add(book);
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