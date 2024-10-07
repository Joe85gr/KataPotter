namespace KataPotter.Domain;

public interface ICalculate
{
    decimal TotalPrice(IEnumerable<Book> rawBooks);
}