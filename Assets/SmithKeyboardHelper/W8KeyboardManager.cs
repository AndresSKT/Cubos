using System;
using System.Collections.Generic;
using UnityEngine.WSA;
using UnityEngine;


namespace SmithKeyboardHelper
{
    public static class W8KeyboardManager
    {

        private static bool firstUpdate = false;

        public delegate void SLayoutActionHandler();
        public delegate void SLayoutHandler(List<STextField> textFields);
        public static event SLayoutHandler UpdateLayout;
        public static event SLayoutActionHandler hideLayer;
        public static event SLayoutActionHandler showLayer;

        public static List<STextField> fields = new List<STextField>(); 

		private static bool keyboardIsOpen=false;

		public  static bool isKeyboardOpen{
			get{
				return keyboardIsOpen;
			}
			set{
				keyboardIsOpen=value;
			}
		}

        public static void InvokeUpdateLayout() {
            List<STextField> fieldsClone = new List<STextField>(fields);
            //Debug.Log("Invoke");
            //Debug.Log(fieldsClone.Count);
            UnityEngine.WSA.Application.InvokeOnUIThread(() => {
                SLayoutHandler tmp = UpdateLayout;
                if (tmp != null)
                {
                    UpdateLayout(fieldsClone);
                }
            }, false);      
        }

        public static void clearLayout(){
            fields.Clear();
            InvokeUpdateLayout();
            firstUpdate = false;
        }

        public static void InvokeFirstUpdate() {
            if (!firstUpdate && Event.current.type == EventType.Repaint) {
                firstUpdate = true;
                InvokeUpdateLayout();
            }
        }

        public static void Show() {
            SLayoutActionHandler tmp = showLayer;
            if (showLayer != null) {
                UnityEngine.WSA.Application.InvokeOnUIThread(() => {
                    tmp();
                }, false);
            }
        }


        public static void Hide()
        {
            SLayoutActionHandler tmp = hideLayer;
            if (showLayer != null)
            {
                UnityEngine.WSA.Application.InvokeOnUIThread(() =>
                {
                    tmp();
                }, false);
            }
        }


        }

}
