using UnityEngine;
using System.Collections;
using Configuracion;
using Herramientas;


namespace Menu
{
	[RequireComponent(typeof(SeleccionDeIdioma))]
	public class MenuPrincipal : MonoBehaviour
	{

		string campo="******";


		public GUISkin estiloMenu;
		public float anchoMenuCentral = 600;
		public string primerNivel="";

		Rect posMenuCentral;
		Rect posBotonesAudio;
		Rect posBotonSonido;
		Rect posBotonMusica;
		Rect posFondo;
		GUIStyle estiloBotonMusicaOff;
		GUIStyle estiloBotonMusicaOn;
		GUIStyle estiloBotonSonidoOff;
		GUIStyle estiloBotonSonidoOn;
		Texture2D texturaFondo;
		bool carga = false;
		GUIStyle estiloBotonMenuCentral;
		LanguageManager traduccion;
		SeleccionDeIdioma menuDeIdioma;
		
		// Use this for initialization
		void Start ()
		{
			estiloBotonMenuCentral = estiloMenu.GetStyle ("boton_principal");

			float altoMenuCentral = estiloBotonMenuCentral.CalcHeight (new GUIContent("Dummy"),float.MaxValue)*3;
			posMenuCentral = new Rect ((Screen.width - anchoMenuCentral) / 2,Mathf.Max((Screen.height-altoMenuCentral)/2,0), anchoMenuCentral, altoMenuCentral);

			posBotonesAudio = new Rect (Screen.width - 160, 10, 160, 70);
			posBotonSonido = new Rect (0, 0, 70, 70);
			posBotonMusica = new Rect (posBotonesAudio.width - 10 - 70, 0, 70, 70);
			estiloBotonMusicaOff = estiloMenu.GetStyle ("boton_musica_off");
			estiloBotonMusicaOn = estiloMenu.GetStyle ("boton_musica_on");
			estiloBotonSonidoOff = estiloMenu.GetStyle ("boton_sonido_off");
			estiloBotonSonidoOn = estiloMenu.GetStyle ("boton_sonido_on");

			configuracionGeneral.Load ();
			traduccion = LanguageManager.Instance;

			menuDeIdioma = GetComponent<SeleccionDeIdioma> ();
			menuDeIdioma.enabled = false;
		
			posFondo = new Rect (0, 0, Screen.width, Screen.height);
			texturaFondo = new Texture2D (1, 1);
			texturaFondo.SetPixel (0, 0, Color.black);
			texturaFondo.Apply ();


			string idioma = configuracionGeneral.Idioma;
			if (idioma==null){
				menuDeIdioma.enabled=true;
				menuDeIdioma.puedeCerrar=false;
			}
			else {
				traduccion.ChangeLanguage(idioma);
			}

		}

        string ultimoCampoSeleccionado = "";
        string campoSeleccionado = "";
        private void actualizarEntrada() {
            
            if (ultimoCampoSeleccionado.Equals(campoSeleccionado)) {
                return;
            }
            ultimoCampoSeleccionado = campoSeleccionado;
            Rect posCampo = new Rect(0, 0, 0, 0);
            switch (ultimoCampoSeleccionado) {
                case "campoTexto": { EntradaWindows8.SetTextoActual(ref campo);
                                        posCampo = posTexto;
                                        break; }
                    default: EntradaWindows8.SetTextoActual(); break;
            }

            if (!ultimoCampoSeleccionado.Equals(""))
            {
                if (!EntradaWindows8.ElTecladoEstaActivo)
                {
                    EntradaWindows8.mostrar(posCampo.x, posCampo.y, posCampo.width, posCampo.height);
                }
            }
            else {
                    EntradaWindows8.cerrarElTeclado();
             
            }
            
            
        }

		// Upis called once per frame
		void Update ()
		{
			if (menuDeIdioma.enabled) {
				return;
			}
            actualizarEntrada();

			if (carga) {
			}
		}

        Rect posTexto = new Rect(200, 0, 300, 20);

		void OnGUI ()
		{
			if (menuDeIdioma.enabled) {
				return;
			}

			//GUI.DrawTexture (posFondo, texturaFondo);

			//botones musica
			GUI.BeginGroup (posBotonesAudio);
			if (GUI.Button (posBotonSonido, "", (configuracionGeneral.estaElSonidoActivo ? estiloBotonSonidoOn : estiloBotonSonidoOff))) {
				configuracionGeneral.estaElSonidoActivo = !configuracionGeneral.estaElSonidoActivo;
			}
			if (GUI.Button (posBotonMusica, "", (configuracionGeneral.estaLaMusicaActiva ? estiloBotonMusicaOn : estiloBotonMusicaOff))) {
				configuracionGeneral.estaLaMusicaActiva = !configuracionGeneral.estaLaMusicaActiva;
			}
			GUI.EndGroup ();
            GUI.SetNextControlName("campoTexto");
            campo = GUI.TextField(posTexto,campo);
            
			GUILayout.BeginArea (posMenuCentral, estiloMenu.box);

			GUILayout.BeginVertical ();
            
			if (GUILayout.Button (traduccion.GetTextValue ("menuprincipalnuevaaventura"), estiloBotonMenuCentral)) {
				//LevelLoader.CargarNivel(primerNivel);
				
			}
			if (GUILayout.Button(traduccion.GetTextValue("menuprincipalnuevaidioma"),estiloBotonMenuCentral)){
				menuDeIdioma.enabled=true;
				menuDeIdioma.puedeCerrar=true;
			}
			GUILayout.EndVertical ();
			GUILayout.EndArea ();
            campoSeleccionado = GUI.GetNameOfFocusedControl();

		}

	}
}