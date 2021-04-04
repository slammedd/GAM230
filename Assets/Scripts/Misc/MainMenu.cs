using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Animator noteBackground;
    public Animator[] noteTextAnimators;
    public Animator[] buttonAnimators;
    public AudioSource source;
    public AudioClip UISound;
    public GameObject[] mainMenuObjects;

    private void Start()
    {
        StartCoroutine(NoteAnimations());
    }

    IEnumerator MenuAnimations()
    {
        yield return new WaitForSeconds(2);
        foreach (Animator anim in buttonAnimators)
        {
            anim.SetBool("Trigger", true);
        }
    }

    IEnumerator NoteAnimations()
    {
        yield return new WaitForSeconds(3);
        noteBackground.SetBool("Trigger", true);
        foreach (Animator anim in noteTextAnimators)
        {
            anim.SetBool("Trigger", true);
        }

        yield return new WaitForSeconds(1);

        foreach(GameObject gO in mainMenuObjects)
        {
            gO.SetActive(true);
        }

        StartCoroutine(MenuAnimations());
    }

    public void StartGame()
    {
        source.PlayOneShot(UISound);
        SceneManager.LoadScene("Levels");
    }

    public void QuitGame()
    {
        source.PlayOneShot(UISound);
        Application.Quit();
    }

}
