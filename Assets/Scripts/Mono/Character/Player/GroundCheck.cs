using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{

    CharacterController controller;

    // Start is called before the first frame update
    void Start()
    {
        controller = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag.Contains("Ground"))
        {
            controller.isGrounded = true;
            if (col.gameObject.tag.Contains("Grass"))
            {
                controller.EmitTrail(new Color(0.39f, 0.29f, 0.23f));
            }
            else if (col.gameObject.tag.Contains("Snow"))
            {
                controller.EmitTrail(Color.white);
            }
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if(col.gameObject.tag.Contains("Ground"))
        {
            controller.isGrounded = false;
        }
    }
}
