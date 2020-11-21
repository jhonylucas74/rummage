using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class PlayerScript : MonoBehaviour {

    public string Id;

    List<Waypoint> waypoints;
    Waypoint last;
    public float speed = 2.0f;
    bool _running = false;
    int energy = 4;

    public int moveFrom = 0;

    void Start() {
        transform.DOLocalRotate(new Vector3 (0, 0, 16f), 0.3f, RotateMode.Fast)
        .SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine)
        .SetId("playerMove")
        .Pause();

        transform.DOScaleY(1.05f, 0.3f)
        .SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);

        Events.OnPlayerMoveSelect += OnPlayerMoveSelect;
        waypoints = new List<Waypoint>();
        // waypoints = WaypointFinder.Instance.FindPath(from, to);
    }

    void OnDestroy () {
        Events.OnPlayerMoveSelect -= OnPlayerMoveSelect;
    }

    void Update() {
        if (waypoints.Count > 0) {
            float step =  speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, waypoints[0].transform.position, step);

            if (Vector3.Distance(transform.position, waypoints[0].transform.position) < 0.001f) {
                if (waypoints.Count == 1 || energy == 1) {
                    moveFrom = WaypointFinder.Instance.getWaypointIndex(waypoints[0]);
                }

                last = waypoints[0];
                waypoints.RemoveAt(0);
                energy -= 1;
            }
        }

        if (_running == false && waypoints.Count > 0) {
            _running = true;
            energy = 4;
            transform.Rotate(0, 0, -16f, Space.Self);
            DOTween.Play("playerMove");
        }

        if (_running && waypoints.Count == 0 || _running && energy == 0) {
            _running = false;
            waypoints.Clear();
            DOTween.Pause("playerMove");
            transform.localRotation = Quaternion.identity;

            Events.OnPlayerMoveEnd?.Invoke(last.id > 0 && last.id < 9);
        }
    }

    void OnPlayerMoveSelect (string id, int to) 
    {
        if (Id != id)
            return;

        waypoints = WaypointFinder.Instance.FindPath(moveFrom, to);
    }
}
