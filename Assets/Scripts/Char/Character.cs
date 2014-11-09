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
			if ( ResourceWeight + resource.WeightValue <= MaxWeight )
			{
				// Check if there is enough room for this resource.
				if( m_resourceList.NumResources <= MaxInventory )
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
				temp.ItemName = "CustomItem-" + item;
				if(item == "attack")
				{
					temp.ItemType = "Weapon";
					temp.AttackValue += value;
				} //end if attack
				else if(item == "defence")
				{
					temp.ItemType = "Armor";
					temp.DefenceValue += value;
				} //end else if defence
				else if(item == "inventory")
				{
					temp.ItemType = "Inventory";
					temp.InventoryValue += value;
				} //end else if inventory
				else if(item == "weight")
				{
					temp.ItemType = "Weight";
					temp.WeightValue += value;
				} //end else if weight
				else
				{
					temp = null;
					print ("No stat of " + item + " type found. Make sure you're" +
					       " using lower case input. Valid types are attack, defence" +
					       " inventory, and weight.");
				} //end else
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
				//Simply return
				//NOTE: I left this here in case there is code we
				//want to add for this condition, kind of a catch
				//all for clean up after invalid input.
				return;
			} //end if NAN
			else if(item == "attack")
			{
				if(m_weapon != null)
				{
					print ("Do you want this weapon? Hit y for yes and n for no.");
					bool Loop = true;
					while(Loop)
					{
						if(Input.GetKeyDown(KeyCode.Y))
						{
							//Clean up old weapon, then apply new
							AttackPower -= m_weapon.AttackValue;
							AttackPower += temp.AttackValue;
							m_weapon = temp;
							temp = null;
							Loop = false;
						}
						else if(Input.GetKeyDown(KeyCode.N))
						{
							//Disable temp and end loop
							temp = null;
							Loop = false;
						} //end else if
					} //end while
				} //end if existing weapon
				else
				{
					AttackPower += temp.AttackValue;
					m_weapon = temp;
				} //end else no existing weapon
			} //end else if attack
			else if(item == "defence")
			{
				if(m_armor != null)
				{
					print ("Do you want this armor? Hit y for yes and n for no.");
					bool Loop = true;
					while(Loop)
					{
						if(Input.GetKeyDown(KeyCode.Y))
						{
							//Clean up old armor, then apply new
							DefencePower -= m_armor.DefenceValue;
							DefencePower += temp.DefenceValue;
							m_armor = temp;
							temp = null;
							Loop = false;
						}
						else if(Input.GetKeyDown(KeyCode.N))
						{
							//Disable temp and end loop
							temp = null;
							Loop = false;
						} //end else if
					} //end while
				} //end if existing armor
				else
				{
					DefencePower += temp.DefenceValue;
					m_armor = temp;
				} //end else no existing armor
			} //end else if defence
			else if(item == "inventory")
			{
				m_bonuses.Add(temp);
			} //end else if inventory
			else if(item == "weight")
			{
				m_bonuses.Add(temp);
			} //end else if weight
			else
			{
				//Explain error, disable temp, then return
				print ("Stat value " + item + " does not exist.");
				temp = null;
				return;
			} //end else
		} //end EquipItem(string item, int value)

		public void RemoveItem(string item)
		{
			if(item == "weapon")
			{
				//Verify item exists
				if(m_weapon != null)
				{
					print (m_weapon.ItemName + " removed.");
					AttackPower -= m_weapon.AttackValue;
					m_weapon = null;
				} //end if existing weapon
				else
				{
					print ("No weapon found.");
				} //end else no existing weapon
			} //end if weapon
			else if(item == "armor")
			{
				//Verify item exists
				if(m_armor != null)
				{
					print (m_armor.ItemName + " removed.");
					DefencePower -= m_armor.DefenceValue;
					m_armor = null;
				} //end if existing armor
				else
				{
					print ("No armor found.");
				} //end else no existing armor
			} //end else if armor
			else if(item == "inventory")
			{
				Items temp = m_bonuses.Find(x => x.ItemType == "Inventory");
				MaxInventory -= temp.InventoryValue;
				m_bonuses.Remove(temp);
			} //end else if inventory
			else if(item == "weight")
			{
				Items temp = m_bonuses.Find(x => x.ItemType == "Weight");
				MaxWeight -= temp.WeightValue;
				m_bonuses.Remove(temp);
			} //end else if weight
			else
			{
				print ("No item of " + item + " stat found. Make sure you " +
					"are using lower case. Valid types are weapon, armor, inventory, " +
				       " and weight.");
			} //end else
		} //end RemoveItem(string item)
	} // end Character class
} // end namespace
