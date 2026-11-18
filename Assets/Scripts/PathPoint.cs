using UnityEngine;
using System.Collections.Generic; //for list

//Using a list and a class makes it easier to add more waypoints if needed.
public class PathPoint : MonoBehaviour
{
    //This list will hold all the path points
    //In Unity, drag the path points into the list attribute
    public List<Transform> points = new List<Transform>();


    //IMPORTANT: This function is for debugging.
    //It draws a sphere at a path point and a line that connects the path points
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        for(int i = 0; i < points.Count; i++)
        {
            if(points[i] != null) //error handling: Check if there are points in the list
            {
                //create a sphere at the path point's position
                Gizmos.DrawSphere(points[i].position, 0.5f); //(pos, sizeOfSphere)

                if(i < points.Count-1 && points[i+1] != null) 
                {
                    /*
                    1. "Count-1" due to zero-based indexing
                    2. Check if the current point is the last point before drawing a line
                       - Since it's for debugging, this will show where the path ends. 
                       Even though the cars and drones will be in a loop
                    */
                    Gizmos.DrawLine(points[i].position, points[i + 1].position);
                    // ^ draw a line between the current point and the next point
                }
            }
        }
    }
}
