using System;
using System.Collections.Generic; 
using System.Linq; 
using System.Text; 
using System.IO;  

namespace CircularLinkedList
{
	//C# code to define list element
	//List element class
	class ListNode<T> //T is the generic type.
	{
		public ListNode(T elem) { val = elem; next = null; }
		public T val; //element data
		public ListNode<T> next;//next link
	}

	//List operations are outlined here and will be implemented by the
	//CirLinkedList class
	abstract class Cls<T>
	{
		public void insert(T val, int pos) { } //insert a new element to the list
		public void delete(int pos) { }//delete an element from the list
		public void printlist() { }//print all elements of the list
		public ListNode<T> findmin() { return null; }//show min element
		public ListNode<T> findmax() { return null; }//show max element

	}

	class CirLinkedList<T> : Cls<T>
	{
		public CirLinkedList()
		{
			pfirst = plast = null;
		}

		protected ListNode<T> pfirst;
		protected ListNode<T> plast;

		public void insert(T val, int pos)
		{
			ListNode<T> newnode = new ListNode<T>(val);
			int inserted = 1;
			//empty list
			if (pfirst == null && plast == null)
			{
				pfirst = newnode;
				plast = newnode;
				Console.WriteLine("Inserted:{0}", newnode.val);
			}
			//Insert at the beginning of the list
			else if (pos == 1)
			{
				newnode.next = pfirst;
				pfirst = newnode;
				Console.WriteLine("Inserted:{0}", newnode.val);
			}
			//Insert in the middle of the list
			else if (pos > 1 && pos <= countitem())
			{
				ListNode<T> ta;
				ta = pfirst;
				for (int t = 1; t < pos - 1; t = t + 1)
				{
					ta = ta.next;
				} 
				newnode.next = ta.next;
				ta.next = newnode;
				Console.WriteLine("Inserted:{0}", newnode.val);
			}
			else if (pos == countitem() + 1)
			{
				plast.next = newnode;
				plast = newnode;
				Console.WriteLine("Inserted:{0}", newnode.val);
			}
			else
			{
				inserted = 0; Console.WriteLine("Invalid position!");
			}

			if (inserted != 0 && plast != null)
				plast.next = pfirst; 
			//Make the circularly linked
		} 

		public void printlist()
		{
			ListNode<T> t;
			if (pfirst != null)
			{
				Console.WriteLine(pfirst.val);
				for (t = pfirst.next; t != pfirst; t = t.next)
				{
					Console.WriteLine(t.val);
				}
			}
			else
				Console.WriteLine("No item found!");
		}

		public int countitem()
		{
			ListNode<T> i;
			int t = 0;
			if (pfirst != null)
			{
				t = 1;
				for (i = pfirst.next; i != pfirst; i = i.next)
				{
					t = t + 1;
				}
			}
			return t;
		}             

		//delete an item from the linked list
		public void delete(int pos)
		{
			int deleted = 1;
			if (pfirst != null)
			{
				//make sure the list is not empty.
				ListNode<T> tr, temp;
				if (pos == 1)
				{
					//delete the first item
					if (countitem() == 1)
					{
						//The list contains only one item
						pfirst = null;
						plast = null;
					}
					else
					{
						//The list contains more than one item
						tr = pfirst;
						pfirst = pfirst.next;
						tr = null;
					}
					Console.WriteLine("Deleted");
				}
				else if (pos > 1 && pos <= countitem())
				{
					//delete middle item
					tr = pfirst;
					int i;
					for (i = 1; i < pos - 1; i = i + 1)
					{
						tr = tr.next;
					}
					//move to the item staying before the target item to be deleted
					temp = tr.next;
					//target item to be deleted
					tr.next = temp.next;
					if (temp.next == null)
						plast = tr;
					//delete last item
					temp = null;
					Console.WriteLine("Deleted");
				}
				else
				{
					deleted = 0; Console.WriteLine("Invalid position!");
				}

				if (deleted != 0 && plast != null)
					plast.next = pfirst;

				//keep the list circularly linked
			}
			else Console.WriteLine("No item found");
		}

