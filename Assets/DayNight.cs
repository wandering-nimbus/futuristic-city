using UnityEngine;

public class DayNight : MonoBehaviour
{
    public Light sun;
    public Light moon;
    public Light[] streetlights;

    private bool night = false;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.N))
	    {
		    night = !night;
		    ChangeLight();
	    }
    }

    public void ChangeLight()
    {
        sun.enabled = !night;
        moon.enabled = night;

        foreach(Light l in streetlights)
        {
            l.enabled = night;
        }
    }

}
