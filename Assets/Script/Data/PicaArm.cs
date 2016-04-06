using UnityEngine;
using System.Collections;

public class PicaArm : MonoBehaviour {

	private int _flagState;
	public Transform _armPos;
	public Transform _flagPos;
	public SpriteRenderer _flagSprite;

	void Awake(){

		_flagState = 1;
		_armPos = GetComponent<Transform> ();
		_flagPos = transform.FindChild ("FlagPos");
		_flagSprite = transform.FindChild ("FlagPos").GetComponentInChildren<SpriteRenderer> ();
	}

	void Start(){

		_flagSprite.sprite = null;
	}

}
