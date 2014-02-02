using UnityEngine;
using System.Collections;


public class Camara_seguirObjeto : MonoBehaviour {

	public GameObject objetivo;
	public float Velocidad = 2f;
	float ZOriginal=0;
	Camera camara;
	float internalVel=0;
	float smoothRate=0.5f;
	Vector3 velocidadCamara;
	// Use this for initialization
	void Start () {
		ZOriginal = transform.position.z;
		velocidadCamara = new Vector3(Velocidad,Velocidad,0);
	}
	
	// Update is called once per frame
	void Update () {
		if (objetivo == null) {
			return;		
		}
		Vector3 tmp = Vector3.SmoothDamp(transform.position,
		                                 objetivo.transform.position,
		                                 ref velocidadCamara,
		                                 this.smoothRate);
		tmp.z=ZOriginal;
		this.transform.position=tmp;
		 
	}
}
