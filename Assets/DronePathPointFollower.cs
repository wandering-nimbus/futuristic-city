using UnityEngine;
using System.Collections.Generic; //for list of path points


public class DronePathPointFollower : MonoBehaviour
{
    public float speed = 4f;
    public float turningSpeed = 4f;

    public PathPoint dronePath;

    private int currPathPoint = 0; //Index to be used in path list
    private List<Transform> points;

    private bool isStopped = true; //used to start and stop 2 drones
    private Vector3 direction;
    private Quaternion lookRotation;
    private float distanceToPoint;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(dronePath != null)
        {
            points = dronePath.points;
        }
    }

    // Update is called once per frame
    void Update()
    {
        StopStartDrones();
        if(isStopped == true || points == null)
        {
            return; //error handling
        }

        direction = (points[currPathPoint].position - transform.position).normalized; 
        //normalised helps maintain a constant speed between the target path point and the drone

        lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turningSpeed);

        //move forward towards point
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        //drone is near path point
        distanceToPoint = Vector3.Distance(transform.position, points[currPathPoint].position); //find the distance between the target point and the drone
        
        //switch to the next point as the target to create a loop
        if(distanceToPoint < 0.5f)
        {
            currPathPoint++; //move index
            if(currPathPoint == points.Count)
            {
                currPathPoint = 0;
            }
        }
    }

    void StopStartDrones()
    {
        if(Input.GetKeyDown(KeyCode.G)) //press G to start and stop the drones with this script
        {
            isStopped = !isStopped;
        }
    }
}
