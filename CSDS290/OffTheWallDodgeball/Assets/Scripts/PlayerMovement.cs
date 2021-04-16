﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float speed = 5.0f;
    public Rigidbody2D rb;
    public Camera cam;
    Vector2 movement;
    Vector2 mousePos;
    private bool dash = false;
    [SerializeField] private LayerMask dashLayerMask;

    // Update is called once per frame
    void Update()
    {
        //Input
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            dash = true;
        }
    }

    void FixedUpdate()
    {
        //Movement
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);

        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 270f;
        rb.rotation = angle;

        if (dash)
        {
            float dashAmount = 0.25f;
            rb.MovePosition((Vector2)transform.position + lookDir * dashAmount);

            RaycastHit2D raycastHit2D = Physics2D.Raycast(transform.position, lookDir, dashAmount, dashLayerMask);
            dash = false;
        }
    }

}
