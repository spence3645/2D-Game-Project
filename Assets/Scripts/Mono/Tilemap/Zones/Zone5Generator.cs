using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Zone5Generator : MapGenerator
{
    // Start is called before the first frame update
    void Awake()
    {
        grassMap = this.transform.Find("Grass").GetComponent<Tilemap>();
        waterMap = this.transform.Find("Water").GetComponent<Tilemap>();

        GenerateBeginningSection(current_x, current_x + (width / 4), current_height);
        current_x += width / 4;

        for (int i = 0; i < 4; i++)
        {
            end_x = current_x + width;
            current_height = GenerateGrassSection(current_x, current_x + width, current_height);
            current_x += width;
        }

        GenerateEndSection(current_x, current_x + (width / 4), current_height);

        SpawnPoints();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
