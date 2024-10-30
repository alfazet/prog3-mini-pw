using System;
using System.Collections;
using System.Collections.Generic;

namespace Lab08
{
	interface IMultiSeqOperation
	{
		// TODO: Add your code here.
        
		string Info { get; }
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
}