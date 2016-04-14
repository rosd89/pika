using UnityEngine;
using System.Collections;

using System.Collections.Generic;

public class PicaArm : MonoBehaviour {

	private int _flagState;

	private Transform _armPos;
	private Transform _flagPos;
	private SpriteRenderer _armSprite;
	private SpriteRenderer _flagSprite;

	void Awake(){

		_armPos = GetComponent<Transform> ();
		_flagPos = transform.FindChild ("FlagPos");

		_armSprite = GetComponent<SpriteRenderer> ();
		_flagSprite = transform.FindChild ("FlagPos").GetComponentInChildren<SpriteRenderer> ();
	}
    
    public int GetFlagState(){
        return _flagState;
    }

	/// <summary>
	/// Gets the index of the flag.
	/// </summary>
	/// <returns>The flag index.</returns>
	public int getFlagIndex(){

		if (_flagSprite.sprite == null) {
			return -1;
		}

		return int.Parse ( _flagSprite.sprite.name.Split ('_')[1]);
	}

	/// <summary>
	/// Gets the name of the flag sprite.
	/// </summary>
	/// <returns>The flag sprite name.</returns>
	public string getFlagSpriteName(){

		return _flagSprite.sprite == null ? 
			"NONE" : 
			_flagSprite.sprite.name;
	}

	/// <summary>
	/// Sets the flag sprite.
	/// </summary>
	/// <param name="flagSprite">Flag sprite.</param>
	public void SetFlagSprite(Sprite flagSprite){
		
		_flagSprite.sprite = flagSprite;
	}

	/// <summary>
	/// Arms the transform.
	/// </summary>
	/// <param name="armSprite">Arm sprite.</param>
	/// <param name="armPosMap">Arm position map.</param>
	public void ArmTransform(Sprite armSprite, Dictionary<string, object> armPosMap){

		_armSprite.sprite = armSprite;

		_flagState = int.Parse (armPosMap ["flag_state"].ToString ());

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
