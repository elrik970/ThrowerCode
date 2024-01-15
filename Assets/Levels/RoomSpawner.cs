using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    [SerializeField] int RoomAmount;
    public List<List<string>> RoomSpots;
    [SerializeField] int LevelSize;
    public GameObject[] rooms;
    private int roomsSpawned;
    [SerializeField] int breakAmount = 40;
    

    public Vector2 CenterOfSpawnRoomSpotWorldSpace;

    // Vector2 is in relation to the center
    [SerializeField] private Dictionary<string, Vector2> moveDirections = new Dictionary<string,Vector2>() 
    {
        {"leftDoor", new Vector2(-1,0)},
        {"upDoor", new Vector2(0,1)},
        {"downDoor", new Vector2(0,-1)},
        {"rightDoor", new Vector2(1,0)}
    };


    private string LastMove = "";
    private List<List<string>> ValidRoomMatrix;
    [SerializeField] float RoomSpotSize = 10f;
    public Vector2 CurrentSpot;
    private Vector2 localDoorSpot;
    private string DoorDirection;
    private RoomClass PlacedRoom;
    public GameObject StartingRoom;
    

    // Start is called before the first frame update
    void Start()
    {
        RoomSpots = new List<List<string>>();
        for (int i = 0; i < LevelSize;i++) {
            RoomSpots.Add(new List<string>());
            for (int j = 0; j < LevelSize;j++)  {
                RoomSpots[i].Add("");
            }
        }

        CurrentSpot = new Vector2((float)Mathf.Floor(LevelSize/2),(float)Mathf.Floor(LevelSize/2));

        RoomClass StartingRoomClass = StartingRoom.GetComponent<RoomClass>();

        CreateMatrixOutOfArray(StartingRoomClass.RowSize,StartingRoomClass.spots);

        FillRoomSpotsInWithInstantiatedRoom(CurrentSpot,3,3);

        RoomSpawning();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void RoomSpawning() {
        roomsSpawned = 0;
        int timesRan = 0;
        while (roomsSpawned < RoomAmount) {
            GetValidRoomToSpawn();
            timesRan++;
            if (timesRan > breakAmount * RoomAmount) {
                return;
            }
        }
    }
    void GetValidRoomToSpawn() {
        RoomClass Room = GetRandomRoom();
        if (CheckIfRoomContainsOppositeDoor(Room.spots,Room.RowSize)) {
            if (BumpsIntoBuiltRoom(Room.RowSize,(int)Mathf.Ceil(Room.spots.Length/Room.RowSize))) {
                CreateMatrixOutOfArray(Room.RowSize,Room.spots);

                float Offset = LevelSize/2*RoomSpotSize;
                Vector3 Position = new Vector3(RoomSpotSize*CenterOfSpawnRoomSpotWorldSpace.x-Offset,0f,RoomSpotSize*-CenterOfSpawnRoomSpotWorldSpace.y+Offset);
                
                PlacedRoom = GameObject.Instantiate(Room.gameObject,Position,Quaternion.identity).GetComponent<RoomClass>();
                
                CurrentSpot = CenterOfSpawnRoomSpotWorldSpace;

                FillRoomSpotsInWithInstantiatedRoom(CenterOfSpawnRoomSpotWorldSpace,PlacedRoom.RowSize,(int)Mathf.Ceil(PlacedRoom.spots.Length/PlacedRoom.RowSize));

                moveDirections = PlacedRoom.doors();

                roomsSpawned += 1;
            }
            else {
                RoomSpawning();
            }
        }
    }
    void CreateMatrixOutOfArray(int RowSize, string[] FlattenedSpots) {
        ValidRoomMatrix = new List<List<string>>();
        int RowIndex = 0;
        int ColumnIndex = 0;
        ValidRoomMatrix.Add(new List<string>());
        foreach (string spot in FlattenedSpots) {
            RowIndex++;
            if (RowIndex > RowSize) {
                RowIndex = 1;
                ColumnIndex++;
                ValidRoomMatrix.Add(new List<string>());
            }
            ValidRoomMatrix[ColumnIndex].Add(spot);
        }
    }
    bool CheckIfRoomContainsOppositeDoor(string[] FlattenedSpots, int RowSize) {
        int iterations = 0;
        List<string> ValidDirections = new List<string>();
        List<Vector2> ValidPositions = new List<Vector2>();
        foreach (string Spot in FlattenedSpots) {
            if (Spot != "Room") {
                foreach(KeyValuePair<string,Vector2> Dir in moveDirections) {
                    if (Dir.Key == "upDoor"&&Spot == "downDoor") {
                        ValidDirections.Add(Dir.Key);
                        ValidPositions.Add(new Vector2(iterations%RowSize,Mathf.Floor(iterations/RowSize)));
                    }
                    if (Dir.Key == "downDoor"&&Spot == "upDoor") {
                        ValidDirections.Add(Dir.Key);
                        ValidPositions.Add(new Vector2(iterations%RowSize,Mathf.Floor(iterations/RowSize)));
                    }
                    if (Dir.Key == "rightDoor"&&Spot == "leftDoor") {
                        ValidDirections.Add(Dir.Key);
                        ValidPositions.Add(new Vector2(iterations%RowSize,Mathf.Floor(iterations/RowSize)));
                    }
                    if (Dir.Key == "leftDoor"&&Spot == "rightDoor") {
                        ValidDirections.Add(Dir.Key);
                        ValidPositions.Add(new Vector2(iterations%RowSize,Mathf.Floor(iterations/RowSize)));
                    }
                }
            }
            iterations++;
        }
        if (ValidDirections.Count > 0) {
            int index = (int)Random.Range(0,ValidDirections.Count);
            localDoorSpot = ValidPositions[index];
            DoorDirection = ValidDirections[index];
            return true;
        }
        return false;
    }
    bool BumpsIntoBuiltRoom(int RowSize, int ColumnSize) {
        Vector2 worldSpaceDoorSpot = Vector2.zero;
        Vector2 CurrentDoorSpot = Vector2.zero;

        if (DoorDirection == "rightDoor") {
            CurrentDoorSpot = new Vector2(CurrentSpot.x+moveDirections["rightDoor"].x,CurrentSpot.y-moveDirections["rightDoor"].y);
            worldSpaceDoorSpot = new Vector2(CurrentDoorSpot.x+1,CurrentDoorSpot.y);
        }
        if (DoorDirection == "leftDoor") {
            CurrentDoorSpot = new Vector2(CurrentSpot.x+moveDirections["leftDoor"].x,CurrentSpot.y-moveDirections["leftDoor"].y);
            worldSpaceDoorSpot = new Vector2(CurrentDoorSpot.x-1,CurrentDoorSpot.y);
        }
        if (DoorDirection == "downDoor") {
            CurrentDoorSpot = new Vector2(CurrentSpot.x+moveDirections["downDoor"].x,CurrentSpot.y-moveDirections["downDoor"].y);
            worldSpaceDoorSpot = new Vector2(CurrentDoorSpot.x,CurrentDoorSpot.y+1);
        }
        if (DoorDirection == "upDoor") {
            CurrentDoorSpot = new Vector2(CurrentSpot.x+moveDirections["upDoor"].x,CurrentSpot.y-moveDirections["upDoor"].y);
            worldSpaceDoorSpot = new Vector2(CurrentDoorSpot.x,CurrentDoorSpot.y-1);
        }
        
        Vector2 StartingSpot = new Vector2(worldSpaceDoorSpot.x-(RowSize-(RowSize-localDoorSpot.x-1)-1),worldSpaceDoorSpot.y-localDoorSpot.y);
        CenterOfSpawnRoomSpotWorldSpace = new Vector2(StartingSpot.x+Mathf.Floor(RowSize/2),StartingSpot.y+Mathf.Floor(ColumnSize/2));

        for (int i = 0; i < ColumnSize;i++) {
            for (int j = 0; j < RowSize; j++) {
                if (RoomSpots[(int)StartingSpot.y+i][(int)StartingSpot.x+j] != "") {
                    return false;
                }
            }
        }

        return true;
    }

    void FillRoomSpotsInWithInstantiatedRoom(Vector2 Center,int RowSize,int ColumnSize) {
        Vector2 StartingSpot = new Vector2(Center.x-Mathf.Floor(RowSize/2),Center.y-Mathf.Floor(ColumnSize/2));
        for (int i = 0; i < ColumnSize;i++) {
            for (int j = 0; j < RowSize; j++) {
                RoomSpots[(int)StartingSpot.y+i][(int)StartingSpot.x+j] = ValidRoomMatrix[i][j];
            }
        }
    }
    RoomClass GetRandomRoom() {
        int startingIndex = Random.Range(0,rooms.Length);
        
        for (int i = startingIndex; i < rooms.Length;i++) {
            RoomClass RoomComponent = rooms[i].GetComponent<RoomClass>();

            Dictionary<string,Vector2> allDir = RoomComponent.doors();
            foreach (KeyValuePair<string,Vector2> dir in moveDirections) {
                if (dir.Key == "rightDoor") {
                    if (allDir.ContainsKey("leftDoor")) {
                        return rooms[i].GetComponent<RoomClass>();
                    }
                }
                if (dir.Key == "leftDoor") {
                    if (allDir.ContainsKey("rightDoor")) {
                        return rooms[i].GetComponent<RoomClass>();
                    }
                }
                if (dir.Key == "upDoor") {
                    if (allDir.ContainsKey("downDoor")) {
                        return rooms[i].GetComponent<RoomClass>();
                    }
                }
                if (dir.Key == "downDoor") {
                    if (allDir.ContainsKey("upDoor")) {
                        return rooms[i].GetComponent<RoomClass>();
                    }
                }

            }
        }
        for (int i = 0; i < startingIndex;i++) {
            RoomClass RoomComponent = rooms[i].GetComponent<RoomClass>();

            Dictionary<string,Vector2> allDir = RoomComponent.doors();
            foreach (KeyValuePair<string,Vector2> dir in moveDirections) {
                if (dir.Key == "rightDoor") {
                    if (allDir.ContainsKey("leftDoor")) {
                        return rooms[i].GetComponent<RoomClass>();
                    }
                }
                if (dir.Key == "leftDoor") {
                    if (allDir.ContainsKey("rightDoor")) {
                        return rooms[i].GetComponent<RoomClass>();
                    }
                }
                if (dir.Key == "upDoor") {
                    if (allDir.ContainsKey("downDoor")) {
                        return rooms[i].GetComponent<RoomClass>();
                    }
                }
                if (dir.Key == "downDoor") {
                    if (allDir.ContainsKey("upDoor")) {
                        return rooms[i].GetComponent<RoomClass>();
                    }
                }

            }
        }
        Debug.LogWarning("No Match Found. Returning first room in array");
        return rooms[0].GetComponent<RoomClass>();
    }
}
