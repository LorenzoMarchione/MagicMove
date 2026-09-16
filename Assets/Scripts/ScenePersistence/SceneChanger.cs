using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;

    public void ChangeSceneNow()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
