using NUnit.Framework;

public sealed class TestObservable
{
    private sealed class Imitator<T>
    {
        private T m_Value;

        public T value
        {
            get
            {
                return m_Value;
            }
        }

        public void OnChanged(T value)
        {
            m_Value = value;
        }
    }

    [Test]
    public void SetStringAndAppend()
    {
        Observable<string> state = new Observable<string>();
        Imitator<string> imitator = new Imitator<string>();
        state.onChanged += imitator.OnChanged;
        Assert.AreEqual(null, state.value);
        Assert.AreEqual(null, imitator.value);

        state.value = "none";
        Assert.AreEqual("none", state.value);
        Assert.AreEqual("none", imitator.value);
        state.value = "active";
        Assert.AreEqual("active", state.value);
        Assert.AreEqual("active", imitator.value);

        state.value += "_begin";
        Assert.AreEqual("active_begin", state.value);
        Assert.AreEqual("active_begin", imitator.value);
        state.onChanged -= imitator.OnChanged;
    }

    [Test]
    public void SetIntAndAddAndDivide()
    {
        Observable<int> state = new Observable<int>();
        Imitator<int> imitator = new Imitator<int>();
        state.onChanged += imitator.OnChanged;
        Assert.AreEqual(0, state.value);
        Assert.AreEqual(0, imitator.value);

        state.value = 3;
        Assert.AreEqual(3, state.value);
        Assert.AreEqual(3, imitator.value);

        state.value += 4;
        Assert.AreEqual(7, state.value);
        Assert.AreEqual(7, imitator.value);

        state.value /= 3;
        Assert.AreEqual(2, state.value);
        Assert.AreEqual(2, imitator.value);
        state.onChanged -= imitator.OnChanged;
    }

    [Test]
    public void SetBoolAndXor()
    {
        Observable<bool> state = new Observable<bool>();
        Imitator<bool> imitator = new Imitator<bool>();
        state.onChanged += imitator.OnChanged;
        Assert.AreEqual(false, state.value);
        Assert.AreEqual(false, imitator.value);

        state.value = true;
        Assert.AreEqual(true, state.value);
        Assert.AreEqual(true, imitator.value);

        state.value ^= true;
        Assert.AreEqual(false, state.value);
        Assert.AreEqual(false, imitator.value);
        state.onChanged -= imitator.OnChanged;
    }
}
