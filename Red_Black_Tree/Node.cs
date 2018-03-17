using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Red_Black_Tree
{
    /// <summary>
    /// Tree's node's colors
    /// </summary>
    internal enum Color
    {
        Black,
        Red
    }

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
        /// Parent
        /// </summary>
        internal Node<TKey, TValue> Parent;

        /// <summary>
        /// Node's color
        /// </summary>
        internal Color Color
        {
            get;
            set;
        }

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
        public Node(TKey k,TValue v)
        {
            Left = null;
            Right = null;
            Parent = null;
            Key = k;
            Value = v;
            Color = Color.Black;
        }
    }
}
