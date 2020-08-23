using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapGenerator : MonoBehaviour
{

    public Tilemap grassMap;
    public Tilemap waterMap;
    public Tilemap snowMap;

    public Tile grass_tile;
    public Tile dirt_tile;
    public Tile grass_tile_slope_up;
    public Tile grass_tile_slope_down;
    public Tile grass_slope_connector_up;
    public Tile grass_slope_connector_down;

    public Tile snow_tile;
    public Tile snow_tile_slope_up;
    public Tile snow_tile_slope_down;
    public Tile snow_tile_connector_up;
    public Tile snow_tile_connector_down;

    public Tile stone_tile;

    public Tile water_tile;

    public Vector3Int bossTile;

    public List<Vector3Int> tile_locations = new List<Vector3Int>();
    public List<Vector3Int> water_tiles = new List<Vector3Int>();

    public int width = 100;
    public int max_height = 200;
    public int current_x = 0;
    public int end_x = 0;
    public int current_height = 100;
    public int num_of_zones;

    int caveOffset = 20; //Make sure there is grass/dirt above ground

    public bool isBossSpawning;

    public GameObject player_spawn;

    public GameObject commando_boss_spawn;

    public GameObject rappel_rope;
    public GameObject chest;

    public void GenerateBeginningSection(int startLocation, int endLocation, int heightmap)
    {
        for (int x = startLocation; x < endLocation; x++)
        {
            grassMap.SetTile(new Vector3Int(x, heightmap, 0), grass_tile);
            if (x == startLocation)
            {
                tile_locations.Add(new Vector3Int(x, heightmap, 0));
            }

            for (int i = heightmap - 1; i >= 0; i--)
            {
                if (i > 90)
                {
                    grassMap.SetTile(new Vector3Int(x, i, 0), dirt_tile);
                }
                else
                {
                    grassMap.SetTile(new Vector3Int(x, i, 0), stone_tile);
                }
            }
        }
    }

    public void GenerateEndSection(int startLocation, int endLocation, int heightmap)
    {
        for (int x = startLocation; x < endLocation; x++)
        {
            grassMap.SetTile(new Vector3Int(x, heightmap, 0), grass_tile);

            if (x == (startLocation + endLocation) / 2 || x == ((startLocation + endLocation) / 2) - 10)
            {
                tile_locations.Add(new Vector3Int(x, heightmap, 0));
            }

            for (int i = heightmap - 1; i >= 0; i--)
            {
                if (i > 90)
                {
                    grassMap.SetTile(new Vector3Int(x, i, 0), dirt_tile);
                }
                else
                {
                    grassMap.SetTile(new Vector3Int(x, i, 0), stone_tile);
                }
            }
        }
    }

    public int GenerateGrassSection(int startLocation, int endLocation, int heightmap)
    {
        for (int x = startLocation; x < endLocation; x++)
        {
            float roll = Random.Range(0f, 1f);

            //Up one to the terrain
            if (roll <= 0.2f && heightmap != max_height)
            {
                GrassRaise(ref x, ref heightmap);
            }
            //Down one to the terrain
            else if (roll >= 0.8f && heightmap != 0)
            {
                GrassLower(ref x, ref heightmap);
            }
            //Stay flat one to the terrain
            else
            {
                grassMap.SetTile(new Vector3Int(x, heightmap, 0), grass_tile);
                tile_locations.Add(new Vector3Int(x, heightmap, 0));

                for (int i = heightmap - 1; i >= 0; i--)
                {
                    if (i > 90)
                    {
                        grassMap.SetTile(new Vector3Int(x, i, 0), dirt_tile);
                    }
                    else
                    {
                        grassMap.SetTile(new Vector3Int(x, i, 0), stone_tile);
                    }
                }
            }
        }

        return heightmap;
    }

    public void GrassRaise(ref int x, ref int heightmap)
    {
        heightmap += 1;
        grassMap.SetTile(new Vector3Int(x, heightmap, 0), grass_tile_slope_up);
        grassMap.SetTile(new Vector3Int(x, heightmap - 1, 0), grass_slope_connector_up);
        tile_locations.Add(new Vector3Int(x, heightmap, 0));

        //-2 here because the connector needs to be added one below
        for (int i = heightmap - 2; i >= 0; i--)
        {
            if (i > 90)
            {
                grassMap.SetTile(new Vector3Int(x, i, 0), dirt_tile);
            }
            else
            {
                grassMap.SetTile(new Vector3Int(x, i, 0), stone_tile);
            }
        }

        x += 1;
        grassMap.SetTile(new Vector3Int(x, heightmap, 0), grass_tile);
        tile_locations.Add(new Vector3Int(x, heightmap, 0));

        for (int i = heightmap - 1; i >= 0; i--)
        {
            if (i > 90)
            {
                grassMap.SetTile(new Vector3Int(x, i, 0), dirt_tile);
            }
            else
            {
                grassMap.SetTile(new Vector3Int(x, i, 0), stone_tile);
            }
        }
    }

    public void GrassLower(ref int x, ref int heightmap)
    {
        grassMap.SetTile(new Vector3Int(x, heightmap, 0), grass_tile_slope_down);
        grassMap.SetTile(new Vector3Int(x, heightmap - 1, 0), grass_slope_connector_down);
        tile_locations.Add(new Vector3Int(x, heightmap, 0));

        //-2 here because the connector needs to be added one below
        for (int i = heightmap - 2; i >= 0; i--)
        {
            if (i > 90)
            {
                grassMap.SetTile(new Vector3Int(x, i, 0), dirt_tile);
            }
            else
            {
                grassMap.SetTile(new Vector3Int(x, i, 0), stone_tile);
            }
        }

        heightmap -= 1;
        x += 1;
        grassMap.SetTile(new Vector3Int(x, heightmap, 0), grass_tile);
        tile_locations.Add(new Vector3Int(x, heightmap, 0));

        for (int i = heightmap - 1; i >= 0; i--)
        {
            if (i > 90)
            {
                grassMap.SetTile(new Vector3Int(x, i, 0), dirt_tile);
            }
            else
            {
                grassMap.SetTile(new Vector3Int(x, i, 0), stone_tile);
            }
        }
    }

    public int GenerateSnowSection(int startLocation, int endLocation, int heightmap)
    {
        for (int x = startLocation; x < endLocation; x++)
        {
            float roll = Random.Range(0f, 1f);

            //Up one to the terrain
            if (roll <= 0.2f && heightmap != max_height)
            {
                SnowRaise(ref x, ref heightmap);
            }
            //Down one to the terrain
            else if (roll >= 0.8f && heightmap != 0)
            {
                SnowLower(ref x, ref heightmap);
            }
            //Stay flat one to the terrain
            else
            {
                snowMap.SetTile(new Vector3Int(x, heightmap, 0), snow_tile);
                tile_locations.Add(new Vector3Int(x, heightmap, 0));

                for (int i = heightmap - 1; i >= 0; i--)
                {
                    if (i > 90)
                    {
                        snowMap.SetTile(new Vector3Int(x, i, 0), dirt_tile);
                    }
                    else
                    {
                        snowMap.SetTile(new Vector3Int(x, i, 0), stone_tile);
                    }
                }
            }
        }

        return heightmap;
    }

    public void SnowRaise(ref int x, ref int heightmap)
    {
        heightmap += 1;
        snowMap.SetTile(new Vector3Int(x, heightmap, 0), snow_tile_slope_up);
        snowMap.SetTile(new Vector3Int(x, heightmap - 1, 0), snow_tile_connector_up);
        tile_locations.Add(new Vector3Int(x, heightmap, 0));

        //-2 here because the connector needs to be added one below
        for (int i = heightmap - 2; i >= 0; i--)
        {
            if (i > 90)
            {
                snowMap.SetTile(new Vector3Int(x, i, 0), dirt_tile);
            }
            else
            {
                snowMap.SetTile(new Vector3Int(x, i, 0), stone_tile);
            }
        }

        x += 1;
        snowMap.SetTile(new Vector3Int(x, heightmap, 0), snow_tile);
        tile_locations.Add(new Vector3Int(x, heightmap, 0));

        for (int i = heightmap - 1; i >= 0; i--)
        {
            if (i > 90)
            {
                snowMap.SetTile(new Vector3Int(x, i, 0), dirt_tile);
            }
            else
            {
                snowMap.SetTile(new Vector3Int(x, i, 0), stone_tile);
            }
        }
    }

    public void SnowLower(ref int x, ref int heightmap)
    {
        snowMap.SetTile(new Vector3Int(x, heightmap, 0), snow_tile_slope_down);
        snowMap.SetTile(new Vector3Int(x, heightmap - 1, 0), snow_tile_connector_down);
        tile_locations.Add(new Vector3Int(x, heightmap, 0));

        //-2 here because the connector needs to be added one below
        for (int i = heightmap - 2; i >= 0; i--)
        {
            if (i > 90)
            {
                snowMap.SetTile(new Vector3Int(x, i, 0), dirt_tile);
            }
            else
            {
                snowMap.SetTile(new Vector3Int(x, i, 0), stone_tile);
            }
        }

        heightmap -= 1;
        x += 1;
        snowMap.SetTile(new Vector3Int(x, heightmap, 0), snow_tile);
        tile_locations.Add(new Vector3Int(x, heightmap, 0));

        for (int i = heightmap - 1; i >= 0; i--)
        {
            if (i > 90)
            {
                snowMap.SetTile(new Vector3Int(x, i, 0), dirt_tile);
            }
            else
            {
                snowMap.SetTile(new Vector3Int(x, i, 0), stone_tile);
            }
        }
    }

    public int GenerateCaveSection(int startLocation, int endLocation, int heightmap)
    {
        int caveHeight = 10;
        int caveLocation = heightmap + caveHeight;

        //Move cave downward
        for (int x = startLocation; x < endLocation; x++)
        {
            CaveDown(ref x, ref heightmap, ref caveLocation, ref caveHeight);
        }

        //Bring cave to the surface
        for (int x = endLocation; x < endLocation+width; x++)
        {
            CaveUp(ref x, ref heightmap, ref caveLocation, ref caveHeight);
        }

        return caveLocation - caveHeight;
    }

    public void CaveUp(ref int x, ref int heightmap, ref int caveLocation, ref int caveHeight)
    {
        float roll = Random.Range(0f, 1f);

        //Up one to the terrain
        if (roll <= 0.2f && caveLocation != max_height)
        {
            CaveRaise(ref x, ref heightmap, ref caveLocation, ref caveHeight, false);
        }
        //Down one to the terrain
        else if (roll >= 0.8f && heightmap != 0)
        {
            CaveLower(ref x, ref heightmap, ref caveLocation, ref caveHeight, false);
        }
        //Stay flat one to the terrain
        else
        {
            grassMap.SetTile(new Vector3Int(x, heightmap + caveOffset, 0), grass_tile);
            grassMap.SetTile(new Vector3Int(x, heightmap + caveOffset - 1, 0), dirt_tile);
            tile_locations.Add(new Vector3Int(x, heightmap + caveOffset, 0));

            //Generate Cave
            if (caveLocation != 0)
            {
                CaveCutout(ref x, ref heightmap, ref caveLocation, ref caveHeight, false);
            }
            //Stay flat one to the terrain
            else
            {
                grassMap.SetTile(new Vector3Int(x, heightmap + 15, 0), grass_tile);
                tile_locations.Add(new Vector3Int(x, heightmap + 15, 0));

                for (int i = heightmap + caveOffset - 1; i >= 0; i--)
                {
                    grassMap.SetTile(new Vector3Int(x, i, 0), dirt_tile);
                }
            }
        }
    }

    public void CaveDown(ref int x, ref int heightmap, ref int caveLocation, ref int caveHeight)
    {
        float roll = Random.Range(0f, 1f);

        //Up one to the terrain
        if (roll <= 0.2f && caveLocation != max_height)
        {
            CaveRaise(ref x, ref heightmap, ref caveLocation, ref caveHeight, true);
        }
        //Down one to the terrain
        else if (roll >= 0.8f && heightmap != 0)
        {
            CaveLower(ref x, ref heightmap, ref caveLocation, ref caveHeight, true);
        }
        //Stay flat one to the terrain
        else
        {
            grassMap.SetTile(new Vector3Int(x, heightmap + caveOffset, 0), grass_tile);
            grassMap.SetTile(new Vector3Int(x, heightmap + caveOffset - 1, 0), dirt_tile);
            tile_locations.Add(new Vector3Int(x, heightmap + caveOffset, 0));

            //Generate Cave
            if (caveLocation != 0)
            {
                CaveCutout(ref x, ref heightmap, ref caveLocation, ref caveHeight, true);
            }
            //Stay flat one to the terrain
            else
            {
                grassMap.SetTile(new Vector3Int(x, heightmap + caveOffset, 0), grass_tile);
                tile_locations.Add(new Vector3Int(x, heightmap + caveOffset, 0));

                for (int i = heightmap + caveOffset - 1; i >= 0; i--)
                {
                    grassMap.SetTile(new Vector3Int(x, i, 0), dirt_tile);
                }
            }
        }
    }

    public void CaveRaise(ref int x, ref int heightmap, ref int caveLocation, ref int caveHeight, bool isDown)
    {
        heightmap += 1;
        grassMap.SetTile(new Vector3Int(x, heightmap + caveOffset, 0), grass_tile_slope_up);
        grassMap.SetTile(new Vector3Int(x, heightmap + caveOffset - 1, 0), grass_slope_connector_up);
        tile_locations.Add(new Vector3Int(x, heightmap + caveOffset, 0));

        //Generate Cave
        if (caveLocation != 0)
        {
            CaveCutout(ref x, ref heightmap, ref caveLocation, ref caveHeight, isDown);
        }
        //Stay flat one to the terrain
        else
        {
            grassMap.SetTile(new Vector3Int(x, heightmap + caveOffset, 0), grass_tile);
            tile_locations.Add(new Vector3Int(x, heightmap + caveOffset, 0));

            for (int i = heightmap + caveOffset - 1; i >= 0; i--)
            {
                grassMap.SetTile(new Vector3Int(x, i, 0), dirt_tile);
            }
        }

        x += 1;
        grassMap.SetTile(new Vector3Int(x, heightmap + caveOffset, 0), grass_tile);
        grassMap.SetTile(new Vector3Int(x, heightmap + caveOffset - 1, 0), dirt_tile);
        tile_locations.Add(new Vector3Int(x, heightmap + caveOffset, 0));

        //Generate Cave
        if (caveLocation != 0)
        {
            CaveCutout(ref x, ref heightmap, ref caveLocation, ref caveHeight, isDown);
        }
        //Stay flat one to the terrain
        else
        {
            grassMap.SetTile(new Vector3Int(x, heightmap + caveOffset, 0), grass_tile);
            tile_locations.Add(new Vector3Int(x, heightmap + caveOffset, 0));

            for (int i = heightmap + caveOffset - 1; i >= 0; i--)
            {
                grassMap.SetTile(new Vector3Int(x, i, 0), dirt_tile);
            }
        }
    }

    public void CaveLower(ref int x, ref int heightmap, ref int caveLocation, ref int caveHeight, bool isDown)
    {
        grassMap.SetTile(new Vector3Int(x, heightmap + caveOffset, 0), grass_tile_slope_down);
        grassMap.SetTile(new Vector3Int(x, heightmap + caveOffset - 1, 0), grass_slope_connector_down);
        tile_locations.Add(new Vector3Int(x, heightmap + caveOffset, 0));

        //Generate Cave
        if (caveLocation != 0)
        {
            CaveCutout(ref x, ref heightmap, ref caveLocation, ref caveHeight, isDown);
        }
        //Stay flat one to the terrain
        else
        {
            grassMap.SetTile(new Vector3Int(x, heightmap + caveOffset, 0), grass_tile);
            tile_locations.Add(new Vector3Int(x, heightmap + caveOffset, 0));

            for (int i = heightmap + caveOffset - 1; i >= 0; i--)
            {
                grassMap.SetTile(new Vector3Int(x, i, 0), dirt_tile);
            }
        }

        heightmap -= 1;
        x += 1;
        grassMap.SetTile(new Vector3Int(x, heightmap + caveOffset, 0), grass_tile);
        grassMap.SetTile(new Vector3Int(x, heightmap + caveOffset - 1, 0), dirt_tile);
        tile_locations.Add(new Vector3Int(x, heightmap + caveOffset, 0));

        //Generate Cave
        if (caveLocation != 0)
        {
            CaveCutout(ref x, ref heightmap, ref caveLocation, ref caveHeight, isDown);
        }
        //Stay flat one to the terrain
        else
        {
            grassMap.SetTile(new Vector3Int(x, heightmap + caveOffset, 0), grass_tile);
            tile_locations.Add(new Vector3Int(x, heightmap + caveOffset, 0));

            for (int i = heightmap + caveOffset - 1; i >= 0; i--)
            {
                grassMap.SetTile(new Vector3Int(x, i, 0), dirt_tile);
            }
        }
    }

    public void CaveCutout(ref int x, ref int heightmap, ref int caveLocation, ref int caveHeight, bool isDown)
    {
        for (int i = (heightmap + caveOffset) - 2; i > caveLocation || i > 0; i--)
        {
            if (i > 90)
            {
                grassMap.SetTile(new Vector3Int(x, i, 0), dirt_tile);
            }
            else
            {
                grassMap.SetTile(new Vector3Int(x, i, 0), stone_tile);
            }
        }

        for (int i = caveLocation; i >= caveLocation - caveHeight; i--)
        {
            grassMap.SetTile(new Vector3Int(x, i, 0), null);
        }

        for (int i = caveLocation - caveHeight - 1; i >= 0; i--)
        {
            if (i > 90)
            {
                grassMap.SetTile(new Vector3Int(x, i, 0), dirt_tile);
            }
            else
            {
                grassMap.SetTile(new Vector3Int(x, i, 0), stone_tile);
            }
        }

        if (isDown)
        {
            float roll = Random.Range(0f, 1f);
            if (roll <= 0.2f)
            {
                caveLocation -= 1;
            }
        }
        else
        {
            float roll = Random.Range(0f, 1f);
            if (roll <= 0.2f)
            {
                caveLocation += 1;
            }
        }
    }

    public int GenerateWaterLocation(int startLocation, int endLocation, int heightmap)
    {
        int lakeLocation = heightmap;
        int lakeHeight = heightmap;

        for (int x = startLocation; x < endLocation; x++)
        {
            WaterDown(ref x, ref lakeHeight, ref heightmap);
        }

        for (int x = endLocation; x < endLocation + width; x++)
        {
            WaterUp(ref x, ref lakeHeight, ref heightmap);
        }

        return lakeLocation;
    }

    public void WaterUp(ref int x, ref int lakeHeight, ref int heightmap)
    {
        float roll = Random.Range(0f, 1f);

        if (roll <= 0.2)
        {
            lakeHeight += Random.Range(0, 4);

            for (int i = heightmap; i >= 0; i--)
            {
                if (i > lakeHeight)
                {
                    waterMap.SetTile(new Vector3Int(x, i, 0), water_tile);
                }
                else
                {
                    if(i == heightmap)
                    {
                        grassMap.SetTile(new Vector3Int(x, i, 0), grass_tile);
                    }
                    else
                    {
                        grassMap.SetTile(new Vector3Int(x, i, 0), dirt_tile);
                    }
                }
            }
        }
        else
        {
            for (int i = heightmap; i >= 0; i--)
            {
                if (i > lakeHeight)
                {
                    waterMap.SetTile(new Vector3Int(x, i, 0), water_tile);
                }
                else
                {
                    if (i == heightmap)
                    {
                        grassMap.SetTile(new Vector3Int(x, i, 0), grass_tile);
                    }
                    else
                    {
                        grassMap.SetTile(new Vector3Int(x, i, 0), dirt_tile);
                    }
                }
            }
        }
    }

    public void WaterDown(ref int x, ref int lakeHeight, ref int heightmap)
    {
        float roll = Random.Range(0f, 1f);

        if (roll <= 0.2)
        {
            lakeHeight -= Random.Range(0, 4);

            for (int i = heightmap; i >= 0; i--)
            {
                if (i > lakeHeight)
                {
                    waterMap.SetTile(new Vector3Int(x, i, 0), water_tile);
                }
                else
                {
                    if (i == heightmap)
                    {
                        grassMap.SetTile(new Vector3Int(x, i, 0), grass_tile);
                    }
                    else
                    {
                        grassMap.SetTile(new Vector3Int(x, i, 0), dirt_tile);
                    }
                }
            }
        }
        else
        {
            for (int i = heightmap; i >= 0; i--)
            {
                if (i > lakeHeight)
                {
                    waterMap.SetTile(new Vector3Int(x, i, 0), water_tile);
                }
                else
                {
                    if (i == heightmap)
                    {
                        grassMap.SetTile(new Vector3Int(x, i, 0), grass_tile);
                    }
                    else
                    {
                        grassMap.SetTile(new Vector3Int(x, i, 0), dirt_tile);
                    }
                }
            }
        }
    }

    public void SpawnPoints()
    { 
        Vector3 first_tile = tile_locations[0];
        Vector3 spawn_tile = new Vector3(first_tile.x, first_tile.y + 10, 0);
        Instantiate(player_spawn, spawn_tile / 2, Quaternion.identity, this.transform); //Divided by two because the cell size is 0.5

        Vector3 last_tile = tile_locations[tile_locations.Count - 1];
        Vector3 spawn_rope = new Vector3(last_tile.x, last_tile.y + 60, 0);
        GameObject.Find("Character").GetComponent<MissionLog>().rappelRope = Instantiate(rappel_rope, spawn_rope/2, Quaternion.identity, this.transform);
        GameObject.Find("Character").GetComponent<MissionLog>().rappelRope.SetActive(false);

        Vector3 second_to_last_tile = tile_locations[tile_locations.Count - 2];
        Vector3 spawn_chest = new Vector3(second_to_last_tile.x, second_to_last_tile.y + 2, 0);
        GameObject.Find("Character").GetComponent<MissionLog>().chest = Instantiate(chest, spawn_chest / 2, Quaternion.identity, this.transform);
        GameObject.Find("Character").GetComponent<MissionLog>().chest.SetActive(false);
    }

    public virtual void SpawnBoss(GameObject bossSpawn)
    {

    }

    public virtual int GenerateBossSection(int startLocation, int endLocation, int heightmap)
    {
        return 0;
    }
}