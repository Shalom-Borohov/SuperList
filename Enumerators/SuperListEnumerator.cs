using System;
using System.Collections;
using System.Collections.Generic;
using SuperList.Lists;

namespace SuperList.Enumerators
{
    public class SuperListEnumerator<T> : IEnumerator<T>
    {
        public DoublyLinkedList<T> List;
        public int Index = -1;

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

        public T Current => List.GetNodeAt(Index).Value;

        public bool MoveNext()
        {
            Index++;

            return !List.IsIndexOutOfRange(Index);
        }

        public void Reset()
        {
            Index = -1;
        }

        public void Dispose() { }
    }
}
