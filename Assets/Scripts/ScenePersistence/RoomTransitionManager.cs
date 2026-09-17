using NUnit.Framework;
using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomTransitionManager : MonoBehaviour
{
    //this script is the bridge between bootstrap scene an room scenes
    //control screen fade from transitions
    [SerializeField] private ScreenFader screenFader;
    [SerializeField] private CameraManager cameraManager;
    //room that is currently loaded
    private string currentRoom = "";
    //lock for transitions
    private bool isTransitioning;

    void Start()
    { 
        //pass it empty strings so it doesnt load nor unloads but saves current scene
        EnterRoom("", "");
    }
    //doors call this method
    public void EnterRoom(string sceneName, string spawnID)
    {
        //this is to stop player from immediatly going to another room while transition isnt finished
        if (isTransitioning)
            return;
        StartCoroutine(Transition(sceneName, spawnID));
    }
    //unload current scene load another scene in parallel and save current scene
    private IEnumerator Transition(string sceneName, string spawnID)
{
    isTransitioning = true;

    // 1. Fade out
    if (!string.IsNullOrEmpty(spawnID))
        yield return screenFader.Fade(0f, 1f, 0.5f);

    // 2. Cambiar habitación
    if (!string.IsNullOrEmpty(currentRoom))
    {
        yield return SceneManager.UnloadSceneAsync(currentRoom);
        yield return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

        Scene newScene = SceneManager.GetSceneByName(sceneName);

        if (!newScene.IsValid() || !newScene.isLoaded)
        {
            Debug.LogError($"Room {sceneName} couldn't be loaded.");
            isTransitioning = false;
            yield break;
        }

        SceneManager.SetActiveScene(newScene);
    }

    // 3. Obtener RoomService de la habitación actual
    RoomService service = ServiceLocator.Get<RoomService>();

    if (service == null)
    {
        Debug.LogError("Current room doesn't have a registered RoomService.");
        isTransitioning = false;
        yield break;
    }

    currentRoom = SceneManager.GetActiveScene().name;

    // 4. Bloquear puertas
    foreach (Door door in service.Doors)
        door.LockDoor(true);

    // 5. Configurar habitación
    SetupRoom(service, spawnID);
    SetupCameraConfiner(service);

    // Dar tiempo a Cinemachine para actualizar posición
    yield return new WaitForSeconds(0.75f);

    ResetParallax(service);

    // 6. Fade in
    if (!string.IsNullOrEmpty(spawnID))
        yield return screenFader.Fade(1f, 0f, 1f);

    // 7. Desbloquear
    foreach (Door door in service.Doors)
        door.LockDoor(false);

    isTransitioning = false;
}
    //set spawnpoint of player for this room
    private void SetupRoom(RoomService service, string spawnID)
    {
        if (string.IsNullOrEmpty(spawnID))
            return;

        //get spawn from room service
        SpawnPoint spawnToUse = service.GetSpawn(spawnID);

        if(spawnToUse != null)
            transform.position = spawnToUse.transform.position;
    }
    //get camera confiner from room service
    private void SetupCameraConfiner(RoomService service)
    {
        if (service.Provider != null)
            cameraManager.SetConfiner(service.Provider.confiner);
        else
            Debug.Log("no service");
    }
    //get parallax manager from room service
    private void ResetParallax(RoomService service)
    {
        if(service.ParallaxManager != null)
            service.ParallaxManager.Initialize(cameraManager.camTransform);
    }
}
