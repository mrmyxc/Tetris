using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameBoard : MonoBehaviour {

    public static Transform[,] gameBoard = new Transform[10, 20];

/*
    public static void Draw ()
    {
        string arrayOutput = "";

        // Gets size of gameboard array 
        int rows = gameBoard.GetLength(0) - 1;
        int columns = gameBoard.GetLength(1) - 1;

        // Cycle through the array and print N or X 
        // depending on if you have a null or transform
        for (int column = columns; column >= 0; column--)
        {
            for (int row = 0; row <= rows; row++)
            {

                if (gameBoard[row, column] == null)
                {
                    arrayOutput += ".";
                }
                else
                {
                    arrayOutput += "*";
                }
            }

            arrayOutput += "\n";

        }

        // Get a reference to the Text component
        // and change its value
        var myArrayComp = GameObject.Find("MyArray").GetComponent<Text>();
        myArrayComp.text = arrayOutput;
    }
*/
    
    // called to delete multiple rows 
    public static bool DeleteAllFullRows()
    {
        for (int row= 0; row < 20; row++)
        {
            if (IsRowFull(row))
            {
                DeleteGameBoardRow (row);
                SoundManager.Instance.PlayOneShot(SoundManager.Instance.rowDelete);
                return true;
            }
        }
        return false;
    }

    // row destruction check
    public static bool IsRowFull ( int row )
    {
        for (int col = 0; col < 10; col++)
        {
            if (gameBoard[col, row] == null)
            {
                return false;
            }
        }
        return true;
    }

    // called to delete a single row
    public static void DeleteGameBoardRow (int row )
    {
        for (int col = 0; col < 10; col++)
        {
            Destroy(gameBoard[col, row].gameObject);
            gameBoard[col, row] = null;
        }

        //move any filled rows above deleted row(s) downwards
        row++;
        for (int rowIndex = row; rowIndex < 20; rowIndex++)
        {
            for (int col = 0; col < 10; col++)
            {
                if (gameBoard[col, rowIndex] != null)
                {
                    gameBoard[col, rowIndex - 1] = gameBoard[col, rowIndex];

                    gameBoard[col, rowIndex] = null;

                    gameBoard[col, rowIndex - 1].position +=  Vector3.down;
                }
            }
        }
    }
}