		public ListNode<T> find(T tar)
		{
			ListNode<T> t;
			bool f = false;
			if (pfirst != null)
			{
				if (pfirst.val.ToString ().CompareTo (tar.ToString ()) == 0) 
				{
					return pfirst;
				}
				//found at the beginning of the list
				else
				{
					t = pfirst.next;
					while (t != pfirst)
					{
						if (t.val.ToString().CompareTo(tar.ToString()) == 0)
						{
							f = true; break;
						}
						t = t.next;
					}
				}

				if (f != false)
					return t;
				else
					return null;
			}
			else
				return null;
		}

		//find the min item
		public ListNode<T> findmin()
		{
			ListNode<T> t, min;
			min = pfirst;
			if (pfirst != null) //not empty list
			{
				//continue to find the min by comparison
				t = pfirst.next;
				while (t != pfirst)
				{
					if (t.val.ToString().CompareTo(min.val.ToString()) < 0)
						min = t;
					t = t.next;
				}
				return min;
			}
			else
				return null;  //empty list
		} //find the max item

		public ListNode<T> findmax()
		{
			ListNode<T> t, max;
			max = pfirst;
			if (pfirst != null) //not empty list
			{
				//continue to find the max item by comparision
				t = pfirst.next;
				while (t != pfirst)
				{
					if (t.val.ToString().CompareTo(max.val.ToString()) > 0)
						max = t;
					t = t.next;
				}
				return max;
			}
			else
				return null; //empty list
		}
	}


	class MainClass
	{
		public static void Main (string[] args)
		{
			select ();
		}

		static void select()
		{
			CirLinkedList<int> mylist = new CirLinkedList<int>();
			ListNode<int> temp;
			int val, ch, pos;
			char yes = 'y';

			//display menu
			showmenu();
			while (yes == 'y')
			{
				Console.Write("Enter your choice:");
				ch = int.Parse(Console.ReadLine().ToString());
				switch (ch)
				{
				case 1: Console.Write("Value:");
					val = int.Parse(Console.ReadLine());
					Console.Write("Position:");
					pos = int.Parse(Console.ReadLine());
					mylist.insert(val, pos);
					break;
				case 2:
					Console.Write("Position:");
					pos = int.Parse(Console.ReadLine());
					mylist.delete(pos);
					break;
				case 3:
					Console.WriteLine("Number of items:" + mylist.countitem());
					break;
				case 4:
					if (mylist.findmax() != null && mylist.findmin() != null)
						Console.WriteLine("Min item:{0}\nMax item:{1}", mylist.findmin().val, mylist.findmax().val);
					break;
				case 5:
					Console.Write("Find what?");
					val = int.Parse(Console.ReadLine());
					temp = mylist.find(val);
					if (temp != null)
						Console.WriteLine("Found {0}", temp.val);
					else
						Console.WriteLine("Not found");
					break;
				case 6:
					Console.WriteLine("All items:");
					mylist.printlist();
					break;
				case 7:
					Environment.Exit(0);
					break;
				default:
					Console.WriteLine("Invalid choice!");
					break;
				}
				Console.Write("Continue? Press y to continue:");
				yes = char.Parse(Console.ReadLine());
			}
		}

		static void showmenu()
		{
			Console.WriteLine("=================================");
			Console.WriteLine("Circularly LinkedList Operations Menu");
			Console.WriteLine("=================================");
			Console.WriteLine("1.Add a new item");
			Console.WriteLine("2.Delete an item");
			Console.WriteLine("3.Show number of items");
			Console.WriteLine("4.Show min and max items");
			Console.WriteLine("5.Find an item");
			Console.WriteLine("6.Show all items");
			Console.WriteLine("7.Exit");
		} 
	}
} 