using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Pathfinding;


public class customAIMoveScriptGrid : MonoBehaviour
{
    //the object that we are using to generate the path
    Seeker seeker;

    //path to follow stores the path
    Path pathToFollow;

    //a reference from the UI to the green box
    public Transform target;

    //a reference to PointGraphObject
    GameObject graphParent;

    //the node of the graph that is going to correspond with the green box
    GameObject targetNode;

    //EnemySpawner esp;

    LineRenderer lr;

    public bool seekerMode = false;

    public List<Transform> obstacleNodes;

    void Start()
    {
        //esp = Camera.main.GetComponent<EnemySpawner>();
        //lr = this.GetComponent<LineRenderer>();
        lr = GetComponent<LineRenderer>();
        Debug.Log(this.name);

        //the instance of the seeker attached to this game object
        seeker = GetComponent<Seeker>();
        target = GameObject.Find("Black player box").transform;

        graphParent = GameObject.Find("Grid");
        //we scan the graph to generate it in memory
        graphParent.GetComponent<AstarPath>().Scan();

        //generate the initial path
        pathToFollow = seeker.StartPath(transform.position, target.position);

        //update the graph as soon as you can.  Runs indefinitely
        StartCoroutine(updateGraph());

        //move the red robot towards the green enemy
        StartCoroutine(moveTowardsEnemy(this.transform));
    }

    private void Update()
    {
        if (seekerMode)
        {
            Debug.Log("Scan");
            lr.positionCount = pathToFollow.vectorPath.Count;
            Debug.Log(lr.positionCount);
            Debug.Log(pathToFollow.vectorPath.Count);
            for (int i = 0; i < pathToFollow.vectorPath.Count; i++)
            {
                lr.SetPosition(i, pathToFollow.vectorPath[i]);
            }

        }

        if (Input.GetKeyDown(KeyCode.P))
            seekerMode = !seekerMode;
    }

    IEnumerator updateGraph()
    {
        while (true)
        {
            graphParent.GetComponent<AstarPath>().Scan();

            yield return null;

        }

    }

    IEnumerator moveTowardsEnemy(Transform t)
    {

        while (true)
        {

            List<Vector3> posns = pathToFollow.vectorPath;
            Debug.Log("Positions Count: " + posns.Count);

            for (int counter = 0; counter < posns.Count; counter++)
            {
                // Debug.Log("Distance: " + Vector3.Distance(t.position, posns[counter]));
                if (posns[counter] != null)
                {
                    while (Vector3.Distance(t.position, posns[counter]) >= 0.5f)
                    {
                        //esp.savePosition();
                        t.position = Vector3.MoveTowards(t.position, posns[counter], 1f);
                        //esp.drawTail(esp.enemyLength);
                        //since the enemy is moving, I need to make sure that I am following him
                        pathToFollow = seeker.StartPath(t.position, target.position);
                        //wait until the path is generated
                        yield return seeker.IsDone();
                        //if the path is different, update the path that I need to follow
                        posns = pathToFollow.vectorPath;

                        //  Debug.Log("@:" + t.position + " " + target.position + " " + posns[counter]);
                        yield return new WaitForSeconds(0.3f);
                    }

                }
                //keep looking for a path because if we have arrived the enemy will anyway move away
                //This code allows us to keep chasing
                pathToFollow = seeker.StartPath(t.position, target.position);
                yield return seeker.IsDone();
                posns = pathToFollow.vectorPath;
                //yield return null;

            }
            yield return null;
        }
    }


}
