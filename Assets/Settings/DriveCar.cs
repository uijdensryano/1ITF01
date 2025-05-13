using UnityEngine;

public class DriveCar2D : MonoBehaviour
{
    public Rigidbody2D carBody;
    public Rigidbody2D frontWheel;
    public Rigidbody2D rearWheel;

    public ScoreManager scoreManager;
    public endscreenManager endScreenManager;

    public float motorTorque = 150f;
    public float rotationTorque = 300f;

    public float raycastDistance = 3f; // Distance for the raycast to check for platform
    public LayerMask platformLayer; // Layer mask for the finish platform

    private float input;
    private bool autoDrive = false;
    private bool carStarted = false;
    private bool check_var = true;

    private bool isFrontWheelOnPlatform = false;
    private bool isRearWheelOnPlatform = false;
    private bool hasFinished = false;

    void Start()
    {
        // Freeze horizontal movement and rotation before start
        FreezeMovement(true);
    }

    void Update()
    {
        // Check if both wheels are on the finish platform
        CheckWheelsOnPlatform();

        if (carStarted && !isFrontWheelOnPlatform && !isRearWheelOnPlatform && Input.GetKeyDown(KeyCode.Space))
        {
            autoDrive = !autoDrive;
        }

        input = carStarted && !(isFrontWheelOnPlatform && isRearWheelOnPlatform) 
            ? (autoDrive ? 1f : Input.GetAxis("Horizontal")) : 0f;
    }

    void FixedUpdate()
    {
        if (hasFinished) return; // ✅ Skip torque logic if car has finished

        // Apply motor torque to rear wheel
        rearWheel.AddTorque(-input * motorTorque * Time.fixedDeltaTime);

        // Tilt car in air (only manual input)
        if (!autoDrive)
        {
            carBody.AddTorque(-input * rotationTorque * Time.fixedDeltaTime);
        }
    }

    public void StartCar()
    {
        if (carStarted) return;

        carStarted = true;
        autoDrive = true;

        // Unfreeze movement so car can move
        FreezeMovement(false);
    }

   private void FreezeMovement(bool freeze)
    {
        if (freeze)
        {
            carBody.constraints = RigidbodyConstraints2D.FreezePositionX;
        }
        else
        {
            carBody.constraints = RigidbodyConstraints2D.None;
        }
    }


    // Check if both wheels are on the finish platform using raycasts
    private void CheckWheelsOnPlatform()
{
    RaycastHit2D frontHit = Physics2D.Raycast(frontWheel.position, Vector2.down, raycastDistance, platformLayer);
    RaycastHit2D rearHit = Physics2D.Raycast(rearWheel.position, Vector2.down, raycastDistance, platformLayer);

    Debug.DrawRay(frontWheel.position, Vector2.down * raycastDistance, Color.green);
    Debug.DrawRay(rearWheel.position, Vector2.down * raycastDistance, Color.green);

    bool isFrontOnPlatform = frontHit.collider != null;
    bool isRearOnPlatform = rearHit.collider != null;

    if (isFrontOnPlatform && isRearOnPlatform && rearHit.collider != null)
    {
        Collider2D platformCollider = rearHit.collider;
        float rearX = rearWheel.position.x;
        float platformLeft = platformCollider.bounds.min.x;
        float platformRight = platformCollider.bounds.max.x;
        

        if (rearX >= platformLeft + 1f && rearX <= platformRight && check_var == true)
        {
            FreezeMovement(true); // Stop horizontal motion
            hasFinished = true;

            int stars = scoreManager != null ? scoreManager.stars_collected : 0;
            endScreenManager.OpenEndScreenSuccess(stars);
            check_var = false;

        }
    }
}
}
