using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace GSP.Char
{
	public class Character : MonoBehaviour
	{
		// Declare our private variables. The default scope is private.
		ResourceList m_resourceList;	// This is the resource list script object.
		Ally m_allyScript;				// This is the ally script object.
		int m_maxWeight;				// This is the maximum weight the character can hold.
		int m_maxInventory;				// Maximum inventory (max number player can hold)
		int m_currency; 				// This is the amount of currency the character is holding.
		int m_attackPower;				// Attack of the character (from weapons)
		int m_defencePower;				// Defence of the character (from armor)
		Items m_weapon;					// Weapon being wielded
		Items m_armor;					// Armor being worn
		List<Items> m_bonuses;			// Bonuses picked up (Inventory and Weight mods)

		#region Resource
		// Gets the current value of resources the character is holding.
		public int ResourceValue
		{
			get { return m_resourceList.TotalValue; }
		} // end ResourceValue property

		// Gets the current weight of resources the character is holding.
		public int ResourceWeight
		{
			get { return m_resourceList.TotalWeight; }
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

		// Gets or Sets the maximum inventory slots the character has.
		public int MaxInventory
		{
			get { return m_maxInventory; }
			set
			{
				m_maxInventory = value;
				
				// Check if the maximum inventory is less than zero.
				if (value < 0)
				{
					// Clamp the max inventory to zero.
					m_maxInventory = 0;
				} // end if statement
			} // end Set accessor
		} // end MaxInventory property

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

		// Use this for initialisation
		void Start()
		{
			// Initialise the variables.
			m_resourceList = GetComponent<ResourceList>();
			m_allyScript = GetComponent<Ally>();
			m_maxWeight = 300;
			m_maxInventory = 20;
			m_currency = 0;
			m_attackPower = 0;
			m_defencePower = 0;
			m_bonuses = new List<Items> ();
			m_weapon = null;
			m_armor = null;
		} // end Start function

		// Update is called once per frame
		void Update()
		{
			// Nothing to do here at the moment.
		} // end Update function

		// Attempts to pick up a resource for the character.
		public void PickupResource( Resource resource, int amount )
		{
			// Check if picking up this resource will put the character overweight.
			if ( (ResourceWeight + resource.WeightValue) * amount <= MaxWeight )
			{
				// Check if there is enough room for this resource.
				if( m_resourceList.TotalSize + resource.SizeValue <= MaxInventory )
				{
					// Add the resource.
					m_resourceList.AddResource( resource, amount );
				} // end if size
				else
				{
					print( "Pickup failed. Max inventory capacity reached." );
				} // end else size
			} // end if weight
			else
			{
				print( "Pickup failed. Max inventory weight reached." );
			} // end else weight
		} // end PickupResource function

		// Sells the resources the character is currently holding.
		public void SellResource()
		{
			// Credit the character for the resources they are holding.
			Currency += m_resourceList.TotalValue;

			// Clear the resources now.
			m_resourceList.ClearResources();
		} // end SellResource function

		// Transfers currency to another character.
		public void TransferCurrency( GameObject other, int amount )
		{
			// Get the ammount clamping later.
			int amt = amount;

			// Get the character script attached the the other character object.
			Character charScript = other.GetComponent<Character>();

			// Check if the script object exists.
			if ( charScript == null )
			{
				// Simply return.
				return;
			}

			// Only proceed if the amount is greater than zero.
			if ( amt > 0)
			{
				// Check if the ammount is greater than the character's currency.
				if ( amt > this.Currency )
				{
					// Clamp it to the character's currency if so.
					amt = this.Currency;

				} // end if statement

				// Add the amount of currency to the other character.
				charScript.Currency += amt;

				// Subtract the amount of currency from the character this is attached to.
				this.Currency -= amt;
			}
		} // end TransferCurrency function

		// Transfers a resource to another character.
		public void TransferResource( GameObject other, Resource resource )
		{
			// Check if the resource object exists.
			if ( resource == null )
			{
				// Simply return.
				return;
			}

			// Get the resource list script attached the other character object.
			ResourceList otherResourceScript = other.GetComponent<ResourceList>();
			// Get the character script attached the other character object.
			Character otherCharacterScript = other.GetComponent<Character>();
			// Get the resource list script attached the character this is attached to.
			ResourceList charResourceScript = this.gameObject.GetComponent<ResourceList>();
			
			// Check if the script objects exists.
			if ( otherResourceScript == null || charResourceScript == null || otherCharacterScript == null )
			{
				print( "NULL" );
				// Simply return.
				return;
			}

			// Check if the other character recieving this resource will put the character overweight.
			if (  otherResourceScript.TotalWeight + resource.WeightValue <= otherCharacterScript.MaxWeight )
			{
				// Check if there is enough room for this resource.
				if( otherResourceScript.TotalSize + resource.SizeValue <= otherCharacterScript.MaxInventory )
				{
					// Add the resource to the other character.
					otherResourceScript.AddResource(resource, 1);

					// Remove the resource from the character this is attached to.
					charResourceScript.RemoveResource(resource, 1);
				} // end if size
				else
				{
					print( "Transfer failed. Their max inventory capacity reached." );
				} // end else size
			} // end if weight
			else
			{
				print( "WEIGHT: " + otherResourceScript.TotalWeight );
				print( "MAX WEIGHT: " + otherCharacterScript.MaxWeight );
				print( "THIS MAX WEIGHT: " + this.MaxWeight );
				print( "ALLY MAX WEIGHT: " + m_allyScript[0].gameObject.GetComponent<Character>().MaxWeight );
				print( "Transfer failed. Their max inventory weight reached." );
			} // end else weight
		} // end TransferGold function

		//Equips custom or predefined items.
		//NOTE!!
		//For custom items, the "item" is the stat you want to change
		//and the "value" is what you are changing it by, which can be
		//a positive or negative value.
		public void EquipItem(string item, int value = 0)
		{
			//Generic item holder
			Items temp = new Items();

			//Assign values to a custom item
			if(value != 0)
			{
				if(item == "attack")
				{
					temp.AttackValue += value;
				} //end if attack
				else if(item == "defence")
				{
					temp.DefenceValue += value;
				} //end else if defence
				else if(item == "inventory")
				{
					temp.InventoryValue += value;
				} //end else if inventory
				else if(item == "weight")
				{
					temp.WeightValue += value;
				} //end else if weight
			} //end if custom set
			//Assign values to a predefined item
			else
			{
				if(temp.SetItem(item) != "NAN")
				{
					item = temp.SetItem(item);
				} //end if found
				else
				{
					print ("No item of " + item + " name found.");
					temp = null;
				} //end else NAN
			} //end else predefined item

			//Equip item
			if (item == "NAN")
			{
				//Do nothing, code taken care of above
			} //end if NAN
			else if(item == "attack")
			{
				m_weapon = temp;
			} //end else if attack
			else if(item == "defence")
			{
				m_armor = temp;
			} //end else if defence
			else
			{
				m_bonuses.Add(temp);
			} //end else (Inventory and Weight)

			//TODO: Apply stat changes, accounting for no previous weapon/armour
			//or previous weapon/armour (since it's getting replaced). Also bonuses
			//might want to have a limit, but for now leave it infinite. Stat change
			//should only happen once, which is why applying the stat changes here 
			//makes sense, but you might have a better idea.

		} //end EquipItem(string item, int value)

		public void RemoveItem(string item)
		{
			if(item == "weapon")
			{
				m_weapon = null;
			} //end if weapon
			else if(item == "armor")
			{
				m_armor = null;
			} //end else if armor

			//TODO: Figure out how to remove bonus items (since there can be multiple)

			//TODO: Remove stat bonus from player when item is removed
		} //end RemoveItem(string item)
	} // end Character class
} // end namespace
