using System;
using System.Collections;
using System.Collections.Generic;

namespace SuperList
{
    public class SuperListEnumerator<T> : IEnumerator<T>
    {
        public DoublyLinkedList<T> List = null;
        public int position = -1;

        public SuperListEnumerator(DoublyLinkedList<T> list)
        {
            List = list;
        }

        object IEnumerator.Current
        {
            get
            {
                try
                {
                    return Current;
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }

        public T Current => List.GetNodeAt(position).Value;

        public bool MoveNext()
        {
            position++;

            return !List.IsIndexOutOfRange(position);
        }

        public void Reset()
        {
            position = -1;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
