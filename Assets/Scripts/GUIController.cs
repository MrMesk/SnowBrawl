using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class GUIController : MonoBehaviour
{
	Text statusText;
	Text masterText;
	// Use this for initialization
	void Start ()
	{
		statusText = GameObject.Find("StatusText").GetComponent<Text>();
		masterText = GameObject.Find("IsMasterText").GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		statusText.text = "Status : " + PhotonNetwork.connectionStateDetailed.ToString();
		masterText.text = "Is Master : " + PhotonNetwork.isMasterClient;
	}
}
