using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AbstractClasses
{
    public class Explosive : Bullet
    {
        public GameObject explosionRadius;
        GameObject clone;

        private Collider2D boom;

        // Use this for initialization
        void Start()
        {
            boom = explosionRadius.GetComponent<Collider2D>();
        }

        public override void Update()
        {
            base.Update();

            if (endPos.x >= 19)
            {
                clone = Instantiate(explosionRadius, transform.position, Quaternion.identity);
                Destroy(clone, 0.5f);
            }

        }



        public void OnCollisionEnter2D()
        {
            if (boom.tag == "Player")
            {
                // -Health
            }
        }
    }
}
