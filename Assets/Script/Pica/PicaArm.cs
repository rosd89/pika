using UnityEngine;
using System;
using System.Collections;

using System.Collections.Generic;

public class PicaArm : MonoBehaviour {

	private int _flagState;

	private Transform _armPos;
	private Transform _flagPos;
	private SpriteRenderer _armSprite;
	private SpriteRenderer _flagSprite;

	void Awake(){

		_flagState = 1;

		_armPos = GetComponent<Transform> ();
		_flagPos = transform.FindChild ("FlagPos");

		_armSprite = GetComponent<SpriteRenderer> ();
		_flagSprite = transform.FindChild ("FlagPos").GetComponentInChildren<SpriteRenderer> ();
	}

	public string getFlagSpriteName(){

		return _flagSprite.sprite == null ? 
			"NONE" : 
			_flagSprite.sprite.name;
	}

	public void SetFlagSprite(Sprite flagSprite){
		
		_flagSprite.sprite = flagSprite;
	}
		
	public void ArmTransform(Sprite armSprite, Dictionary<string, object> armPosMap){

		_armSprite.sprite = armSprite;

		_armPos.localPosition = new Vector2(
				float.Parse(armPosMap ["arm_transform_x"].ToString()), 
				float.Parse(armPosMap ["arm_transfrom_y"].ToString())
		);

		_flagPos.localPosition = new Vector2(
			float.Parse(armPosMap ["flag_transform_x"].ToString()), 
			float.Parse(armPosMap ["flag_transform_y"].ToString())
		);
			
		_flagPos.localRotation = Quaternion.Euler(new Vector3(0, 0, float.Parse(armPosMap ["flag_rotation_z"].ToString())));

	}
}
