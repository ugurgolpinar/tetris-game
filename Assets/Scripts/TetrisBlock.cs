using UnityEngine;
using UnityEngine.UI;
public class TetrisBlock : MonoBehaviour
{
    public GameObject spawner;
    public Text scoreText;
    public Vector3 rotationPoint;
    public float fallTime = 0.8f;
    public static int height = 20;
    public static int width = 10;
    public static int score = 0;

    private float previousTime;
    private static Transform[,] grid = new Transform[width, height];
    public static bool gameOver = false;

    void Start()
    {
        spawner = GameObject.Find("Spawner");
        scoreText = GameObject.Find("Score Text").GetComponent<Text>();
    }

    void Update()
    {
        if (gameOver)
        {
            spawner.SetActive(false);
            this.enabled = false;
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            transform.position += new Vector3(-1, 0, 0);
            if (!ValidMove())
                transform.position -= new Vector3(-1, 0, 0);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            transform.position += new Vector3(1, 0, 0);
            if (!ValidMove())
                transform.position -= new Vector3(1, 0, 0);
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), 90);
            if (!ValidMove())
            {
                transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), 90);
            }
        }

        if (Time.time - previousTime > ((Input.GetKey(KeyCode.S) ? fallTime / 10 : fallTime)))
        {
            transform.position += new Vector3(0, -1, 0);
            if (!ValidMove())
            {
                transform.position -= new Vector3(0, -1, 0);
                AddToGrid();
                CheckForLines();
                this.enabled = false;
                FindObjectOfType<Spawner>().SpawnBlock();
            }

            previousTime = Time.time;
        }


    }

    void CheckForLines()
    {
        for (int i = height - 1; i >= 0; i--)
        {
            if (HasLine(i))
            {
                DeleteLine(i);
                RowDown(i);
                score += 10;
                scoreText.text = "Score: " + score;
            }
        }
    }

    bool HasLine(int i)
    {
        for (int j = 0; j < width; j++)
        {
            if (grid[j, i] == null)
            {
                return false;
            }
        }
        return true;
    }

    void DeleteLine(int i)
    {
        for (int j = 0; j < width; j++)
        {
            Destroy(grid[j, i].gameObject);
            grid[j, i] = null;
        }
    }

    void RowDown(int i)
    {
        for (int y = i; y < height; y++)
        {
            for (int j = 0; j < width; j++)
            {
                if (grid[j, y] != null)
                {
                    grid[j, y - 1] = grid[j, y];
                    grid[j, y] = null;
                    grid[j, y - 1].transform.position += new Vector3(0, -1, 0);
                }
            }
        }
    }

    void AddToGrid()
    {
        foreach (Transform children in transform)
        {
            int roundedX = Mathf.RoundToInt(children.transform.position.x);
            int roundedY = Mathf.RoundToInt(children.transform.position.y);

            grid[roundedX, roundedY] = children;
        }
    }

    bool ValidMove()
    {
        foreach (Transform children in transform)
        {
            int roundedX = Mathf.RoundToInt(children.transform.position.x);
            int roundedY = Mathf.RoundToInt(children.transform.position.y);

            if (roundedX < 0 || roundedX >= width || roundedY < 0)
                return false;

            if (grid[roundedX, roundedY] != null && roundedY >= height - 3f)
                gameOver = true;

            if (grid[roundedX, roundedY] != null)
                return false;

        }
        return true;
    }

}
