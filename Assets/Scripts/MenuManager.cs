using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuManager : MonoBehaviour
{
    public Text ScoreText;
    public Text PlayerText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //DataSaver.Instance.LoadData();
        Debug.Log(DataSaver.Instance.PlayerName);
        ScoreText.text = "Best Score: " + DataSaver.Instance.HighScore;
        PlayerText.text = "Your name: "+ DataSaver.Instance.PlayerName;
    }

    public void EnterName(string name){
        DataSaver.Instance.PlayerName = name;
        DataSaver.Instance.SavingData();
    }

    public void StartGame(){
        SceneManager.LoadScene(1);
    }

    public void ExitGame(){
        DataSaver.Instance.SavingData();
        #if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
        #else
        Application.Quit(); // original code to quit Unity player
        #endif
    }

    public void DeleteData(){
        DataSaver.Instance.PlayerName = "Player";
        DataSaver.Instance.HighScore = 0;
        DataSaver.Instance.SavingData();
    }
}
