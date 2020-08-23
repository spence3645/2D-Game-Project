using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropForetoldArmor : MonoBehaviour
{
    public List<GameObject> armor_set = new List<GameObject>();
    GameObject droppedArmor;

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
        droppedArmor = Instantiate(armor_set[Random.Range(0,3)], this.transform.position, Quaternion.identity, null);
        droppedArmor.name = droppedArmor.name.Replace("(Clone)", "");
        droppedArmor.GetComponent<ArmorBehavior>().CreateArmor(chance);
    }
}
