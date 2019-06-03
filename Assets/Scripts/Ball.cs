using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    Rigidbody2D rbd;
    [SerializeField] float speed;
    float tweek = 0.2f;
    public bool holded = true;
    GameObject paddle;
    void Start()
    {
        rbd = GetComponent<Rigidbody2D>();
        paddle = FindObjectOfType<Paddle>().gameObject;
        transform.position = new Vector2(paddle.transform.position.x, paddle.transform.position.y + paddle.GetComponent<BoxCollider2D>().bounds.extents.y + 0.5f);
    }
    private void Update()
    {
        if (holded)
        {
            transform.position = new Vector2(paddle.transform.position.x,transform.position.y);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                rbd = GetComponent<Rigidbody2D>();
                rbd.velocity = new Vector2(Random.Range(-10, 10), Random.Range(5, 10)).normalized * speed;
                holded = false;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Abyss"))
        {
            if (LevelManager.Instance.Damage(gameObject))
            {
                rbd.velocity = Vector2.zero;
                transform.position = new Vector2(paddle.transform.position.x, paddle.transform.position.y + paddle.GetComponent<BoxCollider2D>().bounds.extents.y + 0.5f);
                holded = true;
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        rbd.velocity += new Vector2(tweek, tweek);
    }
    public float GetSpeed()
    {
        return speed;
    }
    public void SetSpeed(float _speed)
    {
        speed += _speed;
        rbd.velocity = rbd.velocity.normalized * speed;
        LevelManager.Instance.SetBallSpeed(speed);
    }
}
