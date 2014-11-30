using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GSP.Char;

namespace GSP
{
	public class EndSceneData : MonoBehaviour
	{
		Dictionary<int, EndSceneCharData> m_dataDictionary;


		// Use this for ininitialisation.
		public void Start()
		{
			// Initialises the end scene data dictionary to a new dictionary.
			m_dataDictionary = new Dictionary<int, EndSceneCharData>();
		}

		// Adds data to the end scene data dictionary.
		public void AddData( int playerNum, GameObject player )
		{
			// Create a new EndSceneCharData object using the given info.
			EndSceneCharData charData = new EndSceneCharData( playerNum, player );

			// Add that object to the dictionary using the player number as the key.
			m_dataDictionary.Add( playerNum, charData );
		} // end AddData function

		// Retrieves the end scene data from the dictionary based on the key supplied.
		public EndSceneCharData GetData( int playerNum )
		{
			// Check if the dictionary contains the key.
			if (!m_dataDictionary.ContainsKey( playerNum ))
			{
				// No key found so describe that and return null.
				Debug.LogWarning("The EndSceneData Dictionary contains no such key: '" + playerNum + "'");
				return null;
			}

			// Otherwise get the value and return it.
			return m_dataDictionary[playerNum];
		} // end GetData function.
	}
} // end namespace
