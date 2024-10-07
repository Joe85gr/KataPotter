namespace KataPotter.Domain;

public class BookOrganiser : IBookOrganiser
{
    public IEnumerable<Set> GetSets(int[] books)
    {
        var setTypes = GetSetTypes();
        
        var bookSets = setTypes.Select(setType => GetSets(books, setType));
        
        return bookSets;
    }

    private static IEnumerable<SetType> GetSetTypes() => Enum.GetValues(typeof(SetType)).Cast<SetType>();

    private static Set GetSets(IEnumerable<int> books, SetType setType)
    {
        var bookSets = new Set();

        foreach (var book in books) AddToSet(setType, bookSets, book);

        return bookSets;
    }

    private static void AddToSet(SetType setType, Set bookSets, int book)
    {
        if (IsInAllCollections(bookSets.Collections, book))
        {
            bookSets.Collections.Add([]);
        }
        
        SortSets(setType, bookSets);

        bookSets.Collections
            .First(collection => !collection.Contains(book))
            .Add(book);
    }
    
    private static bool IsInAllCollections(List<HashSet<int>> collections, int book) => 
        collections.TrueForAll(bookSet => bookSet.Contains(book));

    private static void SortSets(SetType setType, Set bookSets)
    {
        if(setType == SetType.Smallest) bookSets.Collections.Sort(CrescentOrder());
    }

    private static Comparison<HashSet<int>> CrescentOrder() => (a,b) => 
        a.Count - b.Count;
}