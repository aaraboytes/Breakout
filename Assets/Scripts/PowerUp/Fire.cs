using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public float speed;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Brick"))
        {
            collision.gameObject.GetComponent<Brick>().DestroyThisBrick();
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Abyss") || collision.gameObject.CompareTag("UnbreakeableBrick"))
        {
            Destroy(gameObject);
        }
    }
}
