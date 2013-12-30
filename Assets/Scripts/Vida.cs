using UnityEngine;
using System.Collections;

public class Vida : MonoBehaviour {

	public int vida=100;
	public int vidaMaxima=100;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (vida <= 0) {
			Destroy(this.gameObject);
		}
	}
	
	public void aplicarDaño(int dano){
		vida -= dano;
	}
		

	}