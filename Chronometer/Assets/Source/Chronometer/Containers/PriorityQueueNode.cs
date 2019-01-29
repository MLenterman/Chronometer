using UnityEngine;
using System.Collections;
using System;

namespace Chronometer
{
    /// <summary>
    /// Node implementation to be used as base class in PriorityQueue's
    /// Using the same node in multiple priority queue's will cause undefined behaviour.
    /// Changing the index or priority manually, while stored in a queue, will cause undefined behaviour.
    /// </summary>
    public class PriorityQueueNode
    {
        /// <summary>
        /// The priority to be used by the queue.
        /// </summary>
        public float Priority { get; protected internal set; }

        /// <summary>
        /// The index within the queue.
        /// </summary>
        public int Index { get; protected internal set; }

        /// <summary>
        /// Empty constructor, instantiates a new instance with default values.
        /// </summary>
        public PriorityQueueNode() : this(1, 0) { }

        /// <summary>
        /// Constructor, instantiates a new instance with the given values.
        /// </summary>
        /// <param name="index">The queue index.</param>
        /// <param name="priority">The priority to be used by the queue.</param>
        public PriorityQueueNode(int index, float priority)
        {
            Index = index;
            Priority = priority;
        }

        public override string ToString()
        {
            return base.ToString() + ": " + Index + ", " + Priority;
        }
    }
}
