using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Generics
{

    [System.Serializable]
    public class CustomList<T>
    {
        public T[] list;
        public int capacity { get; }
        public int amount { get; private set; }
        // Default constructor
        public CustomList()
        {

        }
        public CustomList(int capacity)
        {
            list = new T[capacity + 1];
        }
        public T this[int index]
        {
            get
            {
                return list[index];
            }
            set
            {
                list[index] = value;
            }
        }

        // Adds item to the end of the CustomList<T>
        public void Add(T item)
        {
            // Create a new array of amount + 1
            T[] cache = new T[amount + 1];
            // Check if list has been initialized
            if (list != null)
            {
                // Copy all existing items to new array
                for (int i = 0; i < list.Length; i++)
                {
                    cache[i] = list[i];
                }
            }

            // Place new item at end index
            cache[amount] = item;
            // Replace old array with new array
            list = cache;
            // Increment amount
            amount++;
        }

        public void Remove(T item)
        {
            T[] cache = new T[amount - 1];
            if(list != null)
            {
                for (int i = 0; i < list.Length; i++)
                {
                    cache[i] = list[i];
                }
            }

            cache[amount] = item;
            list = cache;
            amount--;
        }

        public void Clear()
        {
            T[] cache = new T[amount];
        }
    }
}
