#nullable enable
using System;
using MediCal;

namespace Bme121
{
    partial class LinkedList
    {
        // Add a new item at the beginning of the linked list.

        public void Prepend( Drug newData ) { AddFirst( newData ); }

        public void AddFirst( Drug newData )
        {
            if( newData == null ) throw new ArgumentNullException( nameof( newData ) );

            Node  newNode = new Node( newData );
            Node? oldHead = Head;

            if( Tail == null ) Tail = newNode;
            else newNode.Next = oldHead;
            Head = newNode;

            Count ++;
        }
        
        // Add a new item at the end of the linked list.

        public void Append( Drug newData ) { AddLast( newData ); }

        public void AddLast( Drug newData )
        {
            if( newData == null ) throw new ArgumentNullException( nameof( newData ) );

            Node  newNode = new Node( newData );
            Node? oldTail = Tail;

            if( Head == null ) Head = newNode;
            else oldTail!.Next = newNode;
            Tail = newNode;

            Count ++;
        }
    }
}
