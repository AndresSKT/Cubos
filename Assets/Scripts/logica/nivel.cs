using UnityEngine;
using System.Collections;
using Configuracion;
using Logica.Objetivos;
using vida;


public class nivel : MonoBehaviour {

	public delegate void finDelNivelHandler(int puntaje);

	public event finDelNivelHandler HaFinalizadoElNivel;
	public event finDelNivelHandler ELJugadorHaPerdidoElNivel;
	#region declaracion de variables
	public GUISkin estilo;
	public Texture texturaVida;
	public Texture texturaMana;
	public Texture fondoBarra;
	public string MenuPrincipal="MenuPrincipal";
	public bool ReiniciarPuntajeAnterior=true;
	public bool ReiniciarVidaAnterior=true;
	public bool ReiniciarManaAnterior=true;
    public AudioClip audioDeFondo;


	int _puntaje=0;
	public int Puntaje{
		get{
			return _puntaje;
		}
		private set{
			_puntaje=value;
		}

	}
	public GameObject jugador;


	public string siguienteNivel=null;

	Rect posicionPuntaje;
	Rect posGrupoVida;
	Rect posBarraVida;
	Rect posLabelVida;
	Rect posLabelNombreVida;
	Rect recfondoBarraVida;

	Rect posBarraMana;
	Rect posLabelMana;
	Rect posLabelNombreMana;
	Rect recfondoBarraMana;

	GUIStyle estiloPuntaje;
	GUIStyle estiloLabelVida;
	GUIStyle estiloLabelMana;

	float porcentajeVida=0;
	float porcentajeVidaActual=0;

	float porcentajeMana=0;
	float porcentajeManaActual=0;

	float longitudBarraDeVida;
	float longitudBarraDeMana;
	string ContenidoLabelVida;
	string ContenidoLabelMana;


	float _velocidadAnimacionVida=0f;
	float _velocidadAnimacionMana=0f;
	float tiempoEnAlcanzarPorcentejaDeLaBarra=1f;


	Vida vidaJugador=null;
	mana manaJugador=null;
	Objetivo[] objetivos;

	string labelPuntaje="";
	string labelVida="";
	string labelMana="";

	LanguageManager traductor;

	#endregion

	private bool _haFinalizado;
	private int ObjetivoNoCumplidos=0;

	public bool haFinalizado{
		get {
			return _haFinalizado;
		}
		protected set{
			_haFinalizado=value;
		}
	}

	void Start () {
		estiloPuntaje = estilo.GetStyle ("puntaje");
		estiloLabelVida = estilo.GetStyle ("label_vida");
		estiloLabelMana = estilo.GetStyle("label_mana");

		posGrupoVida = new Rect (50,10,Mathf.Min(400,Screen.width),60);
		posLabelNombreVida = new Rect (13, 5, 70, 22);
		longitudBarraDeVida = posGrupoVida.width - 20 -posLabelNombreVida.xMax;
		posBarraVida = new Rect (posLabelNombreVida.xMax, 5,longitudBarraDeVida, 20);
		posLabelVida = new Rect (posBarraVida.xMin,5,longitudBarraDeVida,22);
		recfondoBarraVida = new Rect (posBarraVida.x,posBarraVida.y,posBarraVida.width,posBarraVida.height);

		posLabelNombreMana = new Rect (13, posLabelNombreVida.yMax, 70,22);
		longitudBarraDeMana = posGrupoVida.width - 20-posLabelNombreMana.xMax;
		posBarraMana = new Rect (posLabelNombreMana.xMax, posBarraVida.yMax+5,longitudBarraDeMana, 20);
		posLabelMana = new Rect (posBarraMana.xMin,posBarraMana.yMin,longitudBarraDeMana,22);
		recfondoBarraMana = new Rect (posBarraMana.x,posBarraMana.y,longitudBarraDeMana,posBarraMana.height);


		posicionPuntaje = new Rect (50, posGrupoVida.yMax+4, 300, this.estiloPuntaje.fontSize+estiloPuntaje.padding.bottom);


		if (jugador != null) {
			setJugador(jugador);
		}
		traductor = LanguageManager.Instance;
		labelinit ();
	
		objetivos = gameObject.GetComponents<Objetivo> ();
		foreach(Objetivo o in objetivos){
			if (!o.ObjetivoCumplido()){
				ObjetivoNoCumplidos++;
			}
			o.HaCumplidoElObjetivo+=alFinalizarUnObjetivo;
		}

		if (ReiniciarPuntajeAnterior){
			Herramientas.DatosEntreNiveles.ReiniciarPuntaje();
		}


		Puntaje = Herramientas.DatosEntreNiveles.Puntaje;

		if (!ReiniciarManaAnterior && jugador!=null){
			jugador.GetComponent<mana>().manaActual=Herramientas.DatosEntreNiveles.Mana;
		}

		if (!ReiniciarVidaAnterior && jugador!=null){
			jugador.GetComponent<Vida>().vida=Herramientas.DatosEntreNiveles.Vida;
		}

        if (audioDeFondo != null) {
            Sonido.PlayBackgroundInLoop(audioDeFondo);
        }

	}


