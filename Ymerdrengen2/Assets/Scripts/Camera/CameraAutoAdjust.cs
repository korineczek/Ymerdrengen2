using UnityEngine;
using System.Collections;
using Grid;

public class CameraAutoAdjust : MonoBehaviour
{

    public GridManager grid;

    public void Start()
    {
        //GetGridData();
        Camera.main.transform.position = new Vector3(-18, 26, -18);
        Debug.Log("Start Auto Adjust");
        
        GetGridData();
    }


    public void GetGridData()
    {
        
        Debug.Log(grid.gridSize);
        int gridSize = GridData.gridManager.gridSize;


        //find border max tile
        int firstColumnMaxValue = -1;
        int firstRowMaxValue = -1;
        int lastColumnMaxValue = -1;
        int lastRowMaxValue = -1;


        for (int x = 0; x < gridSize; x++)
        {

            for (int y = 0; y < gridSize; y++)
            {

                //FIRST ROW STARTING LEFT TOWARDS RIGHT
                if (GridData.grid[x, y].Value == FieldStatus.Floor && firstRowMaxValue < 0)
                {
                    firstRowMaxValue = x;
                }


                //FIRST COLUMN STARTING LEFT TOWARDS RIGHT
                if (GridData.grid[y, x].Value == FieldStatus.Floor && firstColumnMaxValue < 0)
                {
                    firstColumnMaxValue = x;
                }

                //LAST ROW STARTING LEFT TOWARDS RIGHT
                if (GridData.grid[gridSize - x - 1, y].Value == FieldStatus.Floor && lastRowMaxValue < 0)
                {
                    lastRowMaxValue = gridSize - x - 1;
                }

                //LAST COLUMN STARTING RIGHT TOWARDS LEFT
                if (GridData.grid[y, gridSize - x - 1].Value == FieldStatus.Floor && lastColumnMaxValue < 0)
                {
                    lastColumnMaxValue = gridSize - x - 1;
                }
            }
        }

        //SQUARE = (firstRowMaxValue, firstColumnMaxValue) ------ (firstRowMaxValue, lastColumnMaxValue)
        //          (lastRowMaxValue, firstColumnMaxValue) ------  (lastRowMaxValue, lastColumnMaxValue)
        Debug.Log("(" + firstRowMaxValue + ", " + firstColumnMaxValue + ") - (" + firstRowMaxValue + ", " + lastColumnMaxValue + ")");
        Debug.Log("(" + lastRowMaxValue + ", " + firstColumnMaxValue + ") - (" + lastRowMaxValue + ", " + lastColumnMaxValue + ")");


        //lastColumnMaxValue * 6

        // first column
        /*


        for (int x = 0; x < gridSize; x++)
        {
            string str = "";
            for (int y = 0; y < gridSize; y++)
            {
                str += GridData.grid[x, y].Value + " ";

            }
            Debug.Log(str);
            Debug.Log("--------------------");
        }
        
        */


    }

    /// <summary>
    /// show current level text and init coroutine to hide it after LevelDelay
    /// </summary>
    public void OnLevelWasLoaded()
    {
        /*int currentLevel = SceneManager.GetActiveScene().buildIndex;
        // set text to label from the level array (only if in the game)
        if (currentLevel > 0)
        {
            TextLevel.text = LevelStartText[currentLevel - 1];
            showPanels.ToggleLevelTitle(true);

            StartCoroutine(DisplayLevelText());
        }
        */

        Debug.Log("NEW LEVEL!!!!");

    }




}