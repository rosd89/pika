using UnityEngine;
using System.Collections;

using UnityEngine.SceneManagement;

public class GameStartManager : MonoBehaviour {

	public void GameStart(){
        
        SceneManager.LoadScene("Game");
    }
}
