using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointFinder : Singleton<WaypointFinder>
{
    public Waypoint [] waypoints;

    private void Start() {

    }

    public List<Waypoint> FindPath(int from, int to)
    {
        List<Waypoint> path = new List<Waypoint>();
        EvaluateWaypoint(from, to, -1, ref path);
        path.Reverse();
        return path;
    }

    public int getWaypointIndex (Waypoint point) {
        return System.Array.IndexOf(waypoints, point);
    }

    private int EvaluateWaypoint (int from, int to, int parent,ref List<Waypoint> path) {
        if (from == to) {
            path.Add(waypoints[from]);
            return 1;
        }

        Waypoint current = waypoints[from];
        int totalNeighbors = current.Neighbors.Length;

        if (totalNeighbors > 0) {
            for (int i = 0; i < totalNeighbors; i++) {
                int fixedindex = System.Array.IndexOf(waypoints, current.Neighbors[i]);

                if (fixedindex != parent) {
                    if (EvaluateWaypoint(fixedindex, to, from, ref path) == 1) {
                        path.Add(waypoints[from]);
                        return 1;
                    }
                }
            }
        }

        return -1;
    }
}
