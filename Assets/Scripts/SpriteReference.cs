using UnityEngine;
using UnityEditor;
using System.Collections;

namespace GSP
{
	public static class SpriteReference
	{
		/*
		 * Sprite References
		 */

		// This is the sprite sheet for the buttons.
		public static Sprite[] buttonSpritesheet = AssetDatabase.LoadAllAssetsAtPath ("Assets/Sprites/buttons_sprite_sheet.png").OfType<Sprite>().ToArray();
		
		// This is the reference to the menu backgrond sprite.
		public static Sprite spriteMenuBackground = buttonSpritesheet[0];

		// This is the reference to the intro backgrond sprite.
		public static Sprite spriteIntroBackground = buttonSpritesheet[1];

		// This is the reference to the start button sprite.
		public static Sprite spriteStart = buttonSpritesheet[2];

		// This is the reference to the exit button sprite.
		public static Sprite spriteExit = buttonSpritesheet[3];

		// This is the reference to the option button sprite.
		public static Sprite spriteOptions = buttonSpritesheet[4];

		// This is the reference to the continue button sprite.
		public static Sprite spriteContinue = buttonSpritesheet[5];

		// This is the reference to the mrnu button sprite.
		public static Sprite spriteMenu = buttonSpritesheet[6];

		// This is the reference to the credit button sprite.
		public static Sprite spriteCredit = buttonSpritesheet[7];
	} // end PrefabReference class
} // end namespace

