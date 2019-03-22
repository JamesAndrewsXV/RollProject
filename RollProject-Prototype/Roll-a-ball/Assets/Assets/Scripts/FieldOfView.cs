using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public float viewRadius;
    [Range(0, 360)]
    public float viewAngle;
    public GameObject player;
    Dictionary<GameObject, bool> walls;

    int invMask;
    int visMask;

    private void Awake()
    {
        invMask = 8;
        visMask = 9;
        walls = new Dictionary<GameObject, bool>(); // wall, is it in the way
        foreach (GameObject wall in GameObject.FindGameObjectsWithTag("Wall"))
        {
            walls.Add(wall, false);
        }
    }

    void Update()
    {
        // if the wall is blocking the cam, make it invisible
        RaycastHit hit;
        GameObject blockingWall;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
        {
            if (hit.transform.gameObject.CompareTag("Wall"))
            {
                blockingWall = hit.transform.gameObject;
                walls[blockingWall] = true;
                
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
                Debug.Log("Did Hit");
                setVisible(blockingWall);
            }
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
            Debug.Log("Did not Hit");
        }

        
    }

    void setVisible(GameObject blockingWall)
    {
        foreach (KeyValuePair<GameObject, bool> wallBool in walls)
        {
            if (wallBool.Value == true)
            {
                wallBool.Key.transform.gameObject.layer = invMask;
            }
            else
            {
                wallBool.Key.transform.gameObject.layer = visMask;
            }
        }
    }
}
