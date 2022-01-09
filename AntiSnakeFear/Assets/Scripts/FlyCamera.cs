using UnityEngine;
using System.Collections;

public class FlyCamera : MonoBehaviour
{
    public float mainSpeed = 10.0f; // Regular speed
    public float shiftAdd = 25.0f; // Amount to accelerate when shift is pressed
    public float maxShift = 100.0f; // Maximum speed when holding shift
    public float camSens = 0.15f; // Mouse sensitivity

    private Vector3 _lastMouse = new(255, 255, 255);
    // kind of in the middle of the screen, rather than at the top (play)

    private float _totalRun = 1.0f;

    private bool _mouseWasDown;

    private void Update()
    {
        // Only handle camera angle when right clicking
        if (Input.GetMouseButton(1))
        {
            _lastMouse = Input.mousePosition - _lastMouse;
            _lastMouse = new Vector3(-_lastMouse.y * camSens, _lastMouse.x * camSens, 0);
            var eulerAngles = transform.eulerAngles;
            _lastMouse = new Vector3(eulerAngles.x + _lastMouse.x, eulerAngles.y + _lastMouse.y, 0);
            if (_mouseWasDown)
            {
                transform.eulerAngles = _lastMouse;
            }

            _lastMouse = Input.mousePosition;
            _mouseWasDown = true;
        }
        else
        {
            _mouseWasDown = false;
        }
        // Mouse camera angle done.  

        // Keyboard commands
        var p = GetBaseInput();
        if (Input.GetKey(KeyCode.LeftShift))
        {
            _totalRun += Time.deltaTime;
            p *= _totalRun * shiftAdd;
            p.x = Mathf.Clamp(p.x, -maxShift, maxShift);
            p.y = Mathf.Clamp(p.y, -maxShift, maxShift);
            p.z = Mathf.Clamp(p.z, -maxShift, maxShift);
        }
        else
        {
            _totalRun = Mathf.Clamp(_totalRun * 0.5f, 1f, 1000f);
            p *= mainSpeed;
        }

        p *= Time.deltaTime;
        transform.Translate(p);
    }

    // Returns the basic values, if it's 0 than it's not active.
    private static Vector3 GetBaseInput()
    {
        var pVelocity = new Vector3();

        // Forwards
        if (Input.GetKey(KeyCode.W))
            pVelocity += new Vector3(0, 0, 1);

        // Backwards
        if (Input.GetKey(KeyCode.S))
            pVelocity += new Vector3(0, 0, -1);

        // Left
        if (Input.GetKey(KeyCode.A))
            pVelocity += new Vector3(-1, 0, 0);

        // Right
        if (Input.GetKey(KeyCode.D))
            pVelocity += new Vector3(1, 0, 0);

        // Up
        if (Input.GetKey(KeyCode.Space))
            pVelocity += new Vector3(0, 1, 0);

        // Down
        if (Input.GetKey(KeyCode.LeftControl))
            pVelocity += new Vector3(0, -1, 0);

        return pVelocity;
    }
}