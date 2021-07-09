using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Walker
{
    public Vector2 position;
    public Vector2 direction;
}

public class Room
{
    public int width;
    public int height;
}

public class WorldManager : MonoBehaviour
{
    public int mapWidth;
    public int mapHeight;

    public int roomNumber = 4;

    public int walkerCount = 10;
    public int walkerSteps = 400;

    public Tilemap groundTilemap;
    public Tilemap onTopOfGroundTilemap;

    public TileBase[] groundTiles;
    public TileBase[] wallTiles;

    void Start()
    {
        for (int i = 0; i < roomNumber; i++)
        {
            var randX = Random.Range(3, 15);
            var randY = Random.Range(3, 15);

            int roomSizeX = (randX * (i)) + randX;
            int roomSizeY = (randY * (i)) + randY;

            CreateSquareRoom(roomSizeX / 2, roomSizeY / 2, randX, randY);
        }
        //CreateCircleRoom(10,10,10);
        //Create our initial canvas
        for (int k = 0; k < walkerCount; k++)
        {
            Walker walker = new Walker();
            walker.position = new Vector2(0,0);//new Vector2(mapWidth / 2, mapHeight / 2);
            
            for (int i = 0; i < walkerSteps; i++)
            {
                //Get a random direction
                int direction = Random.Range(0, 3);
                switch (direction)
                {
                    case 0:
                        walker.position = walker.position + Vector2.up;
                        break;
                    case 1:
                        walker.position = walker.position + Vector2.left;
                        break;
                    case 2:
                        walker.position = walker.position + Vector2.right;
                        break;
                    case 3:
                        walker.position = walker.position + Vector2.down;
                        break;
                }
                groundTilemap.SetTile(groundTilemap.WorldToCell(new Vector3(walker.position.x, walker.position.y, 0)), groundTiles[Random.Range(0, groundTiles.Length - 1)]);
            }
        }
    }

    void CreateSquareRoom(int centerX, int centerY, int width, int height)
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                var dX = j - centerX;
                var dY = i - centerY;

                groundTilemap.SetTile(groundTilemap.WorldToCell(new Vector3(dX, dY, 0)), groundTiles[Random.Range(0, groundTiles.Length - 1)]);
            }
        }
    }

    void CreateCircleRoom(int centerX, int centerY, int radius)
    {
        for (int y = 0; y < 25; y++)
        {
            for (int x = 0; x < 25; x++)
            {
                var dX = x - centerX;
                var dY = y - centerY;

                if (dX * dX + dY * dY < (radius * radius))
                {
                    groundTilemap.SetTile(groundTilemap.WorldToCell(new Vector3(dX, dY, 0)), groundTiles[Random.Range(0, groundTiles.Length - 1)]);
                }
            }
        }
    }
}

