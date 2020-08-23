using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PocketInventory : MonoBehaviour
{

    public List<GameObject> weapons = new List<GameObject>(30);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MoveGunToInventory(GameObject weaponPickedUp)
    {
        weaponPickedUp.GetComponent<BoxCollider2D>().enabled = false;
        weaponPickedUp.GetComponent<CircleCollider2D>().enabled = false;
        weaponPickedUp.GetComponent<Rigidbody2D>().simulated = false;

        ParticleSystem particle = weaponPickedUp.GetComponentInChildren<ParticleSystem>();
        var emission = particle.emission;
        emission.enabled = false;

        weaponPickedUp.transform.parent = this.transform;
        weaponPickedUp.transform.position = this.transform.position;
        weaponPickedUp.transform.rotation = new Quaternion(0, 0, 0, 0);

        weapons.Add(weaponPickedUp);
        weaponPickedUp.SetActive(false);
        weaponPickedUp = null;
    }

    public void DropWeapon(GameObject droppedWeapon)
    {
        for(int i = 0; i < weapons.Count; i++)
        {
            if(droppedWeapon.Equals(weapons[i]))
            {
                weapons[i].transform.parent = null;
                weapons[i].transform.rotation = new Quaternion(0, 0, 0, 0);
                weapons[i].transform.position = new Vector3(GameObject.FindGameObjectWithTag("Player").transform.position.x + 2, GameObject.FindGameObjectWithTag("Player").transform.position.y, 0);
                weapons[i].SetActive(true);
                weapons[i].GetComponent<BoxCollider2D>().enabled = true;
                weapons[i].GetComponent<CircleCollider2D>().enabled = true;
                weapons[i].GetComponent<Rigidbody2D>().simulated = true;

                ParticleSystem particle = weapons[i].GetComponentInChildren<ParticleSystem>();
                var emission = particle.emission;
                emission.enabled = true;

                weapons.Remove(weapons[i]);
            }
        }
    }
}
