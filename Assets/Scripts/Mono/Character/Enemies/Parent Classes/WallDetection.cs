using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallDetection : MonoBehaviour
{

    AIController aiController;

    // Start is called before the first frame update
    void Start()
    {
        aiController = this.GetComponentInParent<AIController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag.Contains("Ground"))
        {
            aiController.isBlocked = true;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if(col.tag.Contains("Ground"))
        {
            aiController.isBlocked = false;
        }
    }
}
