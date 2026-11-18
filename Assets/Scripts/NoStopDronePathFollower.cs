using UnityEngine;
using System.Collections.Generic; //for list of path points

//script is similar to the other drone's script except there's no bool to stop
public class NoStopDronePathFollower : MonoBehaviour
{
    public PathPoint dronePath; //will be used in Unity to manually add the points, hence "public"

    public float speed = 3f; //'public' allows this variable to be adjusted in Unity
    public float turningSpeed = 3f; //public' allows this variable to be adjusted in Unity

    private int currPathPoint = 0; //Index to be used in path list
    private List<Transform> points;

    private Vector3 direction; //calculate the directon of the target path point to the drone
    private Quaternion lookRotation; //used to calculate rotation needed. Quaternion is always used for rotations
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
        if(points == null)
        {
            return; //error handling
        }

        direction = (points[currPathPoint].position - transform.position).normalized;

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
}
