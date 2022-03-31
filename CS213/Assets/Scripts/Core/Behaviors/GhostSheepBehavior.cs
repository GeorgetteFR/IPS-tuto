using System.Linq;
using UnityEngine;

public class GhostSheepBehavior : AgentBehaviour
{    
    public void Start(){
    }
    public override Steering GetSteering()
    {
        
        Steering steering = new Steering();
        //implement your code here.
        GameObject[] p1;
        GameObject[] p2;
        p1 = GameObject.FindGameObjectsWithTag("Player 1");
        p2 = GameObject.FindGameObjectsWithTag("Player 2");
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

        closest.Normalize();
        transform.Translate(Time.deltaTime * (-closest));

        return steering;
    }



}
