using UnityEngine;

public class CameraController : MonoBehaviour //Most GameObjects inherit this class
{
    public float movementSpeed = 10f; //How fast the camera moves. Can be adjusted in Unity

    // - Top View
    private Vector3 topviewPos = new Vector3(0,40,0); //camera will be 40 units above the city
    private Vector3 topviewRotation = new Vector3(90,0,0); //rotate camera 90 degrees on x-axis. Camera points down

    // - Side Views
    private Vector3 backViewPos = new Vector3(0,10,-30);
    private Vector3 backViewRotation =new Vector3(0,0,0);

    private Vector3 leftViewPos = new Vector3(-30,10,0);
    private Vector3 leftViewRotation = new Vector3(0,90,0);

    private Vector3 rightViewPos = new Vector3(30,10,0);
    private Vector3 rightViewRotation = new Vector3(0,-90,0);

    private Vector3 frontViewPos = new Vector3(0,10,30);
    private Vector3 frontViewRotation = new Vector3(0,180,0);

    //values for horizontal and vertical camera movements
    private float horizontal = 0f;
    private float vertical = 0f;

    private float cameraZoom = 0f;

    private Vector3 direction;
    private Vector3 zoom;

    //Runs every frame, once.
    // i.e. it will check if the camera has been moved by the user constantly.
    private void Update()
    {
        cameraControls();
        changeView();
    }

    /*TO-DO:
    * Change input settings in Unity to allow legacy and new input controls
    */
    void cameraControls()
    {
        horizontal = Input.GetAxis("Horizontal"); // Control horizontal movement with A/D and/or arrows
        vertical = Input.GetAxis("Vertical"); //Control vertical movement with W/S and/or arrows.
	
	    cameraZoom = 0f; //For zooming in and out
	    if(Input.GetKey(KeyCode.Q)) // "Q" to zoom in
	    {
		    cameraZoom = 1f;
	    }
        else if(Input.GetKey(KeyCode.E)) //"E" to zoom out
	    {	
	    	cameraZoom = -1f;
	    }
        
        direction = new Vector3(horizontal, vertical, 0); //turn into a movement vector
	    zoom = new Vector3(0, 0, cameraZoom); 

        //access the "transform" attribute for the camera and move it according to user input
        // Time.deltaTime is used to improve the frame rate.
        //   - The movement of the camera will not depend on the frame rate
        transform.Translate((direction + zoom) * movementSpeed * Time.deltaTime, Space.Self);
    }

    void changeView()
    {
        //press 1 to switch to front view
        if(Input.GetKey(KeyCode.Alpha1))
        {
            transform.position = frontViewPos; //access the camera's "position" attribute and change it
            transform.rotation = Quaternion.Euler(frontViewRotation); //Quaternion makes it easier to calculate rotations

        }else if(Input.GetKey(KeyCode.Alpha2)) //press 2 for left view
        {
            transform.position = leftViewPos;
            transform.rotation = Quaternion.Euler(leftViewRotation);

        }else if(Input.GetKey(KeyCode.Alpha3)) //press 3 for back view
        {
            transform.position = backViewPos;
            transform.rotation = Quaternion.Euler(backViewRotation);

        }else if(Input.GetKey(KeyCode.Alpha4)) //press 4 for right view
        {
            transform.position = rightViewPos;
            transform.rotation = Quaternion.Euler(rightViewRotation);

        }else if(Input.GetKey(KeyCode.Alpha5)) //press 5 for top view
        {
            transform.position = topviewPos;
            transform.rotation = Quaternion.Euler(topviewRotation);
        }
    }
}
