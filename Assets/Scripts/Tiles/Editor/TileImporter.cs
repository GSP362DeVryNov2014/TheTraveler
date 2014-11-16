using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEditor;
using Tiled2Unity;
using GSP.Char;

/*
 * This file must remain in an Editor folder to work.
 */

namespace GSP.Tiles
{
	[CustomTiledImporter]
	class TileImporter : ICustomTiledImporter
	{
		// Handle the custom properties.
		public void HandleCustomProperties (GameObject gameObject, IDictionary<string, string> customProperties)
		{
			// Does this game oject have a ResType property?
			if ( !customProperties.ContainsKey( "ResType" ) )
			{
				// Simply return.
				return;
			}

			// Now we meed to determine which type of resource it is.
			ResourceType tmp; 		// Holds the results of parsing.

			ResourceType resType = ResourceType.NONE;	// Holds the resource type.
			GameObject instance = null;					// Holds the game object instance.

			try
			{
				// Attempt to parse the string into the enum value.
				tmp = (ResourceType)Enum.Parse( typeof( ResourceType ), customProperties["ResType"] );
				
				// Switch over the possible values.
				switch ( tmp )
				{
					case ResourceType.WOOL:
					{
						// Instantiate the resource prefab.
						instance = GameObject.Instantiate( PrefabReference.prefabResource_Wool ) as GameObject;
						
						// Set the instances name to the name of the prefab.
						instance.name = PrefabReference.prefabResource_Wool.name;

						// Set the resource type.
						resType = ResourceType.WOOL;
						break;
					} // end case
					case ResourceType.WOOD:
					{
						// Instantiate the resource prefab.
						instance = GameObject.Instantiate( PrefabReference.prefabResource_Wood ) as GameObject;					
						
						// Set the instances name to the name of the prefab.
						instance.name = PrefabReference.prefabResource_Wood.name;
						
						// Set the resource type.
						resType = ResourceType.WOOD;
						break;
					} // end case
					case ResourceType.FISH:
					{
						// Instantiate the resource prefab.
						instance = GameObject.Instantiate( PrefabReference.prefabResource_Fish ) as GameObject;
						
						// Set the instances name to the name of the prefab.
						instance.name = PrefabReference.prefabResource_Fish.name;
						
						// Set the resource type.
						resType = ResourceType.FISH;
						break;
					} // end case
					case ResourceType.ORE:
					{
						// Instantiate the resource prefab.
						instance = GameObject.Instantiate( PrefabReference.prefabResource_Ore ) as GameObject;
						
						// Set the instances name to the name of the prefab.
						instance.name = PrefabReference.prefabResource_Ore.name;
						
						// Set the resource type.
						resType = ResourceType.ORE;
						break;
					} // end case
					default:
					{
						// Couldn't parse correctly so set the instance to null and resource type to size.
						instance = null;
						resType = ResourceType.NONE;

						break;
					} // end default case
				} // end switch statment
			} // end try statement
			catch (Exception ex)
			{
				// The parsing failed so set the instance to null and resource type to size.
				Debug.Log( "Something went wrong. Exception: " + ex.Message );
				instance = null;
				resType = ResourceType.NONE;
			} // end catch statement

			// Make sure instance exists.
			if ( instance == null && resType == ResourceType.NONE )
			{
				// Simply return.
				return;
			}

			// Tag the instance as a Resource.
			instance.tag = "Resource";

			// Use the position of the game object we're attached to.
			instance.transform.parent = gameObject.transform;
			instance.transform.localPosition = Vector3.zero;

			// Scale by a factor of 100. For some reason they're 1/100th the size and this make them big enought o be visible.
			instance.transform.localScale = new Vector3(100.0f, 100.0f, 0.0f);

			// Add to the parent transform's local position. This corrects the placement.
			instance.transform.parent.localPosition += new Vector3(32.0f, 32.0f, 0.0f);
		} // end HandleCustomProperties function

		// Customise the prefab, this is the last chance to do this in code.
		public void CustomizePrefab (GameObject prefab)
		{
			// Do nothing.
		} // end CustomizePrefab function
	} // end TileImporter class
} // end namespace