using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class SeekerAI : MonoBehaviour
{
    [Header("IA setup")]
    public Transform target;
    public float speed = 200f;
    public float nextWaypointDistance = 3f;

    private Path _path;
    private int _currentWaypoint = 0;
    private bool _reachedEndOfPath = false;
    private Seeker _seeker;
    private Rigidbody2D _rb;

    // Start is called before the first frame update
    void Start()
    {
        _seeker = GetComponent<Seeker>();
        _rb = GetComponent<Rigidbody2D>();

        InvokeRepeating(nameof(UpdatePath), 0f, .5f);
    }

    private void UpdatePath()
    {
        if (_seeker.IsDone())
            _seeker.StartPath(_rb.position, target.position, OnPathComplete);
    }

    public void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            _path = p;
            _currentWaypoint = 0;

        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_path == null)
        {
            return;
        }

        if (_currentWaypoint >= _path.vectorPath.Count)
        {
            _reachedEndOfPath = true;
            return;
        }
        else
        {
            _reachedEndOfPath = false;
        }

        Vector2 direction = ((Vector2)_path.vectorPath[_currentWaypoint] - _rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;

        _rb.AddForce(force);

        float distance = Vector2.Distance(_rb.position, _path.vectorPath[_currentWaypoint]);

        if (distance < nextWaypointDistance)
        {
            _currentWaypoint++;
        }
    }
}
