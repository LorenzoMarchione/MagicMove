using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;

    public void ChangeSceneNow()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
    public void DeathScene() => SceneManager.LoadScene("LoseScene");
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<Player>(out Player name))
            ChangeSceneNow();
    }
}
