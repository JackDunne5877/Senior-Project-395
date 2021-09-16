using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingGenerator : MonoBehaviour
{
    public GameObject ground;
    public float buildingWidth;
    public float buildingDepth;
    public int levels;
    public float ceilingHeight;
    public float wallThickness;
    public float floorThickness;
    public float minSplittableRoomDimensionSize;
    public float minWallLength;

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
        map.rooms.Add(new Room(new Vector2(0, 0), buildingWidth, buildingDepth, Random.Range(0.0f, 1.0f) > 0.5f));
        List<Wall> interiorWalls = new List<Wall>();

        while (map.rooms.Count > 0 && interiorWalls.Count < 20)
        {

            if (map.rooms[0].isBigEnoughToSplit(minSplittableRoomDimensionSize))
            {
                Debug.Log("room with width: " + map.rooms[0].width + " and depth: " + map.rooms[0].depth + " is splittable");
                interiorWalls.Add(splitWithWall(map.rooms, 0));
            }
            else {
                Debug.Log("room with width: " + map.rooms[0].width + " and depth: " + map.rooms[0].depth + " is NOT splittable");
                map.rooms.RemoveAt(0);
            }
        }
        return interiorWalls;
    }

    private Wall splitWithWall(List<Room> rooms, int roomNum)
    {
        Debug.Log("splitting with wall");
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
        if (!originalRoom.shouldSplitVert)
        {
            //HORIZONTAL SPLIT
            splitOrigin = new Vector2(originalRoom.pos.x, Random.Range(originalBottom + minWallLength , originalTop - minWallLength));
            Debug.Log(splitOrigin.x + "  " + splitOrigin.y);
            splitWall = new Wall(null, null, splitOrigin, 0, originalRoom.width);
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
            splitOrigin = new Vector2(Random.Range(originalLeft + minWallLength, originalRight - minWallLength),originalRoom.pos.y);
            splitWall = new Wall(null, null, splitOrigin, 90, originalRoom.depth);
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
        return splitWall;
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
        GameObject wall = GameObject.CreatePrimitive(PrimitiveType.Cube);
        wall.transform.parent = level.transform;
        wall.transform.position = new Vector3(wallDef.pos.x, (ceilingHeight / 2) + (ceilingHeight*(float)levelNum), wallDef.pos.y);
        wall.transform.localScale = new Vector3(wallDef.length, wallThickness, ceilingHeight);
        wall.transform.eulerAngles = new Vector3(90, wallDef.direction, 0);
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
    public float[] doors;

    //[start][stop] pairs along the wall that are blanks
    public float[][] blanks;

    public Vector2 pos;

    //direction as degrees
    public int direction;
    public float length;

    public Wall(float[] doors, float[][] blanks, Vector2 pos, int direction, float length)
    {
        this.doors = doors;
        this.blanks = blanks;
        this.pos = pos;
        this.direction = direction;
        this.length = length;
    }
}
