using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    private Vector3 offsetValue;

    // Start is called before the first frame update
    void Start()
    {
        offsetValue = transform.position - player.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = player.transform.position + offsetValue;
    }

    void RotateCamera()
    {
        // if the mouse is to the left or right of the middle of the screen, rotate about the player
        // to look in the direction of the mouse. The farther the mouse is from the player, the
        // more the camera rotates. Holding the mouse at an edge of the screen causes the camera to continue
        // to rotate
        RotateLeftOrRight();


        // if the mouse is above the player, rotate the camera upwards to look toward the mouse
        // if the mouse is below the player, rotate the camera downwards to look toward the mouse
        // Lower or raise the camera as needed
        // Camera stops at edges of screen.
        RotateUpOrDown();

        // pointing directly at the player doesn't change the camera
        // the camera always maintains the same distance (radius) from the player
    }
}
