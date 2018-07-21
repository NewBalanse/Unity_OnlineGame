using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(ConfigurableJoint))]
[RequireComponent(typeof(PLayerMotor))]
public class PlayerControl : NetworkBehaviour {

    [SerializeField]
    private float speed = 3f;
    [SerializeField]
    private float speedMouse = 1.5f;
    [SerializeField]
    private float thusterForce = 1000f;

    [Header("Spring settings:")]
    [SerializeField]
    private JointDriveMode mode = JointDriveMode.Position;
    [SerializeField]
    private float jointSpring = 20f;
    [SerializeField]
    private float jointMaxForce = 40f;

    private PLayerMotor motor;
    private ConfigurableJoint joint;

    private void Start()
    {
        motor = GetComponent<PLayerMotor>();
        joint = GetComponent<ConfigurableJoint>();
        SetJointSettings(jointSpring);
    }
    private void Update()
    {
        //calculate movement velocity as a 3d vector
        float _xMov = Input.GetAxisRaw("Horizontal");
        float _zMOv = Input.GetAxisRaw("Vertical");

        Vector3 _movHorizontal = transform.right * _xMov;
        Vector3 _movvertical = transform.forward * _zMOv;

        //final mov vector
        Vector3 velocity = (_movHorizontal + _movvertical).normalized * speed;

        //apply mov
        motor.Move(velocity);

        //calculate rotation
        float _yRot = Input.GetAxisRaw("Mouse X");
        float _xRot = Input.GetAxisRaw("Mouse Y");
        Vector3 _rotation = new Vector3(0f, _yRot, 0f) * speedMouse;

        float _cameraRotationX = _xRot * speedMouse;
        //apply rotation
        motor.Rotate(_rotation);
        motor.RotateCamera(_cameraRotationX);

        Vector3 _thrusterForse = Vector3.zero;
        if (Input.GetButton("Jump"))
        {
            _thrusterForse = Vector3.up * thusterForce;
            SetJointSettings(0f);
        }
        else
        {
            SetJointSettings(jointSpring);
        }
        motor.Thruster(_thrusterForse);

    }
    private void SetJointSettings(float _jointSpring)
    {
        joint.yDrive = new JointDrive {
            mode = mode,
            positionSpring = _jointSpring,
            maximumForce = jointMaxForce
        };
    }
}
