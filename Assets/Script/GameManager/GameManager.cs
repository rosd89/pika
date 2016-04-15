using UnityEngine;
using System.Collections;

using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    private float _routineTime;
    private int _successCnt;
    private bool _motionCheker;
    
    private Pica _pica;

    private Text _missionText;
    
    private string _missionStr;
    private int _missionTextIndex;
    
    private Text _scoreBoard;
    private HpBarManager _hpBar;
    
    enum FLAGMOTION{
		FLAG_UP,
		FLAG_DOWN,
		FLAG_NOT_UP,
		FLAG_NOT_DOWN,
		FLAG_CHANGE,
		FLAG_NOT_CHANGE
	}
    
    void Awake(){
        
        _routineTime = 3;

		_pica = GameObject.Find ("Pica").GetComponent<Pica> ();
		_missionStr = "";
        
        _missionText = GameObject.Find("MissionArea").GetComponentInChildren<Text>();
        _scoreBoard = GameObject.Find("ScoreBox").GetComponentInChildren<Text>();
        _hpBar = GameObject.Find("Hp").GetComponent<HpBarManager>();

		StartCoroutine ("StartGameCoroutine");
	}

    IEnumerator StartGameCoroutine(){

		while (true) {

			yield return new WaitForSeconds(_routineTime);

			GameManage ();
		}
	}
    
    private void DefaultRoutineTime(){
        _routineTime = 3;
    }

	/// <summary>
	/// Games the manage.
	/// 
	/// Flag UP, DOWN Logic
	/// </summary>
	private void GameManage(){

		_missionStr = "";

		int flagNum = SelectFlag ();
		int flagMotion = 0;

		if (_motionCheker) {

			flagMotion = SelectFlagMotion();
		} 
		else {
			
			flagMotion = SelectFlagChangeMotion (flagNum);
		}

		StartCoroutine(TextGenerator(_missionStr + "\n", flagNum, flagMotion));
	}

	IEnumerator TextGenerator(string missionText, int flagNum, int flagMotion){
        
        _missionText.text = "";
        
		char[] missionTextArr = missionText.ToCharArray ();

		float routineTime = (_routineTime - 1f) / missionTextArr.Length;
		//Debug.Log (missionText);

		for(int i=0; i< missionTextArr.Length; i++){

			yield return new WaitForSeconds(routineTime);

			_missionText.text += missionTextArr [i];
		}
        
        //현재 깃발 모션의 값을 가져옴
        int armFlagNum = _pica._rightArm.getFlagIndex();
        int armFlagMotion = _pica._rightArm.GetFlagState();
       
        if(_pica.isRightArm(flagNum)){
            armFlagNum = _pica._rightArm.getFlagIndex();
            armFlagMotion = _pica._rightArm.GetFlagState();
        }
        else{
            armFlagNum = _pica._leftArm.getFlagIndex();
            armFlagMotion = _pica._leftArm.GetFlagState();
        }
        
        bool successCheck = true;
        // hp 계산 - 깃발 색
        if(armFlagNum != flagNum
            && flagMotion != (int) FLAGMOTION.FLAG_CHANGE
            && flagMotion != (int) FLAGMOTION.FLAG_NOT_CHANGE
            && flagMotion != (int) FLAGMOTION.FLAG_NOT_DOWN
            && flagMotion != (int) FLAGMOTION.FLAG_NOT_UP){
            
            successCheck = false;
            
            _successCnt = 1;
            DefaultRoutineTime();
            _hpBar.HpDown(0.001f);
        }
        
        // 0 top
        // 1 down
        // 2 center
        flagMotion = (flagMotion < 1) ? flagMotion : 2;
        
        // hp 계산 - 상태값
        if(armFlagMotion != flagMotion){
            
            successCheck = false;
            
            _successCnt = 1;
            DefaultRoutineTime();
            _hpBar.HpDown(0.001f);
        }
        
        //게임 점수 계산
        if(successCheck) 
            FlagChangeSuccess();
        
        // 게임 결과값 반영
		_pica.OnFlagCenter ();
	}
    
    private void FlagChangeSuccess(){
        
        // 일정 구간에서 hp증가
        if(_successCnt % 10 == 0)
            _hpBar.HpUp(0.0005f);
        
        // 점수판에 점수입력
        _scoreBoard.text = 
            (int.Parse(_scoreBoard.text) + (_successCnt * 10)).ToString();   
            
        _successCnt += 1;
    }

	/// <summary>
	/// Selects the flag.
	/// </summary>
	/// <returns>The flag.</returns>
	private int SelectFlag(){

		int randNum = Random.Range (0, 40) + 1;
        randNum = (randNum % 4);

		int flagIndex = (randNum == (int) Pica.FLAGCOLOR.blue) || (randNum == (int) Pica.FLAGCOLOR.red) ? 
			_pica._leftArm.getFlagIndex() : 
			_pica._rightArm.getFlagIndex();

		//Debug.Log ("rand : " + randNum + ", flag : " + flagIndex);

		if (randNum == (int)Pica.FLAGCOLOR.white) {
			_missionStr += "백기";
		}
		else if (randNum == (int)Pica.FLAGCOLOR.blue) {
			_missionStr += "청기";
		}
		else if (randNum == (int)Pica.FLAGCOLOR.red) {
			_missionStr += "적기";
		}
		else if (randNum == (int)Pica.FLAGCOLOR.black) {
			_missionStr += "흑기";
		}

		_motionCheker = flagIndex == -1 || flagIndex != randNum ? 
			false : true ;

		return randNum;
	}

	/// <summary>
	/// Selects the flag motion.
	/// </summary>
	/// <returns>The flag motion.</returns>
	private int SelectFlagMotion(){

		int flagMotion = Random.RandomRange(0, 4);

		if (flagMotion == (int)FLAGMOTION.FLAG_UP) {
			_missionStr += "올려";
		}
		else if(flagMotion == (int)FLAGMOTION.FLAG_DOWN) {
			_missionStr += "내려";
		}
		else if(flagMotion == (int)FLAGMOTION.FLAG_NOT_UP) {
			_missionStr += "올리지마";
		}
		else if(flagMotion == (int)FLAGMOTION.FLAG_NOT_DOWN) {
			_missionStr += "내리지마";
		}

		return flagMotion;
	}

	/// <summary>
	/// Selects the flag change motion.
	/// </summary>
	/// <returns>The flag change motion.</returns>
	private int SelectFlagChangeMotion(int flagNum){
        
        int flagIndex = (flagNum == (int) Pica.FLAGCOLOR.blue) || (flagNum == (int) Pica.FLAGCOLOR.red) ? 
			_pica._leftArm.getFlagIndex() : 
			_pica._rightArm.getFlagIndex();
            
        if(flagIndex == -1){
            
            _missionStr += "바꿔";
            return 4;
        }

		int flagMotion = Random.RandomRange (4, 6);

		if (flagMotion == (int)FLAGMOTION.FLAG_CHANGE) {
			_missionStr += "바꿔";
		}
		else if(flagMotion == (int)FLAGMOTION.FLAG_NOT_CHANGE) {
			_missionStr += "바꾸지마";
		}

		return flagMotion;
	}
    
    /// GameOver
    public void GameOver(){
        
        PlayerPrefs.SetString("SCORE_BOARD", _scoreBoard.text);
        
        SceneManager.LoadScene("End");
    }
}
