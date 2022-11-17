using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundMovement : MonoBehaviour
{
    public float groundSpeed;
    public float groundSpeedDefault;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        groundSpeed = groundSpeedDefault;
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(groundSpeed * -1.0f, rb.velocity.y);
    }
}
