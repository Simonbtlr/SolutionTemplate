using System;

namespace SolutionTemplate.Domain.Root;

public class AggregateRoot<T> where T : struct
{
    public T Id
    {
        get => _id ?? throw new ArgumentNullException();
        protected init => _id = value;
    }

    private readonly T? _id;
}
