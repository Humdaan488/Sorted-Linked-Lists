#nullable enable
using System;
using MediCal;

namespace Bme121
{
    partial class LinkedList
    {
        // Convert the linked list to an array of its data elements.

        public Drug[ ] ToArray( )
        {
            Drug[ ] result = new Drug[ Count ];

            int i = 0;
            Node? currentNode = Head;
            while( currentNode != null )
            {
                result[ i ] = currentNode.Data;

                i ++;
                currentNode = currentNode.Next;
            }

            return result;
        }
    }
}
