using UnityEngine;
using System.Collections;
using System.Collections.Generic; //for list of path points

public class PathPointFollower : MonoBehaviour
{
    public float speed = 4f;
    public float turningSpeed = 4f;

    public PathPoint path; //a collection of path points added in Unity
    private int currPathPoint = 0; //Index to be used in path list
    private List<Transform> points;
    
    private bool isStopped = true; //used to start and stop all cars

    private Vector3 direction;
    private Quaternion lookRotation;
    private float distanceToPoint;

    void Start()
    {
        if(path != null)
        {
            points = path.points;
        }
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.T)) //press T to start and stop cars
        {
            isStopped = !isStopped; //Negate the bool to get the opposite value. Useful when alternating
        }//has to come first in order for evaluation to take place

        if(isStopped == true || points == null) //without "isStopped==true", cars won't be stopped.
        {
            return; //error handling: Prevent error if path points haven't been dragged into the points list in Unity
        }

        direction = (points[currPathPoint].position - transform.position).normalized;

        lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turningSpeed);
            //Slerp allows for smooth rotations

        //move forward towards point
        transform.Translate(Vector3.forward * speed * Time.deltaTime); //Vector3.forward is a built-in method

        //car is near path point
        distanceToPoint = Vector3.Distance(transform.position, points[currPathPoint].position); //find the distance between the target point and the car
        
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
