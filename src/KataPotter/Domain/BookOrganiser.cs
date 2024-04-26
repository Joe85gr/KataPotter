namespace KataPotter.Domain;

public class BookOrganiser : IBookOrganiser
{
    public IEnumerable<Set> GetBooksSets(int[] books)
    {
        var setTypes = GetSetTypes();
        
        var bookSets = setTypes.Select(setType => GetBooksSets(books, setType));
        
        return bookSets;
    }

    private static IEnumerable<SetType> GetSetTypes() => Enum.GetValues(typeof(SetType)).Cast<SetType>();

    private static Set GetBooksSets(IEnumerable<int> books, SetType setType)
    {
        var bookSets = new Set();

        foreach (var book in books) AddBookToSet(setType, bookSets, book);

        return bookSets;
    }

    private static void AddBookToSet(SetType setType, Set bookSets, int book)
    {
        if (BookIsInAllCollections(bookSets.Collections, book))
        {
            bookSets.Collections.Add([]);
        }
        
        SortSets(setType, bookSets);

        bookSets.Collections
            .First(collection => !collection.Contains(book))
            .Add(book);
    }
    
    private static bool BookIsInAllCollections(List<HashSet<int>> collections, int book) => 
        collections.TrueForAll(bookSet => bookSet.Contains(book));

    private static void SortSets(SetType setType, Set bookSets)
    {
        if(setType == SetType.Smallest) bookSets.Collections.Sort(CrescentOrder());
    }

    private static Comparison<HashSet<int>> CrescentOrder() => (a,b) => 
        a.Count - b.Count;
}