using UnityEngine;
using System.Collections;
using GSP.Tiles;

namespace GSP
{
	public class Test2 : MonoBehaviour
	{
		
		// Use this for initialization
		void Start()
		{
			// Nothing here.
		}
		
		// Update is called once per frame
		void Update()
		{
			// Set the dimensions and create the tiles.
			if ( Input.GetKeyDown( KeyCode.A ) )
			{
				TileManager.SetDimensions(64, 20, 16);

				TileManager.GenerateAndAddTiles();
			}

			// Empty the tile dictionary.
			if ( Input.GetKeyDown( KeyCode.B ) )
			{
				TileDictionary.Clean();
			}
		}
	}
}
