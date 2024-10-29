namespace KataPotter.Domain;

public interface IPrice
{
    decimal GetTotal(IEnumerable<Book> rawBooks);
}