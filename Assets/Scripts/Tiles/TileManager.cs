using UnityEngine;
using System.Collections;
using System;
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
				// Calculate the height of the map. Then subtract half the tile size. This is the highest coord the tile can be placed at.
				// This is because we start at 32.
				int temp = NumTilesHigh * TileSize - ( TileSize / 2 );

				// Since we're in the fourth quadrant, the y value is negative.
				return ( temp * -1 );
			} // end get accessor
		} // end MaxHeight property

		// Gets the max height you can place.
		public static int MaxWidth
		{
			get
			{
				// Calculate the width of the map. Then subtract half the tile size. This is the highest coord the tile can be placed at.
				// This is because we start at 32.
				return ( NumTilesWide * TileSize - ( TileSize / 2 ) );
			} // end get accessor
		} // end MaxHeight property

		// Get the map size.
		public static Vector2 MapSize
		{
			get
			{
				return new Vector2( NumTilesWide * TileSize, NumTilesHigh * TileSize );
			} // end get accessor
		} // end MapSize property

		// Sets the dimensions of the Tiled map. This should only be called once when initialising the map.
		// You shouldn't change these values unless you redo the map in Tiled.
		// NOTE: Setting these incorrectly will screw things up.
		public static void SetDimensions( int tileSize, int numTilesWide, int numTilesHigh )
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

			// Loop over the map. Width first.
			for ( int width = 32; width < (int)MapSize.x; width += 64 )
			{
				// Then height.
				for ( int height = 32; height < (int)MapSize.y; height += 64 )
				{
					// We are in the fourth quadrant so the y is negative.
					Vector3 key = new Vector3( width, height * -1, 0.0f );

					// Create an empty tile at the given position.
					Tile newTile = new Tile( key, ResourceType.NONE, null );

					// Add the tile to the dictionary.
					TileDictionary.AddEntry( key, newTile );
				} // end inner height for loop
			} // end outer width for loop

			// Now loop over the resource array and set the tiles to resources.
			for (int index = 0; index < resourceObjects.Length; index++)
			{
				Vector3 key = ToPixels( resourceObjects[index].transform.position );

				// HACK: Damien will do this bit better later on.
				// Get the resource name, this will be used for its type.
				string resourceName = resourceObjects[index].name;
				
				// Split the string by the underscore
				string[] tokens = resourceName.Split('_');
				
				// Holds the default value for the enum.
				ResourceType resourceType = ResourceType.NONE;
				
				// Holds the results of the parsing.
				ResourceType tmp = ResourceType.NONE;

				// Attempt to parse the string into the enum value.
				try
				{
					// Get the last component and send it to the enum.
					tmp = (ResourceType)Enum.Parse( typeof( ResourceType ), tokens[tokens.Length - 1].ToUpper() );
					
					// Switch over the possible values.
					switch ( tmp )
					{
						case ResourceType.WOOL:
						{
							// Set the resource type.
							resourceType = ResourceType.WOOL;
							break;
						} // end case
						case ResourceType.WOOD:
						{
							// Set the resource type.
							resourceType = ResourceType.WOOD;
							break;
						} // end case
						case ResourceType.FISH:
						{
							// Set the resource type.
							resourceType = ResourceType.FISH;
							break;
						} // end case
						case ResourceType.ORE:
						{
							// Set the resource type.
							resourceType = ResourceType.ORE;
							break;
						} // end case
						default:
						{
							// Couldn't parse correctly so set the resource type to none.
							resourceType = ResourceType.NONE;
							break;
						} // end default case
					} // end switch statement.
				} // end try clause
				catch (Exception ex)
				{
					// The parsing failed so set the instance to null and resource type to size.
					Debug.Log( "Something went wrong. Exception: " + ex.Message );
				} // end catch clause
				
				// Check if the resource type is not none.
				if ( resourceType != ResourceType.NONE)
				{
					// Update the tile at the given key.
					TileDictionary.UpdateTile( key, resourceType, resourceObjects[index] );
				} // end if statement.
			} // end for loop
		} // end GenerateAndAddTiles function.

		// Converts unity units to pixels for use on the map.
		public static Vector3 ToPixels( Vector3 param )
		{
			// To convert the parameter to pixels that the resource positions use, multiply by 100.
			Vector3 tmp = new Vector3(param.x * 100, param.y * 100, param.z * 100);

			// Check if the width (x) is within the valid map positions and Clamp if not.
			if ( tmp.x > MaxWidth)
			{
				tmp.x = MaxWidth;
			} // end if statement

			// Check if the height (y) is within the valid map positions and Clamp if not.
			if ( tmp.y < MaxHeight)
			{
				tmp.y = MaxHeight;
			} // end if statement

			// We need integers for the keys to work. So convert the temp vector3's params to integers.
			// NOTE: Trying to use "(int)tmp.x" results in the wrong number. So use "Convert.ToInt32(tmp.x)"
			int resX = Convert.ToInt32(tmp.x);
			int resY = Convert.ToInt32(tmp.y);
			int resZ = Convert.ToInt32(tmp.z);

			// Everything should be fine now so return the result.
			return new Vector3( resX, resY, resZ );
		} // end ToPixels function

		// Converts pixels to unity units for use on the map.
		public static Vector3 ToUnits( Vector3 param )
		{
			// To convert the parameter to units that unity positions use, divide by 100.
			Vector3 tmp = new Vector3( param.x / 100.0f, param.y / 100.0f, param.z / 100.0f );

			// Gets the max width and height in units.
			float maxWidth = MaxWidth / 100.0f;
			float maxHeight = MaxHeight / 100.0f;
			
			// Check if the width (x) is within the valid map positions and Clamp if not.
			if ( tmp.x > maxWidth)
			{
				tmp.x = maxWidth;
			} // end if statement
			
			// Check if the height (y) is within the valid map positions and Clamp if not.
			if ( tmp.y < maxHeight)
			{
				tmp.y = maxHeight;
			} // end if statement
			
			// Everything should be fine now sp return the result.
			return tmp;
		} // end ToPixels function

	} // end TileManager class
} // end namespace
