using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Red_Black_Tree
{
    public partial class RedBlack<TKey, TValue>
    {
        /// <summary>
        /// Count of elements
        /// </summary>
        private int count;

        /// <summary>
        /// Trees's start
        /// </summary>
        private Node<TKey, TValue> start;
        
        /// <summary>
        /// Constructor
        /// </summary>
        public RedBlack()
        {
            this.start = null;
            this.count = 0;
        }

        /// <summary>
        /// Left rotation
        /// </summary>
        /// <param name="rotItem"></param>
        private void LRotate(Node<TKey, TValue> rotItem)
        {
            Node<TKey, TValue> temp = rotItem.Right;
            rotItem.Right = temp.Left;
            if (temp.Left != null)
            {
                temp.Left.Parent = rotItem;
            }
            if (temp != null)
            {
                temp.Parent = rotItem.Parent;
            }
            if (rotItem.Parent == null)
            {
                this.start = temp;
            }
            else if (rotItem == rotItem.Parent.Left)
            {
                rotItem.Parent.Left = temp;
            }
            else
            {
                rotItem.Parent.Right = temp;
            }
            temp.Left = rotItem;
            if (rotItem != null)
            {
                rotItem.Parent = temp;
            }
        }

        /// <summary>
        /// Right rotation
        /// </summary>
        /// <param name="rotItem"></param>
        private void RRotate(Node<TKey, TValue> rotItem)
        {
            Node<TKey, TValue> temp = rotItem.Left;
            rotItem.Left = temp.Right;
            if (temp.Right != null)
            {
                temp.Right.Parent = rotItem;
            }
            if (temp != null)
            {
                temp.Parent = rotItem.Parent;
            }
            if (rotItem.Parent == null)
            {
                this.start = temp;
            }
            if (rotItem == rotItem.Parent.Right)
            {
                rotItem.Parent.Right = temp;
            }
            if (rotItem == rotItem.Parent.Left)
            {
                rotItem.Parent.Left = temp;
            }
            temp.Right = rotItem;
            if (rotItem != null)
            {
                rotItem.Parent = temp;
            }
        }
        
        /// <summary>
        /// Display tree
        /// </summary>
        public void Print()
        {
            if (this.start == null)
            {
                Console.WriteLine("No items!!");
            }
            else
            {
                PrintHelper(this.start);
            }
        }

        /// <summary>
        /// Recursive printer
        /// </summary>
        /// <param name="item"></param>
        private void PrintHelper(Node<TKey, TValue> item)
        {
            if (item != null)
            {
                PrintHelper(item.Left);
                Console.WriteLine(item.Value + "-" + item.Color);
                PrintHelper(item.Right);
            }
        }

        /// <summary>
        /// Balance tree after addition
        /// </summary>
        /// <param name="addedItem"></param>
        private void AddBalance(Node<TKey, TValue> addedItem)
        {
            // check elements
            while (addedItem != start && addedItem.Parent.Color == Color.Red)
            {
                if (addedItem.Parent == addedItem.Parent.Parent.Left)
                {
                    Node<TKey, TValue> temp = addedItem.Parent.Parent.Right;
                    if (temp != null && temp.Color == Color.Red) // 1. uncle is red
                    {
                        addedItem.Parent.Color = Color.Black;
                        temp.Color = Color.Black;
                        addedItem.Parent.Parent.Color = Color.Red;
                        addedItem = addedItem.Parent.Parent;
                    }
                    else // 2. uncle is black
                    {
                        if (addedItem == addedItem.Parent.Right)
                        {
                            addedItem = addedItem.Parent;
                            LRotate(addedItem);
                        }
                        // change color and rotate
                        addedItem.Parent.Color = Color.Black;
                        addedItem.Parent.Parent.Color = Color.Red;
                        RRotate(addedItem.Parent.Parent);
                    }
                }
                else
                {
                    // mirror code
                    Node<TKey, TValue> temp = addedItem.Parent.Parent.Left;
                    if (temp != null && temp.Color == Color.Red) //1.
                    {
                        addedItem.Parent.Color = Color.Black;
                        temp.Color = Color.Black;
                        addedItem.Parent.Parent.Color = Color.Red;
                        addedItem = addedItem.Parent.Parent;
                    }
                    else // 2.
                    {
                        if (addedItem == addedItem.Parent.Left)
                        {
                            addedItem = addedItem.Parent;
                            RRotate(addedItem);
                        }
                        // 3.
                        addedItem.Parent.Color = Color.Black;
                        addedItem.Parent.Parent.Color = Color.Red;
                        LRotate(addedItem.Parent.Parent);
                    }
                }
            }
            this.start.Color = Color.Black;
        }

        /// <summary>
        /// Balance tree after deletion
        /// </summary>
        /// <param name="linkedItem"></param>
        private void RemoveBalance(Node<TKey, TValue> linkedItem)
        {
            while (linkedItem != null && linkedItem != start && linkedItem.Color == Color.Black)
            {
                if (linkedItem == linkedItem.Parent.Left)
                {
                    Node<TKey, TValue> temp = linkedItem.Parent.Right;
                    if (temp.Color == Color.Red)
                    {
                        linkedItem.Parent.Color = Color.Red;
                        temp.Color = Color.Black;
                        LRotate(linkedItem.Parent);
                        temp = linkedItem.Parent.Right;
                    }
                    if (temp.Left.Color == Color.Black && temp.Right.Color == Color.Black)
                    {
                        temp.Color = Color.Red;
                        linkedItem = linkedItem.Parent;
                    }
                    else
                    {
                        if (temp.Right.Color == Color.Black)
                        {
                            temp.Left.Color = Color.Black;
                            temp.Color = Color.Red;
                            RRotate(temp);
                            temp = linkedItem.Parent.Right;
                        }
                        linkedItem.Parent.Color = Color.Black;
                        temp.Color = linkedItem.Parent.Color;
                        temp.Right.Color = Color.Black;
                        LRotate(linkedItem.Parent);
                        linkedItem = this.start;
                    }
                }
                else // mirror code
                {
                    Node<TKey, TValue> temp = linkedItem.Parent.Left;
                    if (temp.Color == Color.Red)
                    {
                        linkedItem.Parent.Color = Color.Red;
                        temp.Color = Color.Black;
                        RRotate(linkedItem.Parent);
                        temp = linkedItem.Parent.Left;
                    }
                    if (temp.Right.Color == Color.Black && temp.Left.Color == Color.Black)
                    {
                        temp.Color = Color.Red;
                        linkedItem = linkedItem.Parent;
                    }
                    else
                    {
                        if (temp.Left.Color == Color.Black)
                        {
                            temp.Right.Color = Color.Black;
                            temp.Color = Color.Red;
                            LRotate(temp);
                            temp = linkedItem.Parent.Left;
                        }
                        temp.Color = linkedItem.Parent.Color;
                        linkedItem.Parent.Color = Color.Black;
                        temp.Left.Color = Color.Black;
                        RRotate(linkedItem.Parent);
                        linkedItem = this.start;
                    }
                }
            }
            if (linkedItem != null)
            {
                linkedItem.Color = Color.Black;
            }
        }

        /// <summary>
        /// Find element by key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private Node<TKey, TValue> Find(TKey key)
        {
            for (Node<TKey, TValue> temp = start; temp != null; temp = ((key.CompareTo(temp.Key) >= 0) ? temp.Right : temp.Left))
            {
                if (key.CompareTo(temp.Key) == 0)
                {
                    return temp;
                }
            }
            return null;
        }

        /// <summary>
        /// Run and fill list
        /// </summary>
        /// <param name="st"></param>
        /// <param name="nodeList"></param>
        private void Runner(Node<TKey, TValue> st, List<Node<TKey, TValue>> nodeList)
        {
            if (st.Right != null)
            {
                Runner(st.Right, nodeList);
            }
            nodeList.Add(st);
            if (st.Left != null)
            {
                Runner(st.Left, nodeList);
            }
        }
    }
}
