using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;

    private int count;
    public Text countText;
    public Text winText;
    private bool isGrounded = true;
    private bool isLevelOver = false;

    
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = new Vector3(0, 0, 0);
        count = 0;
        SetCountText();
        winText.text = "";
        Vector3 startPos = GameObject.Find("Start").GetComponent<Transform>().localPosition;
        startPos.y += 0.9f;
        this.GetComponent<Transform>().localPosition = startPos;
        foreach(GameObject g in Resources.FindObjectsOfTypeAll(typeof(GameObject)))
        { 
            if(g.CompareTag("Pick Up") && !(g.activeSelf)) {
                g.SetActive(true);
            }
            
        }
    }

    void FixedUpdate()
    {
        if (!isLevelOver) { 
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");

            Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical);

            rb.AddForce(movement * speed);

            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                rb.AddForce(0, 100, 0);
            }
        }
        else
        {
            rb.velocity = new Vector3(0, 0, 0);
            SetCountText();
        }
    }

    void OnTriggerEnter(Collider other)
    {  
        if(other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive(false);
            count += 1;
            SetCountText();
        }
        else if(other.gameObject.CompareTag("End"))
        {
            if (count == 12)
            {
                isLevelOver = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("OOB"))
        {
            Start();
        }
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if(count == 12 && isLevelOver)
        {
            winText.text = "You Win!";
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Ground")
        {
            isGrounded = true;
        }
    }
    
    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.name == "Ground")
        {
            isGrounded = false;
        }
    }
}
