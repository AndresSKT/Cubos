using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Windows;
using System.IO;

namespace Configuracion
{
	public class AdministradorPuntajes
	{
		private static List<InstanciaPuntaje> puntajes = null;
		static int max=15;
	

		public static List<InstanciaPuntaje> Puntajes{
			get{
				if (puntajes==null){
					leer();
				}
				return new List<InstanciaPuntaje>( puntajes);
			}

		}

		public static void anadirPuntaje(InstanciaPuntaje p){
			if (puntajes==null){
				puntajes = new List<InstanciaPuntaje>();
			}
			puntajes.Add(p);
			puntajes.Sort();
            if (puntajes.Count>max){
				puntajes.RemoveAt(max);
			}

		}

		public static void Guardar(){
			if (puntajes!=null){
				using (System.IO.MemoryStream hilo = new System.IO.MemoryStream()){
					hilo.Write(BitConverter.GetBytes(puntajes.Count),0,4);
                    foreach (InstanciaPuntaje ip in puntajes) { 
                        guardarInstanciaPuntaje(hilo,ip);
                    }
                    string path = UnityEngine.Windows.Directory.localFolder+"/private1";
					UnityEngine.Windows.File.WriteAllBytes(path,hilo.ToArray());
				}
			}
		}

        private static void guardarInstanciaPuntaje(Stream stream, InstanciaPuntaje puntaje) {
            stream.Write(BitConverter.GetBytes(puntaje.Nombre.Length), 0, 4);
            foreach (char c in puntaje.Nombre) {
                stream.WriteByte((byte)c);
            }
            stream.Write(BitConverter.GetBytes(puntaje.Puntaje), 0, 8);
        }

        private static InstanciaPuntaje leerInstanciaPuntaje(Stream stream)
        {
            byte[] buffint = new byte[4];
            byte[] bufflong = new byte[8];
            String tmpnombre = "";

            stream.Read(buffint, 0, 4);
            int tamnombre = BitConverter.ToInt32(buffint,0);
            for (int id = 0; id < tamnombre;id++ )
            {
                tmpnombre += (char)stream.ReadByte();
            }
            stream.Read(bufflong, 0, 8);
            return new InstanciaPuntaje(tmpnombre,BitConverter.ToInt64(bufflong,0));
        }

		private static void leer(){
            string path = UnityEngine.Windows.Directory.localFolder + "/private1";
            if (!UnityEngine.Windows.File.Exists(path)) {
                puntajes = new List<InstanciaPuntaje>();
                return;
            }
            if (puntajes == null) {
                puntajes = new List<InstanciaPuntaje>();
            }

            byte[] datosBinarios = UnityEngine.Windows.File.ReadAllBytes(path);
			using (System.IO.MemoryStream hilo = new System.IO.MemoryStream()){
				hilo.Write(datosBinarios,0,datosBinarios.Length);
				hilo.Position=0;
                byte[] buff = new byte[4];
                hilo.Read(buff, 0, 4);
                int tam = BitConverter.ToInt32(buff, 0);
                for (int i = 0; i < tam; i++) {
                    puntajes.Add(leerInstanciaPuntaje(hilo));
                }
			}

            puntajes.Sort();

		}

	}

	public struct InstanciaPuntaje : IComparable<InstanciaPuntaje>{
		private String nombre;
		private long puntaje;

		public InstanciaPuntaje(String n, long p){
            this.nombre = n;
            this.puntaje = p;
		}

		public string Nombre{
			get {
				return nombre;
			}

			private set{
				nombre=value;
				if (nombre.Length>20){
					nombre=nombre.Substring(0,20);
				}
			}
		}

		public long Puntaje{
			get{
				return puntaje;
			}

			private set{
				puntaje=value;
			}
		}




        public int CompareTo(InstanciaPuntaje other)
        {
            if (other.Puntaje == this.Puntaje) {
                return Nombre.CompareTo(other.Nombre);
            }
            if (this.Puntaje < other.Puntaje)
            {
                return 1;
            }
            else {
                return -1;
            }
        }
    }
}

