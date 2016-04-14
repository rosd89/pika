using UnityEngine;
using System.Collections;

using System.Collections.Generic;

public class Pica : MonoBehaviour {

	private Sprite[] _flagArr;
	private Sprite[] _armArr;

	[HideInInspector]
	public PicaArm _rightArm;
	[HideInInspector]
	public PicaArm _leftArm;
	private Dictionary<string, object> _armPosMap;

	public enum FLAGCOLOR { 
		white, 
		blue, 
		red, 
		black 
	};

	public enum ARMDIR { 
		right, 
		left, 
	};

	public TextAsset _armsPosJSON;

	void Awake(){
		
		_rightArm = transform.FindChild ("RightArm").GetComponent<PicaArm> ();
		_leftArm = transform.FindChild ("LeftArm").GetComponent<PicaArm> ();

		_flagArr = Resources.LoadAll<Sprite>("Flag");
		_armArr = Resources.LoadAll<Sprite>("Arm");

		_armPosMap = GameData.MiniJSON.jsonDecode(_armsPosJSON.text) as Dictionary<string, object>;
	}
    
    /// Arm Index Find 
	private int ArmIndex(string armSpriteName){

		int armSpriteIndex = 0;

		for (int i = 0; i < _armArr.Length; i++) {

			if (_armArr [i].name == armSpriteName) {

				armSpriteIndex = i;
				break;
			}
		}

		return armSpriteIndex;
	}
    
    /// <summary>
    /// 오른손으로 잡는 깃발인지 아닌지 판별
    /// </summary>
    /// <parma name="flagNum">Flag numbernumber</param>
    public bool isRightArm(int flagNum){
        
        if (flagNum == (int) FLAGCOLOR.blue 
			|| flagNum == (int) FLAGCOLOR.red) {
            
            return false;        
        }
        else{
            
            return true;
        }
        
    }
		
	/// <summary>
	/// flag color
	/// * 0 - white
	/// * 1 - blue
	/// * 2 - red
	/// * 3 - balck
	/// </summary>
	/// <param name="flagNum">Flag number.</param>
	public void OnFlagClickEvent(int flagNum){

		string armMotion = "center";

		if (isRightArm(flagNum)) {
			_rightArm.SetFlagSprite (_flagArr [flagNum]);
			OnRightFlagChange(armMotion);
		} 
		else {
            _leftArm.SetFlagSprite (_flagArr [flagNum]);
			OnLeftFlagChange(armMotion);
		}
    }

	/// <summary>
	/// flag center motion
	/// 0 - right
	/// 1 - left
	/// </summary>
	public void OnFlagCenter(){

        string armMotion = "center";

		OnLeftFlagChange(armMotion);
        OnRightFlagChange(armMotion);
	}
		
	/// <summary>
	/// flag up motion
	/// 0 - right
	/// 1 - left
	/// </summary>
	/// <param name="dir">Dir.</param>
	public void OnFlagUp(int dir){

		string armMotion = "up";

		if (dir == (int)ARMDIR.right) {
            if (_rightArm.getFlagSpriteName() == "NONE")
				return;
           
            OnRightFlagChange(armMotion);
		} 
		else {
			if (_leftArm.getFlagSpriteName () == "NONE")
				return;
			
			OnLeftFlagChange(armMotion);
		}
	}

	/// <summary>
	/// flag down motion
	/// 0 - right
	/// 1 - left
	/// </summary>
	/// <param name="dir">Dir.</param>
	public void OnFlagDown(int dir){
        
        string armMotion = "down";

		if (dir == (int)ARMDIR.right) {
			if (_rightArm.getFlagSpriteName() == "NONE")
				return;
           
            OnRightFlagChange(armMotion);
		} 
		else {
            if (_leftArm.getFlagSpriteName () == "NONE")
				return;
			
			OnLeftFlagChange(armMotion);
		}
	}
    
    private void OnRightFlagChange(string armMotion){
        
        int spriteIndex = ArmIndex(armMotion);
        
        Dictionary<string, object> armPosMap = 
            _armPosMap["right_" + armMotion] as Dictionary<string, object>;
        
        _rightArm.ArmTransform (_armArr [spriteIndex], armPosMap);
    }
   
    private void OnLeftFlagChange(string armMotion){
        
        int spriteIndex = ArmIndex(armMotion);
        
         Dictionary<string, object> armPosMap = 
            _armPosMap["left_" + armMotion] as Dictionary<string, object>;
        
        _leftArm.ArmTransform (_armArr [spriteIndex], armPosMap);
    }
}
