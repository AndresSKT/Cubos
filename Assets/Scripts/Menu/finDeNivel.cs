using UnityEngine;
using System.Collections;
using Menu;



[RequireComponent(typeof(nivel))]
[RequireComponent(typeof(Pausa))]
[RequireComponent(typeof(Menu.GuardarPuntaje))]
public class finDeNivel : MonoBehaviour {

	public GUISkin estilo;
	Texture2D texturaFondo;

	LanguageManager lang;
	int anchoPanelPrincipal=400;
	int altoPanelPrincipal=400;
	nivel instNivel;
	Rect posFondo;
	Rect posPanelPrincipal;
    private Menu.GuardarPuntaje instGuardarPuntaje;

	private bool ejecutando=false;
	GUIStyle estiloLabelScoreName;
	GUIStyle estiloScore;
	GUIStyle estiloBotonCancelar;
	GUIStyle estiloBotonPrincipal;

	// Use this for initialization
	void Start () {
		instNivel = GetComponent<nivel>();
		instNivel.HaFinalizadoElNivel+=OnNivelFinalizado;
		instNivel.ELJugadorHaPerdidoElNivel+=OnNivelFinalizado;

		posFondo = new Rect(0,0,Screen.width,Screen.height);
		Color colorDeFondo = new Color(1f,1f,1f,0.5f);
		texturaFondo = new Texture2D(1,1);
		texturaFondo.SetPixel(0,0,colorDeFondo);
		texturaFondo.Apply();

		lang = LanguageManager.Instance;

		estiloLabelScoreName = estilo.GetStyle("label_score");
		estiloScore = estilo.GetStyle("score");
		estiloBotonPrincipal = estilo.GetStyle("boton_principal");
		estiloBotonCancelar = estilo.GetStyle("boton_cancelar");

		posPanelPrincipal = new Rect((Screen.width-anchoPanelPrincipal)/2,(Screen.height-altoPanelPrincipal)/2,anchoPanelPrincipal,altoPanelPrincipal);
        instGuardarPuntaje = GetComponent<GuardarPuntaje>();
        if (instGuardarPuntaje == null) {
            this.gameObject.AddComponent<GuardarPuntaje>();
            instGuardarPuntaje = GetComponent<GuardarPuntaje>();
            instGuardarPuntaje.Init(this.estilo);
        }
        instGuardarPuntaje.enabled = false;
	}

	void OnEnable(){

	}

	void OnDisable(){

	}

	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI(){
		if(!ejecutando || instGuardarPuntaje.enabled){return;}
		GUI.DrawTexture(posFondo,texturaFondo);
		GUILayout.BeginArea(posPanelPrincipal,estilo.box);
		GUILayout.BeginVertical();
		GUILayout.BeginVertical(GUILayout.MaxHeight(200));
		GUILayout.Label(lang.GetTextValue("menufinEscenaScore")+":",estiloLabelScoreName);
		GUILayout.Label(instNivel.Puntaje+"",estiloScore);
		GUILayout.EndVertical();
		GUILayout.FlexibleSpace();
        if (instNivel.elJugadorPuedeAvanzar)
        {
            if (GUILayout.Button(LanguageManager.Instance.GetTextValue("menufinEscenaSiguiente"), estiloBotonPrincipal, GUILayout.MaxHeight(50)))
            {
                instNivel.AlmacenarDatos();
                Herramientas.LevelLoader.CargarNivel(instNivel.siguienteNivel);
            }
        }
        else {
            if (!instGuardarPuntaje.HaGuardado && GUILayout.Button(LanguageManager.Instance.GetTextValue("menufinEscenaGuardar"), estiloBotonPrincipal, GUILayout.MaxHeight(50)))
            {
                
                instGuardarPuntaje.enabled = true;
                instGuardarPuntaje.Puntaje = instNivel.Puntaje;
            }
        }
        if (!instGuardarPuntaje.HaGuardado && GUILayout.Button(LanguageManager.Instance.GetTextValue("menupausareiniciar"), estiloBotonCancelar, GUILayout.MaxHeight(50)))
        {
			instNivel.reiniciarNivel();
		}

		if (GUILayout.Button(LanguageManager.Instance.GetTextValue("menufinEscenaCancelar"),estiloBotonCancelar,GUILayout.MaxHeight(50))){
			instNivel.cargarMenuPrincipal();
		}
		GUILayout.EndVertical();
		GUILayout.EndArea();

	}

	void OnNivelFinalizado(int puntaje){
		ejecutando=true;
        Sonido.PausarBackGround();
	}
}
