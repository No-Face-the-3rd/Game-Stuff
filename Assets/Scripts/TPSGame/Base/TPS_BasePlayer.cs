// These lines allow us to access "libraries".
// Libraries are full of pre-built functions and variables for us to use.
using UnityEngine;

// These are properties. By using the require component property this script won't work without the requirements.
// In most cases unity will automatically add the components to the object.
[RequireComponent(typeof(Collider ))]
[RequireComponent(typeof(Rigidbody))]
public class TPS_BasePlayer : MonoBehaviour
{

    // Unity components attached to the player.
    // These are required.
    protected new Rigidbody rigidbody;
    protected new Collider  collider;

    // We need access to the camera to know what direction is forward.
    // Our transform where we start checking if we're on the ground and how far to check.
    // We only check for objects on the layer specified.
    public new TPS_BaseCamera camera;
    public     Transform      groundChecker;
    public     float          groundCheckDistance;
    public     LayerMask      groundLayer;

    // How strong our jump is, how fast we move and how many times we can jump.
    public  float jumpForce;
    public  float extraJumps;
    public  float accelerationRate;
    public  float maxSpeed;

    private float currentJump;
    private bool  canJump
    {
        get
        {
            return isGrounded || currentJump < extraJumps;
        }
    }

    // These variables will contain our input information.
    // Input X and Y are our WASD keys.
    // Mouse X and Y are moving the mouse, Z is the scroll wheel.
    // Jump and Sprint are unity inputs and can be changed in project settings.
    protected float inputX;
    protected float inputY;
    protected float mouseX;
    protected float mouseY;
    protected float mouseZ;
    protected bool  pressedJump;
    protected bool  pressingSprint;

    // To check if we're grounded we'll cast a ray downwards.
    // Returns true if there is a collision.
    protected bool isGrounded
    {
        get
        {
            return Physics.Raycast(groundChecker.position,  // The origin of the ray
                                   Vector3.down,            // The direction of the ray
                                   groundCheckDistance,     // The distance our ray will go
                                   groundLayer);            // The layer our ray will check for
        }
    }

    // Awake is called before start and if the object exists
    protected virtual void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();              // We need access to our rigidbody to apply physics in script.
        collider  = GetComponent<Collider> ();              // We need access to our collider to adjust it in script.

        rigidbody.useGravity     = false;                   // We disable gravity to do it in script so we have more control.
        rigidbody.freezeRotation = true;                    // We freeze rotation because we handle rotation manually.
    }

    // Start is called when the object is first spawned
    protected virtual void Start()
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
        GetInput();
        ApplyMovement();
        ApplyCameraRotation();
        ApplyGravity();
    }

    // Late update happens after Update and FixedUpdate.
    // Camera logic usually goes here.
    protected virtual void LateUpdate()
    {
    }

    protected virtual void GetInput()
    {
        // This function will set all of our input variables.
        // This way we don't have to call get axis/button multiple times.
        // This will save performance and require less typing.

        inputX = Input.GetAxis("Horizontal");
        inputY = Input.GetAxis("Vertical"  );

        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");
        mouseZ = Input.GetAxis("Mouse Z");

        pressedJump    = Input.GetButtonDown("Jump"  );
        pressingSprint = Input.GetButton    ("Sprint");
    }

    protected virtual void ApplyMovement()
    {
        // We will create a movement vector using our input data.
        // This is the direction we want to move in.
        // By using TransformDirection we'll move forward according to our rotation.
        Vector3 movementVector = camera.transform.TransformDirection(new Vector3(inputX, 0, inputY));
        movementVector *= accelerationRate;

        // We're going to obtain our velocity from our rigidbody.
        Vector3 velocity = rigidbody.velocity;
        Vector3 velocityChange = movementVector - velocity;

        // We're going to calculate how much we move by applying a change to our velocity over fixed updates.
        velocityChange.x = Mathf.Clamp(velocityChange.x, -accelerationRate, accelerationRate);
        velocityChange.z = Mathf.Clamp(velocityChange.z, -accelerationRate, accelerationRate);
        velocityChange.y = 0;

        // Then we apply the force to our rigidbody using the proper force mode.
        // If you want to know more about force modes you can find it in the unity documentation under AddForce.
        rigidbody.AddForce(velocityChange, ForceMode.VelocityChange);

        // If we press the jump button and we can jump...
        if (pressedJump && canJump)
        {
            // And if we're not on the ground...
            if (!isGrounded)
                // We'll use up an extra jump.
                currentJump++;
            else
                currentJump = 0;

            // We'll apply our jump height directly to our velocity here.
            rigidbody.velocity = new Vector3(rigidbody.velocity.x,
                                             CalculateJumpHeight(), 
                                             rigidbody.velocity.z);
        }
    }

    protected virtual void ApplyCameraRotation()
    {
        // We'll send our mouse input to the camera script which will rotate our pivot.
        camera.aimVector = new Vector3(mouseX, mouseY);
    }

    protected virtual void ApplyGravity()
    {
        // We'll apply gravity by multiplaying gravity by our mass.
        rigidbody.AddForce(Physics.gravity * rigidbody.mass);
    }

    protected virtual float CalculateJumpHeight()
    {
        // By calculating the square root of 2 * Jumpforce * -verticalGravity we get a proper jump height.
        // The gravity is negative already so we need to make it positive for the calculation.
        // Negative square roots return NaN (Not a Number).
        return Mathf.Sqrt(2 * jumpForce * -Physics.gravity.y);
    }
}
