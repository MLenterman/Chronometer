using System.Collections.Generic;
using UnityEngine;

/*
 * TODO:
 * Decouple     -   Decouple from Unity and remove the need for an interface.
 * 
 *              -   Decouple it from the object by using and ID or hash?
 *              
 * Implement    -   Retrieve objects by circle bounds.
 * 
 *              -   Batch inserting.
 *              
 * Test         -   Performance and error.          
 */

namespace Chronometer
{
    public interface IQuadTreeObject
    {
        Vector2 GetPosition();
    }

    public class QuadTree<T> where T : IQuadTreeObject
    {
        private int maxTreeSize;
        private List<T> objList;
        private Rect bounds;
        private QuadTree<T>[] subQuads;
        private bool isSubdivided;

        public QuadTree(int maxSize, Rect bounds)
        {
            this.bounds = bounds;
            maxTreeSize = maxSize;
            subQuads = new QuadTree<T>[4];
            objList = new List<T>(maxSize);
            isSubdivided = false;
        }

        public void Insert(T obj)
        {
            if (isSubdivided)
            {
                insertToChild(obj);
                return;
            }

            objList.Add(obj);

            if (objList.Count > maxTreeSize)
            {
                subdivide();

                foreach (T element in objList)
                    insertToChild(element);

                objList.Clear();
            }
        }

        private void insertToChild(T obj)
        {
            int cellIndex = calcCellForPos(obj.GetPosition());

            if (cellIndex != -1)
                subQuads[cellIndex].Insert(obj);
        }

        private void subdivide()
        {
            float subWidth = (bounds.width / 2f);
            float subHeight = (bounds.height / 2f);
            float x = bounds.x;
            float y = bounds.y;
            
            subQuads[0] = new QuadTree<T>(maxTreeSize, new Rect(x + subWidth, y, subWidth, subHeight));
            subQuads[1] = new QuadTree<T>(maxTreeSize, new Rect(x, y, subWidth, subHeight));
            subQuads[2] = new QuadTree<T>(maxTreeSize, new Rect(x, y + subHeight, subWidth, subHeight));
            subQuads[3] = new QuadTree<T>(maxTreeSize, new Rect(x + subWidth, y + subHeight, subWidth, subHeight));

            isSubdivided = true;
        }

        public void Remove(T objectToRemove)
        {
            if (ContainsPosition(objectToRemove.GetPosition()))
            {
                objList.Remove(objectToRemove);
                if (subQuads[0] != null)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        subQuads[i].Remove(objectToRemove);
                    }
                }
            }
        }
        public List<T> RetrieveObjectsInArea(Rect area)
        {
            if (rectOverlap(bounds, area))
            {
                List<T> returnedObjects = new List<T>();
                for (int i = 0; i < objList.Count; i++)
                {
                    if (area.Contains(objList[i].GetPosition()))
                    {
                        returnedObjects.Add(objList[i]);
                    }
                }
                if (subQuads[0] != null)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        List<T> cellObjects = subQuads[i].RetrieveObjectsInArea(area);
                        if (cellObjects != null)
                        {
                            returnedObjects.AddRange(cellObjects);
                        }
                    }
                }
                return returnedObjects;
            }
            return null;
        }

        public void Clear()
        {
            objList.Clear();

            if (isSubdivided)
            {
                for (int i = 0; i < subQuads.Length; i++)
                {
                        subQuads[i].Clear();
                        subQuads[i] = null;
                }
            }

            isSubdivided = false;
        }

        public bool ContainsPosition(Vector2 pos)
        {
            return bounds.Contains(pos);
        }

        private int calcCellForPos(Vector2 pos)
        {
            for (int i = 0; i < 4; i++)
            {
                if (subQuads[i].ContainsPosition(pos))
                    return i;
            }

            Debug.LogError("Position " + pos + " out of bound from " + bounds);
            return -1;
        }

        private bool valueInRange(float value, float min, float max)
        {
            return (value >= min) && (value <= max);
        }

        private bool rectOverlap(Rect A, Rect B)
        {
            bool xOverlap = valueInRange(A.x, B.x, B.x + B.width) ||
                            valueInRange(B.x, A.x, A.x + A.width);

            bool yOverlap = valueInRange(A.y, B.y, B.y + B.height) ||
                            valueInRange(B.y, A.y, A.y + A.height);

            return xOverlap && yOverlap;
        }

        public void DrawDebug()
        {
            Gizmos.DrawLine(new Vector3(bounds.x, 0f, bounds.y), new Vector3(bounds.x, 0f, bounds.y + bounds.height));
            Gizmos.DrawLine(new Vector3(bounds.x, 0f, bounds.y), new Vector3(bounds.x + bounds.width, 0f, bounds.y));
            Gizmos.DrawLine(new Vector3(bounds.x + bounds.width, 0f, bounds.y), new Vector3(bounds.x + bounds.width, 0f, bounds.y + bounds.height));
            Gizmos.DrawLine(new Vector3(bounds.x, 0f, bounds.y + bounds.height), new Vector3(bounds.x + bounds.width, 0f, bounds.y + bounds.height));

            if (isSubdivided)
            {
                foreach (QuadTree<T> element in subQuads)
                    element.DrawDebug();
            }
        }
    }
}
