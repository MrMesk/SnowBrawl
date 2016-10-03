using UnityEngine;
using System.Collections;

public class CharacterController : MonoBehaviour
{
	PhotonView view;
	public float speed;
	public float dashSpeed;
	[Range(0.1f, 5f)]
	public float dashCD;

	Vector3 dashDir;
	Vector3 adjDir;
	Vector3 movement;

	float dashTimer;
	float v;
	float h;

	Rigidbody rigid;

	// Use this for initialization
	void Start ()
	{
		view = GetComponent<PhotonView>();
		dashDir = Vector3.zero;
		rigid = GetComponent<Rigidbody>();
	}

	void Update()
	{
		v = Input.GetAxis("Vertical");
		h = Input.GetAxis("Horizontal");

		movement = new Vector3(h, 0f, v).normalized * speed;

		if (Input.GetButtonDown("Jump") && dashTimer == 0f)
		{
			Dash();
			dashTimer = dashCD;
		}

		if (dashTimer > 0f)
		{
			dashTimer -= Time.deltaTime;
		}
		else
		{
			dashTimer = 0f;
		}

		if (dashDir != Vector3.zero)
		{
			ReduceDash();
		}

		adjDir = movement + dashDir;
	}

	void FixedUpdate ()
	{
		if(view.isMine)
		{
			if (h != 0f || v != 0f)
			{
				//transform.Translate(adjDir * speed * Time.deltaTime, Space.World);
				rigid.velocity = adjDir * Time.fixedDeltaTime;
			}
		}

	}


	void ReduceDash ()
	{
		dashDir = Vector3.MoveTowards(dashDir, Vector2.zero, 10f);
	}

	void Dash ()
	{
		dashDir = movement.normalized * dashSpeed;
	}
}
