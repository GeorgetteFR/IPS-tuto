using System.Linq;
using UnityEngine;

public class GhostSheepBehavior : AgentBehaviour
{

    public float minDistance = 27.0f;
    public float par = 1f;
    public float eps = 0.00001f;

    public void Start()
    {
        //CelluloAgent.tag = "Player 1";
        //CelluloAgent1.tag = "Player 2";
    }
    public override Steering GetSteering()
    {

        Steering steering = new Steering();
        //implement your code here.
        GameObject[] p1;
        GameObject[] p2;
        p1 = GameObject.FindGameObjectsWithTag("Player1");
        p2 = GameObject.FindGameObjectsWithTag("Player2");

        Vector3 position = transform.position;
        Vector3 pos1 = p1[0].transform.position - position;
        Vector3 pos2 = p2[0].transform.position - position;

        float dis1 = pos1.sqrMagnitude;
        float dis2 = pos2.sqrMagnitude;
        Vector3 closest = pos2;

        if (dis1 < dis2)
        {
            closest = pos1;
        }

        if (closest.sqrMagnitude > minDistance)
        {
            closest = Vector3.zero;
        }
        else
        {
            if (dis1<minDistance && dis2<minDistance && (abs((Vector3.Dot(pos1, pos2) / (pos1.magnitude * pos2.magnitude)) + 1) < par))
            {
                Vector3 space = p1[0].transform.position;
                if (abs(pos1.x) < eps)
                {
                    if (space.x > 7.34f)
                    {
                        closest = new Vector3(-1, 0, 0);
                    } else
                    {
                        closest = new Vector3(1, 0, 0);
                    }
                } else
                {
                    if (space.z > -5f)
                    {
                        closest = new Vector3(-(pos1.z / pos1.x), 0, 1);
                    }
                    else
                    {
                        closest = new Vector3(pos1.z / pos1.x, 0, -1);
                    }
                    
                }
            }

            closest.Normalize();
        }

        steering.linear = -closest * agent.maxAccel;
        steering.linear = transform.parent.TransformDirection(Vector3.ClampMagnitude(steering.linear, agent.maxAccel));

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

}