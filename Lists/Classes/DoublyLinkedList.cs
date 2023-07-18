using System;
using System.Collections;
using SuperList.Enumerators;
using SuperList.Events.EventHandlers;
using SuperList.Lists.Interfaces;
using SuperList.Nodes;

namespace SuperList.Lists.Classes
{
    public class DoublyLinkedList<T> : IList, IActionable<T>
    {
        public Node<T> Head = null;
        public int Count = 0;
        public ChangeEventHandler<T> ChangeEventHandler;

        object IList.this[int index] { get => GetNodeAt(index); set => throw new NotImplementedException(); }

        int ICollection.Count => Count;

        public int Add(object value)
        {
            GetLastNode().Next = new Node<T> { Value = (T)value };
            Count++;

            ChangeEventHandler.RaiseAddEvent(this, (T)value);

            return Count - 1;
        }

        public void Remove(object value)
        {
            RemoveAt(IndexOf(value));
            ChangeEventHandler.RaiseRemoveEvent(this, (T)value);
        }

        public void RemoveAt(int index)
        {
            if (IsIndexOutOfRange(index))
            {
                throw new IndexOutOfRangeException();
            }

            var nodeToRemove = GetNodeAt(index);
            var prevNode = nodeToRemove.Prev;
            var nextNode = nodeToRemove.Next;

            nodeToRemove.Prev = null;
            nodeToRemove.Next = null;

            if (!(prevNode is null))
            {
                prevNode.Next = nextNode;
            }

            if (!(nextNode is null))
            {
                nextNode.Prev = prevNode;
            }

            ChangeEventHandler.RaiseRemoveEvent(this, nodeToRemove.Value);
        }

        public int IndexOf(object value)
        {
            var index = 0;
            var currentNode = Head;

            while (!(currentNode is null) && !((T)value).Equals(currentNode.Value))
            {
                currentNode = currentNode.Next;
                index++;
            }

            if (currentNode is null)
            {
                return -1;
            }

            return index;
        }

        public Node<T> GetLastNode() => GetNodeAt(Count - 1);

        public Node<T> GetNodeAt(int index)
        {
            var currentNode = Head;
            var position = 0;

            while (!(currentNode is null) && position != index)
            {
                currentNode = currentNode.Next;
                position++;
            }

            return currentNode;
        }

        public bool IsIndexOutOfRange(int index) => index < 0 || index >= Count;

        public IEnumerator GetEnumerator() => new SuperListEnumerator<T>(this);

        public void ApplyByValue(Action<T> doAction, T value)
        {
            var node = GetNodeAt(IndexOf(value));

            doAction(node.Value);
        }

        public void ApplyAll(Action<T> doAction)
        {
            var currentNode = Head;

            while (!(currentNode is null))
            {
                doAction(currentNode.Value);
                currentNode = currentNode.Next;
            }
        }

        public void ApplyByPredicate(Action<T> doAction, Func<T, bool> predicate)
        {
            var currentNode = Head;

            while (!(currentNode is null))
            {
                if (predicate(currentNode.Value))
                {
                    doAction(currentNode.Value);
                }

                currentNode = currentNode.Next;
            }
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(object value)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(Array array, int index)
        {
            throw new NotImplementedException();
        }

        public bool IsFixedSize => throw new NotImplementedException();

        public bool IsReadOnly => throw new NotImplementedException();

        public bool IsSynchronized => throw new NotImplementedException();

        public object SyncRoot => throw new NotImplementedException();

        public void Insert(int index, object value)
        {
            throw new NotImplementedException();
        }
    }
}
