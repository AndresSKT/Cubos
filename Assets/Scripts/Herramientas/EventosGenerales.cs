using System;
using System.Collections.Generic;
using UnityEngine;
namespace Herramientas
{
    public static class EventosGenerales
    {
        private static bool seHaCargadoElJuego = false;
        
        public static NotificacionSimple juegoCargado;

        public delegate void NotificacionSimple();


        public static void InvocarJuegoCargado() {
            NotificacionSimple evento = juegoCargado;
            if (!seHaCargadoElJuego && evento!=null) {
                seHaCargadoElJuego = true;
                UnityEngine.WSA.Application.InvokeOnUIThread(delegate() {
                    evento();
                }, false);
            }
        }

    }
}
