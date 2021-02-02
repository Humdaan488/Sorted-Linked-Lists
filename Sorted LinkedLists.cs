#nullable enable
using System;
using static System.Console;
using MediCal;

namespace Bme121
{
    partial class LinkedList
    {
        // Method used to indicate a target Drug object when searching.
        
        public static bool IsTarget( Drug data ) 
        { 
            return data.Name.StartsWith( "FOSAMAX", StringComparison.OrdinalIgnoreCase ); 
        }
        
        // Method used to compare two Drug objects when sorting.
        // Return is -/0/+ for a<b/a=b/a>b, respectively.
        
        public static int Compare( Drug a, Drug b )
        {
            return string.Compare( a.Name, b.Name, StringComparison.OrdinalIgnoreCase );
        }
        
        // Method used to add a new Drug object to the linked list in sorted order.
        
        public void InsertInOrder( Drug newData )
        {
                                                                                           
            //Check if new inputted is valid to be added to the list
            if (newData == null)
            {
                throw new ArgumentNullException ( nameof ( newData ) );
            }
            
            //Create a new node since we are adding smth (Data property of newNode)
            Node newNode = new Node (newData);
            
            //Now that we know node with data can be added, we can go to our cases 
            
            // Case 1:  check for if we are adding to an empty list 
            if (Count == 0)
            {
                Head = newNode;
                Tail = newNode;
                Count ++;
            }
            
            else  // if there are actual elements in the LinkedList and we want to add before a target we need
            {
                // comparing drug objects so we can so we need a trailing pointer so need current and previous nodes
                Node? previousNode = null;
                Node? currentNode = Head;
                
                for ( int i = 0; i < Count; i ++)
                {
                    if (Compare ( newNode.Data, currentNode!.Data)< 0) // new node less than currentNode so it can be added to the list
                    {
                        //Add at the beginning
                        if (currentNode == Head)
                        {
                            newNode.Next = currentNode;
                            Head = newNode;
                            Count ++;
                            return;
                        }
                        //Add before Drug object which is larger so add in the middle of the linkedlist 
                        else 
                        {
                            previousNode.Next = newNode;
                            newNode.Next = currentNode;
                            Count++;
                            return;
                        }
                    }
                        //Advancing through list by going to the next Node
                        previousNode = currentNode;
                        currentNode = currentNode.Next;
                }
                //When none of this is the case we add to the end of the linkedList;
                //Insert at the end of the linkedlist when a > b

                Tail.Next = newNode;
                Tail = newNode;
                Count ++;
            }
        }
    }
    
    static class Program
    {
        // Method to test operation of the linked list.
        static void Main( )
        {
            Drug[ ] drugArray = Drug.ArrayFromFile( "RXQT1503-100.txt" );
            
            //Created a new LinkedList Object of LinkedList type
            LinkedList drugList = new LinkedList( );
            foreach( Drug d in drugArray ) drugList.InsertInOrder( d );
            
            WriteLine( "drugList.Count = {0}", drugList.Count );
            foreach( Drug d in drugList.ToArray( ) ) WriteLine( d );
        }
    }
}

/*


--Complete the Insert In Order method so it puts the Drug object passed as its argument into linked list before the first Drug object
    on the list which compares as larger. 
        ----*** ADD BEFORE VERSION
    
     if there are no larger ones in list, new one is added at the end
    

*** Basically working to create a sorted linkedlist list by adding elements one a time using INsertSort metho which places eleemnts
    in their sorted position with respect to those elements alreadt added in the list.
    



    In some way or form, you have to account for all the special cases. Sure the structure might not always be a simple if, else if
                                                                                                            ELSE IF, ELSE
*/
