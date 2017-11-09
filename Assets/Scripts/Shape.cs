using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Shape : MonoBehaviour {

    public static float speed = 1f;
    public float lastMoveDown = 0;
    // Use this for initialization
    void Start()
    {
        InvokeRepeating("IncreaseSpeed", 2f, 2f);

        if (!IsInGrid())
        {
            SoundManager.Instance.PlayOneShot(SoundManager.Instance.gameOver);
            Invoke("OpenGameOverScene", 1f);
        }
    }

    void OpenGameOverScene ()
    {
        Destroy(gameObject);
        SceneManager.LoadScene(1);
    }

    void IncreaseSpeed ()
    {
        speed += 0.1f;
    }

    // Update is called once per frame
    void Update()
    {

        // single input
        if (Input.GetKeyDown("a"))
        {
            transform.position += Vector3.left;

            if (!IsInGrid())
            {
                transform.position += Vector3.right;
                SoundManager.Instance.PlayOneShot(SoundManager.Instance.shapeStop);
            }
            else
            {
                SoundManager.Instance.PlayOneShot(SoundManager.Instance.shapeMove);
                UpdateGameBoard();
            }
        }

        if (Input.GetKeyDown("d"))
        {
            transform.position += Vector3.right;
            if (!IsInGrid())
            {
                transform.position += Vector3.left;
                SoundManager.Instance.PlayOneShot(SoundManager.Instance.shapeStop);
            }
            else
            {
                SoundManager.Instance.PlayOneShot(SoundManager.Instance.shapeMove);
                UpdateGameBoard();
            }

        }

        // jump down one square after 1 second or if user presses down
        if (Input.GetKeyDown("s") || (Time.time - lastMoveDown >= speed) )
        {
            transform.position += Vector3.down;

            if (!IsInGrid())
            {
                transform.position += Vector3.up;
                SoundManager.Instance.PlayOneShot(SoundManager.Instance.shapeStop);
                bool rowDeleted = GameBoard.DeleteAllFullRows();

                if (rowDeleted)
                {
                    GameBoard.DeleteAllFullRows();
                    var scoreThing = GameObject.Find("Score").GetComponent<Text>();
                    int score = int.Parse(scoreThing.text);
                    score += 10;
                    scoreThing.text = score.ToString();
                }
                
                //disable ability to control object
                enabled = false;
                FindObjectOfType<ShapeSpawner>().SpawnShape();
            }
            else
            {
                SoundManager.Instance.PlayOneShot(SoundManager.Instance.shapeMove);
                UpdateGameBoard();
            }

            lastMoveDown = Time.time;
        }

        // anti-clockwise rotation - undo rotating if not legal
        if (Input.GetKeyDown("k"))
        {
            transform.Rotate(0, 0, 90);
            if (!IsInGrid())
            {
                transform.Rotate(0, 0, -90);
            }
            SoundManager.Instance.PlayOneShot(SoundManager.Instance.rotateSound);
        }

        // clockwise rotation - undo rotating if not legal
        if (Input.GetKeyDown("j"))
        {
            transform.Rotate(0, 0, -90);
            if (!IsInGrid())
            {
                transform.Rotate(0, 0, 90);
            }
            SoundManager.Instance.PlayOneShot(SoundManager.Instance.rotateSound);
        }        
    }

    public void UpdateGameBoard ()
    {
        for (int col = 0; col < 20; ++col)
        {
            for (int row = 0; row < 10; ++row)
            {
                if (GameBoard.gameBoard[row, col] != null && GameBoard.gameBoard[row, col].parent == transform)
                {
                    GameBoard.gameBoard[row, col] = null;
                }
            }
        }

        foreach (Transform childBlock in transform)
        {
            Vector2 cVector = RoundVector(childBlock.position);
            // include in gameboard
            GameBoard.gameBoard[(int)cVector.x, (int)cVector.y] = childBlock;
            Debug.Log("Cube at " + cVector.x + " " + cVector.y);
        }
        //GameBoard.Draw();
    }

    public bool IsInGrid()
    {
        foreach (Transform childBlock in transform)
        {
            Vector2 cVector = RoundVector(childBlock.position);
            Debug.Log("one child block is " + cVector);
            // world boundaries
            if (cVector.x >= 0 && cVector.x <= 9 && cVector.y >= 0)
            {
                //if space is not empty and not a transform
                if (GameBoard.gameBoard[(int)cVector.x, (int)cVector.y] != null &&
                    GameBoard.gameBoard[(int)cVector.x, (int)cVector.y].parent != transform)
                {
                    return false;
                }
            }
            else
            {
                return false;
            }   
        }
        return true;
    }

    // round to nearest int floats aren't very precise
    public Vector2 RoundVector (Vector2 vect)
    {
        return new Vector2(Mathf.Round(vect.x), Mathf.Round(vect.y));
    }
}

    
   