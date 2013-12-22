using UnityEngine;
using System.Collections;

public class Camara_seguirObjeto : MonoBehaviour {

	public GameObject objetivo;
	public float Velocidad = 20f;
	public Rect areaLibre = new Rect(0.2f,0.2f,0.6f,0.6f);
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
		Vector3 posicionPantalla = Camera.main.WorldToViewportPoint (objetivo.transform.position);
		if (!areaLibre.Contains(new Vector2(posicionPantalla.x,posicionPantalla.y))){
			Vector3 tmp = Vector3.Lerp (transform.position, objetivo.transform.position, Time.deltaTime*Velocidad);
			tmp.z = ZOriginal;
			transform.position = tmp;
		}
		 
	}
}
