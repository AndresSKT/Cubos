using UnityEngine;
using System.Collections;

public class animacionDeExplosion : MonoBehaviour {

	public float tiempoDeVida=1f;

	// Use this for initialization
	void Start () {
		Destroy(this.gameObject,tiempoDeVida);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
