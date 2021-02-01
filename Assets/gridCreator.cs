using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gridCreator : MonoBehaviour
{
    public int width;
    public int height;
    Camera cam;
    void Start()
    {
        cam = GetComponent<Camera>();
        for(int i = 0; i < width; i++)
        {
            for(int j = 0; j < height; j++)
            {
                Instantiate(Resources.Load<GameObject>("PreFabs/Cube"), new Vector3(i, j), Quaternion.identity);
            }
        }
        cam.orthographicSize = (width/2) + 10;
        cam.transform.position = new Vector3(width/2, height/2, -10);
    }
}
