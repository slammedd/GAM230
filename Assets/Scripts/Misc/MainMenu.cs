using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public Animator[] buttonAnimators;
    public GameObject selectionArrows;

    private void Start()
    {
        StartCoroutine(MenuAnimations());
    }

    IEnumerator MenuAnimations()
    {
        yield return new WaitForSeconds(2);
        foreach (Animator anim in buttonAnimators)
        {
            anim.SetBool("Trigger", true);
        }
        yield return new WaitForSeconds(2);
        selectionArrows.SetActive(true);

    }


}
