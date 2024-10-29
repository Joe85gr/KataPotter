namespace KataPotter.Domain;

public static class BookOrganiser
{
    public static IEnumerable<Set> GetSets(int[] books)
    {
        var setTypes = GetAllSetTypes();
        
        var bookSets = setTypes.Select(setType => GetSet(books, setType));
        
        return bookSets;
    }

    private static IEnumerable<SetType> GetAllSetTypes() => 
        Enum.GetValues(typeof(SetType)).Cast<SetType>();

    private static Set GetSet(IEnumerable<int> books, SetType setType)
    {
        var bookSets = new Set([]);

        foreach (var book in books) AddCollection(setType, bookSets, book);

        return bookSets;
    }

    private static void AddCollection(SetType setType, Set bookSets, int book)
    {
        if (BookIsInAllCollections(bookSets.Collections, book))
            bookSets.Collections.Add([]);

        if (setType == SetType.Smallest) 
            bookSets.SortCrescent();
        
        bookSets.Collections
            .First(collection => !collection.Contains(book))
            .Add(book);
    }
    
    private static bool BookIsInAllCollections(List<HashSet<int>> collections, int book) => 
        collections.TrueForAll(bookSet => bookSet.Contains(book));
}