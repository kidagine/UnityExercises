using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemManager : MonoBehaviour
{
	public ParticleSystem[] particleSystems;

	void Start () {
		GameEventManager.GameStart += GameStart;
		GameEventManager.GameOver += GameOver;
		GameOver();
	}

	private void GameStart () {
		for(int i = 0; i < particleSystems.Length; i++){
			particleSystems[i].Clear();
			ParticleSystem.EmissionModule emission = particleSystems[i].emission;
			emission.enabled = true;
		}
	}

	private void GameOver () {
		for(int i = 0; i < particleSystems.Length; i++){
			ParticleSystem.EmissionModule emission = particleSystems[i].emission;
			emission.enabled = false;
		}
	}
}
