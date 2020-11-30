using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeReaction : MonoBehaviour
{
    public Color highlightedColor;
    private Color initialColor;

    // Start is called before the first frame update
    void Start()
    {
        initialColor = GetComponent<MeshRenderer>().material.color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        // If the collider is the hand
        if (other.gameObject.tag == "Hand") //          <--- what is other?
        {
            // Change the color of the cube
            //Get compo on object currently in
            GetComponent<MeshRenderer>().material.color = highlightedColor;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Hand")
        {
            // Change the color of the cube
            //Get compo on object currently in
            GetComponent<MeshRenderer>().material.color = initialColor;
        }
    }
    

      
    
}

