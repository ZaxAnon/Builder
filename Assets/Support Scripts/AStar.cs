using UnityEngine;
using System.Collections.Generic;
using Priority_Queue;

public class AStar : MonoBehaviour {
    // A-star pathfinding algorithm.
    // IntPoints are IntPoints, arcs are a property of the IntPoint
    // currently at line 5/18
    /*
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
    }*/
    public static List<IntPoint> pathfind (IntPoint start, IntPoint goal){

        long time = (long)(System.DateTime.UtcNow - new System.DateTime(1970, 1, 1)).TotalMilliseconds;
	    LinkedList<IntPoint> tovisit = new LinkedList<IntPoint>();
	    List<IntPoint> visited = new List<IntPoint>();
		tovisit.AddFirst(start);


		while(true){
			if (tovisit.Count<1){
				return null;
			}
            long tmp = (long)(System.DateTime.UtcNow - new System.DateTime(1970, 1, 1)).TotalMilliseconds;
            Debug.Log("elapsed: "+ (tmp - time));

            if (tmp-time > 3000)
            {
                Debug.Log("Time out!");
                return new List<IntPoint>();
            }
            Debug.Log("Looping!");
			IntPoint current = getNext(tovisit);

			
			
			List<IntPoint> successors = current.neighbors();
			foreach(IntPoint successor in successors){
                Debug.Log("elapsed: " + (tmp - time));
                Debug.Log("Successors: " + successors.Count);
                if (tmp - time > 3000)
                {
                    Debug.Log("Time out!");
                    return new List<IntPoint>();
                }
				if (successor==goal){
					successor.parent=current;
                    current = successor;
                    Debug.Log("Path found!");
                    return reconstruct(current);

				}

				if (visited.Contains(successor)){
					float temp_g = current.g + successor.cost();
					float temp_f = temp_g + heuristic(successor, goal);
					if (temp_f < successor.f){
						successor.g = temp_g;
						successor.h = heuristic(successor, goal);
						successor.f = temp_f;
						successor.parent=current;
					}
					continue;
				}
				if (tovisit.Contains(successor)){
					float temp_g = current.g + successor.cost();
					float temp_f = temp_g + heuristic(successor, goal);
					if (temp_f < successor.f){
						successor.g = temp_g;
						successor.h = heuristic(successor, goal);
						successor.f = temp_f;
						successor.parent=current;
					}
					continue;
				}
				successor.g = current.g + successor.cost();
				successor.h = heuristic(successor, goal);
				successor.f = successor.g + successor.h;
				successor.parent=current;
				visited.Add(current);
				tovisit.AddFirst(successor);
				
			}			
		}
	}
	private static IntPoint getNext(LinkedList<IntPoint> list){
		float min = float.MaxValue;
		IntPoint ret=null;;
		foreach (IntPoint IntPoint in list){
			if (IntPoint.f < min){
				min=IntPoint.f;
				ret = IntPoint;
			}
		}
        list.Remove(ret);
		return ret;
	}
    
    private static float heuristic(IntPoint pos, IntPoint goal)
    {
        return Mathf.Abs(pos.x - goal.x) + Mathf.Abs(pos.z + goal.z);
    }
    public static List<IntPoint> reconstruct(IntPoint end){
		List<IntPoint> succession = new List<IntPoint>();
		IntPoint current=end;
		while(current != null){
			succession.Add(current);
			current=current.parent;
		}
        succession.Reverse();
		return succession;
	}
}


public class IntPoint
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
        int[,] q = new int[4, 2] { { 1, 0 }, { 0, 1 }, { -1, 0 }, { 0, -1 } };
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

    public static bool operator==(IntPoint o1, IntPoint o2)
    {
        // If parameter is null return false.
        object oo1 = (object)o1;
        object oo2 = (object)o2;
        if (oo1 == null && oo2 == null)
        {
            return true;
        }

        // If parameter cannot be cast to Point return false.
        if (oo1 == null || oo2 == null)
        {
            return false;
        }

        // Return true if the fields match:
        return (o1.x == o2.x) && (o1.z == o2.z);
    }
    public static bool operator !=(IntPoint o1, IntPoint o2)
    {
        return !(o1 == o2);
    }

    public float cost()
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