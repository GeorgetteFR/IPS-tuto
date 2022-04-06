using System.Linq;
using UnityEngine;

public class GhostSheepBehavior : AgentBehaviour
{

    public float minDistance = 27.0f;
    public float par = 3f;
    public float eps = 0.00001f;
    public float xmin = 287.8f;
    public float zmin = -19.7f;
    public float xmid = 301.2f;
    public float zmid = -10.5f;
    public float xmax = 313f;
    public float zmax = -2.84f;

    private Steering steering;
    private Vector3 position;
    private Vector3 closest;
    private Vector3 pos1;
    private Vector3 pos2;
    private float dis1;
    private float dis2;

    public void Start() { }

    public override Steering GetSteering()
    {
        locatePlayers();
        //        sheep();
        wolf();

        return steering;
    }

    private float abs(float f)
    {
        if (f < 0)
        {
            return -f;
        }
        return f;
    }

    private void locatePlayers()
    {
        steering = new Steering();
        //implement your code here.
        GameObject[] p1;
        GameObject[] p2;
        p1 = GameObject.FindGameObjectsWithTag("Player1");
        p2 = GameObject.FindGameObjectsWithTag("Player2");

        position = transform.position;
        pos1 = p1[0].transform.position - position;
        pos2 = p2[0].transform.position - position;

        dis1 = pos1.sqrMagnitude;
        dis2 = pos2.sqrMagnitude;
        closest = pos2;

        if (dis1 < dis2)
        {
            closest = pos1;
        }
    }

    private void sheep()
    {
        if (closest.sqrMagnitude > minDistance)
        {
            closest = Vector3.zero;
        }
        else
        {
            float dir = Vector3.Dot(pos1, pos2) / (pos1.magnitude * pos2.magnitude);
            if (dis1 < minDistance && dis2 < minDistance && abs(dir) < par && dir < 0)
            {
                if (position.x < xmin + par)
                {
                    closest = new Vector3(-1, 0, 0);
                }
                else if (position.x > xmax - par)
                {
                    closest = new Vector3(1, 0, 0);
                }
                else
                {
                    if (position.z > zmid)
                    {
                        closest = new Vector3(-(pos1.z / pos1.x), 0, 1);
                    }
                    else
                    {
                        closest = new Vector3(pos1.z / pos1.x, 0, -1);
                    }

                }
            }
            else
            {
                if (position.x >= xmax || position.x <= xmin)
                {
                    if (closest.z < 0)
                    {
                        closest = new Vector3(0, 0, -1);
                    }
                    else
                    {
                        closest = new Vector3(0, 0, 1);
                    }
                }
                else if (position.z >= zmax || position.z <= zmin)
                {
                    if (closest.x < 0)
                    {
                        closest = new Vector3(-1, 0, 0);
                    }
                    else
                    {
                        closest = new Vector3(1, 0, 0);
                    }
                }
            }

            closest.Normalize();
        }

        steering.linear = -closest * agent.maxAccel;
        steering.linear = transform.parent.TransformDirection(Vector3.ClampMagnitude(steering.linear, agent.maxAccel));
    }

    private void wolf()
    {
        closest.Normalize();
        steering.linear = closest * agent.maxAccel;
        steering.linear = transform.parent.TransformDirection(Vector3.ClampMagnitude(steering.linear, agent.maxAccel));
    }

}

