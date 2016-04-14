using UnityEngine;
using System.Collections;

using UnityEngine.UI;

public class HpBarManager : MonoBehaviour {

	public float _hp;
	private Image _hpImg;

	void Awake(){
		_hpImg = GetComponent<Image> ();
	}
    
	void Start () {
	
		_hp = 1;
		StartCoroutine ("HpTimeDelete");
	}

	IEnumerator HpTimeDelete(){

		while (true) {

			yield return new WaitForSeconds (0.0002f);

			_hp -= 0.0002f;
			_hpImg.fillAmount = _hp;
            
            //GamOver
            if(_hp < 0){
                GameObject.Find("GameManager").GetComponent<GameManager>().GameOver();
            }
		}
	}

	/// <summary>
	/// Hit Point Up
	/// </summary>
	/// <param name="addHp">Add hp.</param>
	public void HpUp(float addHp){

		_hp += addHp;

		if (_hp > 1f)
			_hp = 1f;
            
        _hpImg.fillAmount = _hp;
	}
    
    /// <summary>
	/// Hit Point Downs
	/// </summary>
	/// <param name="addHp">Add hp.</param>
    public void HpDown(float delHp){
        
        _hp -= delHp;
        
        _hpImg.fillAmount = _hp;
    }
}
