using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteStretch : MonoBehaviour
{
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private bool KeepAspectRatio;

    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = sprites[Random.Range(0, sprites.Length)];
        Vector3 topRightCorner = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        float worldSpaceWidth = topRightCorner.x * 2;
        float worldSpaceHeight = topRightCorner.y * 2;

        Vector3 spriteSize = gameObject.GetComponent<SpriteRenderer>().bounds.size;

        float scaleFactorX = worldSpaceWidth / spriteSize.x;
        float scaleFactorY = worldSpaceHeight / spriteSize.y;

        if (KeepAspectRatio)
        {
            if (scaleFactorX > scaleFactorY)
            {
                scaleFactorY = scaleFactorX;
            }
            else
            {
                scaleFactorX = scaleFactorY;
            }
        }

        gameObject.transform.localScale = new Vector3(scaleFactorX, scaleFactorY, 1);
    }
}