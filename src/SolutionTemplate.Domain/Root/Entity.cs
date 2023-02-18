using System;

namespace SolutionTemplate.Domain.Root;

public class Entity<T> where T : struct
{
    public T Id
    {
        get
        {
            if (_id is null)
                throw new ArgumentNullException();

            return _id.Value;
        }
        set => _id = value;
    }

    private T? _id;
}
