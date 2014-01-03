using UnityEngine;
using System.Collections;

namespace bala{

public class logicaBalaExplota : logicaBala {

	public GameObject area;
	

		float radio=0;
		// Use this for initialization
	void Start () {
			instVida = GetComponent<vida.Vida>();
			radio=area.GetComponent<CircleCollider2D>().radius;
			morir(tiempoDeVida);

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnDestroy(){
			Vector2 punto = new Vector2(area.transform.position.x,area.transform.position.y);
			Collider2D[] objetos =Physics2D.OverlapCircleAll(punto,radio);
			foreach(Collider2D coll in objetos){
				if (coll.gameObject!=null && coll.gameObject.CompareTag("enemigo")){
					coll.gameObject.GetComponent<vida.Vida>().aplicarDaño(this.daño);
				}
			}

	}
}
}