using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagazineScript : MonoBehaviour
{
    float timeAlive;
    float lifetime = 3f;

    // Start is called before the first frame update
    void Start()
    {
        timeAlive += Time.time + lifetime;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > timeAlive)
        {
            Destroy(this.gameObject);
        }
    }
}
