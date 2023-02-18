using SolutionTemplate.Domain.Root;

namespace SolutionTemplate.Domain;

public sealed class Item : Entity<long>
{
    public string? Note { get; private set; }
    
    private Item() { }

    private Item(string? note)
    {
        Note = note;
    }

    public static Item Create(string? note) =>
        new(note);
}
