using UnityEngine;
using System.Collections;

public class Camara_seguirObjeto : MonoBehaviour {

	public GameObject objetivo;
	public float Velocidad = 5f;
	float ZOriginal=0;

	// Use this for initialization
	void Start () {
		ZOriginal = transform.position.z;
	}
	
	// Update is called once per frame
	void Update () {
		if (objetivo == null) {
			return;		
		}
		Vector3 tmp = Vector3.Lerp (transform.position, objetivo.transform.position, Time.deltaTime*Velocidad);
		tmp.z = ZOriginal;
		transform.position = tmp; 
	}
}
