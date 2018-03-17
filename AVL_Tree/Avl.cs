using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;

namespace AVL_Tree
{
    /// <summary>
    /// AVL tree implementation
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public partial class Avl<TKey, TValue> : IDictionary<TKey, TValue> where TKey : IComparable
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
                return Find(this.start, key).Value;
            }
            set
            {
                Find(this.start, key).Value = value;
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
                Runner(this.start, t);
                List<TKey> keyArr = new List<TKey>();
                for (int i = 0; i < t.Count; i++)
                {
                    keyArr.Add(t[i].Key);
                }
                return keyArr;
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
                Runner(this.start, t);
                List<TValue> valueArr = new List<TValue>();
                for (int i = 0; i < t.Count; i++)
                {
                    valueArr.Add(t[i].Value);
                }
                return valueArr;
            }
        }

        /// <summary>
        /// Return count of elements
        /// </summary>
        public int Count { get => this.count; }

        /// <summary>
        /// Return true, if tree is readonly, else false
        /// </summary>
        public bool IsReadOnly { get => false; }    

        /// <summary>
        /// Add new element
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Add(TKey key, TValue value)
        {
            // creat new element
            Node<TKey, TValue> newItem = new Node<TKey, TValue>(key, value);
            if (start == null)
            {
                this.start = newItem;
            }
            else
            {
                // call function for addition
                this.start = RecAdd(this.start, newItem);
            }
            this.count++;
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
            return ContainsKey(item.Key);
        }

        /// <summary>
        /// Return true, if key exists, else false
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool ContainsKey(TKey key)
        {
            if (Find(this.start, key) != null)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Copy elements to array
        /// </summary>
        /// <param name="array"></param>
        /// <param name="arrayIndex"></param>
        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            // check arrayIndex value
            if (arrayIndex >= 0 && array != null && array.Length - arrayIndex >= Count)
            {
                int index = arrayIndex;

                // get all elements
                List<Node<TKey, TValue>> nodeList = new List<Node<TKey, TValue>>();
                if (this.start != null)
                {
                    Runner(this.start, nodeList);
                }

                // fill array
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
            // if key exists delete element
            if (ContainsKey(key))
            {
                this.start = Delete(this.start, key);
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
            value = Find(this.start, key).Value;
            if (Find(this.start, key) == null)
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
