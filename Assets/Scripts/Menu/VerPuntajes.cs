using System;
using System.Collections.Generic;
using UnityEngine;
using Configuracion;

namespace Menu
{
    class VerPuntajes: MonoBehaviour
    {

        public GUISkin estilo;
        public Texture2D texturaFondo;

        Rect posPanelCentral;
        int anchoPanelCentral = 500;
        int altoPanelCentral = 500;
        Rect posBotonCerrar;
        Rect posFondo,coordFondo;

        LanguageManager traductor;
        GUIStyle estiloBotonAtras;
        GUIStyle estiloPanel;
        GUIStyle estiloPanelPuntaje,estiloNombrePuntaje,estiloValorPuntaje;

        Vector2 posisionScrollPuntajes = Vector2.zero;

        List<Configuracion.InstanciaPuntaje> puntajes = new List<Configuracion.InstanciaPuntaje>();

        void GenerarUI() {
            estiloBotonAtras = estilo.GetStyle("boton_atras");
            estiloPanel = new GUIStyle(estilo.box);
            estiloPanel.padding.top += 20;
            estiloPanel.padding.left += 20;
            estiloPanel.padding.right += 20;
            estiloPanel.padding.bottom += 20;
            estiloPanelPuntaje = estilo.GetStyle("panel_nombre_puntuacion");
            estiloNombrePuntaje = estilo.GetStyle("label_nombre_puntuacion");
            estiloValorPuntaje = estilo.GetStyle("label_valor_puntuacion");

            posBotonCerrar = new Rect(20, 20, 64, 64);
            posFondo = new Rect(0, 0, Screen.width, Screen.height);
            coordFondo = new Rect(1 - (Mathf.Min(texturaFondo.width, Screen.width) / (float)texturaFondo.width), 1 - (Mathf.Min(texturaFondo.height, Screen.height) / (float)texturaFondo.height), 1, 1);
            posPanelCentral = new Rect((Screen.width - anchoPanelCentral) / 2, 80, anchoPanelCentral, altoPanelCentral);
            traductor = LanguageManager.Instance;
        }

        void Start() {
            GenerarUI();
            puntajes = Configuracion.AdministradorPuntajes.Puntajes;
        }

        void Update() {
            
        }

        void OnEnable()
        {
            puntajes = Configuracion.AdministradorPuntajes.Puntajes;
        }

        void OnGUI() {
            GUI.depth = 0;
            GUI.DrawTextureWithTexCoords(posFondo, texturaFondo, coordFondo);
            if (GUI.Button(posBotonCerrar, "", estiloBotonAtras)) {
                this.enabled = false;
            }
            GUILayout.BeginArea(posPanelCentral,estiloPanel);
            GUILayout.Label(traductor.GetTextValue("ver_puntajes_titulo"),estilo.label);
            posisionScrollPuntajes = GUILayout.BeginScrollView(posisionScrollPuntajes);
            foreach (InstanciaPuntaje inst in puntajes) {
                GUILayout.BeginHorizontal(estiloPanelPuntaje);
                GUILayout.Label(inst.Nombre, estiloNombrePuntaje);
                GUILayout.FlexibleSpace();
                GUILayout.Label(inst.Puntaje+"",estiloValorPuntaje);
                GUILayout.EndHorizontal();
            }
            GUILayout.EndScrollView();
            GUILayout.EndArea();
            
        }

    }
}
