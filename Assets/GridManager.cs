using UnityEngine;
using System.Collections.Generic;

public class GridManager : MonoBehaviour {
    public Sprite[] sprites;    //grey, yellow, green, red
    public int dimensions;
    public GameObject gridPiece;
    int currentX, currentZ;

    List<List<GameObject>> grid;

	// Use this for initialization
	void Start () {
        grid = new List<List<GameObject>>();
	    for(int i = 0; i < dimensions; i++)
        {
            grid.Add(new List<GameObject>());
            for (int j = 0; j < dimensions; j++)
            {
                grid[i].Add((GameObject) Instantiate(gridPiece, new Vector3(i - dimensions / 2, 0.1f , j - dimensions / 2), Quaternion.Euler(new Vector3(-90, 0, 0))));
                grid[i][j].transform.SetParent(gameObject.transform, false);
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
	    if(GameControl.control.myModel != null)
        {
            int x, z;
            x = Mathf.RoundToInt(GameControl.control.myModel.transform.position.x);
            z = Mathf.RoundToInt(GameControl.control.myModel.transform.position.z);

            if (x != currentX || z != currentZ)
            {
                SetGridPiece(currentX, currentZ, 0);
                SetGridPiece(x, z, 1);
                currentX = x;
                currentZ = z;
            }
        }
	}

    void SetGridPiece(int x, int z, int sprite)
    {
        int shiftedX = x + (dimensions / 2);
        int shiftedZ = z + (dimensions / 2);
        //print("grid width: " + grid.Count + " grid height: " + grid[0].Count + " x location: " + (x + (dimensions / 2)).ToString() + " z location: " + (z + (dimensions / 2)).ToString());
        if (grid.Count > shiftedX && grid[shiftedX].Count > shiftedZ)
        {
            grid[shiftedX][shiftedZ].GetComponent<SpriteRenderer>().sprite = sprites[sprite];
        }
    }
}
