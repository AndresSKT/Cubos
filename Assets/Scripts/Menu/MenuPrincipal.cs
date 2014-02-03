using UnityEngine;
using System.Collections;
using Configuracion;
using Herramientas;


namespace Menu
{
	[RequireComponent(typeof(SeleccionDeIdioma)),RequireComponent(typeof(VerPuntajes))]
	public class MenuPrincipal : MonoBehaviour
	{

		private bool  aPantallaCompleta=false;
		

		public GUISkin estiloMenu;
		public float anchoMenuCentral = 600;
		public string primerNivel="";
        public Texture2D texturaFondo;
        
        public Texture2D logo;


		private Texture2D texturaFondoMono;
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

		bool mustShowLanguage=false;

		// Use this for initialization
		void Awake ()
		{
			estiloBotonMenuCentral = estiloMenu.GetStyle ("boton_principal");

			estiloBotonMusicaOff = estiloMenu.GetStyle ("boton_musica_off");
			estiloBotonMusicaOn = estiloMenu.GetStyle ("boton_musica_on");
			estiloBotonSonidoOff = estiloMenu.GetStyle ("boton_sonido_off");
			estiloBotonSonidoOn = estiloMenu.GetStyle ("boton_sonido_on");
            

			configuracionGeneral.Load ();
			traduccion = LanguageManager.Instance;

			menuDeIdioma = GetComponent<SeleccionDeIdioma> ();
			menuDeIdioma.enabled = false;
            menuPuntajes = GetComponent<VerPuntajes>();
            menuPuntajes.enabled = false;
		
			string idioma = configuracionGeneral.Idioma;
			if (idioma==null){
				traduccion.ChangeLanguage("en");
				mustShowLanguage=true;
			}
			else {
				mustShowLanguage=false;
				traduccion.ChangeLanguage(idioma);
			}
			CambioTamanoVentanaWindowsStore.cambioTamanoPantalla+=alCambiarTamanoPantalla;
			alCambiarTamanoPantalla(Screen.width,Screen.height);
		
			texturaFondoMono = new Texture2D(1,1);
			texturaFondoMono.SetPixel(0,0,new Color(142/255f,223/255f,1));
			texturaFondoMono.Apply();                
		}

		void alCambiarTamanoPantalla(int ancho, int alto){
			//Debug.Log("ReaL: "+ancho);
			float altoMenuCentral = estiloBotonMenuCentral.CalcHeight (new GUIContent("Dummy"),float.MaxValue)*4;
			posMenuCentral = new Rect ((Screen.width - anchoMenuCentral) / 2,Mathf.Max((Screen.height-altoMenuCentral)/2,0), anchoMenuCentral, altoMenuCentral);
			posBotonesAudio = new Rect (Screen.width - 160, 10, 160, 70);
			posBotonSonido = new Rect (0, 0, 70, 70);
			posBotonMusica = new Rect (posBotonesAudio.width - 10 - 70, 0, 70, 70);
			posLogo = new Rect((Screen.width - logo.width) / 2, 30, logo.width, logo.height);
			posFondo = new Rect (0, 0, Screen.width, Screen.height);
			coordFondo = new Rect(1 - (Mathf.Min(texturaFondo.width, Screen.width) / (float)texturaFondo.width), 1 - (Mathf.Min(texturaFondo.height, Screen.height) / (float)texturaFondo.height), 1, 1);

			aPantallaCompleta= ancho>alto;
			if (aPantallaCompleta){
				Sonido.continuarBackground();
			}else{
				Sonido.PausarBackGround();
			}

		}

		// Upis called once per frame
		void Update ()
		{

			if (!aPantallaCompleta){
				return;
			}

			if (mustShowLanguage){
				menuDeIdioma.enabled=true;
				menuDeIdioma.puedeCerrar=false;
				mustShowLanguage=false;
			}

			if (menuDeIdioma.enabled || menuPuntajes.enabled) {
				return;
			}
            
		}

		void FixedUpdate(){
			aPantallaCompleta = Screen.width>Screen.height;
		}

		void OnGUI ()
		{
			GUI.depth=0;
			if (!aPantallaCompleta){
				GUI.DrawTexture(posFondo,texturaFondoMono);
				GUI.Label(posFondo,traduccion.GetTextValue("general_debe_ser_fullscreen"),estiloMenu.label);
				return;
			}

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