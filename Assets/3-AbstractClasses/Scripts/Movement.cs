﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AbstractClasses
{
    public class Movement : MonoBehaviour
    {
        public float acceleration;
        public float hyperSpeed;
        [Range(0, 1)] public float deceleration = 0.1f;
        public float rotationSpeed = 5f;

        private Rigidbody2D rigid;

        // Use this for initialization
        void Awake()
        {
            rigid = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        void Update()
        {
            Accelerate();
            Decelerate();
            Rotate();
        }

        void Accelerate()
        {
            float inputV = Input.GetAxisRaw("Vertical");
            Vector3 force = transform.right * inputV;
            // Check if left shift is pressed
            if(Input.GetKey(KeyCode.LeftShift))
            {
                // Go hyperspeed
                force *= hyperSpeed;
            }
            // Else
            else
            {
                // Go regular speed
                force *= acceleration;
            }

            rigid.AddForce(force);
        }

        void Decelerate()
        {
            // velocity = -velocity * decceleration
            rigid.velocity += -rigid.velocity * deceleration;
        }

        void Rotate()
        {
            float inputH = Input.GetAxisRaw("Horizontal");
            transform.rotation *= Quaternion.AngleAxis(rotationSpeed * inputH, Vector3.back);
        }
    }
}
