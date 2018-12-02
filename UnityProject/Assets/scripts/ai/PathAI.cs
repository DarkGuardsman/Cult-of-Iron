using UnityEngine;
using System.Collections;
using Pathfinding;

public class PathAI : MonoBehaviour {    
    
    public EntityMob host;
    
    public float speed = 2;  

    public Path path;
    public float nextWaypointDistance = 3;  
    public float repathRate = 0.5f;
    
    private Rigidbody2D rigidbody2D;
    private Seeker seeker;
    private int currentWaypoint = 0;
    private float lastRepath = float.NegativeInfinity;

    public bool reachedEndOfPath;

    public void Start () 
    {
        seeker = host.GetComponent<Seeker>(); 
        rigidbody2D = host.GetComponent<Rigidbody2D>();
    }

    public void OnPathComplete (Path p) 
    {
        Debug.Log("A path was calculated. Did it fail with an error? " + p.error);

        // Path pooling. To avoid unnecessary allocations paths are reference counted.
        // Calling Claim will increase the reference count by 1 and Release will reduce
        // it by one, when it reaches zero the path will be pooled and then it may be used
        // by other scripts. The ABPath.Construct and Seeker.StartPath methods will
        // take a path from the pool if possible. See also the documentation page about path pooling.
        p.Claim(this);
        
        if (!p.error) 
        {
            if (path != null) 
            {
                path.Release(this);
            }
            path = p;
            // Reset the waypoint counter so that we start to move towards the first point in the path
            currentWaypoint = 0;
        } 
        else 
        {
            p.Release(this);
        }
    }

    public void Update () 
    {
        if (Time.time > lastRepath + repathRate && seeker.IsDone()) 
        {
            lastRepath = Time.time;
            if(host.currentTarget != null)
            {
                seeker.StartPath(transform.position, host.currentTarget.transform.position, OnPathComplete);
            }
        }

        if (path == null) 
        {
            // We have no path to follow yet, so don't do anything
            return;
        }
        else if(host.currentTarget == null)
        {
            path.Release(this);
        }

        // Check in a loop if we are close enough to the current waypoint to switch to the next one.
        // We do this in a loop because many waypoints might be close to each other and we may reach
        // several of them in the same frame.
        reachedEndOfPath = false;
        // The distance to the next waypoint in the path
        float distanceToWaypoint;
        while (true) 
        {
            // If you want maximum performance you can check the squared distance instead to get rid of a
            // square root calculation. But that is outside the scope of this tutorial.
            distanceToWaypoint = Vector3.Distance(transform.position, path.vectorPath[currentWaypoint]);
            if (distanceToWaypoint < nextWaypointDistance) 
            {
                // Check if there is another waypoint or if we have reached the end of the path
                if (currentWaypoint + 1 < path.vectorPath.Count) 
                {
                    currentWaypoint++;
                } 
                else 
                {
                    // Set a status variable to indicate that the agent has reached the end of the path.
                    // You can use this to trigger some special code if your game requires that.
                    reachedEndOfPath = true;
                    break;
                }
            } 
            else 
            {
                break;
            }
        }

        //Get speed, slow down as we get closer
        float speedFactor = reachedEndOfPath ? Mathf.Sqrt(distanceToWaypoint/nextWaypointDistance) : 1f;

        //Get movement direction
        Vector3 dir = (path.vectorPath[currentWaypoint] - transform.position).normalized;
        Vector3 velocity = dir * speed * speedFactor;

        //Apply movement force
        Vector3 moveForce = rigidbody2D.mass * velocity;
        rigidbody2D.AddForce(moveForce);
    }
} 