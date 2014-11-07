using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GSP.Char
{
	public class Character : MonoBehaviour
	{
		// Declare our private variables. The default scope is private.
		Resource m_resource;			// This is the resource script object.
		Ally m_allyScript;				// This is the ally script object.
		int m_maxWeight;				// This is the maximum weight the character can hold.
		int m_currency; 				// This is the amount of currency the character is holding.
		int m_attackPower;				// Attack of the character (from weapons)
		int m_defencePower;				// Defence of the character (from armor)

		#region Resource
		// Gets the current value of resources the character is holding.
		public int ResourceValue
		{
			get { return m_resource.Amount; }
		} // end ResourceValue property

		// Gets the current value of resources the character is holding.
		public int ResourceWeight
		{
			get { return m_resource.Amount; }
		} // end ResourceWeight property
		#endregion

		#region Ally
		// Gets the number of allies the character has.
		public int NumAllies
		{
			get { return m_allyScript.NumAllies; }
		} // end NumAllies function
		#endregion

		// Gets or Sets the maximum weight the character can hold.
		public int MaxWeight
		{
			get { return m_maxWeight; }
			set
			{
				m_maxWeight = value;

				// Check if the maximum weight is less than zero.
				if (value < 0)
				{
					// Clamp the max weight to zero.
					m_maxWeight = 0;
				} // end if statement
			} // end Set accessor
		} // end MaxWeight property

		// Gets and Sets the currency a character is holding.
		public int Currency
		{
			get { return m_currency; }
			set
			{
				m_currency = value;

				// Check if the currency is less than zero
				if(m_currency < 0)
				{
					// Clamp the currency to zero.
					m_currency = 0;
				} // end if statement
			} // end Set accessor
		} // end Currency property
	
		// Gets and Sets the attack power of character.
		public int AttackPower
		{
			get { return m_attackPower; }
			set 
			{ 
				m_attackPower = value;

				// Check if the attack power is less than zero.
				if(m_attackPower < 0)
				{
					// Clamp the attack power to zero.
					m_attackPower = 0;
				} // end if statement
			} // end Set accessor
		} // end AttackPower property

		//Gets and Sets the defence power of the character.
		public int DefencePower
		{
			get { return m_defencePower; }
			set 
			{ 
				m_defencePower = value;

				// Check if the defense power is less than zero.
				if(m_defencePower < 0)
				{
					// Clamp the defence power to zero.
					m_defencePower = 0;
				} // end if statement
			} // end Set accessor
		} // end DefencePower property

		// Use this for initialization
		void Start()
		{
			// Initialise the variables.
			m_resource = GetComponent<Resource>();
			m_allyScript = GetComponent<Ally>();
			m_maxWeight = 300;
			m_currency = 0;
			m_attackPower = 0;
			m_defencePower = 0;
		} // end Start function

		// Update is called once per frame
		void Update()
		{
			// Nothing to do here at the moment.
		} // end Update function

		// Picks up a resource for the character
		public void PickupResource( int resourceValue, int resourceWeight )
		{
			// Check if picking up this resource will put the character overweight.
			if ( ( ResourceWeight ) <= MaxWeight )
			{
				// Add the resource.
				m_resource.AddResource( resourceValue, resourceWeight );
			} // end if statement
			else
			{
				print ("Pickup failed. Max inventory reached.");
			} // end else statement
		} // end PickupResource function

		// Sells the resources the character is currently holding.
		public void SellResource()
		{
			// Credit the character for the resources they are holding.
			Currency += m_resource.Amount;

			// Clear the resources now.
			m_resource.ClearResources();
		} // end SellResource function
	} // end Character class
} // end namespace
