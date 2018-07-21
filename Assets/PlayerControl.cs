using UnityEngine;


[RequireComponent(typeof(PLayerMotor))]
public class PlayerControl : MonoBehaviour {

    [SerializeField]
    private float speed = 3f;
    [SerializeField]
    private float speedMouse = 1.5f;

    private PLayerMotor motor;

    private void Start()
    {
        motor = GetComponent<PLayerMotor>();
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
        Vector3 _cameraRotation = new Vector3(_xRot, 0f, 0f) * speedMouse;
        //apply rotation
        motor.Rotate(_rotation);
        motor.RotateCamera(_cameraRotation);

    }
}
