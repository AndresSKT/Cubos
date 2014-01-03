using UnityEngine;
using System.Collections;
using System;


namespace ataque{
public abstract class AtaqueBase : MonoBehaviour,IComparable<AtaqueBase> {


		public int index=0;
		public int consumoDeMana=0;

	protected bool _puedeDisparar=false;
		[HideInInspector]
		public Vector2 direccion;


	public abstract bool Disparar();
	


	public bool PuedeDisparar{
		get{
			return _puedeDisparar;
		}
		protected set{
			_puedeDisparar=value;
		}

	}

		#region IComparable implementation
		public int CompareTo (AtaqueBase obj)
		{
			return this.index-obj.index;
		}
		#endregion
}
}