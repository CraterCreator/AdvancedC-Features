using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Minesweeper3D
{
    [RequireComponent(typeof(Renderer))]
    public class Block : MonoBehaviour
    {
        // Score x, y and z coordinate in array for later use
        public int x, y, z;
        public bool isMine = false; //Is the current block a mine?

        private bool isRevealed; // Has the block already been revealed?

        [Header("References")]
        public Color[] textColors;
        public TextMesh textElement; // Reference to the text element
        public Transform mine; // Reference to the mine

        private Renderer rend; // Reference to the renderer
        // Use this for initialization
        void Awake()
        {
            //Grab the reference to renderer
            rend = GetComponent<Renderer>();
        }

        void Start()
        {
            // Detach text Element from block
            textElement.transform.SetParent(null);
            // Randomly decide if its a mine or not
            isMine = Random.value < 0.05f; 
        }

        // Update is called once per frame
        void UpdateText(int adjacentMines)
        {
            // Are there adjacent mines?
            if(adjacentMines > 0)
            {
                // Set text to amount of mines
                textElement.text = adjacentMines.ToString();

                // Check if adjacent mines are within textColor's array
                if(adjacentMines < textColors.Length)
                {
                    // Set text color to whatever was presented
                    textElement.color = textColors[adjacentMines];
                }
            }
        }

        public void Reveal(int adjacentMines)
        {
            // Flag the block as being revealed
            isRevealed = true;
            //If block is a mine
            if(isMine)
            {
                //Activate the mine
                mine.gameObject.SetActive(true);
                //Detach it from children
                mine.SetParent(null);
            }
            else
            {
                // Updates the text to display adjacentMines
                UpdateText(adjacentMines);
            }

            // Deactivates the block
            gameObject.SetActive(false);
        }
    }
}
