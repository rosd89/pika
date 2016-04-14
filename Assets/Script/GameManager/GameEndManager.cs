using UnityEngine;
using System.Collections;

using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameEndManager : MonoBehaviour {

    private Text _scoreBoard;

	void Awake(){
        
        _scoreBoard = GameObject.Find("ScoreBox").GetComponentInChildren<Text>();
    }
    
    void Start(){
        
        _scoreBoard.text = PlayerPrefs.GetString("SCORE_BOARD");
    }
    
    public void LoadGame(){
        SceneManager.LoadScene("Game");
    }
    
}
