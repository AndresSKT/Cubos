using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ParallaxBackground : MonoBehaviour {

	public GameObject fondoInstancia;
	public int clones=3;
	public bool FixedEnX=true;
	public bool FixedEnY=true;
	public Vector2 offSet=Vector2.zero;
	public float divisorDeVelocidad=10f;
	private List<GameObject> instancias = new List<GameObject>();

	private Vector3 lastPos;
	private Camera camara;
	private Plane[] planosCamara;
	// Use this for initialization
	void Start () {
		camara = Camera.main;
		this.transform.position = new Vector3(camara.transform.position.x,this.transform.position.y,transform.position.z);
		for(int i=0;i<clones;i++){
			GameObject tmpGO = Instantiate(fondoInstancia) as GameObject;
			if (instancias.Count==0){
				tmpGO.transform.position=this.transform.position+new Vector3(offSet.x,offSet.y,0);
			}else{
				Bounds limites = (instancias[instancias.Count-1].renderer.bounds);
				tmpGO.transform.position=new Vector3(limites.center.x+(limites.extents.x*2),this.transform.position.y+offSet.y,this.transform.position.z);
			}
			tmpGO.transform.parent=this.transform;
			instancias.Add(tmpGO);
		}
		this.transform.parent=camara.transform;
		lastPos=transform.position;
	}
	// Update is called once per frame
	void FixedUpdate () {
		planosCamara = GeometryUtility.CalculateFrustumPlanes(camara); //la funcion que mas me preocupa

		bool moviendoAlaDerecha=camara.velocity.x>0;
		bool moviendoAlaIzquierda=camara.velocity.x<0;

		if (moviendoAlaDerecha){
			GameObject item = instancias[0];
			if (!GeometryUtility.TestPlanesAABB(planosCamara,item.renderer.bounds)){
				instancias.RemoveAt(0);
				Bounds limites = instancias[instancias.Count-1].renderer.bounds;
				instancias.Add(item);
				item.transform.position=new Vector3(limites.center.x+(limites.extents.x*2),limites.center.y,this.transform.position.z);
			}
		}else if (moviendoAlaIzquierda){
			GameObject item = instancias[instancias.Count-1];
			if (!GeometryUtility.TestPlanesAABB(planosCamara,item.renderer.bounds)){
				instancias.RemoveAt(instancias.Count-1);
				Bounds limitesPrimer = (instancias[0].renderer.bounds);
				instancias.Insert(0,item);
				item.transform.position=new Vector3(limitesPrimer.center.x-(limitesPrimer.extents.x*2),limitesPrimer.center.y,this.transform.position.z);
			}
		}

	}

	void Update(){
		Vector3 deltaPos = this.transform.position-lastPos;
		foreach(GameObject g in instancias){
			if (!FixedEnY){
				g.transform.position = new Vector3(g.transform.position.x,g.transform.position.y-deltaPos.y,g.transform.position.z);
			}
			if (!FixedEnX){
				g.transform.position = new Vector3(g.transform.position.x-deltaPos.x,g.transform.position.y,g.transform.position.z);
			}
		}
		if (divisorDeVelocidad!=0){
				float mult=camara.velocity.x*-1;
				float delta=0;
				delta = (mult*Time.deltaTime)/divisorDeVelocidad;
				Vector3 tmp =this.transform.position;
				tmp.x+=delta;
				this.transform.position = tmp;
			}

		lastPos=transform.position;

	}
}
