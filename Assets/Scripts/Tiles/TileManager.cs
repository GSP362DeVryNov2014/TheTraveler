using UnityEngine;
using System.Collections;
using GSP.Char;

namespace GSP.Tiles
{
	public static class TileManager
	{
		// Declare the tile values that will be used here. This will be the same that is used in Tiled when creating the map.
		static int m_tileSize = 0;		// The size of each tile.
		static int m_numTilesWide = 0;	// The number of tiles in width.
		static int m_numTilesHigh = 0;	// The number of tile in height.

		// Gets the tile size.
		public static int TileSize
		{
			get { return m_tileSize; }
		} // end TileSize property.

		// Gets the number of tiles in width
		public static int NumTilesWide
		{
			get { return m_numTilesWide; }
		} // end NumTilesWide property

		// Gets the number of tiles in width
		public static int NumTilesHigh
		{
			get { return m_numTilesHigh; }
		} // end NumTilesWide property

		// Gets the max height you can place.
		public static int MaxHeight
		{
			get
			{
				// Calculate the height of the map. Then subtract the tile size. This is the highest coord the tile can be placed at.
				int temp = NumTilesHigh * TileSize - TileSize;

				// Since we're in the fourth quadrant, the y value is negative.
				return (temp * -1);
			} // end get accessor
		} // end MaxHeight property

		// Gets the max height you can place.
		public static int MaxWidth
		{
			get
			{
				// Calculate the width of the map. Then subtract the tile size. This is the highest coord the tile can be placed at.
				return (NumTilesWide * TileSize - TileSize);
			} // end get accessor
		} // end MaxHeight property

		// Get the map size.
		public static Vector2 MapSize
		{
			get
			{
				return new Vector2( NumTilesWide * TileSize, NumTilesHigh * TileSize );
			}
		}

		// Sets the dimensions of the Tiled map. This should only be called once when initialising the map.
		// You shouldn't change these values unless you redo the map in Tiled.
		// NOTE: Setting these incorrectly will screw things up.
		public static void SetDimensions(int tileSize, int numTilesWide, int numTilesHigh)
		{
			m_tileSize = tileSize;
			m_numTilesWide = numTilesWide;
			m_numTilesHigh = numTilesHigh;
		} // end SetDimensions function

		// Generates tiles and adds them to the dictionary.
		public static void GenerateAndAddTiles()
		{
			// Get all the game objects tagged as resources.
			GameObject[] resourceObjects = GameObject.FindGameObjectsWithTag( "Resource" );
			Debug.Log("ResourceObject Name: " + resourceObjects[0].name);
			Debug.Log("ResourceObject Position: " + resourceObjects[0].transform.position);

			// Loop over the map. Width first.
			for (int width = 32; width < (int)MapSize.x; width += 64)
			{
				// Then height.
				for (int height = 32; height < (int)MapSize.y; height += 64)
				{
					// We are in the fourth quadrant so the y is negative.
					Vector3 key = new Vector3(width, height * -1, 0.0f);

					// Create an empty tile at the given position.
					Tile newTile = new Tile(key, ResourceType.NONE, null);

					// Add the tile to the dictionary.
					TileDictionary.AddEntry(key, newTile);
				} // end inner height for loop
			} // end outer width for loop

			// Now loop over the resource array and set the tiles to resources.
			foreach (GameObject resourceObject in resourceObjects)
			{
				// Find the position in the dictionary.
				Debug.Log("Position: " + resourceObject.transform.position);
				//TileDictionary.GetTile(resourceObject.transform.position);
			} // end for each loop
		} // end GenerateAndAddTiles function.

	} // end TileManager class
} // end namespace
