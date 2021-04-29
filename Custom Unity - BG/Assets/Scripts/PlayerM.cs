using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerM : MonoBehaviour
{
    public GameObject Camera;

    Quaternion StartingRotation;

    float _Vertical, _Horizontal, Jump, RotationHorizontal, RotationVertical;

    bool isGround;

    [SerializeField] private float CurrentSpeed = 10, JumpSpeed = 100, Sensivity = 5;
    [SerializeField] private float RunSpeed = 15, StepSpeed = 5, NormalSpeed = 5;

    private void Start()
    {
        StartingRotation = transform.rotation;

    }

    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.tag == "Ground")
        {
            isGround = true;
        }
    }
    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag == "Ground")
        {
            isGround = false;
        }
    }
    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            CurrentSpeed = RunSpeed;
        }
        else if (Input.GetKey(KeyCode.LeftControl))
        {
            CurrentSpeed = StepSpeed;
        }
        else
        {
            CurrentSpeed = NormalSpeed;
        }

        RotationHorizontal += Input.GetAxis("Mouse X") * Sensivity;
        RotationVertical += Input.GetAxis("Mouse Y") * Sensivity;

        RotationVertical = Mathf.Clamp(RotationVertical, -60, 60);

        Quaternion RotationY = Quaternion.AngleAxis(RotationHorizontal, Vector3.up);
        Quaternion RotationX = Quaternion.AngleAxis(-RotationVertical, Vector3.right);

        Camera.transform.rotation = StartingRotation * transform.rotation * RotationX;
        transform.rotation = StartingRotation * RotationY;


        if (isGround)
        {
            _Vertical = Input.GetAxis("Vertical") * Time.deltaTime * CurrentSpeed;
            _Horizontal = Input.GetAxis("Horizontal") * Time.deltaTime * CurrentSpeed;
            Jump = Input.GetAxis("Jump") * Time.deltaTime * JumpSpeed;

            GetComponent<Rigidbody>().AddForce(transform.up * Jump, ForceMode.Impulse);
        }
        transform.Translate(new Vector3(_Horizontal, 0, _Vertical));
    }
}