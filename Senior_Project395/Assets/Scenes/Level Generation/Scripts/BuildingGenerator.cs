using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingGenerator : MonoBehaviour
{
    public GameObject ground;
    public GameObject doorPrefab;
    public float buildingWidth;
    public float buildingDepth;
    public int levels;
    public float ceilingHeight;
    public float wallThickness;
    public float floorThickness;
    public float minSplittableRoomDimensionSize;
    public float maxSplitAttempts;
    public int MaxInteriorWalls;
    public float minWallLength;
    public float doorWidth;
    public float doorWallClearance;
    public float doorThickness;
    

    private GameObject buildingParent;
    private Vector3 buildingPos;
    private List<LevelMap> levelMaps;
    // Start is called before the first frame update
    void Start()
    {
        buildingParent = new GameObject();
        buildingParent.name = "Building";
        buildingParent.transform.position = ground.transform.position;
        buildingPos = buildingParent.transform.position;
        levelMaps = new List<LevelMap>(levels);

        for(int i = 0; i < levels; i++)
        {
            LevelMap map = new LevelMap(minSplittableRoomDimensionSize);
            levelMaps.Add(map);

            //DESIGN MAP
            map.walls.AddRange(ExteriorWalls());
            //need a function to work with rooms and walls, and return Walls
            map.walls.AddRange(InteriorWalls(map));

            //BUILD MAP
            int currentLevel = i;
            GenerateLevel(map, currentLevel);
        }
    }


    private List<Wall> ExteriorWalls()
    {
        List<Wall> extWalls = new List<Wall>();

        int[,] midpoints = { { -1, 0, 1}, { 0, 1, 0}, { 1, 0, 1}, { 0, -1, 0} };

        for(int i = 0; i < midpoints.GetLength(0); i++)
        {
                extWalls.Add(new Wall(
                null,
                new Vector2(buildingPos.z + (midpoints[i,0]) * ((buildingWidth / 2f)-(wallThickness/2f)), buildingPos.x + (midpoints[i, 1]) * ((buildingDepth / 2f) - (wallThickness / 2f))),
                (midpoints[i, 2]*90),
                midpoints[i, 2] == 0 ? buildingWidth : buildingDepth
                )
            );
            
        }

        //new Vector2(buildingPos.z - (buildingWidth / 2f), buildingPos.x - (buildingDepth / 2f)),
        return extWalls;
    }

    private List<Wall> InteriorWalls(LevelMap map)
    {
        map.rooms.Add(new Room(new Vector2(0, 0), buildingWidth, buildingDepth, UnityEngine.Random.Range(0.0f, 1.0f) > 0.5f));
        List<Wall> interiorWalls = new List<Wall>();

        while (map.rooms.Count > 0 && interiorWalls.Count < MaxInteriorWalls)
        {

            if (map.rooms[0].isBigEnoughToSplit(minSplittableRoomDimensionSize))
            {
                Wall newWall = splitWithWall(map, interiorWalls, 0);
                if (newWall != null)
                {
                    interiorWalls.Add(newWall);
                }
            }
            else {
                map.rooms.RemoveAt(0);
            }
        }
        return interiorWalls;
    }

    private Wall splitWithWall(LevelMap map, List<Wall> existingWalls, int roomNum)
    {
        List<Room> rooms = map.rooms; 
        Room originalRoom = rooms[roomNum];
        float originalTop = originalRoom.pos.y + (originalRoom.depth / 2.0f); //y coord
        float originalBottom = originalRoom.pos.y - (originalRoom.depth / 2.0f); //y coord
        float originalLeft = originalRoom.pos.x - (originalRoom.width / 2.0f); //x coord
        float originalRight = originalRoom.pos.x + (originalRoom.width / 2.0f); //x coord

        //split the room into two random pieces
        //creating a wall
        //remove the old room
        //then add the newly created rooms onto LevelMap.rooms
        Vector2 splitOrigin;
        Wall splitWall;
        int splitAttempts = 0;
        if (!originalRoom.shouldSplitVert)
        {
            //HORIZONTAL SPLIT
            do{
                splitAttempts++;
                splitOrigin = new Vector2(originalRoom.pos.x, UnityEngine.Random.Range(originalBottom + minWallLength, originalTop - minWallLength));
                splitWall = new Wall(null, splitOrigin, 0, originalRoom.width);
            } while (isWallCollidingWithDoors(existingWalls, splitWall) && splitAttempts <= maxSplitAttempts);

            if(splitAttempts > maxSplitAttempts)
            {
                Debug.Log("couldn't split room without hitting door");
                rooms.Remove(originalRoom);
                return null;
            }
            //add bottom
            rooms.Add(new Room(
                new Vector2(originalRoom.pos.x, (splitOrigin.y + originalBottom)/2.0f), //center
                originalRoom.width, //width
                splitOrigin.y - originalBottom, //depth
                true
                )
            );
            //add top
            rooms.Add(new Room(
                new Vector2(originalRoom.pos.x, (splitOrigin.y + originalTop) / 2.0f), //center
                originalRoom.width, //width
                originalTop - splitOrigin.y, //depth 
                true
                )
            );
        }
        else
        {
            //VERTICAL SPLIT
            do
            {
                splitAttempts++;
                splitOrigin = new Vector2(UnityEngine.Random.Range(originalLeft + minWallLength, originalRight - minWallLength),originalRoom.pos.y);
                splitWall = new Wall(null, splitOrigin, 90, originalRoom.depth);
            } while (isWallCollidingWithDoors(existingWalls, splitWall) && splitAttempts <= maxSplitAttempts);
            //add left
            rooms.Add(new Room(
                new Vector2((splitOrigin.x + originalLeft) / 2.0f, originalRoom.pos.y), //center
                splitOrigin.x - originalLeft, //width
                originalRoom.depth, //depth
                false
                )
            );
            //add right
            rooms.Add(new Room(
                new Vector2((splitOrigin.x + originalRight) / 2.0f, originalRoom.pos.y), //center
                originalRight - splitOrigin.x, //width
                originalRoom.depth,  //depth 
                false
                )
            );
        }
        rooms.Remove(originalRoom);
        splitWall.doors.Add(UnityEngine.Random.Range(doorWallClearance + (doorWidth / 2.0f), splitWall.length - doorWallClearance - (doorWidth / 2.0f)));
        return splitWall;
    }

    private bool isWallCollidingWithDoors(List<Wall> existingWalls, Wall newWall)
    {
        foreach(Wall existingWall in existingWalls)
        {
            foreach (float door in existingWall.doors)
            {
                //find door center
                //wall pos + door(in X or Y depending on dir)
                //if wall.start or wall.end is within doorWidth/2 radius of door center, return false

                Vector2 doorCenter;
                if(existingWall.direction == 90)
                {
                    //this is vertical wall, travel along y axis
                    doorCenter = existingWall.pos + new Vector2(0, door - (existingWall.length / 2f));
                }
                else
                {
                    //this is horizontal wall, travel along x axis
                    doorCenter = existingWall.pos + new Vector2(door - (existingWall.length / 2f), 0);
                }

                Vector2 newWallStartCoord;
                Vector2 newWallEndCoord;
                if (newWall.direction == 90)
                {
                    //this is vertical wall, travel along y axis
                    newWallStartCoord = newWall.pos - new Vector2(0, (newWall.length/2f));
                    newWallEndCoord = newWall.pos + new Vector2(0, (newWall.length/2f));
                }
                else
                {
                    //this is horizontal wall, travel along x axis
                    newWallStartCoord = newWall.pos - new Vector2((newWall.length / 2f),0);
                    newWallEndCoord = newWall.pos + new Vector2((newWall.length / 2f),0);
                }

                if (
                    Vector2.Distance(newWallStartCoord, doorCenter) < ((doorWidth / 2f) + doorWallClearance)
                    ||
                    Vector2.Distance(newWallEndCoord, doorCenter) < ((doorWidth / 2f) + doorWallClearance)
                    )
                {
                    //this wall collides, tell it to pick another one!
                    return true;
                }
            }
        }

        return false;
    }

    private void GenerateLevel(LevelMap map, int levelNum)
    {
        GameObject level = new GameObject();
        level.name = "Level" + levelNum;
        level.transform.parent = buildingParent.transform;
        level.transform.Translate(new Vector3(0,(float)levelNum) * (ceilingHeight + floorThickness),0);

        GenerateFloor(levelNum, level);

        foreach (Wall w in map.walls)
        {
            GenerateWall(w, levelNum, level);
        }
    }

    private void GenerateFloor(int levelNum, GameObject level)
    {
        GameObject floorbox = GameObject.CreatePrimitive(PrimitiveType.Cube);
        floorbox.name = level.name + "_" + levelNum;
        floorbox.transform.parent = level.transform;
        floorbox.transform.localPosition = new Vector3();
        floorbox.transform.localScale = new Vector3(buildingWidth, floorThickness, buildingDepth);
    }

    private void GenerateWall(Wall wallDef, int levelNum, GameObject level)
    {
        List<float[]> wallPieces = new List<float[]>();//{[start][stop], [start][stop], [start][stop]}
        List<float[]> doorPieces = new List<float[]>();

        float wallDistanceCovered = 0; //will always keep track of the starting point for the next wall
        wallDef.doors.Sort();
        foreach (float d in wallDef.doors)
        {
            float doorStart = d - (doorWidth / 2.0f);//relative to whole wall
            float doorStop = d + (doorWidth / 2.0f); //relative to whole wall

            wallPieces.Add(new float[2] {wallDistanceCovered, doorStart });
            doorPieces.Add(new float[2] { doorStart, doorStop });
            wallDistanceCovered = doorStop;

        }


        wallPieces.Add(new float[2] { wallDistanceCovered, wallDef.length }); // add the rest of the wall

        foreach(float[] wallPiece in wallPieces) {
            GameObject wall = GameObject.CreatePrimitive(PrimitiveType.Cube);
            wall.transform.parent = level.transform;
            if(wallDef.direction == 90)
            {
                wall.transform.position = new Vector3(
                    wallDef.pos.x,
                    (ceilingHeight / 2) + (ceilingHeight * (float)levelNum),
                    wallDef.pos.y - (wallDef.length / 2f) + ((wallPiece[0] + wallPiece[1]) / 2.0f)
                );
            }
            else //dir = 0
            {
                wall.transform.position = new Vector3(
                    wallDef.pos.x - (wallDef.length / 2f) + ((wallPiece[0] + wallPiece[1]) / 2.0f),
                    (ceilingHeight / 2) + (ceilingHeight * (float)levelNum),
                    wallDef.pos.y 
                );
            }
            wall.transform.localScale = new Vector3(wallPiece[1]-wallPiece[0], wallThickness, ceilingHeight);
            wall.transform.eulerAngles = new Vector3(90, wallDef.direction, 0);
        }

        foreach( float[] doorPiece in doorPieces)
        {
            GameObject door = Instantiate(doorPrefab);
            door.transform.parent = level.transform;
            if (wallDef.direction == 90)
            {
                door.transform.position = new Vector3(
                    wallDef.pos.x,
                    (ceilingHeight / 2) + (ceilingHeight * (float)levelNum),
                    wallDef.pos.y - (wallDef.length / 2f) + ((doorPiece[0] + doorPiece[1]) / 2.0f)
                );
            }
            else //dir = 0
            {
                door.transform.position = new Vector3(
                    wallDef.pos.x - (wallDef.length / 2f) + ((doorPiece[0] + doorPiece[1]) / 2.0f),
                    (ceilingHeight / 2) + (ceilingHeight * (float)levelNum),
                    wallDef.pos.y
                );
            }
            door.transform.localScale = new Vector3(doorPiece[1] - doorPiece[0], doorThickness, ceilingHeight);
            door.transform.eulerAngles = new Vector3(90, wallDef.direction, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public class LevelMap {
    public List<Room> rooms = new List<Room>();
    public List<Wall> walls = new List<Wall>();
    private float minSplittableRoomDimensionSize;
    public LevelMap(float minSplittableRoomDimensionSize)
    {
        this.minSplittableRoomDimensionSize = minSplittableRoomDimensionSize;
    }

    public bool hasRoomsBigEnoughToSplit()
    {
        return rooms.Exists(room => room.isBigEnoughToSplit(minSplittableRoomDimensionSize));
    }
}

public class Room
{
    /// <summary>
    /// pos variable refers to bottom left corner of the room
    /// </summary>
    public Vector2 pos;
    public float width;
    public float depth;
    public bool shouldSplitVert;
    public float area { get => width * depth; }

    public Room(Vector2 pos, float width, float depth, bool shouldSplitVert)
    {
        this.pos = pos;
        this.width = width;
        this.depth = depth;
        this.shouldSplitVert = shouldSplitVert;
    }

    public bool isBigEnoughToSplit(float minSplittableRoomDimensionSize)
    {
        return width > minSplittableRoomDimensionSize && depth > minSplittableRoomDimensionSize;
    }
}

public class Wall {
    //positions along the wall that doors appear
    public List<float> doors; 


    public Vector2 pos;

    //direction as degrees
    public int direction;
    public float length;

    public Wall(List<float> doors, Vector2 pos, int direction, float length)
    {

        this.doors = doors != null ? doors : new List<float>();
        this.pos = pos;
        this.direction = direction;
        this.length = length;
    }
}
