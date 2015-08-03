using UnityEngine;
using System.Collections;

public class LangManager : MonoBehaviour {

	public Lang lang;
	public TextAsset text;

	void Start () {
		lang = new Lang(text, PlayerPrefs.GetString("languaje",Application.systemLanguage.ToString()));
	}	
}