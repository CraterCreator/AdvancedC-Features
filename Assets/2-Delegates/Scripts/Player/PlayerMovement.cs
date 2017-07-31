using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Delegates
{
    public class PlayerMovement : MonoBehaviour
    {
        public float acceleration = 200f;
        public float deceleration = .01f;

        private Rigidbody rigid;

        void Awake()
        {
            acceleration = 200f;
            rigid = GetComponent<Rigidbody>();
        }

        void Update()
        {
            Accelerate();
            Decelerate();
        }

        void Accelerate()
        {
            // Get input
            float inputH = Input.GetAxis("Horizontal");
            float inputV = Input.GetAxis("Vertical");
            // Calculate inputDir
            Vector3 inputDir = new Vector3(inputH, 0, inputV);
            // Add force in inputDir's direction
            rigid.AddForce(inputDir * acceleration);

        }

        void Decelerate()
        {
            // Decelerate (velocity = -velocity * deceleration)
            rigid.velocity = -rigid.velocity * deceleration;
        }
    }

}
