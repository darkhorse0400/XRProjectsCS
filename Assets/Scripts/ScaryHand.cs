using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaryHand : MonoBehaviour
{
    public float mouseSensitivity;
    public float moveSpeed;

    // Start is called before the first frame update
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        // If mouse has moved left or right
        var mouseHorizontalDelta = Input.GetAxis("Mouse X");  //get is holding a key or button down
        if (mouseHorizontalDelta != 0)
        {
            // Rotate the hand about the Y axis by how much the mouse has moved L or R based on how much time has passed (Time.deltaTime is frame rate independent)
            transform.Rotate(Vector3.up, (mouseHorizontalDelta * mouseSensitivity * Time.deltaTime), Space.World);
        }
        // If mouse has moved up or down
        var mouseVerticalDelta = Input.GetAxis("Mouse Y");
        if (mouseVerticalDelta != 0)
        {
            // Rotate the hand about the X axis by how much the mouse has moved Up or Down based on how much time has passed
            transform.Rotate(Vector3.left, (mouseVerticalDelta * mouseSensitivity * Time.deltaTime));
        }

        // If the W key is pressed
        if (Input.GetKey(KeyCode.W))
        {
            // Move forward
            transform.Translate(transform.forward * moveSpeed * Time.deltaTime, Space.World);
        }


        // If the S key is pressed
        if (Input.GetKey(KeyCode.S))
        {
            // Move back
            transform.Translate(transform.forward * -moveSpeed * Time.deltaTime, Space.World);
        }




    }
}
