using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class userInterfaceController : MonoBehaviour {

	public RectTransform levelPanel;
	public Text levelx1Text;
	public Text levelx2Text;
	public Text levelx3Text;
	public Text levelx4Text;
	public Text levelx5Text;
	public Text levelx6Text;
	public Text levelx7Text;
	public Text levelx8Text;
	public Text levelx9Text;
	public Text warningText;
	public Text notEnoughScore;

	private int previousLevelTotalScore;
	private int chosenLevel;
	private bool checkPrevWorld = false;

	// Use this for initialization
	void Start () {
//		PlayerPrefs.SetInt ("level11",0);
//		PlayerPrefs.SetInt ("level12",0);
//		PlayerPrefs.SetInt ("level13",0);
//		PlayerPrefs.SetInt ("level14",0);
//		PlayerPrefs.SetInt ("level15",0);
//		PlayerPrefs.SetInt ("level16",0);
//		PlayerPrefs.SetInt ("level17",0);
//		PlayerPrefs.SetInt ("level18",0);
//		PlayerPrefs.SetInt ("level19",0);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void createLevelButtons(int level){

		previousLevelTotalScore = 
				  PlayerPrefs.GetInt ("level" + (level - 1).ToString () + "1") 
				+ PlayerPrefs.GetInt ("level" + (level - 1).ToString () + "2")
				+ PlayerPrefs.GetInt ("level" + (level - 1).ToString () + "3")
				+ PlayerPrefs.GetInt ("level" + (level - 1).ToString () + "4")
				+ PlayerPrefs.GetInt ("level" + (level - 1).ToString () + "5")
				+ PlayerPrefs.GetInt ("level" + (level - 1).ToString () + "6")
				+ PlayerPrefs.GetInt ("level" + (level - 1).ToString () + "7")
				+ PlayerPrefs.GetInt ("level" + (level - 1).ToString () + "8")
				+ PlayerPrefs.GetInt ("level" + (level - 1).ToString () + "9");

		if(PlayerPrefs.GetInt ("level" + (level - 1).ToString () + "9") > 0) checkPrevWorld = true;
		else checkPrevWorld = false;

		if(level == 1 || (previousLevelTotalScore >= 4500 && checkPrevWorld == true)){
			notEnoughScore.gameObject.SetActive (false);
			chosenLevel = level;
			levelx1Text.text = level.ToString () + "-1\n\n" + PlayerPrefs.GetInt ("level" + level.ToString () + "1");
			levelx2Text.text = level.ToString () + "-2\n\n" + PlayerPrefs.GetInt ("level" + level.ToString () + "2");
			levelx3Text.text = level.ToString () + "-3\n\n" + PlayerPrefs.GetInt ("level" + level.ToString () + "3");
			levelx4Text.text = level.ToString () + "-4\n\n" + PlayerPrefs.GetInt ("level" + level.ToString () + "4");
			levelx5Text.text = level.ToString () + "-5\n\n" + PlayerPrefs.GetInt ("level" + level.ToString () + "5");
			levelx6Text.text = level.ToString () + "-6\n\n" + PlayerPrefs.GetInt ("level" + level.ToString () + "6");
			levelx7Text.text = level.ToString () + "-7\n\n" + PlayerPrefs.GetInt ("level" + level.ToString () + "7");
			levelx8Text.text = level.ToString () + "-8\n\n" + PlayerPrefs.GetInt ("level" + level.ToString () + "8");
			levelx9Text.text = level.ToString () + "-9\n\n" + PlayerPrefs.GetInt ("level" + level.ToString () + "9");

			levelPanel.gameObject.SetActive (true);
		}

		else notEnoughScore.gameObject.SetActive (true);
	}

	public void goToLevel(int chapter){
		switch(chosenLevel){
		case 1:
			if(chapter - 1 != 0 && (PlayerPrefs.GetInt ("level1" + (chapter - 1).ToString ()) > 0))
				Application.LoadLevel (chapter);
			else if(chapter == 1)
				Application.LoadLevel (chapter);
			else
				warningText.gameObject.SetActive (true);
			break;

		case 2:
			if(chapter - 1 != 0 && (PlayerPrefs.GetInt ("level2" + (chapter - 1).ToString ()) > 0))
				Application.LoadLevel (9 + chapter);
			else if(chapter == 1)
				Application.LoadLevel (9 + chapter);
			else
				warningText.gameObject.SetActive (true);
			break;

		case 3:
			if(chapter - 1 != 0 && (PlayerPrefs.GetInt ("level3" + (chapter - 1).ToString ()) > 0))
				Application.LoadLevel (18 + chapter);
			else if(chapter == 1)
				Application.LoadLevel (18 + chapter);
			else
			{
				warningText.gameObject.SetActive (true);
			}
			break;

		case 4:
			if(chapter - 1 != 0 && (PlayerPrefs.GetInt ("level4" + (chapter - 1).ToString ()) > 0))
				Application.LoadLevel (27 + chapter);
			else if(chapter == 1)
				Application.LoadLevel (27 + chapter);
			else
				warningText.gameObject.SetActive (true);
			break;
		}


	}

	public void goBack(){
		levelPanel.gameObject.SetActive (false);
	}


}
