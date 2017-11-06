using NUnit.Framework;

public sealed class TestObservable
{
	private sealed class Observer<T>
	{
		private T m_Value;

		public T value
		{
			get
			{
				return m_Value;
			}
		}

		public void OnChanged(T previousValue, T value)
		{
			m_Value = value;
		}
	}

	[Test]
	public void SetStringAndAppend()
	{
		Observable<string> state = new Observable<string>();
		Observer<string> observer = new Observer<string>();
		state.onChanged += observer.OnChanged;
		Assert.AreEqual(null, state.value);
		Assert.AreEqual(null, observer.value);
		state.value = "none";
		Assert.AreEqual("none", state.value);
		Assert.AreEqual("none", observer.value);
		state.value = "active";
		Assert.AreEqual("active", state.value);
		Assert.AreEqual("active", observer.value);

		state.value += "_begin";
		Assert.AreEqual("active_begin", state.value);
		Assert.AreEqual("active_begin", observer.value);
	}

	[Test]
	public void SetIntAndAddAndDivide()
	{
		Observable<int> state = new Observable<int>();
		Observer<int> observer = new Observer<int>();
		state.onChanged += observer.OnChanged;
		Assert.AreEqual(0, state.value);
		Assert.AreEqual(0, observer.value);
		state.value = 3;
		Assert.AreEqual(3, state.value);
		Assert.AreEqual(3, observer.value);
		state.value += 4;
		Assert.AreEqual(7, state.value);
		Assert.AreEqual(7, observer.value);

		state.value /= 3;
		Assert.AreEqual(2, state.value);
		Assert.AreEqual(2, observer.value);
	}

	[Test]
	public void SetBoolAndXor()
	{
		Observable<bool> state = new Observable<bool>();
		Observer<bool> observer = new Observer<bool>();
		state.onChanged += observer.OnChanged;
		Assert.AreEqual(false, state.value);
		Assert.AreEqual(false, observer.value);
		state.value = true;
		Assert.AreEqual(true, state.value);
		Assert.AreEqual(true, observer.value);
		state.value ^= true;
		Assert.AreEqual(false, state.value);
		Assert.AreEqual(false, observer.value);
	}
}
