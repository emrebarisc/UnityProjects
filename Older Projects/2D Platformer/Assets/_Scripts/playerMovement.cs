using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class playerMovement : MonoBehaviour {

	public float speed = 8f;
	public float jumpSpeed = 4f;
	public Text scoreText;
	public RectTransform gameOverPanel;
	public RectTransform nextLevelPanel;
	public RectTransform pausePanel;
	public Text levelScoreText;
	public Text highScoreText;
	public Text hScoreWarning;
	public AudioSource coinAudioSource;

	private float time = 0;
	private int score = 0;
	private bool isRunning;
	private bool isJumped;
	private Rigidbody2D rbPlayer;
	private Animator animator;

	void Start () {
		animator = GetComponent<Animator>();
		rbPlayer = GetComponent<Rigidbody2D>();
		Time.timeScale = 1;
	}


	public void runButton(bool a){
		isRunning = a;
	}

	void Run(){
		if(isRunning == true){
			transform.position += Vector3.right * speed * Time.deltaTime;
			animator.SetBool("isRunning", true);
		}
		else{
			animator.SetBool("isRunning", false);
		}

	}

	public void Jump(){
		if(isJumped == false){
			isJumped = true;
			rbPlayer.velocity += Vector2.up * jumpSpeed;
			animator.SetTrigger ("isJumped");
		}
	}

	void OnCollisionEnter2D(Collision2D other){
		if(other.gameObject.tag == "floor"){
			isJumped = false;
		}
	}	

	void OnTriggerEnter2D(Collider2D other){
		if(other.gameObject.tag == "coin"){

			coinAudioSource.Play();
			Destroy (other.gameObject);

			score++;
			scoreText.text = score.ToString();
		}
		
		else if(other.gameObject.tag == "gameOver"){
			gameOver();
		}

		else if(other.gameObject.tag == "death"){
			gameOver();
		}

		else if(other.gameObject.tag == "key"){
			calculateScore();
			Time.timeScale = 0;

			if(score > PlayerPrefs.GetInt("level1" + Application.loadedLevel)){
				PlayerPrefs.SetInt("level1" + Application.loadedLevel, score);
				hScoreWarning.gameObject.SetActive (true);
			}
			levelScoreText.text = "SCORE: " + score;
			highScoreText.text = "HIGH SCORE: " + PlayerPrefs.GetInt("level1" + Application.loadedLevel).ToString();
			nextLevelPanel.gameObject.SetActive (true);
		}
	}

	void FixedUpdate () {
		time += Time.deltaTime;


		if(Input.GetKeyDown(KeyCode.UpArrow) == true){
			Jump ();
		}		

		if(Input.GetKey(KeyCode.RightArrow) == true){
			isRunning = true;
		}else isRunning = false;

		if(Input.GetKeyDown(KeyCode.Escape) && nextLevelPanel.gameObject.activeInHierarchy == false && gameOverPanel.gameObject.activeInHierarchy == false)
		{
			Time.timeScale = 0;
			pausePanel.gameObject.SetActive (true);
		}

		Run();
	}

	void calculateScore(){
		score *= 10;
		if(time < 60) score += (60 - (int)time) * 10;
	}

	public void loadNextLevel(bool mainMenu){
		if(!mainMenu){
			if(Application.loadedLevel + 1 <= 9 ) Application.LoadLevel (Application.loadedLevel + 1);
			else Application.LoadLevel (0);
		}

		if(mainMenu) Application.LoadLevel(0);
	}

	void gameOver(){
		Time.timeScale = 0;
		gameOverPanel.gameObject.SetActive (true);
	}

	public void resume(){
		Time.timeScale = 1;
		pausePanel.gameObject.SetActive (false);
	}

	public void restart(){
		Application.LoadLevel(Application.loadedLevel);
	}

	public void quit(){
		print ("Quit");
		Application.Quit();
	}
}
