using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


public class TileCollisionChecker : MonoBehaviour
{

	private int currentConnections;
	private int goalConnections;


	void Start() 
	{		
		goalConnections = GetComponents<BoxCollider2D> ().Length; 
	}

	void OnTriggerEnter2D(Collider2D other) 
	{ 		
		currentConnections++;

		if (currentConnections >= goalConnections)
			MainGameController.Instance.LevelEndChecker.currentConnectedTilesCount++;

		ParticlesManager.Instance.PlayStars (other.bounds.center);
	}

	void OnTriggerExit2D(Collider2D other) 
	{ 
		currentConnections--; 

		if (currentConnections == goalConnections - 1)
			MainGameController.Instance.LevelEndChecker.currentConnectedTilesCount--;
	}
}
