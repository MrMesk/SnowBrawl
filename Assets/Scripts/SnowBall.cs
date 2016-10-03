using UnityEngine;
using System.Collections;

public class SnowBall : MonoBehaviour
{
	public float lifeTime;
	public int teamIndex;
	float _timer;

	// Use this for initialization
	void Start ()
	{
		_timer = lifeTime;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(_timer > 0f)
		{
			_timer -= Time.deltaTime;
		}
		else
		{
			Destroy(gameObject);
		}
	}

	void OnCollisionEnter(Collision other)
	{
		Destroy(this.gameObject);
	}
}
