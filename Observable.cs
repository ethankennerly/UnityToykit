using System;
using System.Collections.Generic;

// Generically dispatches an event when the value changes.
// Example: Editor/Tests/TestObservable.cs
//
// A tradeoff is the value is publicly exposed to changing.
public sealed class Observable<T>
{
    public event Action<T> onChanged;

    private T m_Value;

    public T value
    {
        get
        {
            return m_Value;
        }
        set
        {
            if (Equals(m_Value, value))
            {
                return;
            }
            m_Value = value;
            if (onChanged == null)
            {
                return;
            }
            onChanged(value);
        }
    }

    // Check if equal when not sure if struct or class.
    // https://stackoverflow.com/questions/390900/cant-operator-be-applied-to-generic-types-in-c
    private static bool Equals(T x, T y)
    {
        return EqualityComparer<T>.Default.Equals(x, y);
    }

    public override string ToString()
    {
        return base.ToString() + "<" + m_Value.ToString() + ">";
    }
}
