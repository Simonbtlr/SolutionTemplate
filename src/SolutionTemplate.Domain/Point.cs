namespace SolutionTemplate.Domain;

public sealed class Point
{
    public long Id { get; init; }

    // ReSharper disable once UnusedMember.Local
    private Point() { }

    private Point(long id)
    {
        Id = id;
    }

    public static Point Create(long id) => 
        new(id);
}
