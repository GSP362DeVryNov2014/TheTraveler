using UnityEngine;
using System.Collections;

namespace GSP
{
	public class PrefabReference : MonoBehaviour
	{
		public GameObject prefabCharacter;		// This is the reference to the character prefab.
		public GameObject prefabDiceButton;		// This is the reference to the character prefab.
		public GameObject prefabResource_Rock;	// This is the reference to the character prefab.

		// Use this for initialisation.
		void Start()
		{
			// Load the character prefab.
			prefabCharacter = Resources.Load( "Character" ) as GameObject;

			// Load the character prefab.
			prefabDiceButton = Resources.Load( "DiceButton" ) as GameObject;

			// Load the character prefab.
			prefabResource_Rock = Resources.Load( "Resource_Rock" ) as GameObject;
		} // end Start function
		
		// Update is called once per frame.
		void Update()
		{
			// Nothing to do here.
		} // end Update function
	} // end PrefabReference class
} // end namespace
