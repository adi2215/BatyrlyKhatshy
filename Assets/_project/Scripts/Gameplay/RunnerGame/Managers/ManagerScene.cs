using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManagerScene : MonoBehaviour
{
    public void ExitToGame() => Application.Quit();

    public Animator transition;

    public float transitionTime = 1f;

    private void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
            MusicManager.Instance.PlayMusic("Menu");
        if (SceneManager.GetActiveScene().buildIndex == 3)
            MusicManager.Instance.PlayMusic("Village");
        if (SceneManager.GetActiveScene().buildIndex == 4)
            MusicManager.Instance.PlayMusic(null);
        if (SceneManager.GetActiveScene().buildIndex == 2 || (SceneManager.GetActiveScene().buildIndex == 6))
            MusicManager.Instance.PlayMusic("Game");
    }

    public void LoadNextLevel(int Num)
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + Num));
    }

    IEnumerator LoadLevel(int levelIndex)
    {

        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelIndex);
    }
}
