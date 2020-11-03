using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class PlayerMovement : MonoBehaviour {
    List<Waypoint> waypoints;
    public float speed = 1.0f;
    bool _running = false;

    public int from = 0;
    public int to = 5;

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
                waypoints.RemoveAt(0);
            }
        }

        if (_running == false && waypoints.Count > 0) {
            _running = true;
            transform.Rotate(0, 0, -16f, Space.Self);
            DOTween.Play("playerMove");
        }

        if (_running && waypoints.Count == 0) {
            _running = false;
            DOTween.Pause("playerMove");
            transform.localRotation = Quaternion.identity;
        }
    }

    void OnPlayerMoveSelect () {
        waypoints = WaypointFinder.Instance.FindPath(from, to);
    }
}
