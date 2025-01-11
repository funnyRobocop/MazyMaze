using UnityEngine;
using System.Collections;

public class ParticlesManager : MonoBehaviour 
{

	private ObjectsPool<ParticleSystem> starsParticlesPool = new ObjectsPool<ParticleSystem> ();
	public static ParticlesManager Instance { get; private set; }
	public bool canPlay;


	void Awake()
	{
		Instance = this;
	}

	public void PlayStars (Vector3 position) 
	{
		if (!canPlay)
			return;

		ParticleSystem starsParticles;

		if (starsParticlesPool.IsEmpty ()) 
			starsParticles = (GameObject.Instantiate(Resources.Load("StarsParticles")) as GameObject).GetComponent<ParticleSystem> ();
		else 
			starsParticles = starsParticlesPool.GetObjectFromPool();

		starsParticles.transform.position = position;
		starsParticles.Play ();
		this.Invoke (starsParticles.time, () => 
		{ 
			starsParticles.Clear();
			starsParticlesPool.SendObjectToPool(starsParticles); 
		});
	}
}
