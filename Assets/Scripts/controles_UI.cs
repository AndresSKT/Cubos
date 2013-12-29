using UnityEngine;
using System.Collections;
using TouchScript;

[RequireComponent(typeof(TouchManager))]
public class controles_UI :InputWrapper {


	public botonUI FlechaIzquierda;
	public botonUI FlechaDerecha;
	public botonUI Saltar;

	public int alturaBotones=72;
	float margenInferior=10;


	TouchManager entradaTouch;


	void Start () {
		FlechaIzquierda.posicion = new Rect(20,0,alturaBotones,alturaBotones);
		FlechaDerecha.posicion = new Rect(100,0,alturaBotones,alturaBotones);
		Saltar.posicion = new Rect(0,0,alturaBotones,alturaBotones);

		FlechaIzquierda.posicion.y = Screen.height - (FlechaIzquierda.posicion.height + margenInferior);
		FlechaDerecha.posicion.y = Screen.height - (FlechaDerecha.posicion.height + margenInferior);
		Saltar.posicion.y = Screen.height - (2*(Saltar.posicion.height + margenInferior));
		Saltar.posicion.x = Screen.width - ((Saltar.posicion.width*2) + 30);



		entradaTouch = GetComponent<TouchManager> ();

	}
	
	// Update is called once per frame
	void Update () {
		this.Horizontal= 0;
		Jump = 0;

		FlechaDerecha.ishover = false;
		FlechaIzquierda.ishover = false;
		Saltar.ishover = false;
		Vector2 posicion = new Vector2();

		foreach (TouchPoint dedo in entradaTouch.TouchPoints) {
			posicion.x=dedo.Position.x;
			posicion.y=Screen.height-dedo.Position.y;
			checkHover(FlechaIzquierda,posicion);
			checkHover(FlechaDerecha,posicion);
			checkHover(Saltar,posicion);
		}
		/*
		if (Input.GetKey (KeyCode.Mouse0) || Input.GetKey (KeyCode.Mouse1)) {
			Vector2 posmouse = new Vector2 (Input.mousePosition.x,Screen.height-Input.mousePosition.y);
			checkHover (FlechaIzquierda, posmouse);
			checkHover (FlechaDerecha, posmouse);
			checkHover (Saltar, posmouse);
		}*/

		if (FlechaIzquierda.ishover) {
			_horizontal-=1;
		}

		if (FlechaDerecha.ishover) {
			_horizontal+=1;
		}

		if (Saltar.ishover) {
			_jump=1;
		}


	}

	void OnGUI(){

		GUI.DrawTexture (FlechaIzquierda.posicion, FlechaIzquierda.Texture);
		GUI.DrawTexture (FlechaDerecha.posicion, FlechaDerecha.Texture);
		GUI.DrawTexture (Saltar.posicion, Saltar.Texture);


	}

	public void checkHover(botonUI boton, Vector2 posicion){
		if (boton.posicion.Contains (posicion)) {
			boton.ishover=true;
			return;
		}
	}

	[System.Serializable]
	public class botonUI {
		public Texture normal;
		public Texture hover;

		[HideInInspector]
		public bool ishover=false;

		[HideInInspector]
		public Rect posicion;

		public Texture Texture{
			get{
				if (ishover){
					return hover;
				}
				return normal;
			}
		}

	}
}
