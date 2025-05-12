using UnityEngine;

public class DriveCar2D : MonoBehaviour
{
    public Rigidbody2D carBody;
    public Rigidbody2D frontWheel;
    public Rigidbody2D rearWheel;

    public float motorTorque = 150f;
    public float rotationTorque = 300f;

    private float input;
    private bool autoDrive = false;

    void Update()
    {
        // Toggle auto-drive on Space key press
        if (Input.GetKeyDown(KeyCode.Space))
        {
            autoDrive = !autoDrive;
        }

        // Manual input (still works if autoDrive is off)
        input = autoDrive ? 1f : Input.GetAxis("Horizontal");
    }

    void FixedUpdate()
    {
        // Apply motor torque to rear wheel
        rearWheel.AddTorque(-input * motorTorque * Time.fixedDeltaTime);

        // Tilt car in air (only manual input)
        if (!autoDrive)
        {
            carBody.AddTorque(-input * rotationTorque * Time.fixedDeltaTime);
        }
    }

    public void EnableAutoDrive()
    {
        autoDrive = true;
    }

}
