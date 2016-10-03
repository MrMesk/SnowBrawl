using UnityEngine;
using System.Collections;

public class NetworkController : MonoBehaviour
{
	public Vector3 spawnPos;
	public CameraFollow cameraFollower;
	public GameObject playerPrefab;

	string _gameVersion = "0.1";	
	// Use this for initialization
	void Start ()
	{
		PhotonNetwork.ConnectUsingSettings(_gameVersion);
		spawnPos = new Vector3(Random.Range(-10f, 10f), 0f, Random.Range(-10f, 10f));
		if(cameraFollower == null)
		{
			cameraFollower = GameObject.Find("CameraHolder").GetComponent<CameraFollow>();
		}
	}
	
	// Update is called once per frame
	void Update ()
	{

	}

	void OnJoinedLobby()
	{
		Debug.Log("Connecting to random room");
		PhotonNetwork.JoinRandomRoom();
	}

	void OnPhotonRandomJoinFailed()
	{
		Debug.Log("Can't join random room ! Creating one");
		PhotonNetwork.CreateRoom(null);
	}

	void OnJoinedRoom()
	{
		Debug.Log("Room joined");
		GameObject character = PhotonNetwork.Instantiate(playerPrefab.name, spawnPos, Quaternion.identity,0);
		character.name = "Player " + PhotonNetwork.playerList.Length;
		character.transform.Find("PlayerName").GetComponent<TextMesh>().text = "Player " + PhotonNetwork.playerList.Length;
		cameraFollower.player = character;
	}
}
