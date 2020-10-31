using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointFinder : Singleton<MonoBehaviour>
{
    [SerializeField] Transform _waypointContainer;
    Transform[] _waypoints;


    private void Start()
    {
        _waypoints = new Transform[_waypointContainer.childCount];

        for (int i = 0; i < _waypointContainer.childCount; i++)
        {
            _waypoints[i].name = i.ToString();
            _waypoints[i] = _waypointContainer.GetChild(i);
        }
    }

    public List<Waypoint> FindPath(Waypoint fromWaypoint, Waypoint toWaypoint)
    {
        List<Waypoint> path = new List<Waypoint>();

        for(int i = 0; i < fromWaypoint.Neighbors.Length; i++)
        {
            if(fromWaypoint.Neighbors[i] == toWaypoint)
            { }
        }



        return path;
    }
}
