using SolutionTemplate.Domain.Root;

namespace SolutionTemplate.Domain;

public sealed class Point : Entity<long>
{
    public string? Note { get; private set; }
    
    private Point() { }
    
    private Point(string? note)
    {
        Note = note;
    }

    public static Point Create(string? note) => 
        new(note);
}
