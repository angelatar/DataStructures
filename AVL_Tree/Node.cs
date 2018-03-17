using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVL_Tree
{
    internal class Node<TKey, TValue> where TKey : IComparable
    {
        /// <summary>
        /// Left side
        /// </summary>
        internal Node<TKey, TValue> Left;

        /// <summary>
        /// Right side
        /// </summary>
        internal Node<TKey, TValue> Right;

        /// <summary>
        /// Key
        /// </summary>
        internal TKey Key
        {
            get;
            set;
        }

        /// <summary>
        /// Data
        /// </summary>
        internal TValue Value
        {
            get;
            set;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="k"></param>
        /// <param name="v"></param>
        internal Node(TKey k, TValue v)
        {
            Left = null;
            Right = null;
            Key = k;
            Value = v;
        }
    }
}
