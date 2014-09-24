using System;
using System.Collections.Generic;


public class QueueList<T> : List<T>
{
    public int Enqueue(T item)
    {
        Add(item);
        return Count - 1;
    }

    public T Dequeue()
    {
        if (Count == 0) return default(T);
        T result = this[0];
        RemoveAt(0);
        return result;
    }

    public T Peek()
    {
        if (Count == 0) return default(T);
        return this[0];
    }

    public override string ToString()
    {
        return string.Format("Count={0}", Count);
    }
}
