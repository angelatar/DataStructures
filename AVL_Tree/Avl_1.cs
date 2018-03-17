using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVL_Tree
{
    /// <summary>
    /// Private side of AVL tree
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public partial class Avl<TKey, TValue>
    {
        /// <summary>
        /// Tree's start
        /// </summary>
        private Node<TKey, TValue> start;

        /// <summary>
        /// Count of nodes
        /// </summary>
        private int count;

        /// <summary>
        /// Constuctor
        /// </summary>
        public Avl()
        {
            this.start = null;
            this.count = 0;
        }

        /// <summary>
        /// Print tree in console
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
                Console.WriteLine(item.Key + "-" + item.Value);
                PrintHelper(item.Right);
            }
        }

        /// <summary>
        /// Recursive addition
        /// </summary>
        /// <param name="temp"></param>
        /// <param name="newItem"></param>
        /// <returns></returns>
        private Node<TKey, TValue> RecAdd(Node<TKey, TValue> temp, Node<TKey, TValue> newItem)
        {
            // if temp is empty add elemnt and return
            if (temp == null)
            {
                temp = newItem;
                return temp;
            }

            // else 
            // recursive call function and find empty node and at the same time balance tree

            TKey key = newItem.Key;
            if (key.CompareTo(temp.Key) < 0)
            {
                temp.Left = RecAdd(temp.Left, newItem);
                temp = Balance(temp);
            }
            else
            {
                key = newItem.Key;
                if (key.CompareTo(temp.Key) > 0)
                {
                    temp.Right = RecAdd(temp.Right, newItem);
                    temp = Balance(temp);
                }
            }
            return temp;
        }

        /// <summary>
        /// Balance the tree
        /// </summary>
        /// <param name="temp"></param>
        /// <returns></returns>
        private Node<TKey, TValue> Balance(Node<TKey, TValue> temp)
        {
            int bal = Balancer(temp);
            if (bal > 1)
            {
                // depends on difference of txe sides rotate tree
                temp = ((Balancer(temp) <= 0) ? RotateLR(temp) : RotateLL(temp));
            }
            else if (bal < -1)
            {
                // depends on difference of txe sides rotate tree
                temp = ((Balancer(temp.Right) <= 0) ? RotateRR(temp) : RotateRL(temp));
            }
            return temp;
        }

        /// <summary>
        /// Return difference of two sides
        /// </summary>
        /// <param name="temp"></param>
        /// <returns></returns>
        private int Balancer(Node<TKey, TValue> temp)
        {
            return Height(temp.Left) - Height(temp.Right);
        }

        /// <summary>
        /// Return height 
        /// </summary>
        /// <param name="temp"></param>
        /// <returns></returns>
        private int Height(Node<TKey, TValue> temp)
        {
            int height = 0;
            if (temp != null)
            {
                int left = Height(temp.Left);
                int right = Height(temp.Right);
                height = ((left > right) ? left : right) + 1;
            }
            return height;
        }

        /// <summary>
        /// Delete element
        /// </summary>
        /// <param name="temp"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        private Node<TKey, TValue> Delete(Node<TKey, TValue> temp, TKey key)
        {
            Node<TKey, TValue> parent;
            // if temp is empty, return null
            if (temp == null)
            {
                return null;
            }
            // else
            else
            {
                // left side
                if (key.CompareTo(temp.Key) < 0)
                {
                    temp.Left = Delete(temp.Left, key);
                    if (Balancer(temp) == -2)
                    {
                        temp = ((Balancer(temp.Right) > 0) ? RotateRL(temp) : RotateRR(temp));
                    }
                }

                // right side
                else if (key.CompareTo(temp.Key) > 0)
                {
                    temp.Right = Delete(temp.Right, key);
                    if (Balancer(temp) == 2)
                    {
                        temp = ((Balancer(temp.Left) > 0) ? RotateLR(temp) : RotateLL(temp));
                    }
                }

                // key is found
                else
                {
                    if (temp.Right != null)
                    {
                        parent = temp.Right;
                        while (parent.Left != null)
                        {
                            parent = parent.Left;
                        }
                        temp.Value = parent.Value;
                        temp.Key = parent.Key;
                        temp.Right = Delete(temp.Right, parent.Key);
                        if (Balancer(temp) == 2)
                        {
                            temp = ((Balancer(temp.Left) < 0) ? RotateLR(temp) : RotateLL(temp));
                        }
                    }
                    else
                    {
                        return temp.Left;
                    }
                }
            }
            return temp;
        }
        
        /// <summary>
        /// Find element by key
        /// </summary>
        /// <param name="temp"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        private Node<TKey, TValue> Find(Node<TKey, TValue> temp, TKey key)
        {
            while (temp != null)
            {
                if (key.CompareTo(temp.Key) == 0)
                {
                    return temp;
                }
                temp = ((key.CompareTo(temp.Key) >= 0) ? temp.Right : temp.Left);
            }
            return null;
        }

        /// <summary>
        /// Fill list
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

        /// <summary>
        /// Right - right rotate
        /// </summary>
        /// <param name="rotItem"></param>
        /// <returns></returns>
        private Node<TKey, TValue> RotateRR(Node<TKey, TValue> rotItem)
        {
            Node<TKey, TValue> temp = rotItem.Right;
            rotItem.Right = temp.Left;
            temp.Left = rotItem;
            return temp;
        }

        /// <summary>
        /// Left - left rotate
        /// </summary>
        /// <param name="rotItem"></param>
        /// <returns></returns>
        private Node<TKey, TValue> RotateLL(Node<TKey, TValue> rotItem)
        {
            Node<TKey, TValue> temp = rotItem.Left;
            rotItem.Left = temp.Right;
            temp.Right = rotItem;
            return temp;
        }

        /// <summary>
        /// Right - Left rotate
        /// </summary>
        /// <param name="rotItem"></param>
        /// <returns></returns>
        private Node<TKey, TValue> RotateRL(Node<TKey, TValue> rotItem)
        {
            Node<TKey, TValue> temp = rotItem.Right;
            rotItem.Right = RotateLL(temp);
            return RotateRR(rotItem);
        }

        /// <summary>
        /// Left - Right rotate
        /// </summary>
        /// <param name="rotItem"></param>
        /// <returns></returns>
        private Node<TKey, TValue> RotateLR(Node<TKey, TValue> rotItem)
        {
            Node<TKey, TValue> temp = rotItem.Left;
            rotItem.Left = RotateRR(temp);
            return RotateLL(rotItem);
        }

    }
}
