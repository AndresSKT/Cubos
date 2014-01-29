using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ParallaxBackground : MonoBehaviour {

	public GameObject fondoInstancia;
	public int clones=3;
	public bool parallax=true;
	public float velocidadPorSegundo=10f;
	private List<GameObject> instancias = new List<GameObject>();



	private Camera camara;
	private Plane[] planosCamara;
	// Use this for initialization
	void Start () {
		camara = Camera.main;
		this.transform.position = new Vector3(camara.transform.position.x,this.transform.position.y,transform.position.z);
		for(int i=0;i<clones;i++){
			GameObject tmpGO = Instantiate(fondoInstancia) as GameObject;
			tmpGO.transform.parent=this.transform;
			if (instancias.Count==0){
				tmpGO.transform.position=this.transform.position;
			}else{
				Bounds limites = (instancias[instancias.Count-1].renderer.bounds);
				tmpGO.transform.position=new Vector3(limites.center.x+(limites.extents.x*2),this.transform.position.y,this.transform.position.z);
			}
			instancias.Add(tmpGO);
		}
		if (this.parallax){
			this.transform.parent=camara.transform;
		}
	}
	
	// Update is called once per frame
	void Update () {
		planosCamara = GeometryUtility.CalculateFrustumPlanes(camara); //la funcion que mas me preocupa

		bool moviendoAlaDerecha=camara.velocity.x>0;
		if (moviendoAlaDerecha){
			GameObject item = instancias[0];
			if (!GeometryUtility.TestPlanesAABB(planosCamara,item.renderer.bounds)){
				instancias.RemoveAt(0);
				Bounds limites = instancias[instancias.Count-1].renderer.bounds;
				instancias.Add(item);
				item.transform.position=new Vector3(limites.center.x+(limites.extents.x*2),this.transform.position.y,this.transform.position.z);
			}
		}else{
			GameObject item = instancias[instancias.Count-1];
			if (!GeometryUtility.TestPlanesAABB(planosCamara,item.renderer.bounds)){
				instancias.RemoveAt(instancias.Count-1);
				Bounds limitesPrimer = (instancias[0].renderer.bounds);
				instancias.Insert(0,item);
				item.transform.position=new Vector3(limitesPrimer.center.x-(limitesPrimer.extents.x*2),this.transform.position.y,this.transform.position.z);
			}
		}

		if (parallax){
			float mult=0;
			if (camara.velocity.x>0){
				mult=-1;
			}else if (camara.velocity.x<0){
				mult=1;
			}
			float delta = (velocidadPorSegundo*Time.deltaTime)*mult;
			Vector3 tmp =this.transform.position;
			tmp.x+=delta;
			this.transform.position = tmp;
		}
	}
}
