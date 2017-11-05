using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Minesweeper3D
{
    public class Grid : MonoBehaviour
    {
        public GameObject blockPrefab;
        // The grids dimensions
        public int width = 10;
        public int height = 10;
        public int depth = 10;
        public float spacing = 1.2f; // How much space between blocks

        // Multi dimensional array storing the blocks
        private Block[,,] blocks;

        void Start()
        {
            GenerateBlocks();
        }

        void Update()
        {
            mouseClick();
        }

        Block SpawnBlock(Vector3 pos)
        {
            GameObject clone = Instantiate(blockPrefab); // Instantiate clone
            clone.transform.position = pos; // Set position
            Block currentBlock = clone.GetComponent<Block>(); // Get Block component
            return currentBlock; // Return it
        }

        void GenerateBlocks()
        {
            // Create 3D array to store all the blocks
            blocks = new Block[width, height, depth];

            // Loop through the X, Y, Z axis of the 3D array
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    for (int z = 0; z < depth; z++)
                    {
                        // Calculate half size using array dimensions
                        Vector3 halfSize = new Vector3(width / 2, height / 2, depth / 2);

                        // Create position for element to pivot around Grid zero
                        Vector3 pos = new Vector3(x - halfSize.x, y - halfSize.y, z - halfSize.z);

                        // Apply spacing
                        pos *= spacing;

                        // Spawn the block at that position
                        Block block = SpawnBlock(pos);

                        // Attach block to grid as a child
                        block.transform.SetParent(transform);

                        // Store array coordinate inside the block itself
                        block.x = x;
                        block.y = y;
                        block.z = z;
                        // Store block in the array at coordinates
                        blocks[x, y, z] = block;
                    }
                }
            }
        }

        public int GetAdjacentMineCountAt(Block b)
        {
            int count = 0;

            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    for (int z = -1; z <= 1; z++)
                    {
                        // Calculate adjacent element's index
                        int desiredX = b.x + x;
                        int desiredY = b.y + y;
                        int desiredZ = b.z + z;

                        //IF desiredX is within range of blocks array
                        if (desiredX >= 0 && desiredY >= 0 && desiredZ >= 0 && 
                            desiredX < width && desiredY < height && desiredZ < depth)
                        {
                            Block currentBlock = blocks[desiredX, desiredY, desiredZ];
                            // IF the element at index is a mine
                            if (currentBlock.isMine)
                            {
                                // Increment count by 1
                                count++;
                            }
                        }
                    }
                }

            }


            return count;
        }

        void mouseClick()
        {
            RaycastHit hit;

            if (Input.GetMouseButtonUp(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                Debug.DrawLine(ray.origin, ray.origin + ray.direction * 1000, Color.red);

                if (Physics.Raycast(ray, out hit, 1000))
                {
                    Block b = hit.collider.GetComponent<Block>();
                    if (b != null)
                    {
                        SelectBlock(b);
                    }
                }
            }


        }

        void FFuncover(int x, int y, int z, bool[,,] visted)
        {
            if (x >= 0 && y >= 0 && z >= 0 && 
                x < width && y < height && z < depth)
            {
                if (visted[x, y, z])
                {
                    return;
                }

                Block block = blocks[x, y, z];
                int adjacentMines = GetAdjacentMineCountAt(block);
                block.Reveal(adjacentMines);

                if (adjacentMines > 0)
                {
                    return;
                }

                visted[x, y, z] = true;

                FFuncover(x - 1, y, z - 1, visted);
                FFuncover(x + 1, y, z - 1, visted);
                FFuncover(x, y - 1, z - 1, visted);
                FFuncover(x, y + 1, z - 1, visted);

                FFuncover(x - 1, y, z, visted);
                FFuncover(x + 1, y, z, visted);
                FFuncover(x, y - 1, z, visted);
                FFuncover(x, y + 1, z, visted);

                FFuncover(x - 1, y, z + 1, visted);
                FFuncover(x + 1, y, z + 1, visted);
                FFuncover(x, y - 1, z + 1, visted);
                FFuncover(x, y + 1, z + 1, visted);



            }
        }

        public void UncoverMines()
        {
            // Loop through all elements in array
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    for (int z = 0; z < depth; z++)
                    {
                        // Get currentblock at index
                        Block currentBlock = blocks[x, y, z];
                        // If currentBlock is a mine
                        if (currentBlock.isMine == true)
                        {
                            int mineCount = GetAdjacentMineCountAt(currentBlock);
                            // Reveal the mine
                            currentBlock.Reveal(mineCount);
                        }

                    }
                }

            }
        }

        public void SelectBlock(Block selectedBlock)
        {
            int mineCount = GetAdjacentMineCountAt(selectedBlock);
            // Reveal the selectedBlock
            selectedBlock.Reveal(mineCount);
            // If the selected block is a mine
            if (selectedBlock.isMine == true)
            {
                // uncover all other mines
                UncoverMines();
            }
            // ElseIf there are no adjacent mines
            else if (mineCount == 0)
            {
                // Perform flood fill algorithm to reveal all empty blocks 
                FFuncover(selectedBlock.x, selectedBlock.y, selectedBlock.z, new bool[width, height, depth]);
            }
        }
    }

}
