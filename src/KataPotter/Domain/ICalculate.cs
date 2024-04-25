namespace KataPotter.Domain;

public interface ICalculate
{
    decimal TotalBooksPrice(IEnumerable<Book> rawBooks);
}