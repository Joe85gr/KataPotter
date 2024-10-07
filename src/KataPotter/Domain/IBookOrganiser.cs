namespace KataPotter.Domain;

public interface IBookOrganiser
{
    IEnumerable<Set> GetSets(int[] books);
}