namespace SolutionTemplate.Domain;

public sealed class Item
{
    public long Id { get; init; }

    private Item() { }

    private Item(long id)
    {
        Id = id;
    }

    public static Item Create(long id) =>
        new(id);
}
