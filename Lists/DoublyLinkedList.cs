using System;
using System.Collections;
using System.Collections.Generic;
using SuperList.Enumerators;
using SuperList.Events.EventHandlers;
using SuperList.Nodes;

namespace SuperList.Lists
{
    public class DoublyLinkedList<T> : IList<T>, IActionable<T>
    {
        public Node<T> Head { get; set; }
        public int Count { get; set; }
        public ChangeEventHandler<T> ChangeEventHandler { get; set; }

        public DoublyLinkedList()
        {
            Head = null;
            Count = 0;
            ChangeEventHandler = new ChangeEventHandler<T>();
        }

        T IList<T>.this[int index]
        {
            get
            {
                if (IsIndexOutOfRange(index))
                {
                    throw new IndexOutOfRangeException();
                }

                return GetNodeAt(index).Value;
            }
            set => throw new NotSupportedException();
        }

        public void Add(T value)
        {
            var nodeToAdd = new Node<T>(value);

            if (Head is null)
            {
                Head = nodeToAdd;
            }
            else
            {
                var lastNode = GetLastNode();
                lastNode.Next = nodeToAdd;
                nodeToAdd.Prev = lastNode;
            }

            Count++;
            ChangeEventHandler.RaiseAddEvent(this, value);
        }

        public bool Remove(T value)
        {
            RemoveAt(IndexOf(value));
            ChangeEventHandler.RaiseRemoveEvent(this, value);

            return true;
        }

        public void RemoveAt(int index)
        {
            if (IsIndexOutOfRange(index))
            {
                throw new IndexOutOfRangeException();
            }

            if (Count == 1)
            {
                Head = null;

                return;
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

            Count--;
            ChangeEventHandler.RaiseRemoveEvent(this, nodeToRemove.Value);
        }

        public int IndexOf(T value)
        {
            var index = 0;
            var currentNode = Head;

            while (!(currentNode is null) && !value.Equals(currentNode.Value))
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

        public void ApplyByValue(Action<T> action, T value)
        {
            var node = GetNodeAt(IndexOf(value));

            action.Invoke(node.Value);
        }

        public void ApplyAll(Action<T> action)
        {
            var currentNode = Head;

            while (!(currentNode is null))
            {
                action.Invoke(currentNode.Value);
                currentNode = currentNode.Next;
            }
        }

        public void ApplyByPredicate(Action<T> action, Predicate<T> predicate)
        {
            var currentNode = Head;

            while (!(currentNode is null))
            {
                if (predicate(currentNode.Value))
                {
                    action.Invoke(currentNode.Value);
                }

                currentNode = currentNode.Next;
            }
        }

        public IEnumerator<T> GetEnumerator() => new SuperListEnumerator<T>(this);

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(T item) => throw new NotImplementedException();

        public bool IsReadOnly => throw new NotImplementedException();

        public void Insert(int index, T item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }
    }
}
