namespace KataPotter.Infrastructure;

public class InMemoryClient : IClient
{
    public float Get(int book) => book switch
    {
        2 => 0.95f,
        3 => 0.9f,
        4 => 0.8f,
        5 => 0.75f,
        _ => 1f
    };
}