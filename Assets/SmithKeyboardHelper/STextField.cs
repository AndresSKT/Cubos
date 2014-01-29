using System;
using UnityEngine;

namespace SmithKeyboardHelper
{
    public class STextField:SGUIComponent
    {

        public string Text="";

        #region DrawUsingGUI
        public void DrawUsingGUI() {
            Text = GUI.TextField(this.RealPosition, Text);
        }

        public void DrawUsingGUI(int maxLength)
        {
            Text = GUI.TextField(this.RealPosition, Text,maxLength);
        }

        public void DrawUsingGUI(GUIStyle style)
        {
            Text = GUI.TextField(this.RealPosition, Text, style);
        }

        public void DrawUsingGUI(int maxLength, GUIStyle style)
        {
            Text = GUI.TextField(this.RealPosition, Text, maxLength,style);
        }

        #endregion

        #region DrawUsingGUILayout

        public void DrawUsingGUILayout()
        {
            Text = GUILayout.TextField(Text);
            base.getPositionFromGUILayout();
        }

        public void DrawUsingGUILayout(params GUILayoutOption[] options)
        {
            Text = GUILayout.TextField(Text,options);
            base.getPositionFromGUILayout();
        }

        public void DrawUsingGUILayout(int maxLength, params GUILayoutOption[] options)
        {
            Text = GUILayout.TextField(Text,maxLength, options);
            base.getPositionFromGUILayout();
        }

        public void DrawUsingGUILayout(GUIStyle style, params GUILayoutOption[] options)
        {
            Text = GUILayout.TextField(Text,style,options);
            base.getPositionFromGUILayout();
        }

        public void DrawUsingGUILayout(int maxLength, GUIStyle style, params GUILayoutOption[] options)
        {
            Text = GUILayout.TextField(Text,maxLength,style,options);
            base.getPositionFromGUILayout();
        }
        #endregion

    }
}
