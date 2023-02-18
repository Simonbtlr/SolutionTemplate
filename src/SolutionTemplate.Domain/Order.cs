using System.Collections.Generic;
using SolutionTemplate.Domain.Root;

namespace SolutionTemplate.Domain;

public sealed class Order : AggregateRoot<long>
{
    public string? Note { get; private set; }
    public IReadOnlyList<Item> Items { get; private set; }
    public IReadOnlyList<Point> Points { get; private set; }

    internal Order(
        long id, 
        string? note,
        IReadOnlyList<Item> items,
        IReadOnlyList<Point> points)
    {
        Id = id;
        Note = note;
        Items = items;
        Points = points;
    }
    
    private Order()
    {
        Items = new List<Item>();
        Points = new List<Point>();
    }

    private Order(
        string? note,
        IReadOnlyList<Item> items,
        IReadOnlyList<Point> points)
    {
        Note = note;
        Items = items;
        Points = points;
    }

    public static Order Create(
        string? note,
        IReadOnlyList<Item> items,
        IReadOnlyList<Point> points) => 
        new(note, items, points);
}
