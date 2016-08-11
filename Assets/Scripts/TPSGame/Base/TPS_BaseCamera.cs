// These lines allow us to access "libraries".
// Libraries are full of pre-built functions and variables for us to use.
using UnityEngine;

// These are properties. By using the require component property this script won't work without the requirements.
// In most cases unity will automatically add the components to the object.
[RequireComponent(typeof(Camera))]
public class TPS_BaseCamera : MonoBehaviour
{

    // We need access to our actual camera component and our transform to pivot around.
    // We'll use the aim vector to move around the pivot.
    // The values will be supplied from the TPS_BasePlayer script.
    protected new Camera    camera;
    public        Transform pivot;

    // This allows a value to be public but not show up in unitys inspector.
    [HideInInspector]
    public        Vector2   aimVector;

    // How fast our camera moves
    public        Vector2   sensitivity;

    // Awake is called before start and if the object exists
    protected virtual void Awake()
    {
        camera = GetComponent<Camera>();
    }

    // Use this for initialization
    protected virtual void Start ()
    {
	}

    // Update is called once per frame
    protected virtual void Update()
    {
    }

    // Fixed update is called a certain amount of timed per second.
    // It's better for physics calculations.
    protected virtual void FixedUpdate()
    {
    }

    // Late update happens after Update and FixedUpdate.
    // Camera logic usually goes here.
    protected virtual void LateUpdate ()
    {
        RotatePivot();
	}

    protected virtual void RotatePivot()
    {
        // Here we'll apply our aim vector to the rotation
        pivot.rotation = Quaternion.Euler(new Vector3(ClampAngle(pivot.rotation.eulerAngles.x + (aimVector.y * sensitivity.x), 45, 45),
                                                      pivot.rotation.eulerAngles.y + (aimVector.x * sensitivity.y)));

        
    }

    protected virtual float ClampAngle(float a, float min, float max)
    {
        // This function will allow us to clamp angles to a value below max and above min.
        if (a > 360) a -= 360;
        else if (a < 0) a += 360;

        if (a < 180 && a > max) a = max;
        else if (a > 180 && a < 360 - min) a = 360 - min;
        
        return a;
    }
}
