using UnityEngine;
using System.Collections;

using System.Collections.Generic;

public class Pica : MonoBehaviour {

	private Sprite[] _flagArr;
	private Sprite[] _armArr;
		
	private SpriteRenderer _bodySR;

	private PicaArm _rightArm;
	private PicaArm _leftArm;

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
		
	/*
	 * flag color
	 *
	 * 0 - white
	 * 1 - blue
	 * 2 - red
	 * 3 - balck
	 * 
	 */
	public void OnFlagClickEvent(int flagNum){

		int spriteIndex = ArmIndex("center");
		Dictionary<string, object> armPosMap;

		if (flagNum == (int) FLAGCOLOR.blue 
			|| flagNum == (int) FLAGCOLOR.red) {

			_leftArm.SetFlagSprite (_flagArr [flagNum]);

			armPosMap =  _armPosMap["left_center"] as Dictionary<string, object>;
			_leftArm.ArmTransform (_armArr [spriteIndex], armPosMap);
		} 
		else {

			_rightArm.SetFlagSprite (_flagArr [flagNum]);

			armPosMap =  _armPosMap["right_center"] as Dictionary<string, object>;
			_rightArm.ArmTransform (_armArr [spriteIndex], armPosMap);
		}
	}

	/*
	 * Arm Index Find 
	 */
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
		
	/*
	 * flag up motion
	 * 
	 * 0 - right
	 * 1 - left
	 */
	public void OnFlagUp(int dir){

		int topSpriteIndex = ArmIndex("up");
		Dictionary<string, object> armPosMap;

		if (dir == (int)ARMDIR.right) {

			armPosMap =  _armPosMap["right_top"] as Dictionary<string, object>;

			if (_rightArm.getFlagSpriteName() == "NONE")
				return;

			_rightArm.ArmTransform (_armArr [topSpriteIndex], armPosMap);
		} 
		else {

			armPosMap =  _armPosMap["left_top"] as Dictionary<string, object>;

			if (_leftArm.getFlagSpriteName () == "NONE")
				return;

			_leftArm.ArmTransform (_armArr [topSpriteIndex], armPosMap);
		}
	}

	/*
	 * flag down motion
	 * 
	 * 0 - right
	 * 1 - left
	 */
	public void OnFlagDown(int dir){

		int downSpriteIndex = ArmIndex("down");
		Dictionary<string, object> armPosMap;

		if (dir == (int)ARMDIR.right) {

			armPosMap =  _armPosMap["right_down"] as Dictionary<string, object>;

			if (_rightArm.getFlagSpriteName() == "NONE")
				return;
			
			_rightArm.ArmTransform (_armArr [downSpriteIndex], armPosMap);
		} 
		else {

			armPosMap =  _armPosMap["left_down"] as Dictionary<string, object>;

			if (_leftArm.getFlagSpriteName () == "NONE")
				return;
			
			_leftArm.ArmTransform (_armArr [downSpriteIndex], armPosMap);
		}
	}
}
