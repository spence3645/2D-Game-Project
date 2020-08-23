using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ZoneGenerator : MonoBehaviour
{
    Tilemap grass;
    Tilemap snow;
    Tilemap foliage;

    public Tile grass_tile;
    public Tile grass_slope_up;
    public Tile grass_slope_down;
    public Tile grass_connector_up;
    public Tile grass_connector_down;

    public Tile snow_tile;
    public Tile snow_slope_up;
    public Tile snow_slope_down;
    public Tile snow_connector_up;
    public Tile snow_connector_down;

    public Tile bloody_tile;
    public Tile bloody_tile_top;

    public Tile tree_stump;
    public Tile tree_body;

    public Tile dirt_tile;

    public GameObject player_spawn;
    public GameObject rappel_rope;
    public GameObject chest;

    public List<Vector3Int> grassTileLocations = new List<Vector3Int>();

    int minX = -400;
    int maxX = 400;
    int minY = -10;
    int maxY = 2;

    int caveHeight = 25;

    int undergroundHeight = 100;

    PerlinNoise perlin;

    // Start is called before the first frame update
    void Awake()
    {
        if (GameObject.Find("Grass"))
        {
            grass = GameObject.Find("Grass").GetComponent<Tilemap>();
        }
        if (GameObject.Find("Snow"))
        {
            snow = GameObject.Find("Snow").GetComponent<Tilemap>();
        }
        if (GameObject.Find("Foliage"))
        {
            foliage = GameObject.Find("Foliage").GetComponent<Tilemap>();
        }

        perlin = new PerlinNoise(Random.Range(100000, 1000000000));

        //caveMinX = minX / 3;
        //caveMaxX = maxX / 2;
        //caveMinY = undergroundHeight / -2;
        //caveMaxY = undergroundHeight / -3;
    }

    public virtual void Regenerate()
    {
        
    }

    public void GrassPerlin()
    {
        for (int x = minX; x < maxX; x++)
        {
            int columnHeight = perlin.GetNoise(x - minX, maxY - minY);

            for (int y = minY - undergroundHeight; y < minY + columnHeight; y++)
            {
                if (y == minY + columnHeight - 1)
                {
                    grass.SetTile(new Vector3Int(x, y, 0), grass_tile);
                    grassTileLocations.Add(new Vector3Int(x, y, 0));
                }
                else
                {
                    grass.SetTile(new Vector3Int(x, y, 0), dirt_tile);
                }
            }
        }

        FixGrassTerrain();
        GenerateTrees();
        SpawnPoints();
    }

    public void SnowPerlin()
    {
        for (int x = minX; x < maxX; x++)
        {
            int columnHeight = perlin.GetNoise(x - minX, maxY - minY);

            for (int y = minY - undergroundHeight; y < minY + columnHeight; y++)
            {
                if (y == minY + columnHeight - 1)
                {
                    snow.SetTile(new Vector3Int(x, y, 0), snow_tile);
                    grassTileLocations.Add(new Vector3Int(x, y, 0));
                }
                else
                {
                    snow.SetTile(new Vector3Int(x, y, 0), dirt_tile);
                }
            }
        }

        FixSnowTerrain();
        SpawnPoints();
    }

    public void CavePerlin()
    {
        for (int x = minX; x < maxX; x++)
        {
            int columnHeight = perlin.GetNoise(x - minX, maxY - minY);

            for (int y = minY - undergroundHeight; y < minY + columnHeight; y++)
            {
                if (y == minY + columnHeight - 1)
                {
                    grass.SetTile(new Vector3Int(x, y, 0), bloody_tile_top);
                    grassTileLocations.Add(new Vector3Int(x, y, 0));
                }
                else
                {
                    grass.SetTile(new Vector3Int(x, y, 0), bloody_tile);
                }
            }

            for (int y = (minY + columnHeight) + caveHeight; y < (minY + columnHeight + caveHeight) + 100; y++)
            {
                grass.SetTile(new Vector3Int(x, y, 0), bloody_tile);
            }

            for (int y = minY + columnHeight; y < (minY + columnHeight) + caveHeight; y++)
            {
                grass.SetTile(new Vector3Int(x, y, 0), null);
            }
        }

        SpawnPoints();
    }

    private void FixGrassTerrain()
    {
        foreach(Vector3Int location in grassTileLocations)
        {
            TileBase r_tile = grass.GetTile(new Vector3Int(location.x + 1, location.y, 0));
            TileBase l_tile = grass.GetTile(new Vector3Int(location.x - 1, location.y, 0));

            if (r_tile && !l_tile)
            {
                grass.SetTile(location, grass_slope_up);
                grass.SetTile(new Vector3Int(location.x, location.y - 1, 0), grass_connector_up);
            }
            else if (!r_tile && l_tile)
            {
                grass.SetTile(location, grass_slope_down);
                grass.SetTile(new Vector3Int(location.x, location.y - 1, 0), grass_connector_down);
            }
        }
    }

    private void FixSnowTerrain()
    {
        foreach (Vector3Int location in grassTileLocations)
        {
            TileBase r_tile = snow.GetTile(new Vector3Int(location.x + 1, location.y, 0));
            TileBase l_tile = snow.GetTile(new Vector3Int(location.x - 1, location.y, 0));

            if (r_tile && !l_tile)
            {
                snow.SetTile(location, snow_slope_up);
                snow.SetTile(new Vector3Int(location.x, location.y - 1, 0), snow_connector_up);
            }
            else if (!r_tile && l_tile)
            {
                snow.SetTile(location, snow_slope_down);
                snow.SetTile(new Vector3Int(location.x, location.y - 1, 0), snow_connector_down);
            }
        }
    }

    private void GenerateTrees()
    {
        foreach(Vector3Int location in grassTileLocations)
        {
            float roll = Random.Range(0f, 1f);
            if(roll <= 0.05f && grass.GetTile(location).name == "grass_block")
            {
                CreateTree(location);
            }
            else
            {
                //Do nothing
            }
        }
    }

    private void CreateTree(Vector3Int location)
    {
        int treeHeight = Random.Range(15, 20);
        int currentHeight = location.y;

        for (int i = 0; i < treeHeight; i++)
        {
            if(i == 0)
            {
                currentHeight++;
                foliage.SetTile(new Vector3Int(location.x, currentHeight, 0), tree_stump);
            }
            else
            {
                currentHeight++;
                foliage.SetTile(new Vector3Int(location.x, currentHeight, 0), tree_body);
            }
        }
    }

    public void SpawnPoints()
    {
        Vector3 first_tile = grassTileLocations[0];
        Vector3 spawn_tile = new Vector3(first_tile.x, first_tile.y + 10, 0);
        Instantiate(player_spawn, spawn_tile / 2, Quaternion.identity, this.transform); //Divided by two because the cell size is 0.5

        Vector3 last_tile = grassTileLocations[grassTileLocations.Count - 1];
        Vector3 spawn_rope = new Vector3(last_tile.x, last_tile.y + 60, 0);
        GameObject.Find("Character").GetComponent<MissionLog>().rappelRope = Instantiate(rappel_rope, spawn_rope / 2, Quaternion.identity, this.transform);
        GameObject.Find("Character").GetComponent<MissionLog>().rappelRope.SetActive(false);

        Vector3 second_to_last_tile = grassTileLocations[grassTileLocations.Count - 2];
        Vector3 spawn_chest = new Vector3(second_to_last_tile.x, second_to_last_tile.y + 2, 0);
        GameObject.Find("Character").GetComponent<MissionLog>().chest = Instantiate(chest, spawn_chest / 2, Quaternion.identity, this.transform);
        GameObject.Find("Character").GetComponent<MissionLog>().chest.SetActive(false);
    }
}
