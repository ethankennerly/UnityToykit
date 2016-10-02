using System/*<IComparable>*/;

namespace Finegamedesign.Utils
{
	// Useful to detect when a value has changed, and when it has changed to an exact value.
	// Can watch external data structures.
	// Can also be used to store the state itself.
	// This explicit polling is more flexible and lighter than an Observer pattern.
	// An Observer pattern would automatically push when the original value changes.
	// Example: Editor/Tests/TestWatcher.cs
	public sealed class Watcher<T> where T : IComparable
	{
		public static Watcher<T> Create(T subjectValue)
		{
			Watcher<T> watch = new Watcher<T>();
			watch.Setup(subjectValue);
			return watch;
		}

		public T next;
		private T current;
		public T previous;

		public void Setup(T subjectValue)
		{
			next = subjectValue;
			previous = subjectValue;
			current = subjectValue;
		}

		public void Update(T subjectValue)
		{
			previous = current;
			next = subjectValue;
			current = subjectValue;
		}

		public bool IsChange()
		{
			return !IsEqual(current, previous);
		}

		public bool IsChangeTo(T subjectValue)
		{
			return IsChange() && IsEqual(current, subjectValue);
		}

		// Work around comparison of values for C# generics:
		// http://stackoverflow.com/questions/6480577/how-to-compare-values-of-generic-types
		// If first is null, check if both are null.
		// Example: Editor/Tests/TestWatcher.cs
		private bool IsEqual(T a, T b)
		{
			bool isEqualValue;
			if (null == a || null == b)
			{
				isEqualValue = a == null && b == null;
			}
			else
			{
				isEqualValue = 0 == a.CompareTo(b);
			}
			return isEqualValue;
		}
	}
}
