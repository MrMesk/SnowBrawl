using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{

	private Vector2 velocity;
	public float smoothTimeY;
	public float smoothTimeX;
	public float smoothTimeShakeY;
	public float smoothTimeShakeX;

	[HideInInspector]public GameObject player;

	Vector3 pos;
	GameObject cameraChild;
	Ray ray;

	void Start ()
	{
		cameraChild = transform.Find("Camera").gameObject;
	}

	void FixedUpdate()
	{
		if(player)
		{
			pos = player.transform.position;
			Vector3 toMouse = transform.position;
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray, out hit))
			{

				toMouse = hit.point - player.transform.position;
				toMouse = toMouse / 10;
				pos += toMouse;


			}
			float posX = Mathf.SmoothDamp(transform.position.x, pos.x, ref velocity.x, smoothTimeX);
			float posY = Mathf.SmoothDamp(transform.position.z, pos.z, ref velocity.y, smoothTimeY);
			transform.position = new Vector3(posX, transform.position.y, posY);
		}
		
	}

	public IEnumerator ScreenShake (Vector3 dir)
	{
		Vector3 initialPos;
		initialPos = cameraChild.transform.position;
		float posXChild = cameraChild.transform.position.x;
		float posYChild = cameraChild.transform.position.z;

		while (posXChild != initialPos.x + dir.x && posYChild != initialPos.z + dir.z)
		{
			posXChild = Mathf.SmoothDamp(initialPos.x, initialPos.x + dir.x, ref velocity.x, smoothTimeShakeY);
			posYChild = Mathf.SmoothDamp(initialPos.z, initialPos.z + dir.z, ref velocity.y, smoothTimeShakeX);
			yield return null;
		}

		while (posXChild != initialPos.x && posYChild != initialPos.y)
		{
			posXChild = Mathf.SmoothDamp(initialPos.x + dir.x, initialPos.x, ref velocity.x, smoothTimeShakeY);
			posYChild = Mathf.SmoothDamp(initialPos.z + dir.z, initialPos.z, ref velocity.y, smoothTimeShakeX);
			yield return null;
		}

		/*
		cameraChild.transform.position = Vector3.Lerp(cameraChild.transform.position, cameraChild.transform.position + dir.normalized / cameraShakePan, 0.05f);
		yield return new WaitForSeconds(0.05f);
		cameraChild.transform.position = Vector3.Lerp(cameraChild.transform.position, cameraChild.transform.position - dir.normalized / cameraShakePan, 0.05f);
		*/
	}
}
