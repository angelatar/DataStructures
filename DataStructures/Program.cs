using AVL_Tree;
using Red_Black_Tree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures
{
    class Program
    {
        private static void Main(string[] args)
        {
            Avl<int, string> avlTree = new Avl<int, string>();
            RedBlack<int, string> rbTree = new RedBlack<int, string>();
            Dictionary<int, string> dicTree = new Dictionary<int, string>();

            int entriesNumber = 0;

            Console.Write("Enter entries number (1 if 320, 2 if 640 3 if 1280) : ");

            switch (int.Parse(Console.ReadLine()))
            {
                case 1:
                    entriesNumber = 320;
                    break;
                case 2:
                    entriesNumber = 640;
                    break;
                case 3:
                    entriesNumber = 1280;
                    break;
            }

            Tester(avlTree, entriesNumber, out TimeTester avlTimeTester);
            Tester(rbTree, entriesNumber, out TimeTester rbTimeTester);
            Tester(dicTree, entriesNumber, out TimeTester dicTimeTester);

            avlTimeTester.Print();
            Console.WriteLine();
            rbTimeTester.Print();
            Console.WriteLine();
            dicTimeTester.Print();
            Console.WriteLine();
            
        }

        /// <summary>
        /// Class - timer
        /// </summary>
        public class TimeTester
        {
            public TimeSpan InsertionTime
            {
                get;
                set;
            }

            public TimeSpan DeletionTime
            {
                get;
                set;
            }

            public TimeSpan SearchTime
            {
                get;
                set;
            }

            public TimeTester()
            {
                InsertionTime = default(TimeSpan);
                DeletionTime = default(TimeSpan);
                SearchTime = default(TimeSpan);
            }

            /// <summary>
            /// Print Dates
            /// </summary>
            public void Print()
            {
                Console.WriteLine("Insertion time - " + InsertionTime);
                Console.WriteLine("Deletion time - " + DeletionTime);
                Console.WriteLine("Search time - " + SearchTime);
            }
        }

        /// <summary>
        /// Function - tester
        /// </summary>
        /// <param name="item"></param>
        /// <param name="entriesNumber"></param>
        /// <param name="time"></param>
        public static void Tester(IDictionary<int, string> item, int entriesNumber, out TimeTester time)
        {
            time = new TimeTester();
            Random r = new Random();

            DateTime temp1 = default(DateTime);
            DateTime temp2 = default(DateTime);

            // determine addition time
            temp1 = DateTime.Now;
            item.Add(0, r.Next(1, entriesNumber).ToString() + " - item");
            temp2 = DateTime.Now;
            time.InsertionTime = temp2 - temp1;

            for (int i = 1; i < entriesNumber; i++)
            {
                item.Add(i, r.Next(1, entriesNumber).ToString() + " - item");
            }

            // determine deletion time
            temp1 = DateTime.Now;
            item.Remove(r.Next(0, entriesNumber));
            temp2 = DateTime.Now;
            time.DeletionTime = temp2 - temp1;

            // determine search time
            temp1 = DateTime.Now;
            item.ContainsKey(r.Next(0, entriesNumber));
            temp2 = DateTime.Now;
            time.SearchTime = temp2 - temp1;
        }
    }
}
