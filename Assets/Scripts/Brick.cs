using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    [SerializeField] bool multipleLifes;
    [SerializeField] Sprite[] sprites;
    int currentLife;
    private void Start()
    {
        if (multipleLifes) currentLife = 1;
        else currentLife = 0;
        GetComponent<SpriteRenderer>().sprite = sprites[currentLife];
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (currentLife > 0)
        {
            currentLife--;
            GetComponent<SpriteRenderer>().sprite = sprites[currentLife];
        }
        else
            DestroyThisBrick();
    }
    public void DestroyThisBrick()
    {
        if (Random.Range(0, 100) > 90)
            LevelManager.Instance.SpawnItem(transform.position);
        LevelManager.Instance.DestroyBrick();
        Destroy(gameObject);
    }
}
