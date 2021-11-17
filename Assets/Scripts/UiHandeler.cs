using UnityEngine;
using UnityEngine.SceneManagement;

public class UiHandeler : MonoBehaviour
{
    public void SwitchScene(string name)
    {
        SceneManager.LoadScene(name);
    }
    public void SwitchScene(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void SetActive(GameObject go)
    {
        go.SetActive(true);
    }
    public void SetDeactive(GameObject go)
    {
        go.SetActive(false);
    }
    public void Pause()
    {
        Time.timeScale = 0;
    }
    public void Resume()
    {
        Time.timeScale = 1;
    }
    public void Restart()
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(sceneIndex);
    }
    public void Quit()
    {
        Application.Quit();
    }
}