using UnityEngine;
using System.Collections;

namespace Generics
{
    [System.Serializable]

    public class GenericsTest : MonoBehaviour
    {
        public CustomList<GameObject> gameObjects;

        // Use this for initialization
        void Start()
        {
            gameObjects = new CustomList<GameObject>();
            gameObjects[0] = new GameObject();
            gameObjects.Add(new GameObject());
            gameObjects.Remove(new GameObject());
            gameObjects.Clear(new GameObject());
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
