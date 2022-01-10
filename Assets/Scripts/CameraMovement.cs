using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    //public PerlinNoise_Map target;//the target object
    //private float speedMod = 10.0f;//a speed modifier
    //private Vector3 point;//the coord to the point where the camera looks at


    //public float lookSpeedH = 2f;
    //public float lookSpeedV = 2f;
    //public float zoomSpeed = 2f;
    //public float dragSpeed = 6f;

    //private float yaw = 0f;
    //private float pitch = 0f;


    //void Start()
    //{//Set up things on the start method

    //    StartCoroutine(getCentreObj());
    //}



    //IEnumerator getCentreObj()
    //{
    //    yield return new WaitForSeconds(2);
    //    point = target.centerBuildingGameObject.transform.position;//get target's coords
    //    point.y -= 10f;
    //    transform.LookAt(point);//makes the camera look to it
    //}




    //void Update()
    //{
    //    //Look around with Right Mouse
    //    if (Input.GetMouseButton(1))
    //    {
    //        yaw += lookSpeedH * Input.GetAxis("Mouse X");
    //        pitch -= lookSpeedV * Input.GetAxis("Mouse Y");

    //        transform.eulerAngles = new Vector3(pitch, yaw, 0f);
    //    }

    //    //drag camera around with Middle Mouse
    //    if (Input.GetMouseButton(2))
    //    {
    //        transform.Translate(-Input.GetAxisRaw("Mouse X") * Time.deltaTime * dragSpeed, -Input.GetAxisRaw("Mouse Y") * Time.deltaTime * dragSpeed, 0);
    //    }

    //    //Zoom in and out with Mouse Wheel
    //    transform.Translate(0, 0, Input.GetAxis("Mouse ScrollWheel") * zoomSpeed, Space.Self);

    //    //transform.RotateAround(point, new Vector3(0.0f, 2.0f, 0.0f), 2 * Time.deltaTime * speedMod);

    //}

    public CharacterController characterController;
    public float speed = 12f;
    public float mouseSensitivity = 100f;
    float xRotation = 0f;

    private void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        float y = 0;
        if (Input.GetKey(KeyCode.Space)) y = 1;
        if (Input.GetKey(KeyCode.LeftShift)) y = -1;
        
        Vector3 move = transform.right * x + transform.forward * z + transform.up * y;
        characterController.Move(move * speed * Time.deltaTime);

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        Camera.main.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);


        if (Input.GetKey(KeyCode.Escape))
            Screen.lockCursor = false;
        else
            Screen.lockCursor = true;

    }










    //Vector2 _mouseAbsolute;
    //Vector2 _smoothMouse;

    //public Vector2 clampInDegrees = new Vector2(360, 180);
    //public Vector2 sensitivity = new Vector2(2, 2);
    //public Vector2 smoothing = new Vector2(3, 3);
    //public Vector2 targetDirection;
    //public Vector2 targetCharacterDirection;

    //// Assign this if there's a parent object controlling motion, such as a Character Controller.
    //// Yaw rotation will affect this object instead of the camera if set.
    //// Use this for initialization
    //void Start()
    //{
    //    Cursor.lockState = CursorLockMode.Confined;
    //    mouseOrigin = Input.mousePosition;

    //    // Set target direction to the camera's initial orientation.
    //    targetDirection = transform.localRotation.eulerAngles;

    //    // Set target direction for the character body to its inital state.
    //    Vector3 vec = transform.localRotation.eulerAngles;
    //    targetCharacterDirection = vec;
    //}

    //public Vector3 mouseOrigin = new Vector3();
    //public float speed = 6.0F;
    //public float jumpSpeed = 8.0F;
    //public float gravity = 20.0F;
    //public float turnSpeed = 200.0F;
    //private Vector3 moveDirection = Vector3.zero;
    //void Update()
    //{

    //    CharacterController controller = GetComponent<CharacterController>();
    //    //if (controller.isGrounded)
    //    //{
    //    //    moveDirection = new Vector3(Input.GetAxis("Vertical"), 0, -Input.GetAxis("Horizontal"));
    //    //    moveDirection = transform.TransformDirection(moveDirection);
    //    //    moveDirection *= speed;
    //    //    if (Input.GetButton("Jump"))
    //    //        moveDirection.y = jumpSpeed;

    //    //}
    //    ////else
    //    ////{
    //    //moveDirection.y -= gravity * Time.deltaTime;
    //    //moveDirection.y -= gravity;
    //    //}
    //    controller.Move(moveDirection * Time.deltaTime);
    //    //Camera/Rotation
    //    //Vector3 pos = GameObject.Find("Camera").GetComponent<Camera>().ScreenToViewportPoint((Input.mousePosition) - mouseOrigin);
    //    //GameObject.Find("Head").transform.RotateAround(GameObject.Find("Head").transform.position, transform.right, -pos.y * turnSpeed);
    //    //transform.RotateAround(transform.position, Vector3.up, pos.x * turnSpeed);
    //    //mouseOrigin = Input.mousePosition;
    //    // Ensure the cursor is always locked when set
    //    //if (lockCursor)
    //    //{
    //    Cursor.lockState = CursorLockMode.Locked;
    //    Cursor.visible = false;
    //    //}

    //    // Allow the script to clamp based on a desired target Value.
    //    var targetOrientation = Quaternion.Euler(targetDirection);
    //    var targetCharacterOrientation = Quaternion.Euler(targetCharacterDirection);

    //    // Get raw mouse input for a cleaner reading on more sensitive mice.
    //    var mouseDelta = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

    //    // Scale input against the sensitivity setting and multiply that against the smoothing Value.
    //    mouseDelta = Vector2.Scale(mouseDelta, new Vector2(sensitivity.x * smoothing.x, sensitivity.y * smoothing.y));

    //    // Interpolate mouse movement over time to apply smoothing delta.
    //    _smoothMouse.x = Mathf.Lerp(_smoothMouse.x, mouseDelta.x, 1f / smoothing.x);
    //    _smoothMouse.y = Mathf.Lerp(_smoothMouse.y, mouseDelta.y, 1f / smoothing.y);

    //    // Find the absolute mouse movement Value from point zero.
    //    _mouseAbsolute += _smoothMouse;

    //    // Clamp and apply the local x Value first, so as not to be affected by world transforms.
    //    if (clampInDegrees.x < 360)
    //        _mouseAbsolute.x = Mathf.Clamp(_mouseAbsolute.x, -clampInDegrees.x * 0.5f, clampInDegrees.x * 0.5f);

    //    // Then clamp and apply the global y Value.
    //    if (clampInDegrees.y < 360)
    //        _mouseAbsolute.y = Mathf.Clamp(_mouseAbsolute.y, -clampInDegrees.y * 0.5f, clampInDegrees.y * 0.5f);


    //    // If there's a character body that acts as a parent to the camera
    //    var yRotation = Quaternion.AngleAxis(_mouseAbsolute.x, Vector3.up);
    //    transform.localRotation = yRotation * targetCharacterOrientation;
    //}



}
