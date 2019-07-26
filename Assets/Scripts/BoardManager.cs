using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Random = UnityEngine.Random;

public class BoardManager : MonoBehaviour
{
    [Serializable]
    public class Count
    {
        public int minimum;
        public int maximum;

        public Count(int min, int max)
        {
            minimum = min;
            maximum = max;
        }
    }

    private List<List<string>> levelMap;

    //public Count wallCount = new Count(5, 9);
    public Count wallCount = new Count(0, 0);
    public Count foodCount = new Count(1, 5);
    public GameObject exit;
    public GameObject[] floorTiles;
    public GameObject[] wallTiles;
    public GameObject[] foodTiles;
    public GameObject[] enemyTiles;
    public GameObject[] outerWallTiles;
    public GameObject NPC;

    private Transform boardHolder;
    private List<Vector3> gridPositions = new List<Vector3>();
    private string gameDataPath = "Levels/intro.json";

    public void SetupScene(LevelData data)
    {
        Debug.Log($"Loading level");

        levelMap = data.map;
        BoardSetup();
        InitializeList();

        LayoutUnits(data.enemies, enemyTiles);
        LayoutUnits(data.NPCs, new GameObject[] { NPC });

        //LayoutObjectAtRandom(wallTiles, wallCount.minimum, wallCount.maximum);
        //LayoutObjectAtRandom(foodTiles, foodCount.minimum, foodCount.maximum);
        //int enemyCount = (int)Mathf.Log(level, 2);
        //LayoutObjectAtRandom(enemyTiles, enemyCount, enemyCount);

        Instantiate(exit, new Vector3(levelMap[0].Count - 1, levelMap.Count- 1, 0), Quaternion.identity);

    }

    void InitializeList()
    {
        gridPositions.Clear();

        for (int y = 1; y < levelMap.Count - 1; y++)
        {
            for (int x = 1; x < levelMap[y].Count - 1; x++)
            {
                gridPositions.Add(new Vector3(x, y, 0));
            }
        }
    }

    void BoardSetup()
    {
        boardHolder = new GameObject("Board").transform;

        for (int y = -1; y < levelMap.Count + 1; y++)
        {
            for (int x = -1; x < levelMap[0].Count + 1; x++)
            {
                GameObject toInstantiate;

                if (x == -1 || x == levelMap[0].Count || y == -1 || y == levelMap.Count)
                {
                    toInstantiate = outerWallTiles[Random.Range(0, outerWallTiles.Length)];
                }
                else
                {
                    
                    switch (levelMap[y][x])
                    {
                        case "1":
                            toInstantiate = wallTiles[Random.Range(0, wallTiles.Length)];
                            break;

                        default:
                            toInstantiate = floorTiles[Random.Range(0, floorTiles.Length)];
                            break;
                    }
                }

                GameObject instance = Instantiate(toInstantiate, new Vector3(x, y, 0), Quaternion.identity) as GameObject;

                instance.transform.SetParent(boardHolder);

            }
        }
    }

    Vector3 RandomPosition()
    {
        int randomIndex = Random.Range(0, gridPositions.Count);

        Vector3 randomPosiiton = gridPositions[randomIndex];
        gridPositions.RemoveAt(randomIndex);
        return randomPosiiton;
    }

    //void LayoutObjectAtRandom(GameObject[] tileArray, int minimum, int maximum)
    //{
    //    int objectCount = Random.Range(minimum, maximum + 1);

    //    for (int i = 0; i < objectCount; i++)
    //    {
    //        Vector3 randomPosition = RandomPosition();
    //        GameObject tileChoice = tileArray[Random.Range(0, tileArray.Length)];
    //        Instantiate(tileChoice, randomPosition, Quaternion.identity);
    //    }
    //}

    void LayoutUnits(List<UnitData> units, GameObject[] tileArray)
    {
        foreach (var unit in units)
        {
            Vector3 position = new Vector3(unit.StartingX, unit.StartingY);
            GameObject tileChoice = tileArray[Random.Range(0, tileArray.Length)];
            Instantiate(tileChoice, position, Quaternion.identity);
        }
    }




}
