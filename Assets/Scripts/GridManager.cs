using UnityEngine;
using System.Collections.Generic;

public class GridManager : MonoBehaviour {
    public Sprite[] sprites;    //grey, yellow, green, red
    public int dimensions;
    public GameObject gridPiece;
    public int movementRange;
    int currentX, currentZ;
    bool generatedMovementArea;
    List<TwoInts> movementArea;
    int previousColor;

    List<List<GameObject>> grid;
    List<List<int>> gridColors;

    // Use this for initialization
    void Start () {
        previousColor = 0;
        currentX = -1;
        currentZ = -1;
        grid = new List<List<GameObject>>();
        gridColors = new List<List<int>>();
        movementArea = new List<TwoInts>();
	    for(int i = 0; i < dimensions; i++)
        {
            grid.Add(new List<GameObject>());
            gridColors.Add(new List<int>());
            for (int j = 0; j < dimensions; j++)
            {
                gridColors[i].Add(0);
                grid[i].Add((GameObject) Instantiate(gridPiece, new Vector3(i - (dimensions / 2) + 0.5f, 0.01f , (j - dimensions / 2) + 0.5f), Quaternion.Euler(new Vector3(-90, 0, 0))));
                grid[i][j].transform.SetParent(gameObject.transform, false);
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
	    if(GameControl.control.myModel != null)
        {
            int x, z;
            x = Mathf.RoundToInt((GameControl.control.myModel.transform.position.x / 2) - 0.5f);
            z = Mathf.RoundToInt((GameControl.control.myModel.transform.position.z / 2) - 0.5f);

            if (x != currentX || z != currentZ)
            {
                SetGridPiece(currentX, currentZ, GetGridPiece(currentX, currentZ));
                SetGridPiece(x, z, 1);
                currentX = x;
                currentZ = z;
            }

            if (GameControl.control.isMyTurn && !generatedMovementArea)
            {
                print("dimensions: " + dimensions.ToString() + " currentX: " + currentX.ToString() + " currentZ: " + currentZ.ToString());
                for (int i = -dimensions; i < dimensions; i++)
                {
                    for (int j = -dimensions; j < dimensions; j++)
                    {
                        if (Mathf.Pow(currentX + i, 2) + Mathf.Pow(currentZ + j, 2) < Mathf.Pow(movementRange, 2))
                        {
                            movementArea.Add(new TwoInts(-i, -j));
                            print("CurrentX: " + (currentX).ToString() + " i: " + i.ToString() + ", CurrentZ" + (currentZ).ToString() + " j: " + j.ToString());
                        }
                        else
                        {
                            //print("Not adding: " + (currentX + i).ToString() + ", " + (currentZ + j).ToString());
                        }
                    }
                }
                foreach (TwoInts square in movementArea)
                {
                    SetGridPiece(square.x, square.z, 2);
                }
                SetGridPiece(currentX, currentZ, 1);
                generatedMovementArea = true;
            }
            else if (!GameControl.control.isMyTurn && generatedMovementArea)
            {
                foreach (TwoInts square in movementArea)
                {
                    SetGridPiece(square.x, square.z, 0);
                }
                SetGridPiece(currentX, currentZ, 1);
                movementArea.Clear();
                generatedMovementArea = false;
            }
        }
        else if (currentX != -1 && currentZ != -1)
        {
            SetGridPiece(currentX, currentZ, GetGridPiece(currentX, currentZ));
            currentX = -1;
            currentZ = -1;
        }
	}

    void SetGridPiece(int x, int z, int sprite)
    {
        int shiftedX = x + (dimensions / 2);
        int shiftedZ = z + (dimensions / 2);
        print("grid width: " + grid.Count + " grid height: " + grid[0].Count + " x location: " + (x + (dimensions / 2)).ToString() + " z location: " + (z + (dimensions / 2)).ToString());
        if (grid.Count > shiftedX && grid[shiftedX].Count > shiftedZ)
        {
            grid[shiftedX][shiftedZ].GetComponent<SpriteRenderer>().sprite = sprites[sprite];
            if (sprite != 1)
            {
                gridColors[shiftedX][shiftedZ] = sprite;
            }
        }
    }
    int GetGridPiece(int x, int z)
    {
        int shiftedX = x + (dimensions / 2);
        int shiftedZ = z + (dimensions / 2);
        print("grid width: " + grid.Count + " grid height: " + grid[0].Count + " x location: " + (x + (dimensions / 2)).ToString() + " z location: " + (z + (dimensions / 2)).ToString());
        if (grid.Count > shiftedX && grid[shiftedX].Count > shiftedZ)
        {
            return gridColors[shiftedX][shiftedZ];
        }
        else
        {
            return -1;
        }
    }
}

class TwoInts
{
    public int x;
    public int z;

    public TwoInts(int x_int, int z_int)
    {
        x = x_int;
        z = z_int;
    }
}