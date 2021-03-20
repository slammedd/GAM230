using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureAnimator : MonoBehaviour
{
    public float xSpeed;
    public float ySpeed;

    private float currentX;
    private float currentY;

    private void Start()
    {
        currentX = GetComponent<Renderer>().material.mainTextureOffset.x;
        currentY = GetComponent<Renderer>().material.mainTextureOffset.y;
    }

    private void Update()
    {
        currentX += Time.deltaTime * xSpeed;
        currentY += Time.deltaTime * ySpeed;
        GetComponent<Renderer>().material.SetTextureOffset("_MainTex", new Vector2(currentX, currentY));
    }
}
