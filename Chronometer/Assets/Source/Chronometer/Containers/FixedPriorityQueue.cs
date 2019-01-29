using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

/* TODO:
 * Dequeue -    Get rid of recursion. Should increase performance by not fully swapping nodes on each iteration.
 *              Atleast get rid of the full swaps
 */

namespace Chronometer
{
    /// <summary>
    /// A fixed-size max-priority queue implementation using a binary heap.
    /// The node must extend PriorityQueueNode
    /// Manually changing the index or priority will cause undefined behaviour
    /// Node[0] is not used for less indexing overhead.
    /// </summary>
    /// <typeparam name="T">The node object type stored in the queue. Must extend PriorityQueueNode</typeparam>
    public class FixedPriorityQueue<T> : IEnumerable<T> where T : PriorityQueueNode
    {
        public const int DEFAULT_MAX_SIZE = 100;
        private int count;

        private T[] nodes;

        /// <summary>
        /// Empty constructor, instantiates a new priority queue with default capacity.
        /// </summary>
        public FixedPriorityQueue() : this(DEFAULT_MAX_SIZE) { }

        /// <summary>
        /// Constructor with capacity parameter, instantiates a new priority queue with given capacity.
        /// Exceeding the maximum amount will cause undefined behaviour.
        /// </summary>
        /// <param name="capacity">The maximum amount of nodes that can be stored at any given time.</param>
        public FixedPriorityQueue(int capacity)
        {
            count = 0;
            nodes = new T[capacity + 1];
        }

        /// <summary>
        /// Returns the number of nodes stored in the queue. O(1)
        /// </summary>
        public int Count
        {
            get { return count; }
        }

        /// <summary>
        /// Returns the maximum amount of nodes that can be stored in the queue at any given time. O(1)
        /// </summary>
        public int Capacity
        {
            get { return nodes.Length - 1; }
        }

        /// <summary>
        /// Enqueue an object to the queue. O(log n)
        /// Exceeding the capacity will cause undefind behaviour.
        /// </summary>
        /// <param name="node">The node to be added to the queue.</param>
        public void Enqueue(T node)
        {
            count++;
            node.Index = count;
            nodes[count] = node;

            heapifyUp(node);
        }

        /// <summary>
        /// Enqueue an object to the queue with the given priority O(log n)
        /// Exceeding the capacity will cause undefined behaviour.
        /// </summary>
        /// <param name="node">The node to be added to the queue.</param>
        /// <param name="priority">The priority of the given node</param>
        public void Enqueue(T node, float priority)
        {
            node.Priority = priority;
            count++;
            node.Index = count;
            nodes[count] = node;

            heapifyUp(node);
        }

        /// <summary>
        /// Removes the highest priority node from the queue and returns it. O(log n)
        /// An empty queue will cause undefined behaviour.
        /// </summary>
        /// <returns>The highest priority node in the queue.</returns>
        public T Dequeue()
        {
            T returnObj = nodes[1];

            // Last one in the queue, remove and return
            if(count == 1)
            {
                nodes[1] = null;
                count--;
                return returnObj;
            }

            // Swap with last node
            T lastNode = nodes[count];
            nodes[1] = lastNode;
            lastNode.Index = 1;
            nodes[count] = null;
            count--;

            // Fix heap
            heapifyDown(lastNode, 1);

            return returnObj;
        }

        /// <summary>
        /// Return the highest priority node stored in the queue without removing it. O(1)
        /// </summary>
        /// <returns>The highest priority node stored in the queue. null if queue is empty</returns>
        public T Peek()
        {
            return nodes[1];
        }

        /// <summary>
        /// Update node priority within the queue, O(log n)
        /// If the node is not in the queue, it will cause undefined behaviour.
        /// </summary>
        /// <param name="node">The node to update.</param>
        /// <param name="priority">The new priority</param>
        public void UpdatePriority(T node, float priority)
        {
            node.Priority = priority;
            heapifySingleNode(node);
        }

        /// <summary>
        /// Remove the given node from the queue. Does not have to be the highest priority. O(log n)
        /// If the node is not stored in the queue, it will cause undefined behaviour.
        /// </summary>
        /// <param name="node">The node to remove from the queue.</param>
        public void Remove(T node)
        {
            // Is it the last node? remove and done
            if(node.Index == count)
            {
                nodes[count] = null;
                count--;
                return;
            }

            // Swap with last node
            T lastNode = nodes[count];
            nodes[node.Index] = lastNode;
            lastNode.Index = node.Index;
            nodes[count] = null;
            count--;

            heapifySingleNode(lastNode);
        }

        /// <summary>
        /// Returns true if the queue has no object stored. O(1)
        /// </summary>
        /// <returns>True if the queue is empty.</returns>
        public bool IsEmpty()
        {
            if (count == 0)
                return true;

            return false;
        }

        /// <summary>
        /// Remove all objects from the queue. O(n)
        /// </summary>
        public void Clear()
        {
            Array.Clear(nodes, 1, count);
            count = 0;
        }

        /// <summary>
        /// Returns true if the given object is stored in the queue. O(n)
        /// </summary>
        /// <param name="obj">The object to be checked.</param>
        /// <returns>Returns true if the given object is stored in the queue.</returns>
        public bool Contains(T node)
        {
            if (node == nodes[node.Index])
                return true;

            return false;
        }

        public IEnumerator<T> GetEnumerator()
        {
            for(int i = 1; i <= count; i++)
            {
                yield return nodes[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private void heapifyUp(T node)
        {
            int parentIndex = 0;

            if (node.Index > 1)
            {
                parentIndex = node.Index >> 1; // i/2
                T parent = nodes[parentIndex];

                if (node.Priority < parent.Priority)
                    return;

                // Move parent down
                nodes[node.Index] = parent;
                parent.Index = node.Index;
                node.Index = parentIndex;
            }
            else
            {
                return;
            }

            while (parentIndex > 1)
            {
                parentIndex >>= 1; // i/2
                T parent = nodes[parentIndex];

                if (node.Priority < parent.Priority)
                    break;

                // Move parent down
                nodes[node.Index] = parent;
                parent.Index = node.Index;
                node.Index = parentIndex;
            }

            nodes[node.Index] = node;
        }

        private void heapifyDown(T node, int index)
        {
            int highestIndex = index;
            int leftChildIndex = index << 1;
            int rightChildIndex = leftChildIndex + 1;

            // There is a left child
            if (leftChildIndex <= count)
            {
                // Is the left childs priority higher than the current node
                if (nodes[leftChildIndex].Priority > node.Priority)
                {
                    highestIndex = leftChildIndex;
                }
            }

            // There is a right child
            if (rightChildIndex <= count)
            {
                // Is the right childs priority higher than the highest node(current node or left node)
                if (nodes[rightChildIndex].Priority > nodes[highestIndex].Priority)
                {
                    highestIndex = rightChildIndex;
                }
            }

            if (highestIndex != index)
            {
                // Swap node with highest priority child
                node.Index = highestIndex;
                nodes[index] = nodes[highestIndex];
                nodes[highestIndex].Index = index;
                nodes[highestIndex] = node;

                // Move down
                heapifyDown(node, highestIndex);
            }
        }

        private void heapifySingleNode(T node)
        {
            int parentIndex = node.Index >> 1;

            if (parentIndex > 0 && node.Priority > nodes[parentIndex].Priority)
            {
                heapifyUp(node);
            }
            else
            {
                heapifyDown(node, node.Index);
            }
        }
    }
}
