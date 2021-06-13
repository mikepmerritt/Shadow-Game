using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitioner : MonoBehaviour
{

    public Animator Animator;
    public float AnimationTime;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            LoadNextLevel();
        }
    }

    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        Animator.SetTrigger("Start");

        yield return new WaitForSeconds(AnimationTime);

        SceneManager.LoadScene(levelIndex);
    }
}
