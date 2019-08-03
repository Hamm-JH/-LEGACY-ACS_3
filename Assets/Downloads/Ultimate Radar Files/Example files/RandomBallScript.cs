using UnityEngine;
using System.Collections;

public class RandomBallScript : MonoBehaviour {

	public GameObject BallDot;
	public GameObject BallRandom;


    public float spawnTimer = 1;
	private float ballTimer = 0;

	// Use this for initialization
	void Start () {
		Application.runInBackground = true;

		/*for(int i=0; i<100; i++) {
			CreateBall();
		}*/
	}
	
	// Update is called once per frame
	void Update () {
        if (ballTimer > spawnTimer)
        {

			CreateBall();

			ballTimer = 0;
		} else {
			ballTimer += Time.deltaTime;
		}


	}

	private void CreateBall() {
		GameObject newBall = (GameObject) Instantiate(BallDot);
		
		float ballX = Random.Range(-30, 30);
		
		newBall.transform.position = new Vector3(ballX + transform.position.x, transform.position.y + 1, transform.position.z - 1);
		
		GameObject newBallRandom = (GameObject) Instantiate(BallRandom);

		
		float ballRandomX = Random.Range(-30, 30);
		
		int ballBlip = Random.Range(1, 5);
		
		switch(ballBlip) {
		case 1:
			newBallRandom.GetComponent<TrackedObject>().BlipType = TrackedObject.BlipTypes.Diamond;
			break;
			
		case 2:
			newBallRandom.GetComponent<TrackedObject>().BlipType = TrackedObject.BlipTypes.Dot;
			break;
			
		case 3:
			newBallRandom.GetComponent<TrackedObject>().BlipType = TrackedObject.BlipTypes.Square;
			break;
			
		case 4:
			newBallRandom.GetComponent<TrackedObject>().BlipType = TrackedObject.BlipTypes.Triangle;
			break;
			
		case 5:
			newBallRandom.GetComponent<TrackedObject>().BlipType = TrackedObject.BlipTypes.X;
			break;
		}
		
		newBallRandom.transform.position = new Vector3(ballRandomX + transform.position.x, transform.position.y + 1, transform.position.z - 1);
	}
}
