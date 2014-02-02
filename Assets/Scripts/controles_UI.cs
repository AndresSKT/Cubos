using UnityEngine;
using System.Collections;
using TouchScript;

public class controles_UI :InputWrapper {



	public botonUI FlechaIzquierda;
	public botonUI FlechaDerecha;
	public botonUI Saltar;

	public botonUI Disparar1;
	public botonUI Disparar2;





	public int alturaBotones=100;
	float margenInferior=30;
	Rect grupoBotonesDisparar;
	Pausa menuPausa;
	TouchManager entradaTouch;


	void Start () {
		menuPausa = GameObject.FindObjectOfType<Pausa>();

		if (menuPausa==null){
			Debug.LogWarning("No hay opcion de Pausa en el nivel");

		}

		FlechaIzquierda.posicion = new Rect(30,10,alturaBotones,alturaBotones);
		FlechaDerecha.posicion = new Rect(FlechaIzquierda.posicion.width*2,10,alturaBotones,alturaBotones);
		Saltar.posicion = new Rect(0,0,alturaBotones,alturaBotones);

		FlechaIzquierda.posicion.y = Screen.height - (FlechaIzquierda.posicion.height + margenInferior);
		FlechaDerecha.posicion.y = Screen.height - (FlechaDerecha.posicion.height + margenInferior);
		Saltar.posicion.y = Screen.height - (2*(Saltar.posicion.height + margenInferior));
		Saltar.posicion.x = Screen.width - ((Saltar.posicion.width*2) + 30);

		grupoBotonesDisparar = new Rect(0,Saltar.posicion.yMax+margenInferior,(alturaBotones+margenInferior)*3,alturaBotones);
		grupoBotonesDisparar.x = Saltar.posicion.center.x-(grupoBotonesDisparar.width/2);
		Disparar1.posicion =  new Rect(grupoBotonesDisparar.x,grupoBotonesDisparar.y,alturaBotones,alturaBotones);
		Disparar2.posicion = new Rect(grupoBotonesDisparar.x+((alturaBotones+margenInferior)*2),grupoBotonesDisparar.y,alturaBotones,alturaBotones);


		entradaTouch = TouchManager.Instance;

	}
	
	// Update is called once per frame
	void Update () {
		if (!menuPausa.estaEnPausa){
			ProcesarEntradaTactil ();
		}

	}

	void ProcesarEntradaTactil ()
	{
		this.Horizontal = 0;
		Jump = 0;
		Disparar [0] = false;
		Disparar [1] = false;
		bool tmoFlechaDerecha = false;
		bool tmoFlechaIzquierda = false;
		bool tmoSaltar = false;
		bool tmoDisparar1 = false;
		bool tmoDisparar2 = false;

		Vector2 posicion = new Vector2 ();
		foreach (TouchPoint dedo in entradaTouch.TouchPoints) {
			posicion.x = dedo.Position.x;
			posicion.y = Screen.height - dedo.Position.y;
			if (checkHover (FlechaIzquierda, posicion)){
				tmoFlechaIzquierda=true;
			}
			if (checkHover (FlechaDerecha, posicion)){
				tmoFlechaDerecha=true;
			}
			if (checkHover (Saltar, posicion)){
				tmoSaltar=true;
			}
			if (checkHover (Disparar1, posicion)){
				tmoDisparar1=true;
			}
			if (checkHover (Disparar2, posicion)){
				tmoDisparar2=true;
			}
		}

		FlechaDerecha.ishover = tmoFlechaDerecha;
		FlechaIzquierda.ishover = tmoFlechaIzquierda;
		Saltar.ishover = tmoSaltar;
		Disparar1.ishover = tmoDisparar1;
		Disparar2.ishover = tmoDisparar2;


		if (FlechaIzquierda.ishover) {
			_horizontal -= 1;
		}
		if (FlechaDerecha.ishover) {
			_horizontal += 1;
		}
		if (Saltar.firstClick) {
			_jump = 1;
		}
		if (Disparar1.ishover) {
			Disparar [0] = true;
		}
		if (Disparar2.ishover) {
			Disparar [1] = true;
		}
	}

	void OnGUI(){
		if (menuPausa.estaEnPausa){
			return;
		}

		GUI.depth=10;

		Color colOriginal=GUI.color;
		GUI.color=FlechaIzquierda.color;
		GUI.DrawTexture (FlechaIzquierda.posicion, FlechaIzquierda.Texture);
		GUI.color=FlechaDerecha.color;
		GUI.DrawTexture (FlechaDerecha.posicion, FlechaDerecha.Texture);
		GUI.color=Saltar.color;
		GUI.DrawTexture (Saltar.posicion, Saltar.Texture);

		GUI.color=Disparar1.color;
		GUI.DrawTexture(Disparar1.posicion,Disparar1.Texture);
		GUI.color=Disparar2.color;
		GUI.DrawTexture(Disparar2.posicion,Disparar2.Texture);

		GUI.color=colOriginal;
	}

	public bool checkHover(botonUI boton, Vector2 posicion){
		return  boton.posicion.Contains (posicion);
		
	}


	[System.Serializable]
	public class botonUI {
		public Texture normal;
		public Color normalColor;
		public Texture hover;
		public Color hoverColor;


		private bool _ishover=false;

		public bool ishover{
			get {return _ishover;}
			set {
				if (value){
					firstClick=!_ishover;

				}else{
					firstClick=false;
				}
				_ishover=value;
			}
		}

		[HideInInspector]
		public bool firstClick=false;


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

		public Color color{
			get{
				if (ishover){
					return hoverColor;
				}
				return normalColor;
			}
		}

	}
}
