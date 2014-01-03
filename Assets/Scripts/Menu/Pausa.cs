using UnityEngine;
using System.Collections;
using TouchScript;
using Configuracion;
using Herramientas;


[RequireComponent(typeof(nivel))]
public class Pausa : MonoBehaviour {



	public Texture botonPausa;
	public GUISkin estilo;
	public string nombreEscenaMenuPrincipal;

	Texture2D fondo;
	private bool _estaEnPausa=false;
	private float escalaDeTiempoOriginal=1;
	private Rect posBotonPausa;
	private TouchManager entradaTactil;
	private Rect posFondo;
	private Rect posMenuCentral;
	private Rect posBotonContinuar;
	private Rect posBotonVolver;
	private Rect posBotonMusica;
	private Rect posBotonSonido;
	private Rect posBotonReiniciar;

	float anchoMenuCentral=300;

	Color colorDeFondo;
	GUIStyle estiloBotonPrincipal;
	GUIStyle estiloBotonVolverAlMenuPrincipal;
	GUIStyle estiloBotonMusicaOff;
	GUIStyle estiloBotonMusicaOn;
	GUIStyle estiloBotonSonidoOff;
	GUIStyle estiloBotonSonidoOn;

	LevelLoader cargarNivel;
	SpriteRenderer spr;
	nivel instNivel;


	public bool estaEnPausa{
		get {
			return _estaEnPausa;
		}

	}

	public void pausar(){
		_estaEnPausa=true;
		Time.timeScale=0;
	}

	public void continuar(){
		_estaEnPausa=false;
		Time.timeScale=escalaDeTiempoOriginal;
	}

	void Start () {
		Vector2 tamtmp= new Vector2();
		escalaDeTiempoOriginal=Time.timeScale;
		estiloBotonPrincipal = estilo.GetStyle("boton_principal");
		estiloBotonVolverAlMenuPrincipal=estilo.GetStyle("boton_cancelar");
		posBotonPausa = new Rect(Screen.width-64-10,10,64,64);
		posFondo = new Rect(0,0,Screen.width,Screen.height);
		entradaTactil=TouchManager.Instance;
		colorDeFondo = new Color(1f,1f,1f,0.5f);
		fondo = new Texture2D(1,1);
		fondo.SetPixel(0,0,colorDeFondo);
		fondo.Apply();

		instNivel = GetComponent<nivel>();


		posMenuCentral = new Rect((Screen.width-anchoMenuCentral)/2,0,anchoMenuCentral,300);
		posMenuCentral.y = (Screen.height-posMenuCentral.height)/2;
		tamtmp=estiloBotonPrincipal.CalcSize(new GUIContent(LanguageManager.Instance.GetTextValue("menupausacontinuar")));
		posBotonContinuar = new Rect((posMenuCentral.width-tamtmp.x)/2,10,tamtmp.x,tamtmp.y);
		tamtmp=estiloBotonVolverAlMenuPrincipal.CalcSize(new GUIContent(LanguageManager.Instance.GetTextValue("menupausamenu")));
		posBotonVolver = new Rect((posMenuCentral.width-tamtmp.x)/2,posMenuCentral.height-tamtmp.y-10,tamtmp.x,tamtmp.y);

		tamtmp=estiloBotonVolverAlMenuPrincipal.CalcSize(new GUIContent(LanguageManager.Instance.GetTextValue("menupausareiniciar")));
		posBotonReiniciar = new Rect((posMenuCentral.width-tamtmp.x)/2,posBotonVolver.yMin-tamtmp.y-10,tamtmp.x,tamtmp.y);

		posBotonSonido = new Rect((posMenuCentral.width/2)-70-10,posBotonContinuar.yMax+20,70,70);
		posBotonMusica = new Rect((posMenuCentral.width/2)+10,posBotonContinuar.yMax+20,70,70);
	
		estiloBotonMusicaOff = estilo.GetStyle("boton_musica_off");
		estiloBotonMusicaOn = estilo.GetStyle("boton_musica_on");
		estiloBotonSonidoOff = estilo.GetStyle("boton_sonido_off");
		estiloBotonSonidoOn = estilo.GetStyle("boton_sonido_on");

		Configuracion.configuracionGeneral.Load();

		cargarNivel = GameObject.FindObjectOfType<LevelLoader>();
	}

	void Update () {
		if (instNivel.haFinalizado){return;}
		if (!estaEnPausa){
			entradaNoPausa();
		}
	}

	void entradaNoPausa(){
		Vector2 puntotmp=new Vector2();
		bool botonpausapresionado=false;
		foreach(TouchPoint punto in entradaTactil.TouchPoints){
			puntotmp.x=punto.Position.x;
			puntotmp.y=Screen.height-punto.Position.y;
			if (posBotonPausa.Contains(puntotmp)){
				botonpausapresionado=true;
				break;
			}
		}

		if (botonpausapresionado){
			pausar();
		}
	}

	void OnApplicationPause(bool estado){
		if (estado){
			pausar();

		}else{
		//	continuar();
		}

	}

	Vector2 scrollPosition = Vector2.zero;

	void OnGUI(){
		GUI.depth=5;
		if (!estaEnPausa){
			GUI.DrawTexture(posBotonPausa,this.botonPausa);
		}else{
			GUI.DrawTexture(posFondo,this.fondo);
			GUI.BeginGroup(posMenuCentral,estilo.box);
			if (GUI.Button(posBotonContinuar,LanguageManager.Instance.GetTextValue("menupausacontinuar"),estiloBotonPrincipal)){
				continuar();
			}

			if (GUI.Button(posBotonSonido,"",(configuracionGeneral.estaElSonidoActivo)?estiloBotonSonidoOn:estiloBotonSonidoOff)){
					configuracionGeneral.estaElSonidoActivo=!configuracionGeneral.estaElSonidoActivo;
			}

			if (GUI.Button(posBotonMusica,"",(configuracionGeneral.estaLaMusicaActiva)?estiloBotonMusicaOn:estiloBotonMusicaOff)){
					configuracionGeneral.estaLaMusicaActiva=!configuracionGeneral.estaLaMusicaActiva;
			}
			if (GUI.Button(posBotonVolver,LanguageManager.Instance.GetTextValue("menupausamenu"),estiloBotonVolverAlMenuPrincipal)){
					LevelLoader.CargarNivel(nombreEscenaMenuPrincipal);
					continuar();
			}
			if (GUI.Button(posBotonReiniciar,LanguageManager.Instance.GetTextValue("menupausareiniciar"),estiloBotonVolverAlMenuPrincipal)){
				LevelLoader.CargarNivel(Application.loadedLevelName);
				continuar();
			}

			GUI.EndGroup();

		}

	}

}
