#nullable enable
using System;
using MediCal;

namespace Bme121
{
    partial class LinkedList
    {
        // Determine whether the linked list contains a target Drug object.

        public bool Contains( )
        {
            Node? currentNode = Head;
            
            while( currentNode != null )
            {
                if( IsTarget( currentNode.Data ) ) return true;
                currentNode = currentNode.Next;
            }
            
            return false;
        }
        
        // Remove from the linked list the first instance of a target Drug object.

        public Drug? Remove( )
        {
            Remove( out Drug? removedData );
            return removedData;
        }

        public bool Remove( out Drug? removedData )
        {
            Node? previousNode = null;
            Node? currentNode = Head;
            
            while( currentNode != null )
            {
                if( IsTarget( currentNode.Data ) )
                {
                    Node? nextNode = currentNode.Next;
                    
                    if( currentNode == Head ) Head = nextNode;
                    else previousNode!.Next = nextNode;
                    if( currentNode == Tail ) Tail = previousNode;
                    currentNode.Next = null;
                    
                    Count --;
                    
                    removedData = currentNode.Data;
                    return true;
                }
                
                previousNode = currentNode;
                currentNode = currentNode.Next;
            }
            
            removedData = null;
            return false;
        }
    }
}
