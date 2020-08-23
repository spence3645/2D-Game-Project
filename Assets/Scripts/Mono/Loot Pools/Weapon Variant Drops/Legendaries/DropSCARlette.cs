using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropSCARlette : MonoBehaviour
{
    public GameObject base_SCARlette;
    GameObject droppedWeapon;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Drop(float chance)
    {
        droppedWeapon = Instantiate(base_SCARlette, this.transform.position, Quaternion.identity, null);
        droppedWeapon.name = droppedWeapon.name.Replace("(Clone)", "");
        droppedWeapon.GetComponent<WeaponBehavior>().CreateGun(chance);
    }
}
