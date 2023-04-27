using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabSnow : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private List<Sprite> sprites;

    [SerializeField] private int lifeCycle;

    void Awake()
    {
        spriteRenderer.sprite = sprites[lifeCycle];
    }
    
    public void NextLifeCycle()
    {
        lifeCycle++;

        if (lifeCycle == sprites.Count)
        {
            Destroy(gameObject);
            return;
        }

        spriteRenderer.sprite = sprites[lifeCycle];
    }
}
