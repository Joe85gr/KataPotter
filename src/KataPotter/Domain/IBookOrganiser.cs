namespace KataPotter.Domain;

public interface IBookOrganiser
{
    IEnumerable<Set> GetBooksSets(int[] books);
}