using System.Collections.Generic;
using UnityEngine;

public class RoomService : MonoBehaviour
{
    //this script saves all necessary components of this room
    [SerializeField] private CameraConfinerProvider provider;
    [SerializeField] private Parallax parallaxManager;
    [SerializeField] public List<SpawnPoint> spawnPoints;
    [SerializeField] private List<Door> doors;

    public CameraConfinerProvider Provider => provider;
    public Parallax ParallaxManager => parallaxManager;
    public IReadOnlyList<Door> Doors => doors;

    void Awake()
    {
        ServiceLocator.Register<RoomService>(this);
    }
    public SpawnPoint GetSpawn(string id)
    {
        return spawnPoints.Find(spawn => spawn.spawnID == id);
    }
}
