using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FetchItemDrop : MonoBehaviour
{
    public GameObject apple;

    public void DropItem()
    {
        Instantiate(apple, this.transform.position, Quaternion.identity, null);
    }
}
