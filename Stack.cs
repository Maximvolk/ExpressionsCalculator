using System;

namespace ExpressionsCalculation
{
    class Stack<T>
    {
        private const int _initialSize = 20;
        private const int _resizeCoefficient = 2;

        private T[] _array;
        private int _cursor = -1;

        public Stack()
        {
            _array = new T[_initialSize];
        }

        public void Push(T item)
        {
            if (_cursor == _array.Length)
                Array.Resize(ref _array, _array.Length * _resizeCoefficient);

            _cursor++;
            _array[_cursor] = item;
        }

        public T Pop()
        {
            if (_cursor < 0)
                return default;

            var lastItem = _array[_cursor];
            _cursor--;

            return lastItem;
        }
    }
}
