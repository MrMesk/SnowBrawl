using UnityEngine;
using System.Collections;

public class ShotManager : MonoBehaviour
{
	PhotonView view;
	public float shotSpeed;
	public float cooldown;
	[Range(1,2)]
	public int teamIndex;
	public int ballAmount;
	public int snowCount;

	float cdTimer;
	Ray ray;
	Vector3 pos;
	// Use this for initialization
	void Start ()
	{
		cdTimer = cooldown;
		view = GetComponent<PhotonView>();
	}

	void RefillBalls()
	{
		if(snowCount >= 100)
		{
			snowCount -= 100;
			ballAmount++;
		}
	}

	// Update is called once per frame
	void Update ()
	{
		if (view.isMine)
		{
			if (cdTimer > 0f)
			{
				cdTimer -= Time.deltaTime;
			}
			else
			{
				cdTimer = 0f;
			}

			if (Input.GetMouseButton(0) && cdTimer == 0f && ballAmount > 0)
			{
				Shoot();
			}


			RefillBalls();

		}
	}
	void Shoot ()
	{

		Vector3 toMouse = transform.position;
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

		if (Physics.Raycast(ray, out hit))
		{
			toMouse = hit.point - transform.position;
			toMouse = toMouse / 10;
			toMouse.y = 0f;
			pos += toMouse;

			
			GameObject shot = PhotonNetwork.Instantiate("Snowball", transform.position + toMouse.normalized, transform.rotation,0);
			shot.GetComponent<Rigidbody>().AddForce(toMouse.normalized * shotSpeed);
			shot.GetComponent<SnowBall>().teamIndex = teamIndex;
			cdTimer = cooldown;
			ballAmount--;
		}
	}

}
