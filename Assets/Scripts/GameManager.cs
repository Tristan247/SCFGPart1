using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    gridCreator grid;

    void Start()
    {
        grid = GetComponent<gridCreator>();
        for (int i = 0; i < 5; i++)
        {
            AddObs();
        }
        Scan();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Scan()
    {
        AstarPath.active.Scan();
    }
    public void AddObs()
    {
        Instantiate(Resources.Load<GameObject>("PreFabs/Obstacle"), new Vector3(Random.Range(0, grid.width), Random.Range(0, grid.height)), Quaternion.identity);
    }
    public void AddAI()
    {
        Instantiate(Resources.Load<GameObject>("PreFabs/AI"), new Vector3(Random.Range(0, grid.width), Random.Range(0, grid.height)), Quaternion.identity);
    }
    public void StartGame()
    {

    }
}
