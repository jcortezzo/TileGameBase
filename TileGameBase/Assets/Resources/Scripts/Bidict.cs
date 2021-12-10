using System.Collections.Generic;

public class Bidict<T1, T2>
{
    private IDictionary<T1, T2> forward;
    private IDictionary<T2, T1> backward;

    public Bidict()
    {
        forward = new Dictionary<T1, T2>();
        backward = new Dictionary<T2, T1>();
    }

    public int Count { get { return forward.Count; }}

    public bool Remove(T1 item)
    {
        T2 value = forward[item];
        bool success = forward.Remove(item);
        backward.Remove(value);
        return success;
    }
    
    public bool Remove(T2 item)
    {
        T1 value = backward[item];
        bool success = backward.Remove(item);
        forward.Remove(value);
        return success;
    }

    public void Clear()
    {
        forward.Clear();
        backward.Clear();
    }

    public bool ContainsKey(T1 item)
    {
        return forward.ContainsKey(item);
    }
    
    public bool ContainsKey(T2 item)
    {
        return backward.ContainsKey(item);
    }

    public T1 this[T2 item]
    {
        get
        {
            return backward[item];
        }

        set
        {
            backward[item] = value;
            forward[value] = item;
        }
    }

    public T2 this[T1 item]
    {
        get
        {
            return forward[item];
        }
        set
        {
            forward[item] = value;
            backward[value] = item;
        }
    }
}
