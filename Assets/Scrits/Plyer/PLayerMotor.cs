using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class PLayerMotor : MonoBehaviour {

    //vectors
    private Vector3 velocity = Vector3.zero;
    private Vector3 rotate = Vector3.zero;
    private Vector3 thruster = Vector3.zero;

    [SerializeField]
    private float cameraRotLimit = 85f;
    private float cameraRotation = 0f;
    private float currentCameraRot = 0f;

    //physics component
    private Rigidbody rb;

    //serialized component
    [SerializeField]
    private Camera cam;
   

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (cam == null)
            cam = GetComponent<Camera>();
    }

    private void FixedUpdate()
    {
        PerformMovement();
        PerformRotate();
    }

    public void Move(Vector3 _velocity)
    {
        velocity = _velocity; 
    }
    public void Rotate(Vector3 _rotate)
    {
        rotate = _rotate;
    }
    public void RotateCamera(float _cameraRotate)
    {
        cameraRotation = _cameraRotate;
    }
    void PerformMovement()
    {
        if(velocity != Vector3.zero)
        {
            rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
        }

        if(thruster != Vector3.zero)
        {
            rb.AddForce(thruster * Time.fixedDeltaTime, ForceMode.Acceleration);
        }
    }
    void PerformRotate()
    {
        rb.MoveRotation(rb.rotation * Quaternion.Euler(rotate));

        if (cam != null)
        {

            //Set our rot and clat it
            currentCameraRot -= cameraRotation;
            currentCameraRot = Mathf.Clamp(currentCameraRot, -cameraRotLimit,cameraRotLimit);
            //apply
            cam.transform.localEulerAngles = new Vector3(currentCameraRot, 0f, 0f);
        }
    }

    public void Thruster(Vector3 _thruster)
    {
        thruster = _thruster;
    }
}
