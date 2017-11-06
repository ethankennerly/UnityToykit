using System;

public sealed class Observable<T>
{
	public event Action<T, T> onChanged;

	private T m_ObservedField;

	public T value
	{
		get
		{
			return m_ObservedField;
		}
		set
		{
			if (object.ReferenceEquals(m_ObservedField, value))
			{
				return;
			}
			T previousValue = m_ObservedField;
			m_ObservedField = value;
			if (onChanged == null)
			{
				return;
			}
			onChanged(previousValue, value);
		}
	}

	public override string ToString()
	{
		return base.ToString() + "<" + value.ToString() + ">";
	}

	public static implicit operator Observable<T>(T value)
	{
		Observable<T> observable = new Observable<T>();
		observable.value = value;
		return observable;
	}

	public static implicit operator T(Observable<T> observable)
	{
		return observable.value;
	}
}
