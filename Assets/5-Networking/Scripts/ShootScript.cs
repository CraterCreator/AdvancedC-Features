using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace Networking
{
    public class ShootScript : NetworkBehaviour
    {

        public float fireRate = 1f;

        public float range = 100f;

        public LayerMask mask;

        private float fireFactor = 0f;

        private GameObject mainCamera;

        // Use this for initialization
        void Start()
        {
            mainCamera = GetComponentInChildren<GameObject>();
        }

        [Command]
        void Cmd_PlayerShot(string id)
        {
            Debug.Log("Player" + id + "has been shot!");
        }

        [Client]
        void Shoot()
        {
            RaycastHit hit;

            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
                Debug.DrawLine(ray.origin, ray.origin + ray.direction * 1000, Color.red);

                if (Physics.Raycast(ray, out hit, range, mask))
                {
                    if(hit.transform.tag == "Player")
                    {
                        Cmd_PlayerShot(hit.transform.name);
                    }
                }
            }
        }
        // Update is called once per frame
        void Update()
        {

        }
    }
}
