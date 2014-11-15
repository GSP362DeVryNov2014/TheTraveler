using UnityEngine;
using System.Collections;

namespace GSP
{
	public class PrefabReference : MonoBehaviour
	{
		// These are assigned through the inspector in the editor.
		public static GameObject prefabCharacter;		// This is the reference to the character prefab.
		public static GameObject prefabDiceButton;		// This is the reference to the dice button prefab.
		public static GameObject prefabResource_Rock;	// This is the reference to the character prefab.
		
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

