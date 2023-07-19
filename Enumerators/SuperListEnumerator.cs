using System;
using System.Collections;
using System.Collections.Generic;
using SuperList.Lists.Classes;

namespace SuperList.Enumerators
{
    public class SuperListEnumerator<T> : IEnumerator<T>
    {
        public DoublyLinkedList<T> List = null;
        public int Position = -1;

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

        public T Current => List.GetNodeAt(Position).Value;

        public bool MoveNext()
        {
            Position++;

            return !List.IsIndexOutOfRange(Position);
        }

        public void Reset()
        {
            Position = -1;
        }

        public void Dispose() { }
    }
}
