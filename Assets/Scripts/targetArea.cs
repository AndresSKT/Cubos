using UnityEngine;
using System.Collections;

namespace Logica.otros
{
	[RequireComponent(typeof(Collider2D))]
	public class targetArea : MonoBehaviour
	{

		public delegate void onTriggerEnterHandler (Collider2D coll);

		public event onTriggerEnterHandler TriggerEnter2D;

		// Use this for initialization
		void Start ()
		{

		}

		// Update is called once per frame
		void Update ()
		{

		}

		void OnTriggerEnter2D (Collider2D coll)
		{
			if (coll.gameObject.CompareTag ("Player")) {
				onTriggerEnterHandler tmpEvent = TriggerEnter2D;
				if (tmpEvent != null) {
					TriggerEnter2D (coll);
				}
			}
		}
	}
}