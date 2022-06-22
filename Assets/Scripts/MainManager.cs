using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;
    public GameObject ball;

    public Text ScoreText;
    public Text PersistentScoreText;
    public GameObject GameOverText;
    public GameObject WinText;
    public GameObject MenuButton;

    public AudioSource gameOver;
    public AudioSource Win;
    
    private bool m_Started = false;
    //private bool isMenuActtive = true;
    private int m_Points;
    
    private bool m_GameOver = false;

    private int bricks;

    // Start is called before the first frame update
    void Start()
    {
        ball.SetActive(true);
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
        bricks = LineCount * perLine;
    }

    private void Update()
    {
        if (!m_Started)
        {
            MenuButton.SetActive(true);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                MenuButton.SetActive(false);
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }

        if(bricks == 0){
            Win.Play();
            DataSaver.Instance.SavingData();
            ball.SetActive(false);
            WinText.SetActive(true);
            MenuButton.SetActive(true);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }

        PersistentScoreText.text = $"Best Score: {DataSaver.Instance.HighScore}" + $" Name: {DataSaver.Instance.PlayerName}";
    }

    void AddPoint(int point)
    {
        m_Points += point;
        bricks--;
        ScoreText.text = $"Score : {m_Points}";
    }

    public void GameOver()
    {
        gameOver.Play();
        MenuButton.SetActive(true);
        if(m_Points >  DataSaver.Instance.HighScore){
            DataSaver.Instance.HighScore = m_Points;
        }
        DataSaver.Instance.SavingData();
        m_GameOver = true;
        GameOverText.SetActive(true);
    }

    public void GoToMenu(){
        SceneManager.LoadScene(0);
    }
}
