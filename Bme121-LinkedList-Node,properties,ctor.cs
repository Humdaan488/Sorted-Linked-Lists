#nullable enable
using MediCal;

namespace Bme121
{
    partial class LinkedList
    {
        // Define the 'Node' class used to form the linked list.

        class Node
        {
            public Node? Next { get; set; }
            public Drug Data { get; private set; }

            public Node( Drug newData )
            {
                Next = null;
                Data = newData;
            }
        }

        // Define the properties (fields) and constructor for the linked list.

        Node? Tail { get; set; }
        Node? Head { get; set; }
        public int Count { get; private set; }

        public LinkedList( )
        {
            Tail = null;
            Head = null;
            Count = 0;
        }
    }
}
