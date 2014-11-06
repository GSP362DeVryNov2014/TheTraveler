using UnityEngine;
using System.Collections;

namespace GSP
{
	public class PrefabReference : MonoBehaviour
	{
		public GameObject prefabCharacter;	// This is the reference to the character prefab.

		// Use this for initialisation.
		void Start()
		{
			// Load the character prefab.
			prefabCharacter = Resources.Load( "character" ) as GameObject;
		} // end Start function
		
		// Update is called once per frame.
		void Update()
		{
			// Nothing to do here.
		} // end Update function
	} // end PrefabReference class
} // end namespace
