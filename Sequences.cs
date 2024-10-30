using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using static Lab08.SequenceHelpers;

namespace Lab08
{
	interface ISequence : IEnumerable
	{
		string Name { get; }
	}
    class ValueSequenceEnumerator : IEnumerator
    {
        int[] _arr;
        int _i;
        bool _looped;

        public ValueSequenceEnumerator(int[] arr, bool looped)
        {
            _arr = arr;
            _looped = looped;
            _i = -1;
        }

        object IEnumerator.Current
        {
            get
            {
                return _arr[_i];
            }
        }

        public bool MoveNext()
        {
            if (_arr.Length == 0)
            {
                return false;
            }
            _i++;
            if (_i >= _arr.Length)
            {
                if (_looped)
                {
                    _i = 0;
                }
                else
                {
                    return false;
                }
            }
            return true;
        }

        void IEnumerator.Reset()
        {
            throw new NotImplementedException();
        }
    }

    public class ValueSequence : ISequence
	{
		bool _looped;
		int[] _arr;
		int pos = -1;
		public string Name
		{
			get
			{
				return (_looped ? "Looped Value Sequence" : "Value Sequence");
			}
		}
		public ValueSequence(int[] arr, bool looped = true)
		{
			_arr = new int[arr.Length];
            for (int i = 0; i < arr.Length; i++)
            {
                _arr[i] = arr[i];
            }
			_looped = looped;
		}

        public IEnumerator GetEnumerator()
        {
            return new ValueSequenceEnumerator(_arr, _looped);
        }

		public override string ToString()
		{
			var sb = new StringBuilder("[");
			foreach (var s in SeqFormat(FirstNSeqElem(5, this), "{0}, "))
			{
				sb.Append(s);
			}
			sb.Append("...]");
			return sb.ToString();
		}
    }

	class ArithmeticSequenceEnumerator : IEnumerator
	{
		int _cur, _step;

		public ArithmeticSequenceEnumerator(int start, int step)
		{
			_cur = start - step;
			_step = step;
		}

        object IEnumerator.Current
		{
			get
			{
				return _cur;
			}
		}

        public bool MoveNext()
		{
			_cur += _step;
			return true;
		}

        void IEnumerator.Reset()
        {
            throw new NotImplementedException();
        }
    }

    public class ArithmeticSequence : ISequence
    {
		int _start, _step;

        public string Name
        {
            get
            {
                return $"Arithmetic Sequence {{ Start = {_start}, Step = {_step} }}";
            }
        }

        public ArithmeticSequence(int start, int step)
		{
			_start = start;
			_step = step;
		}

		public IEnumerator GetEnumerator()
		{
			return new ArithmeticSequenceEnumerator(_start, _step);
		}

        public override string ToString()
        {
            var sb = new StringBuilder("[");
            foreach (var s in SeqFormat(FirstNSeqElem(5, this), "{0}, "))
            {
                sb.Append(s);
            }
            sb.Append("...]");
            return sb.ToString();
        }
    }

    class FibonacciSequenceEnumerator : IEnumerator
    {
        int _a, _b, _prev;

        public FibonacciSequenceEnumerator()
        {
			_a = 0;
			_b = 1;
            _prev = 0;
        }

        object IEnumerator.Current
        {
            get
            {
                return _prev;
            }
        }

        public bool MoveNext()
        {
            int temp = _b;
			_b = _a + _b;
            _prev = _a;
			_a = temp;
            return true;
        }

        void IEnumerator.Reset()
        {
            throw new NotImplementedException();
        }
    }

    public class FibonacciSequence : ISequence
    {
        public string Name
        {
            get
            {
                return "Fibonacci Sequence";
            }
        }

        public IEnumerator GetEnumerator()
        {
            return new FibonacciSequenceEnumerator();
        }
        public override string ToString()
        {
            var sb = new StringBuilder("[");
            foreach (var s in SeqFormat(FirstNSeqElem(5, this), "{0}, "))
            {
                sb.Append(s);
            }
            sb.Append("...]");
            return sb.ToString();
        }
    }

    // TODO: Add your code here
}
