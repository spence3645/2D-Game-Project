using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Workbench : MonoBehaviour
{

    bool isColliding = false;
    bool isBuilding = false;
    bool isPlacing = false;

    Camera mainCamera;
    Camera workshopCamera;

    Tilemap buildingMap; //Used as a placement grid
    Tilemap map;

    Vector3 mousePosition;
    Vector3Int previousCell;
    Vector3Int cellPosition;

    public Tile[] blockInventory;
    Tile equippedBlock;

    SaveTiles baseTile; //Tile used to find relative positions

    public List<SaveTiles> tileCreation = new List<SaveTiles>();
    List<SaveTiles> relativeTileCreation = new List<SaveTiles>(); //List that holds information in tileCreation but with relative locations

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        workshopCamera = GameObject.Find("Workshop Camera").GetComponent<Camera>();
        buildingMap = GameObject.Find("Building").GetComponent<Tilemap>();
        map = GameObject.Find("Map").GetComponent<Tilemap>();

        equippedBlock = blockInventory[0];

        workshopCamera.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        EnableWorkshop();

        if (isBuilding)
        {
            WorkshopMode();
        }
        if (isPlacing)
        {
            PlaceCreation();
        }
    }

    void EnableWorkshop()
    {
        //Check if player is standing at workshop and pressing E
        if (Input.GetKeyDown(KeyCode.E) && isColliding)
        {
            mainCamera.enabled = false;
            workshopCamera.enabled = true;
            isBuilding = true;

            tileCreation.Clear(); //Clear array so each creation is different
            relativeTileCreation.Clear();
        }
        //Press R to exit workshop mode and gets relative locations
        else if (Input.GetKeyDown(KeyCode.R))
        {
            //Clear all tiles used during workshop when exiting
            foreach (SaveTiles save in tileCreation)
            {
                map.SetTile(save.tileLocation, null);
            }

            GetRelativeLocation();

            mainCamera.enabled = true;
            workshopCamera.enabled = false;

            buildingMap.SetTile(cellPosition, null); //When exiting workshop mode, there is no blocks left
            isBuilding = false;
            isPlacing = false;
        }
        //Test case for placing creation, temporary if statement
        else if (Input.GetKeyDown(KeyCode.T))
        {
            mainCamera.enabled = false;
            workshopCamera.enabled = true;

            isPlacing = true;
        }
    }

    void WorkshopMode()
    {
        //Find mouse position and convert that to the cell on Tilemap
        if (workshopCamera.enabled == true)
        {
            mousePosition = workshopCamera.ScreenToWorldPoint(Input.mousePosition);
        }

        cellPosition = buildingMap.WorldToCell(mousePosition);

        if (Input.GetMouseButton(0))
        {
            map.SetTile(cellPosition, equippedBlock); //Add a new tile to a seperate Tilemap

            //Creating an array of tiles and their positions to be copied and pasted later
            SaveTiles save = SaveTiles.CreateInstance(map.GetTile(cellPosition), cellPosition);
            tileCreation.Add(save);
        }

        //Keep the cursor updated to the current tile
        else if (cellPosition != previousCell)
        {
            buildingMap.SetTile(cellPosition, equippedBlock);

            buildingMap.SetTile(previousCell, null);

            previousCell = cellPosition;
        }
    }

    //Enables placement of creation
    void PlaceCreation()
    {
        //Find mouse position and convert that to the cell on Tilemap
        if (workshopCamera.enabled == true)
        {
            mousePosition = workshopCamera.ScreenToWorldPoint(Input.mousePosition);
        }

        cellPosition = buildingMap.WorldToCell(mousePosition);
        baseTile.tileLocation = cellPosition;

        if (Input.GetMouseButton(0))
        {
            foreach(SaveTiles tiles in relativeTileCreation)
            {
                Vector3Int relativeLoc = new Vector3Int(baseTile.tileLocation.x - tiles.tileLocation.x, baseTile.tileLocation.y - tiles.tileLocation.y, baseTile.tileLocation.z + tiles.tileLocation.z);
                map.SetTile(relativeLoc, tiles.tile);
            }
        }

        //Keep the cursor updated to the current tile
        else if (cellPosition != previousCell)
        {
            buildingMap.SetTile(cellPosition, baseTile.tile);

            buildingMap.SetTile(previousCell, null);

            previousCell = cellPosition;
        }
    }

    //Gets the relative location of all the tiles based on the baseTile
    void GetRelativeLocation()
    {
        baseTile = GetLowestRelativeY(tileCreation);

        foreach (SaveTiles tiles in tileCreation)
        {
            Debug.Log(tiles.tileLocation + "" + baseTile.tileLocation + "" + (baseTile.tileLocation.x - tiles.tileLocation.x));
            Vector3Int relativeLoc = new Vector3Int(baseTile.tileLocation.x - tiles.tileLocation.x, baseTile.tileLocation.y - tiles.tileLocation.y, baseTile.tileLocation.z - tiles.tileLocation.z);
            SaveTiles save = SaveTiles.CreateInstance(tiles.tile, relativeLoc);
            relativeTileCreation.Add(save);
        }
    }

    //Find the lowest block in the creation to use as placement
    SaveTiles GetLowestRelativeY(List<SaveTiles> saveTiles)
    {
        SaveTiles lowestRelativeY = null;

        foreach(SaveTiles tiles in saveTiles)
        {
            if (lowestRelativeY == null)
            {
                lowestRelativeY = tiles;
            }
            else if (lowestRelativeY.tileLocation.y > tiles.tileLocation.y)
            {
                lowestRelativeY = tiles;
            }
        }
        return lowestRelativeY;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            isColliding = true;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        isColliding = false;
    }
}
