using UnityEngine;
using System;
using System.Collections;
using Random = UnityEngine.Random;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class CreateLevel : MonoBehaviour
{
    private const float tileSize = 4;
    private GameObject root, floor, environment, ball;
    public int xHalfExt = 1;
    public int zHalfExt = 1;

    public GameObject outerWall;
    public GameObject innerWall;
    public GameObject exitTile;
    public GameObject[] floorTiles;

    MazeGenerator mazeGenerator;
    private int xExt, zExt;

    void Awake()
    {
        root = GameObject.Find("MovablePlayfield");
        floor = GameObject.Find("DSBasementFloor");
        environment = GameObject.Find("Environment");
        ball = GameObject.Find("DSPlayerBall");

        xExt = (2 * xHalfExt) + 1;
        zExt = (2 * zHalfExt) + 1;

        float scale = Mathf.Max(xExt, zExt) / 3.0f;
        environment.transform.localScale *= scale;
        floor.transform.localPosition *= scale;
        floor.transform.localScale *= scale;

        if (root != null)
        {
            mazeGenerator = new MazeGenerator(xExt, zExt);
            createOuterWalls();
            createFloor();
            createInnerWalls(mazeGenerator);
            placeBallStart();
        }

    }

    void placeBallStart()
    {
         Vector3 randomPosition = new Vector3(Random.Range(-xHalfExt * tileSize, xHalfExt * tileSize), 07, 
            Random.Range(-zHalfExt * tileSize, zHalfExt * tileSize));
        ball.transform.position = randomPosition;
    }

    public void EndzoneTrigger(GameObject other)
    {
        if (ball != null)
        {
            placeBallStart();
        }
    }
    public void winTrigger(GameObject other)
    {
        if (ball != null)
        {
            SceneManager.LoadScene(0);
        }
    }

    public void createFloor()
    {
        int random_a = Random.Range(-xHalfExt, xHalfExt);
        int random_b = Random.Range(-zHalfExt, zHalfExt);
        float y = 0.5f;
        Quaternion eulerZero = Quaternion.Euler(0, 0, 0);
        Transform transformRoot = root.transform;

        if (floorTiles.Length > 0)
        {
            for (int i = -xHalfExt; i <= xHalfExt; i++)
            {
                for (int j = -zHalfExt; j <= zHalfExt; j++)
                {
                    int floorTile = Random.Range(0, floorTiles.Length);
                    if (i.Equals(random_a) && j.Equals(random_b)) Instantiate(exitTile, new Vector3(i, y, j) * tileSize,
                        eulerZero, transformRoot);
                    else Instantiate(floorTiles[floorTile], new Vector3(i, y, j) * tileSize,
                        eulerZero, transformRoot);
                }
            }
        }
    }
    void createOuterWalls()
    {
        float y = 0.5f;
        Quaternion eulerZero = Quaternion.Euler(0, 0, 0);
        Quaternion eulerY90 = Quaternion.Euler(0, 90, 0);
        Transform transformRoot = root.transform;

        for (int i = -xHalfExt; i <= xHalfExt; i++)
        {
            for (int j = -zHalfExt; j <= zHalfExt; j++)
            {
                // lower right corner
                Instantiate(outerWall, new Vector3(i, y, zHalfExt) * tileSize + new Vector3(0, 0, tileSize / 2),
                    eulerZero, transformRoot);
                // upper left corner
                Instantiate(outerWall, new Vector3(i, y, -zHalfExt) * tileSize + new Vector3(0, 0, -tileSize / 2),
                    eulerZero, transformRoot);
                //lower left corner
                Instantiate(outerWall, new Vector3(xHalfExt, y, j) * tileSize + new Vector3(tileSize / 2, 0, 0),
                    eulerY90, transformRoot);
                // upper right corner
                Instantiate(outerWall, new Vector3(-xHalfExt, y, j) * tileSize + new Vector3(-tileSize / 2, 0, 0),
                    eulerY90, transformRoot);
            }
        }
    }

    public void createInnerWalls(MazeGenerator mazeGenerator)
    {
        int[,] maze = mazeGenerator.maze;
        float y = (0.5f * tileSize);
        Quaternion eulerZero = Quaternion.Euler(0, 0, 0);
        Quaternion eulerY90 = Quaternion.Euler(0, 90, 0);
        Transform transformRoot = root.transform;

        for (int i = 0; i < zExt; i++)
        {
            for (int j = 0; j < xExt; j++)
            {
                if ((maze[j, i] & 1) == 0 && i > 0)
                {
                    Instantiate(innerWall, new Vector3((xHalfExt - j) * tileSize, y, ((i - zHalfExt) * tileSize) - tileSize) + 
                        new Vector3(0, 0, tileSize / 2), eulerZero, transformRoot);
                }

                if ((maze[j, i] & 8) == 0 && j > 0)
                {
                    Instantiate(innerWall, new Vector3((xHalfExt - j) * tileSize, y, (i - zHalfExt) * tileSize) + 
                        new Vector3(tileSize / 2, 0, 0), eulerY90, transformRoot);
                }
            }
        }
    }
}