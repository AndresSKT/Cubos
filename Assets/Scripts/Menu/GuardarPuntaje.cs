using System;
using System.Collections.Generic;
using UnityEngine;
using Herramientas;
using Configuracion;
using SmithKeyboardHelper;

namespace Menu
{
    class GuardarPuntaje:MonoBehaviour
    {
        public GUISkin estilo;


        GUIStyle estiloLabelPuntaje,estiloValorPuntaje;
        GUIStyle labelCentralizado;
        GUIStyle estiloBoton;
        GUIStyle estiloBotonCancelar;
        Texture2D texturaFondo;

        int anchoPanelCentral=500;
        int altoPanelCentral = 350;
        int logitudMaximaDelNombre = 20;
        SGUIComponent posPanelCentral;
        SGUIComponent posGrupoPuntaje;
        
        SGUIComponent posGrupoNombre;

        STextField CampoNombre;
        Rect posFondo;

       // Rect posGrupoBotones;

        bool _guardado = false;

        public bool HaGuardado {
            get {
                return _guardado;
            }
            private set {
                _guardado = value;
            }
        }

        long puntaje = 0;
        string strPuntaje = "0";
        public long Puntaje {
            get { return puntaje; }
            set {
                if (value >= 0) {
                    puntaje = value;
                    strPuntaje = Convert.ToString(puntaje);
                }
            }
        }

        LanguageManager traductor;

        void Awake() {
            if (estilo != null) {
                Init();
            }
            
            
        }


        private void Init()
        {
            traductor = LanguageManager.Instance;
            generarUI();
        }
        public void Init(GUISkin estilo) {
            this.estilo = estilo;
            Init();
        
        }
        

        void generarUI() {

			
			W8KeyboardManager.clearLayout();
            posPanelCentral = new SGUIComponent();
            posPanelCentral.Position = new Rect((Screen.width - anchoPanelCentral) / 2, (Screen.height - altoPanelCentral) / 4, anchoPanelCentral, altoPanelCentral);
			posGrupoPuntaje = new SGUIComponent();
            posGrupoPuntaje.Parent = posPanelCentral;
			posGrupoPuntaje.Position = new Rect(10, 10, posPanelCentral.Position.width - 20, 80);
            labelCentralizado = estilo.label;
            labelCentralizado.alignment=TextAnchor.MiddleCenter;
            estiloLabelPuntaje = estilo.GetStyle("label_score");
            estiloValorPuntaje = estilo.GetStyle("score");
            estiloBoton = estilo.GetStyle("boton_principal");
            estiloBotonCancelar = estilo.GetStyle("boton_cancelar");

            posGrupoNombre = new SGUIComponent();
            posGrupoNombre.Parent = posPanelCentral;
            posGrupoNombre.Position = new Rect(10, posGrupoPuntaje.Position.yMax + 10, posPanelCentral.Position.width - 20, 100);
            posFondo = new Rect(0, 0, Screen.width, Screen.height);
            Color colorDeFondo = new Color(1f, 1f, 1f, 0.5f);
            texturaFondo = new Texture2D(1, 1);
            texturaFondo.SetPixel(0, 0, colorDeFondo);
            texturaFondo.Apply();

            CampoNombre = new STextField();
            CampoNombre.Parent = posGrupoNombre;
            W8KeyboardManager.fields.Add(CampoNombre);
			
        }

        

        void OnGUI() {
            GUI.depth = 0;
            GUI.DrawTexture(posFondo, texturaFondo);
            GUILayout.BeginArea(posPanelCentral.Position, estilo.box);
            //puntaje
            GUILayout.BeginArea(posGrupoPuntaje.Position, estilo.box);
            GUILayout.BeginVertical();
            GUILayout.Label(traductor.GetTextValue("guardar_puntaje_label_puntaje")+":",estiloLabelPuntaje);
            GUILayout.Label(strPuntaje, estiloValorPuntaje);
            GUILayout.EndVertical();
            GUILayout.EndArea();

            //nombre
            GUILayout.BeginArea(posGrupoNombre.Position, estilo.box);
            GUILayout.BeginVertical();
            GUILayout.Label(traductor.GetTextValue("guardar_puntaje_label_nombre") + ":", estilo.label,GUILayout.ExpandWidth(true));
            GUI.SetNextControlName("campo_nombre");
            //valorTxtNombre = GUILayout.TextField(valorTxtNombre,estilo.textField);
            CampoNombre.DrawUsingGUILayout(estilo.textField);
			if (CampoNombre.Text.Length > logitudMaximaDelNombre)
			{
				CampoNombre.Text = CampoNombre.Text.Substring(0, logitudMaximaDelNombre);
			}

            GUILayout.EndVertical();
            GUILayout.EndArea();
            GUILayout.FlexibleSpace();
            //GUILayout.BeginArea(posGrupoBotones);
            GUILayout.BeginVertical();

            if (GUILayout.Button(traductor.GetTextValue("guardar_puntaje_boton_guardar_cerrar"), estiloBoton, GUILayout.ExpandWidth(true))) {
                if (CampoNombre.Text.Length > 0)
                {
                    guardarYCerrar();
                }
            }
            if (GUILayout.Button(traductor.GetTextValue("guardar_puntaje_boton_cancelar"), estiloBotonCancelar, GUILayout.ExpandWidth(true))) {
                this.enabled = false;
            }
            GUILayout.EndVertical();
           // GUILayout.EndArea();

            GUILayout.EndArea();
            W8KeyboardManager.InvokeFirstUpdate();
        }

        private void  guardarYCerrar(){
            if (CampoNombre.Text.Length>0 && CampoNombre.Text.Length<=logitudMaximaDelNombre){
                Configuracion.AdministradorPuntajes.anadirPuntaje(new Configuracion.InstanciaPuntaje(CampoNombre.Text, puntaje));
                Configuracion.AdministradorPuntajes.Guardar();
                HaGuardado = true;
                this.enabled = false;
            }

        }

        private void OnEnable() {
            W8KeyboardManager.Show();
        }

        private void OnDisable() {
            W8KeyboardManager.Hide();
        }

    }
}
