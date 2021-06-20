using System;
using System.Collections.Generic;
using UnityEngine;

using Coord = UnityEngine.Vector2Int;
public class MapGenerator : MonoBehaviour
{
   // 랜덤 변수
    [Min(1)]
    public Vector2Int wholeMapSize;
    [Range(0, 1)]
    public float RateOfRoom;

    private Vector3 StartPosition;

    public GameObject TilePrefab;
    public GameObject RoomPrefab;

    List<Coord> allTileCoords;
    Queue<Coord> shuffledTileCoords;
    
    public int seed = 10;

    Coord MapCenter;

    Vector3 TileSize = new Vector3(3.556f, 0, 3.556f);

    //[HideInInspector]
    //public Transform[,] mapArray { set; get; }


    // 규칙 
    // 1. 방의 위치는 정 가운데를 기준으로 한다 
    // 2. 방은 최소 1개 (시작지점)을 간디ㅏ
    // 3. 방과 방 간의 거리는 0일 때 복도 없이 붙이고 1일 때 5개까지 벌림

    // 방의 크기는 3.556 x 3.556... 
    // 복도는  벽이 없는 방 바닥을 만들고 나중에 벽 세우기

    // 생성 방식
    // 1. 방은 3x3 정사각형 복도는 2x3 크기로 가정하고 

    public void GenerateMap()
    {
        ShuffleCoord();

        string holderName = "Generated Map";
        if (transform.Find(holderName))
        {
            DestroyImmediate(transform.Find(holderName).gameObject);
        }

        GameObject MapHolder = new GameObject(holderName);
        MapHolder.transform.parent = this.transform;
        StartPosition = new Vector3(-wholeMapSize.x / 2 * TileSize.x, 0, wholeMapSize.y / 2 * TileSize.z);
        MapCenter = new Coord(wholeMapSize.x / 2, wholeMapSize.y / 2);

        Room[,] roomMap = new Room[wholeMapSize.x, wholeMapSize.y];

        int NumOfRoom = (int)(RateOfRoom * wholeMapSize.x * wholeMapSize.y);

        for(int  i = 0; i < NumOfRoom; ++i){
            Coord randomCoord = GetRandomCoord();
            if(roomMap[randomCoord.x, randomCoord.y] != null){ // roomMap이 뭔가로 채워진 경우 다시 
                i--;
            }else{ // 방 인스턴스화 하기
                Vector3 obstaclePos = CoordToVector(randomCoord.x, randomCoord.y);
                GameObject newObstacle = Instantiate<GameObject>(RoomPrefab, obstaclePos, Quaternion.identity);
                roomMap[randomCoord.x, randomCoord.y] = newObstacle.GetComponent<Room>();
                roomMap[randomCoord.x, randomCoord.y].SetDoorStyle(false, false, false, false);
                newObstacle.transform.parent = MapHolder.transform;
                newObstacle.transform.name = MapUtility.getRoomName(randomCoord.x, randomCoord.y);
            }
        }
        

        for (int i = 0; i < wholeMapSize.x; ++i)
        {
            for (int j = 0; j < wholeMapSize.y; ++j)
            {
                if (!roomMap[i, j])
                {
                    Vector3 tilePos = CoordToVector(i, j);
                    Room newTile = Instantiate<GameObject>(TilePrefab, tilePos, Quaternion.identity).GetComponent<Room>();
                    newTile.transform.name = MapUtility.getRoomName(i, j);
                    newTile.transform.parent = MapHolder.transform;
                    roomMap[i, j] = newTile;
                }
            }
        } // Tile
    }

    bool MapIsFullyAccessible(bool[,] obstacleMap, int currentObstacleCount)
    {
        bool[,] mapFlag = new bool[obstacleMap.GetLength(0), obstacleMap.GetLength(1)];
        Queue<Coord> queue = new Queue<Coord>();
        queue.Enqueue(MapCenter);
        mapFlag[MapCenter.x, MapCenter.y] = true;
        int accessibleTileCount = 1;

        while(queue.Count > 0)
        {
            Coord tile = queue.Dequeue();
            for (int x = -1; x <= 1; ++x)
            {
                for (int y = -1; y <= 1; ++ y)
                {
                    int neighbourX = tile.x + x;
                    int neighbourY = tile.y + y;
                    if (x==0 || y == 0)
                    {
                        if (neighbourX >= 0 && neighbourX < mapFlag.GetLength(0) && neighbourY >= 0 && neighbourY < mapFlag.GetLength(1))
                        {
                            if (!mapFlag[neighbourX, neighbourY] && !obstacleMap[neighbourX, neighbourY])
                            {
                                mapFlag[neighbourX, neighbourY] = true;
                                queue.Enqueue(new Coord(neighbourX, neighbourY));
                                accessibleTileCount++;
                            }
                        }
                    }
                }
            }
        }
        int targetAccessibleTileCount = wholeMapSize.x * wholeMapSize.y - currentObstacleCount;
        return targetAccessibleTileCount == accessibleTileCount;
    }

    public void ShuffleCoord()
    {   
        allTileCoords = new List<Coord>();
        for (int x = 0; x < wholeMapSize.x; ++x)
        {
            for (int y = 0; y < wholeMapSize.y; ++y)
            {
                allTileCoords.Add(new Coord(x, y));
            }
        }
        shuffledTileCoords = new Queue<Coord>(MapUtility.ShuffleArr<Coord>(allTileCoords.ToArray(), seed));
    }

    public Coord GetRandomCoord()
    {
        Coord randomCoord = shuffledTileCoords.Dequeue();
        shuffledTileCoords.Enqueue(randomCoord);
        return randomCoord;
    }

    Vector3 CoordToVector(int x, int y)
    {
        return StartPosition + new Vector3(TileSize.x * x, 0, -TileSize.z * y);
    }
}
