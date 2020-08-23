using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Zone1Generator : ZoneGenerator
{
    // Start is called before the first frame update
    void Start()
    {
        Regenerate();
    }

    public override void Regenerate()
    {
        GrassPerlin();
    }
}
