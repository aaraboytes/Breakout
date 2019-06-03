using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
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
