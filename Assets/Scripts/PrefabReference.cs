using UnityEngine;
using System.Collections;

namespace GSP
{
	public class PrefabReference : MonoBehaviour
	{
		// These are assigned through the inspector in the editor.
		public GameObject prefabCharacter;		// This is the reference to the character prefab.
		public GameObject prefabDiceButton;		// This is the reference to the dice button prefab.
		public GameObject prefabResource_Ore;	// This is the reference to the ore resource prefab.
		public GameObject prefabResource_Wood;	// This is the reference to the wood resource prefab.
		public GameObject prefabResource_Wool;	// This is the reference to the wool resource prefab.
		public GameObject prefabResource_Fish;	// This is the reference to the fish resource prefab.

		// Use this for initialisation.
		void Start()
		{
			// Everything is assigned through the editor inspector.
		} // end Start function
		
		// Update is called once per frame.
		void Update()
		{
			// Nothing to do here.
		} // end Update function
	} // end PrefabReference class
} // end namespace
