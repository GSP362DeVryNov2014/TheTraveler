using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GSP.Tiles
{
	public class TileDictionary : MonoBehaviour
	{
		// Declare our private static dictionary variable here.
		// Vector3 is the key and Tile is the value.
		static Dictionary<Vector3, Tile> m_tileDictionary;
		
		// Use this for initialization
		void Start()
		{
			// Initialise the dictionary here.
			m_tileDictionary = new Dictionary<Vector3, Tile>();
		} // end Start function
		
		// Update is called once per frame
		void Update()
		{
			// Nothing to do here at the moment.
		} // end Update function

		// Returns if the key exists in the dictionary.
		public bool EntryExists( Vector3 key )
		{
			// Check if the key exists returning the result.
			return m_tileDictionary.ContainsKey( key );
		} // end EntryExixts function

		// Gets the tile associated with the given key
		public Tile GetTile( Vector3 key )
		{
			// Just as a caution in case the caller doesn't check for the key's existence first.
			// Or if it's easier to just call this. We'll check for the keys existence first.
			if ( !EntryExists( key ) )
			{
				// The key doesn't exist so return null.
				return null;
			} // end if statement

			// Otherwise the key exists so return the value according to the key.
			return m_tileDictionary[key];
		} // end GetTile function

		// Add an entry to the dictionary.
		public void AddEntry( Vector3 key, Tile tile )
		{
			// Add the entry to the dictionary.
			Debug.Log("TEST!");
			if (m_tileDictionary == null)
				Debug.Log("NULL");
			//m_tileDictionary.Add(Vector3.zero, null); //( key, tile );
		} // end AddEntry function

		// Remove an entry from the dictionary.
		public void RemoveEntry( Vector3 key )
		{
			// As a precautionary measure, check if the key exists.
			if ( !EntryExists( key ) )
			{
				// The key doesn't exist so just return.
				return;
			} // end if statement

			// First get the resource game object in the tile.
			GameObject obj = m_tileDictionary[key].Resource;

			// Now destroy the game object.
			Destroy( obj );

			// Finally, remove the entry from the dictionary.
			m_tileDictionary.Remove( key );
		} // end RemoveEntry function
	} // end TileDictionary class
} // end namespace
