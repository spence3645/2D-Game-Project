using UnityEngine;

public class AIBulletScript : MonoBehaviour
{
    public float damage;

    public PlayerHealth playerHealth;
    public AIWeaponBehavior weaponBehavior;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            playerHealth = col.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth)
            {
                playerHealth.TakeDamage(damage, 0 , false);
                Destroy(this.gameObject);
            }
        }

        if (col.tag.Contains("Ground"))
        {
            Destroy(this.gameObject);
        }
    }

    void OnBecameInvisible()
    {
        this.gameObject.SetActive(false);
    }
}
