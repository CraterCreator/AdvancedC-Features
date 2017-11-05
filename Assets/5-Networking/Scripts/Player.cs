using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Networking
{
    public class Player : MonoBehaviour
    {
        public float moveSpeed = 10f;
        public float rotationSpeed = 10f;
        public float jumpHeight = 2.0f;

        private Rigidbody rigid;
        private bool isGrounded;

        // Use this for initialization
        void Start()
        {
            rigid = GetComponent<Rigidbody>();
        }

        public void Move(float h, float v)
        {
            Vector3 position = rigid.position;

            position += transform.forward * v * moveSpeed * Time.deltaTime;
            position += transform.right * h * moveSpeed * Time.deltaTime;

            rigid.MovePosition(position);
        }

        public void Jump()
        {
            if(isGrounded)
            {
                rigid.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
                isGrounded = false;
            }
        }

        void OnCollisionEnter()
        {
            isGrounded = true;
        }
    }

}

