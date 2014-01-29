using System;
using System.Collections.Generic;
using UnityEngine;


namespace SmithKeyboardHelper
{
    public class SGUIComponent
    {

        protected delegate void positionUpdateHandler(float x, float y);
        protected event positionUpdateHandler RealPositionChange;
        

        protected SGUIComponent _parent=null;
        protected Rect position;
        protected Rect realPosition;

        public Rect Position {
            get {
                return position;
            }
            set {
                
                if (!value.Equals(position)) {
                    realPosition.x -= position.x;
                    realPosition.y -= position.y;
                    position = value;
                    realPosition.x += position.x;
                    realPosition.y += position.y;
                    realPosition.width = position.width;
                    realPosition.height = position.height;
                    invokeRealPositionChange(realPosition.x, realPosition.y);
                }
            }
        
        }

        public Rect RealPosition {
            get {
                return realPosition;
            }
        }

        public SGUIComponent Parent {
            get {
                return _parent;
            }
            set {
                if (_parent != null) {
                    _parent.RealPositionChange -= OnParentPositionChange;
                }
                _parent = value;
                if (_parent != null) {
                    _parent.RealPositionChange += OnParentPositionChange;
                    OnParentPositionChange(_parent.realPosition.x, _parent.realPosition.y);
                }
            }
        }

        protected void OnParentPositionChange(float x, float y){
            realPosition.x = x + position.x;
            realPosition.y = y + position.y;
            invokeRealPositionChange(realPosition.x, realPosition.y);
        }

        protected void getPositionFromGUILayout() {
            if (Event.current.type == EventType.Repaint) {
                Position = GUILayoutUtility.GetLastRect();
            }
        }

        protected void invokeRealPositionChange(float x, float y)
        {
            positionUpdateHandler tmp = RealPositionChange;
            if (tmp != null)
            {
                RealPositionChange(x, y);
            }
        }


    }
}
