using UnityEngine;
using System.Collections.Generic;
using Priority_Queue;

public class AStar : MonoBehaviour {
    // A-star pathfinding algorithm.
    // Nodes are IntPoints, arcs are a property of the node
    // currently at line 5/18
    public static List<IntPoint> run(IntPoint start, IntPoint goal)
    {

        
        
        HeapPriorityQueue<IntPoint> open = new HeapPriorityQueue<IntPoint>(4000);
        HashSet<IntPoint> closed = new HashSet<IntPoint>();
        open.Enqueue(start, 0);
        

        while (open.Count > 0)
        {
            var current = open.Dequeue();
            foreach (IntPoint successor in current.neighbors()){
                // Successor checking & heuristic creation
                if (successor == goal)
                {
                    return successor.path();
                }
                successor.g = current.g + 1;
                successor.h = heuristic(successor, goal);
                successor.f = successor.g + successor.h;

                // skip conditions
                if (open.Contains(successor))
                {
                    continue;
                }
                if (closed.Contains(successor))
                {
                    // add 
                    continue;
                }
                open.Enqueue(successor, successor.f);
            }
          
        }


        return null; //TOREMOVE
    }
    private static float heuristic(IntPoint pos, IntPoint goal)
    {
        return Mathf.Abs(pos.x - goal.x) + Mathf.Abs(pos.z + goal.z);
    }

        

}


public class IntPoint : PriorityQueueNode
{
    public IntPoint parent;
    public int x, z;
    public float f, g, h;

    public IntPoint(int x, int z)
    {
        this.x = x;
        this.z = z;
    }
    public IntPoint(int x, int z, IntPoint parent)
    {
        this.x = x;
        this.z = z;
        this.parent = parent;
    }
    public List<IntPoint> neighbors()
    {
        List<IntPoint> ret = new List<IntPoint>();

        ret.Add(new IntPoint(x + 1, z, this));
        ret.Add(new IntPoint(x - 1, z, this));
        ret.Add(new IntPoint(x, z + 1, this));
        ret.Add(new IntPoint(x, z - 1, this));
        return ret;
    }
    public List<IntPoint> path()
    {
        List<IntPoint> ret = new List<IntPoint>();
        var current = this;
        while (current != null)
        {
            ret.Add(current);
            current = parent;
        }
        return ret;
       
    }

    public override bool Equals(System.Object obj)
    {
        // If parameter is null return false.
        if (obj == null)
        {
            return false;
        }

        // If parameter cannot be cast to Point return false.
        IntPoint p = obj as IntPoint;
        if ((System.Object)p == null)
        {
            return false;
        }

        // Return true if the fields match:
        return (x == p.x) && (z == p.z);
    }

    public double cost()
    {
        return 1f;
    }
    public bool Equals(IntPoint p)
    {
        // If parameter is null return false:
        if ((object)p == null)
        {
            return false;
        }

        // Return true if the fields match:
        return (x == p.x) && (z == p.z);
    }

    public override int GetHashCode()
    {
        return x ^ z;
    }
    
}