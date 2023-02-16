namespace SolutionTemplate.Domain;

public sealed class Order
{
    public long Id { get; init; }
    public IReadOnlyList<Item> Items { get; private set; }
    public IReadOnlyList<Point> Points { get; private set; }
    
    // ReSharper disable once UnusedMember.Local
    private Order()
    {
        Items = new List<Item>();
        Points = new List<Point>();
    }
    
    private Order(
        long id,
        IReadOnlyList<Item> items,
        IReadOnlyList<Point> points)
    {
        Id = id;
        Items = items;
        Points = points;
    }

    public static Order Create(long id, List<Item> items, List<Point> points) => 
        new(id, items, points);
}
