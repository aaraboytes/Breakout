using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    [SerializeField] int maxLifes;
    [SerializeField] float increment;
    [SerializeField] GameObject[] powerUps;
    [SerializeField] float ballSpeed;
    public bool GameOver { get; set; }
    [Header("UI")]
    [SerializeField] Image[] lives;
    [SerializeField] Text scoreText;
    [SerializeField] GameObject gameOverPanel;
    int score = 0;
    int bricks = 0;
    int lifes;
    float speed;
    [SerializeField]Ball ball;

    //Extra ball
    [SerializeField] GameObject ballPrefab;
    int currentBallsInScene = 1;

    [Header("Audio")]
    public AudioClip brickBreakSound;
    public AudioClip[] bgMusic;
    AudioSource audio;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if(Instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        ball = FindObjectOfType<Ball>();
        lifes = maxLifes;
        bricks = FindObjectsOfType<Brick>().Length;

        GameOver = false;
        ballSpeed = FindObjectOfType<Ball>().GetSpeed();
        audio = GetComponent<AudioSource>();
        BackgroundMusic.Instance.PlayBGMusic(bgMusic[Random.Range(0, bgMusic.Length - 1)]);
    }
    public void DestroyBrick()
    {
        audio.PlayOneShot(brickBreakSound);
        bricks--;
        score += 10;
        //Set score in ui
        string scoreString = "Score : 000000";
        scoreString = scoreString.Substring(0, (13 - score.ToString().Length));
        scoreString += score.ToString();
        scoreText.text = scoreString;

        ball.SetSpeed(ball.GetSpeed() * increment);
        if (bricks<= 0)
        {
            Debug.Log("Win");
            FindObjectOfType<Paddle>().EndPowerUp();
            int nextScene = SceneManager.GetActiveScene().buildIndex + 1;
            if (nextScene == 10)
                print("Game ended");
            else
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                Invoke("FindBall", 0.5f);
            }
        }
    }
    public void SpawnItem(Vector2 pos)
    {
        GameObject pu = Instantiate(powerUps[Random.Range(0, powerUps.Length )]);
        pu.transform.position = pos;
        pu.GetComponent<Rigidbody2D>().velocity = Vector2.down * 1.5f;
    }
    public bool Damage(GameObject ballgo)
    {
        if (currentBallsInScene > 1)
        {
            Destroy(ballgo);
            ball = FindObjectOfType<Ball>();
            FindObjectOfType<Paddle>().EndPowerUp();
            currentBallsInScene--;
            return true;
        }
        else
        {
            lifes--;
            lives[lifes].enabled = false;
            if (lifes > 0)
            {
                return true;
            }
            else
            {
                GameOver = true;
                gameOverPanel.SetActive(true);
                return false;
            }
        }
    }
    public float GetBallSpeed() { return ballSpeed; }
    public void SetBallSpeed(float _ballSpeed) { ballSpeed = _ballSpeed; }
    public void SpawnNewBall(Vector2 pos)
    {
        currentBallsInScene++;
        GameObject extraBall = Instantiate(ballPrefab);
        extraBall.transform.position = pos;
        extraBall.GetComponent<Ball>().holded = false;
        extraBall.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-10, 10), Random.Range(5, 10)).normalized * ballSpeed;
    }
    public void Retry()
    {
        lifes = maxLifes;
        score = 0;
        GameOver = false;
        for(int i = 0; i < 3; i++)
        {
            lives[i].enabled = true;
        }
        Invoke("FindBall", 0.5f);
    }
    void FindBall()
    {
        ball = FindObjectOfType<Ball>();
        bricks = FindObjectsOfType<Brick>().Length;
    }
}
