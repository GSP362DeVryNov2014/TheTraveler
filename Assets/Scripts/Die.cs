using UnityEngine;
using System.Collections;

namespace GSP
{
	public class Die : MonoBehaviour
	{
		private SafeRandom rand;

		// Constructor for the die behaviour
		Die()
		{
		}

		// Use this for initialisation
		void Start()
		{
			// Initialise the SafeRandom class for die rolls. Don't worry about the seed; it'll use TickCount.Now for that.
			rand = new SafeRandom();
		}

		// Update is called once per frame
		void Update()
		{
		}

		// Gets the SafeRandom object.
		public SafeRandom Rand
		{
			get { return rand; }
		}

		// Roll the die. This defaults to a single six-sided die.
		// Stuff can be done to the result after this is called.
		public int Roll()
		{
			// Roll the default die.
			Roll( 1, 6 );
		}

		// Roll the die. This takes the number of di(c)e and its number of sides.
		// Stuff can be done to the result after this is called.
		public int Roll( int numDie, int numSides )
		{
			// Since the algorithm we are using excludes the maximum value, we need to add one to it.
			var dieMinValue = 1;
			var dieMaxValue = numSides + 1;

			// Holds the sum of all the di(c)e rolled in this call.
			var dieSum = 0;

			// Roll the die for each die needed.
			for (int i = 0; i < numDie; i++)
			{
				// Add the roll to the sum.
				dieSum += Rand.Next( dieMinValue, dieMaxValue );
			}

			// Return the sum of the rolls to be dealt with later.
			return dieSum;
		}
	}
}