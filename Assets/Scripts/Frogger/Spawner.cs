using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{

	public float spawnTime = 3f;		// The amount of time between each spawn.
	public float spawnDelay = 0f;		// The amount of time before spawning starts.
	public float spawnX;
	public float spawnSpeed = 10;
	public GameObject[] enemies;		// Array of enemy prefabs.

	void Start ()
	{
		// Start calling the Spawn function repeatedly after a delay .
		InvokeRepeating("Spawn", spawnDelay, spawnTime);
	}

	void Spawn ()
	{
		// Instantiate a random enemy.

		int enemyIndex = Random.Range(0, enemies.Length);

		var clone = Instantiate(enemies[enemyIndex], transform.position, Quaternion.identity);

		Fire((GameObject) clone);

		// Play the spawning effect from all of the particle systems.
		foreach(ParticleSystem p in GetComponentsInChildren<ParticleSystem>())
		{
			p.Play();
		}
	}
	

	void Fire (GameObject go)
	{
		go.rigidbody2D.velocity = new Vector2 (spawnX * spawnSpeed, 0);

		Vector3 scale = go.transform.localScale;
		scale.x *= spawnX;
		go.transform.localScale = scale;
	}
}
