using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.WSA;
using UnityEngine;


namespace Herramientas
{
    public class EntradaWindows8
    {

        public delegate void mostrarTecladoHandler(int posx, int posy, int ancho, int largo);
        public delegate void AccionTecladoHandler();
        public static event mostrarTecladoHandler MostrarElTeclado;
        public static event AccionTecladoHandler cerrarTeclado;


		private static bool estaActivoElTeclado=false;

		public  static bool ElTecladoEstaActivo{
			get{
				return estaActivoElTeclado;
			}
			set{
				estaActivoElTeclado=value;
			}
		}

        private static string texto = "";


        public static string TextoActual {

            get {
                return texto;
                
            }

            set {
                if (value != null) {
                    texto = value;
                }
            }

        }

        public static void SetTextoActual(ref string nuevaRef) {
            texto = nuevaRef;
        }

        public static void SetTextoActual() {
            texto = "";
        }

        public static void reiniciarTexto() {
            texto = "";
        }

        public static void mostrar(float x, float y, float ancho, float largo)
        {
            UnityEngine.WSA.Application.InvokeOnUIThread(delegate()
            {
                OnMostrarEnUI(x,y,ancho,largo);
            }, false);
        }

        private static void OnMostrarEnUI(float x, float y, float ancho, float alto)
        {
            if (MostrarElTeclado != null)
            {
                MostrarElTeclado((int)x,(int)y,(int)ancho,(int)alto);
            }
        }


        public static void cerrarElTeclado() {
            UnityEngine.WSA.Application.InvokeOnUIThread(delegate() {
                cerrarTeclado();
            }, false);
            
        }
    }
}
