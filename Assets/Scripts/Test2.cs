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
			
		}
		
		// Update is called once per frame
		void Update()
		{
			if ( Input.GetKeyDown( KeyCode.A ) )
			{
				TileManager.SetDimensions(64, 20, 16);

				TileManager.GenerateAndAddTiles();
			}
		}
	}
}
