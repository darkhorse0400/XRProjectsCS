using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCamera : MonoBehaviour
{
   
    // Update is called once per frame
    void Update()
    {
        // Make health bar canvas always face camera
        // Set forward direction to always face AR Camera?

        transform.LookAt(Camera.main.transform);
        // changes the transform of this canvas to look towards Camera
    }
}
