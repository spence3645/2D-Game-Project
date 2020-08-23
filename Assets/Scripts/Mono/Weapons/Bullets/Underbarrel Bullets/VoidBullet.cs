using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoidBullet : MonoBehaviour
{

    int pullRadius = 100;
    int pullStrength = 50;

    float pullTime = 3f;

    bool activated;

    void Update()
    {
        if (activated)
        {
            StartCoroutine(PullEnemies());
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag == "Enemy")
        {
            this.transform.localScale = new Vector2(1, 1);
            this.transform.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            activated = true;
        }

        else if (col.tag.Contains("Ground"))
        {
            this.transform.localScale = new Vector2(1, 1);
            this.transform.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            activated = true;
        }
    }

    IEnumerator PullEnemies()
    {
        foreach (Collider2D col in Physics2D.OverlapCircleAll(this.transform.position, pullRadius))
        {
            if(col.tag == "Enemy")
            {
                Debug.Log("Pulled");

                Vector3 forceDirection = this.transform.position - col.transform.position;

                col.attachedRigidbody.AddForce(forceDirection.normalized * pullStrength);
            }
        }

        yield return new WaitForSeconds(pullTime);

        Destroy(this.gameObject);
    }

    void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
}
