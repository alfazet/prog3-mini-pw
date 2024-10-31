using System;
using System.Collections;
using System.Collections.Generic;

namespace Lab08
{
	interface IMultiSeqOperation
	{
		// TODO: Add your code here.
        
		string Info { get; }

		public abstract IEnumerable Process(params ISequence[] sequences);
	}

	static class SequenceHelpers
	{
		// TODO: Add your code here.
		public static IEnumerable FirstNSeqElem(int N, IEnumerable enumeration)
        {
			var enumerator = enumeration.GetEnumerator();
			for (int i = 0; i < N; i++)
			{
				if (!enumerator.MoveNext())
				{
					yield break;
				}
				yield return enumerator.Current;
			}
		}

		public static IEnumerable<string> SeqFormat(IEnumerable enumeration, string format = "{0}")
		{
			foreach (var item in enumeration)
			{
				yield return String.Format(format, item);
			}
		}

		public static void PrintSequence(IEnumerable enumeration, int limit = 15)
		{
			if (enumeration is ISequence)
			{
				Console.WriteLine(((ISequence)enumeration).Name);
			}
			int i = 0;
			foreach (var item in enumeration)
			{
				Console.WriteLine("{0,3}: {1}", i, item);
				i++;
				if (i == limit)
				{
					return;
				}
			}
		}
    }

    // TODO: Add your code here.
	public class AddAllSequence : IMultiSeqOperation
	{
        public string Info { get; }

        public IEnumerable Process(params ISequence[] sequences)
		{
			int n = sequences.Length;
			if (n <= 1)
			{
				throw new ArgumentException("Expected at least two sequences");
			}
			var enumerators = new IEnumerator[n];
			for (int i = 0; i < n; i++)
			{
				enumerators[i] = sequences[i].GetEnumerator();
			}
			while (true)
			{
				bool all_ok = true;
				int sum = 0;
				foreach (var enumerator in enumerators)
				{
					if (!enumerator.MoveNext())
					{
						all_ok = false;
						break;
					}
					sum += (int)enumerator.Current;
				}
				if (!all_ok)
				{
					yield break;
				}
				yield return sum;
			}
		}
	}

    public class AddAnySequence : IMultiSeqOperation
    {
        public string Info { get; }

        public IEnumerable Process(params ISequence[] sequences)
        {
            int n = sequences.Length;
            if (n <= 1)
            {
                throw new ArgumentException("Expected at least two sequences");
            }
            var enumerators = new IEnumerator[n];
            for (int i = 0; i < n; i++)
            {
                enumerators[i] = sequences[i].GetEnumerator();
            }
            while (true)
            {
                bool any_running = false;
                int sum = 0;
                foreach (var enumerator in enumerators)
                {
                    if (enumerator.MoveNext())
                    {
						any_running = true;
                        sum += (int)enumerator.Current;
                    }
                }
				if (any_running)
				{
					yield return sum;
				} else
				{
					yield break;
				}
            }
        }
    }
}