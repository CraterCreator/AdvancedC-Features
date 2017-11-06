using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StarFoxClone
{
    public class UserController : MonoBehaviour
    {
        public ArwingController arwingController;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            // Get inputH and inputV
            float inputH = Input.GetAxis("Horizontal");
            float inputV = Input.GetAxis("Vertical");
            // Move Controller based on inputH and inputV
            arwingController.Move(inputH, inputV);

            // XTRAS
            // Call arwing shoot if we press a button

            // Call arwing pulse if we press a button
        }
    }

}
