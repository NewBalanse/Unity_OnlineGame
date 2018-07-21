using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class PLayerMotor : MonoBehaviour {

    //vectors
    private Vector3 velocity = Vector3.zero;
    private Vector3 rotate = Vector3.zero;
    private Vector3 cameraRotation = Vector3.zero;
    
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
    public void RotateCamera(Vector3 _cameraRotate)
    {
        cameraRotation = _cameraRotate;
    }
    void PerformMovement()
    {
        if(velocity != Vector3.zero)
        {
            rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
        }
    }
    void PerformRotate()
    {
        rb.MoveRotation(rb.rotation * Quaternion.Euler(rotate));

        if (cam != null)
        {
            cam.transform.Rotate(-cameraRotation);
        }
    }
}
