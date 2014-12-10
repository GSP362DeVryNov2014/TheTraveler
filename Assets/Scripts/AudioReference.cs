using UnityEngine;
using UnityEditor;
using System.Collections;

namespace GSP
{
	public static class AudioReference
	{
		/*
		 * Audio References
		 */
		
		// This is the reference to the coins clip.
		public static AudioClip sfxCoins = AssetDatabase.LoadAssetAtPath( "Assets/Sounds/GSP362_TeamA_Character_CoinClashing.wav", typeof( AudioClip ) ) as AudioClip;

		// This is the reference to the walking clip.
		public static AudioClip sfxWalking = AssetDatabase.LoadAssetAtPath( "Assets/Sounds/GSP362_TeamA_Character_Walking.wav", typeof( AudioClip ) ) as AudioClip;

		// This is the reference to the first sword hit clip.
		public static AudioClip sfxSwordHit1 = AssetDatabase.LoadAssetAtPath( "Assets/Sounds/GSP362_TeamA_Fight_SwordHit1.wav", typeof( AudioClip ) ) as AudioClip;

		// This is the reference to the second sword hit clip.
		public static AudioClip sfxSwordHit2 = AssetDatabase.LoadAssetAtPath( "Assets/Sounds/GSP362_TeamA_Fight_SwordHit2.wav", typeof( AudioClip ) ) as AudioClip;

		// This is the reference to the third sword hit clip.
		public static AudioClip sfxSwordHit3 = AssetDatabase.LoadAssetAtPath( "Assets/Sounds/GSP362_TeamA_Fight_SwordHit3.wav", typeof( AudioClip ) ) as AudioClip;

		// This is the reference to the rolling dice clip.
		public static AudioClip sfxDice = AssetDatabase.LoadAssetAtPath( "Assets/Sounds/GSP362_TeamA_Overall_RollingDice.wav", typeof( AudioClip ) ) as AudioClip;

		// This is the reference to the fishing resource clip.
		public static AudioClip sfxFishing = AssetDatabase.LoadAssetAtPath( "Assets/Sounds/GSP362_TeamA_Resource_Fishing.wav", typeof( AudioClip ) ) as AudioClip;

		// This is the reference to the mining resource clip.
		public static AudioClip sfxMining = AssetDatabase.LoadAssetAtPath( "Assets/Sounds/GSP362_TeamA_Resource_Mining.wav", typeof( AudioClip ) ) as AudioClip;

		// This is the reference to the shearing resource clip.
		public static AudioClip sfxShearing = AssetDatabase.LoadAssetAtPath( "Assets/Sounds/GSP362_TeamA_Resource_Shearing.wav", typeof( AudioClip ) ) as AudioClip;

		// This is the reference to the woodcutting resource clip.
		public static AudioClip sfxWoodcutting = AssetDatabase.LoadAssetAtPath( "Assets/Sounds/GSP362_TeamA_Resource_Woodcutting.wav", typeof( AudioClip ) ) as AudioClip;
	} // end PrefabReference class
} // end namespace

