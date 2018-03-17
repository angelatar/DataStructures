using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;

namespace Red_Black_Tree
{
    public partial class RedBlack<TKey, TValue> : IDictionary<TKey, TValue> where TKey:IComparable
    {
        /// <summary>
        /// Indexator
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public TValue this[TKey key]
        {
            get
            {
                return Find(key).Value;
            }
            set
            {
                Find(key).Value = value;
            }
        }

        /// <summary>
        /// Return collection of keys
        /// </summary>
        public ICollection<TKey> Keys
        {
            get
            {
                List<Node<TKey, TValue>> t = new List<Node<TKey, TValue>>();
                Runner(start, t);
                List<TKey> keyarr = new List<TKey>();
                for (int i = 0; i < t.Count; i++)
                {
                    keyarr.Add(t[i].Key);
                }
                return keyarr;
            }
        }

        /// <summary>
        /// Return collection of values
        /// </summary>
        public ICollection<TValue> Values
        {
            get
            {
                List<Node<TKey, TValue>> t = new List<Node<TKey, TValue>>();
                Runner(start, t);
                List<TValue> valuearr = new List<TValue>();
                for (int i = 0; i < t.Count; i++)
                {
                    valuearr.Add(t[i].Value);
                }
                return valuearr;
            }
        }

        /// <summary>
        /// Return count of elements
        /// </summary>
        public int Count { get => count; }

        /// <summary>
        /// Return true, if tree is readonly, else false
        /// </summary>
        public bool IsReadOnly { get => false; }

        public void Add(TKey key, TValue value)
        { 
            Node<TKey, TValue> newItem = new Node<TKey, TValue>(key, value);
            // if start is null, assignment newItem to start
            if (this.start == null)
            {
                start = newItem;
                start.Color = Color.Black;
                count++;
            }
            else
            {

                {/*TKey key2;
                for (Node<TKey, TValue> temp = start; temp != null; temp = ((key2.CompareTo(temp.Key) <= 0) ? temp.Left : temp.Right))
                {
                    newItem.Parent = temp;
                    key2 = newItem.Key;
                    if (key2.CompareTo(temp.Key) == 0)
                    {
                        throw new Exception("Key already exists!");
                    }
                    key2 = newItem.Key;
                }
                if (newItem.Parent != null)
                {
                    key2 = newItem.Key;
                    if (key2.CompareTo(newItem.Parent.Key) > 0)
                    {
                        newItem.Parent.Right = newItem;
                    }
                    else
                    {
                        newItem.Parent.Left = newItem;
                    }
                }*/
            }

                Node<TKey,TValue> temp2 = null;
                Node<TKey, TValue> temp1 = this.start;
                while (temp1 != null)
                {
                    temp2 = temp1;
                    if (newItem.Key.CompareTo(temp1.Key) < 0) 
                    {
                        temp1 = temp1.Left;
                    }
                    else
                    {
                        temp1 = temp1.Right;
                    }
                }
                newItem.Parent = temp2;
                if (temp2 == null)
                {
                    this.start = newItem;
                }
                else if (newItem.Key.CompareTo(temp2.Key) < 0)
                {
                    temp2.Left = newItem;
                }
                else
                {
                    temp2.Right = newItem;
                }

                newItem.Left = null;
                newItem.Right = null;
                newItem.Color = Color.Red;
                AddBalance(newItem);
                this.count++;
            }
        }

        /// <summary>
        /// Add new element
        /// </summary>
        /// <param name="item"></param>
        public void Add(KeyValuePair<TKey, TValue> item)
        {
            Add(item.Key, item.Value);
        }

        /// <summary>
        /// Clear tree
        /// </summary>
        public void Clear()
        {
            this.start = null;
            this.count = 0;
        }

        /// <summary>
        /// Return true, if element exists, else false
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            if (Find(item.Key) != null)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Return true, if key exists, else false
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool ContainsKey(TKey key)
        {
            Node<TKey, TValue> temp = Find(key);
            if (temp == null)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Copy elements to array
        /// </summary>
        /// <param name="array"></param>
        /// <param name="arrayIndex"></param>
        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            if (arrayIndex >= 0 && array != null && array.Length - arrayIndex >= Count)
            {
                int index = arrayIndex;
                List<Node<TKey, TValue>> nodeList = new List<Node<TKey, TValue>>();
                if (this.start != null)
                {
                    Runner(this.start, nodeList);
                }
                foreach (Node<TKey, TValue> item in nodeList)
                {
                    array.SetValue(new KeyValuePair<TKey, TValue>(item.Key, item.Value), index);
                    index++;
                }
            }
        }

        /// <summary>
        /// Remove element
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool Remove(TKey key)
        {
            // if element exists, start deletion
            Node<TKey, TValue> deleteItem = Find(key);
            if (deleteItem != null)
            {
                Node<TKey, TValue> temp;
                // if it has no child or one child
                if (deleteItem.Left == null || deleteItem.Right == null)
                {
                    temp = deleteItem;
                }
                else
                {
                    // if it has 2 children
                    temp = deleteItem.Right;
                    while (temp.Left != null)
                    {
                        temp = temp.Left;
                    }
                }

                // copy
                Node<TKey, TValue> linkedItem = (temp.Left == null) ? temp.Right : temp.Left;

                if (temp.Parent != null)
                {
                    if (temp == temp.Parent.Left)
                    {
                        temp.Parent.Left = linkedItem;
                    }
                    else
                    {
                        temp.Parent.Right = linkedItem;
                    }
                }
                else
                {
                    this.start = linkedItem;
                }
                if (temp != deleteItem)
                {
                    deleteItem.Key = temp.Key;
                    deleteItem.Value = temp.Value;
                }
                if (temp.Color == Color.Black)
                {
                    RemoveBalance(linkedItem);
                }
                this.count--;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Remove element
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            return Remove(item.Key);
        }

        /// <summary>
        /// If key exists return true and value, else return false
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool TryGetValue(TKey key, out TValue value)
        {
            value = Find(key).Value;
            if (Find(key) == null)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection
        /// </summary>
        /// <returns></returns>
        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            List<Node<TKey, TValue>> nodeList = new List<Node<TKey, TValue>>();
            Runner(this.start, nodeList);
            return nodeList.Select(i => new KeyValuePair<TKey, TValue>(i.Key, i.Value)).GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            List<Node<TKey, TValue>> nodeList = new List<Node<TKey, TValue>>();
            Runner(this.start, nodeList);
            return nodeList.GetEnumerator();
        }
    }

}
