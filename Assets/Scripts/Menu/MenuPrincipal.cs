using UnityEngine;
using System.Collections;
using Configuracion;
using Herramientas;


namespace Menu
{
	[RequireComponent(typeof(SeleccionDeIdioma)),RequireComponent(typeof(VerPuntajes))]
	public class MenuPrincipal : MonoBehaviour
	{

		

		public GUISkin estiloMenu;
		public float anchoMenuCentral = 600;
		public string primerNivel="";
        public Texture2D texturaFondo;
        
        public Texture2D logo;

		Rect posMenuCentral;
		Rect posBotonesAudio;
		Rect posBotonSonido;
		Rect posBotonMusica;
		Rect posFondo;
        Rect coordFondo;
        Rect posLogo;
		GUIStyle estiloBotonMusicaOff;
		GUIStyle estiloBotonMusicaOn;
		GUIStyle estiloBotonSonidoOff;
		GUIStyle estiloBotonSonidoOn;
		
        bool carga = false;
		GUIStyle estiloBotonMenuCentral;
		LanguageManager traduccion;
		SeleccionDeIdioma menuDeIdioma;
        VerPuntajes menuPuntajes;
		
		// Use this for initialization
		void Awake ()
		{
			estiloBotonMenuCentral = estiloMenu.GetStyle ("boton_principal");

			float altoMenuCentral = estiloBotonMenuCentral.CalcHeight (new GUIContent("Dummy"),float.MaxValue)*4;
			posMenuCentral = new Rect ((Screen.width - anchoMenuCentral) / 2,Mathf.Max((Screen.height-altoMenuCentral)/2,0), anchoMenuCentral, altoMenuCentral);

			posBotonesAudio = new Rect (Screen.width - 160, 10, 160, 70);
			posBotonSonido = new Rect (0, 0, 70, 70);
			posBotonMusica = new Rect (posBotonesAudio.width - 10 - 70, 0, 70, 70);
			estiloBotonMusicaOff = estiloMenu.GetStyle ("boton_musica_off");
			estiloBotonMusicaOn = estiloMenu.GetStyle ("boton_musica_on");
			estiloBotonSonidoOff = estiloMenu.GetStyle ("boton_sonido_off");
			estiloBotonSonidoOn = estiloMenu.GetStyle ("boton_sonido_on");
            posLogo = new Rect((Screen.width - logo.width) / 2, 30, logo.width, logo.height);
            

			configuracionGeneral.Load ();
			traduccion = LanguageManager.Instance;

			menuDeIdioma = GetComponent<SeleccionDeIdioma> ();
			menuDeIdioma.enabled = false;
            menuPuntajes = GetComponent<VerPuntajes>();
            menuPuntajes.enabled = false;
		
			posFondo = new Rect (0, 0, Screen.width, Screen.height);
            coordFondo = new Rect(1 - (Mathf.Min(texturaFondo.width, Screen.width) / (float)texturaFondo.width), 1 - (Mathf.Min(texturaFondo.height, Screen.height) / (float)texturaFondo.height), 1, 1);
            string idioma = configuracionGeneral.Idioma;
			if (idioma==null){
				menuDeIdioma.enabled=true;
				menuDeIdioma.puedeCerrar=false;
			}
			else {
				traduccion.ChangeLanguage(idioma);
			}

		}

		// Upis called once per frame
		void Update ()
		{
			if (menuDeIdioma.enabled || menuPuntajes.enabled) {
				return;
			}
            
		}

        
		void OnGUI ()
		{
			if (menuDeIdioma.enabled || menuPuntajes.enabled) {
				return;
			}

			GUI.DrawTextureWithTexCoords(posFondo, texturaFondo,coordFondo);

            GUI.DrawTexture(posLogo, logo);

			//botones musica
			GUI.BeginGroup (posBotonesAudio);
			if (GUI.Button (posBotonSonido, "", (configuracionGeneral.estaElSonidoActivo ? estiloBotonSonidoOn : estiloBotonSonidoOff))) {
				configuracionGeneral.estaElSonidoActivo = !configuracionGeneral.estaElSonidoActivo;
			}
			if (GUI.Button (posBotonMusica, "", (configuracionGeneral.estaLaMusicaActiva ? estiloBotonMusicaOn : estiloBotonMusicaOff))) {
				configuracionGeneral.estaLaMusicaActiva = !configuracionGeneral.estaLaMusicaActiva;
			}
			GUI.EndGroup ();
            
            GUILayout.BeginArea (posMenuCentral, estiloMenu.box);

			GUILayout.BeginVertical ();
            
			if (GUILayout.Button (traduccion.GetTextValue ("menuprincipalnuevaaventura"), estiloBotonMenuCentral)) {
				LevelLoader.CargarNivel(primerNivel);
				
			}
			if (GUILayout.Button(traduccion.GetTextValue("menuprincipalnuevaidioma"),estiloBotonMenuCentral)){
				menuDeIdioma.enabled=true;
				menuDeIdioma.puedeCerrar=true;
			}
            if (GUILayout.Button(traduccion.GetTextValue("menuprincipalverpuntajes"),estiloBotonMenuCentral))
            {
                menuPuntajes.enabled = true;
            }

			GUILayout.EndVertical ();
			GUILayout.EndArea ();
            

		}

	}
}