	public bool todosLosObjetivosEstanCumplidos(){
		return ObjetivoNoCumplidos<=0;
	}

	void labelinit(){

		labelPuntaje = traductor.GetTextValue("guiscore");
		labelVida = traductor.GetTextValue("guilife");
		labelMana = traductor.GetTextValue("guimana");

	}
	// Update is called once per frame
	void Update () {

		if (vidaJugador != null) {
			porcentajeVida = ((float)vidaJugador.vida/vidaJugador.vidaMaxima);
			ContenidoLabelVida = vidaJugador.vida+" / "+vidaJugador.vidaMaxima;

			porcentajeMana = manaJugador.manaActual/manaJugador.maxMana;
			ContenidoLabelMana = (int)manaJugador.manaActual+" / "+manaJugador.maxMana;
		} else {
			porcentajeVida=0;
			ContenidoLabelVida = "0/?";
		
			porcentajeMana=0;
			ContenidoLabelMana = "0/?";
		}
		//porcentajeVidaActual= Mathf.Lerp(porcentajeVidaActual,porcentajeVida,Time.deltaTime);
		porcentajeVidaActual = Mathf.SmoothDamp (porcentajeVidaActual, porcentajeVida,ref _velocidadAnimacionVida, tiempoEnAlcanzarPorcentejaDeLaBarra,Mathf.Infinity,Time.deltaTime);
		posBarraVida.width=(porcentajeVidaActual*longitudBarraDeVida);

		porcentajeManaActual= Mathf.SmoothDamp (porcentajeManaActual, porcentajeMana,ref _velocidadAnimacionMana, tiempoEnAlcanzarPorcentejaDeLaBarra,Mathf.Infinity,Time.deltaTime);
		posBarraMana.width=(porcentajeManaActual*longitudBarraDeMana);

	}

	void FixedUpdate(){
		bool fintmp=todosLosObjetivosEstanCumplidos();
		if (fintmp && !haFinalizado){
			deshabilitarAlJugador();
            if (HaFinalizadoElNivel != null)
            {
                HaFinalizadoElNivel(Puntaje);
            }
		}
		haFinalizado=fintmp;
	}

	void deshabilitarAlJugador(){
		if (jugador!=null){
			jugador.GetComponent<controlador_jugador>().enabled=false;
		}
	}

	void OnGUI(){
		GUI.depth=1;

		GUI.BeginGroup (posGrupoVida, estilo.box);
			GUI.DrawTexture(recfondoBarraVida,fondoBarra);
			GUI.DrawTexture (posBarraVida, texturaVida);
			GUI.Label (posLabelVida, ContenidoLabelVida,estiloLabelVida);
			GUI.Label (posLabelNombreVida, labelVida + ": ",estilo.label);
			GUI.DrawTexture(recfondoBarraMana,fondoBarra);
			GUI.DrawTexture (posBarraMana, texturaMana);
		GUI.Label (posLabelMana, ContenidoLabelMana,estiloLabelMana);
		GUI.Label (posLabelNombreMana, labelMana + ": ",estilo.label);

		GUI.EndGroup ();
		GUI.Label (posicionPuntaje,labelPuntaje+": "+Puntaje, this.estiloPuntaje);

	}

	public void jugadorMurio(){
        if (ELJugadorHaPerdidoElNivel != null)
        {
            ELJugadorHaPerdidoElNivel(Puntaje);
        }
	}

	public void setJugador(GameObject JUG){
		jugador = JUG;
		vidaJugador = jugador.GetComponent<Vida> ();
		manaJugador = jugador.GetComponent<mana> ();
		vidaJugador.estaMueriendo+=elJugadorEstaMuriendo;
	}

	public void anadirPuntos(int puntos){
		Puntaje += puntos;
	}

	public bool HayMasNiveles{
		get{
			return (!siguienteNivel.Equals("") && siguienteNivel!=null);
		}
	}

	public void reiniciarNivel(){
        
		Herramientas.LevelLoader.CargarNivel(Application.loadedLevelName);
	}

	public void cargarMenuPrincipal(){
		Herramientas.LevelLoader.CargarNivel(MenuPrincipal);
	}

	public void elJugadorEstaMuriendo(GameObject objeto, motivoDeMuerte motivo){
		if (vidaJugador!=null){
			vidaJugador.estaMueriendo-=elJugadorEstaMuriendo;
		}
		jugadorMurio();
		if (motivo!=vida.motivoDeMuerte.Indefinida){

		}
	}

	public bool elJugadorPuedeAvanzar{
		get {return HayMasNiveles && (vidaJugador!=null && vidaJugador.estaVivo);}
	}

	void alFinalizarUnObjetivo(Objetivo o){
		ObjetivoNoCumplidos--;
	}

	public void AlmacenarDatos(){
		Herramientas.DatosEntreNiveles.Puntaje=Puntaje;
		if (jugador!=null){
			Herramientas.DatosEntreNiveles.Mana=jugador.GetComponent<mana>().manaActual;
			Herramientas.DatosEntreNiveles.Vida=jugador.GetComponent<Vida>().vida;
		}
	}

}
