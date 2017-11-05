using UnityEngine;
using System.Collections;

namespace Networking
{
    [AddComponentMenu("Camera-Control/Mouse Look")]
    public class MouseLook : MonoBehaviour
    {
        public enum RotationalAxis
        {
            MouseXAndY = 0,
            MouseX = 1,
            MouseY = 2
        }
        public RotationalAxis axis = RotationalAxis.MouseX;

        public float sensitivityX = 15f;
        public float sensitivityY = 15f;

        public float minimumX = -360f;
        public float maximumX = 360f;

        public float minimumY = -60f;
        public float maximumY = 60f;

        float rotationY = 0;

        // Use this for initialization
        void Start()
        {
            if (this.GetComponent<Rigidbody>())
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                GetComponent<Rigidbody>().freezeRotation = true;
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (axis == RotationalAxis.MouseXAndY)
            {
                float rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivityX;

                rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
                rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

                transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
            }
            else if (axis == RotationalAxis.MouseX)
            {
                transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityX, 0);
            }
            else
            {
                rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
                rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

                transform.localEulerAngles = new Vector3(-rotationY, transform.localEulerAngles.y, 0);
            }
        }
    }
}












