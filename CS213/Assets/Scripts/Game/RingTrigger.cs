using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
       // float secIn = 0.0f;

    }

    // Update is called once per frame
    void Update()
    {
      //  secIn += Time.deltaTime;

      //////  }
        
    }
    
    void OnTriggerEnter(Collider other){
        Debug.Log(other.transform.parent.gameObject.name + " triggers.");
        GameObject gameObject = other.transform.parent.gameObject;
        if(gameObject.tag.Equals("Sheep")) {
            GhostSheepBehavior sheepScript = gameObject.GetComponent<GhostSheepBehavior>();
            sheepScript.incrementScore();
        }
        
    }
}
