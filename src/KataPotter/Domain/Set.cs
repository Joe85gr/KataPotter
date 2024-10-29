namespace KataPotter.Domain;

public record Set(List<HashSet<int>> Collections);

public static class SetExtensions
{
    public static void SortCrescent(this Set set) => 
        set.Collections.Sort(CrescentOrder);
    
    private static int CrescentOrder(HashSet<int> a, HashSet<int> b) => 
        a.Count - b.Count;
}