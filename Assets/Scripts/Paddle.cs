using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(AudioSource))]
public class Paddle : MonoBehaviour
{
    [SerializeField] float speed;
    Rigidbody2D body;
    float minX, maxX;
    Camera cam;
    Vector2 originalSize;

    [Header("Power ups")]
    public bool poweredUp = false;
    public GameObject fireFlowerBullet;
    string currentPowerUp = "";
    float powerUpTime = 0;
    float timer = 0;
    
    //Fireball
    public bool shootFireBalls = false;
    [SerializeField]int currentFireBalls=0;

    //Audio
    [Header("Audio")]
    public AudioClip mushroomSound;
    public AudioClip miniMushroomSound;
    public AudioClip fireSound;
    public AudioClip shotFireSound;
    public AudioClip doubleCherrySound;
    AudioSource audio;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        cam = Camera.main;
        float width = cam.orthographicSize * 2 * cam.aspect;
        minX = -width *0.5f + GetComponent<BoxCollider2D>().bounds.extents.x * transform.localScale.x * 0.5f;
        maxX = width * 0.5f - GetComponent<BoxCollider2D>().bounds.extents.x * transform.localScale.x * 0.5f;
        originalSize = transform.localScale;
        audio = GetComponent<AudioSource>();
    }
    void Update()
    {
        int dir = 0;
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            dir = -1;
        }else if (Input.GetKey(KeyCode.RightArrow))
        {
            dir = 1;
        }

        transform.Translate(Vector2.right * dir * speed * Time.deltaTime);
        Vector2 newPos = transform.position;
        newPos = new Vector2(Mathf.Clamp(newPos.x, minX, maxX), newPos.y);
        transform.position = newPos;

        if (shootFireBalls && !LevelManager.Instance.GameOver && !FindObjectOfType<Ball>().holded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                audio.PlayOneShot(shotFireSound);
                GameObject fire = Instantiate(fireFlowerBullet);
                fire.transform.position = (Vector2)transform.position + Vector2.up * 0.5f;
                fire.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-10, 10), Random.Range(5, 10)).normalized * LevelManager.Instance.GetBallSpeed();
                currentFireBalls--;
                if (currentFireBalls <= 0) EndPowerUp();
            }
        }

        if (poweredUp)
        {
            timer += Time.deltaTime;
            if (timer >= powerUpTime)
            {
                EndPowerUp();
                poweredUp = false;
                timer = 0;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PowerUp") && !poweredUp)
        {
            PowerUpHandler handler = collision.GetComponent<PowerUpHandler>();
            currentPowerUp = handler.powerUp.name;
            if (handler.powerUp.timeable)
            {
                powerUpTime = handler.powerUp.time;
            }
            else
            {
                powerUpTime = 5000;
            }
            StartPowerUp();
            Destroy(collision.gameObject);
        }
    }
    public void StartPowerUp()
    {
        switch (currentPowerUp)
        {
            case "Mushroom":
                transform.localScale = new Vector2(transform.localScale.x * 2, transform.localScale.y);
                audio.PlayOneShot(mushroomSound);
                break;
            case "MiniMushroom":
                transform.localScale = new Vector2(transform.localScale.x * 0.5f,transform.localScale.y);
                audio.PlayOneShot(miniMushroomSound);
                break;
            case "Fireflower":
                shootFireBalls = true;
                currentFireBalls = 5;
                audio.PlayOneShot(fireSound);
                break;
            case "DoubleCherry":
                LevelManager.Instance.SpawnNewBall((Vector2)transform.position + Vector2.up * 0.5f);
                audio.PlayOneShot(doubleCherrySound);
                break;
        }
        poweredUp = true;
    }

    public void EndPowerUp()
    {
        switch (currentPowerUp)
        {
            case "Mushroom":
                transform.localScale = originalSize;
                break;
            case "MiniMushroom":
                transform.localScale = originalSize;
                break;
            case "Fireflower":
                shootFireBalls = false;
                break;

        }
        poweredUp = false;
        currentPowerUp = "";
        powerUpTime = 0;
        timer = 0;
    }
}