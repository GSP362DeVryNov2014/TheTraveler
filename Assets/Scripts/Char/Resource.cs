using UnityEngine;
using System.Collections;

namespace GSP.Char
{
	// Very simple resource script. Handles the resource value and weight collectively.
	// Could be expanded to be more complex with individual resources later.
	public class Resource : MonoBehaviour
	{
		// Declare out private variables. The default scope is private.
		int m_amount;	// This is the value of all the resources combined.
		int m_weight;	// This is the weight of all the resources combined.

		// Gets the current value of the resources.
		public int Amount
		{
			get { return m_amount; }
		} // end Amount property
		
		// Gets the current weight of the resources.
		public int Weight
		{
			get { return m_weight; }
		} // end Weight property

		// Use this for initialisation.
		void Start()
		{
			// Initialise the values to zero.
			m_amount = 0;
			m_weight = 0;
		} // end Start function
		
		// Update is called once per frame.
		void Update()
		{
			// Nothing to do here.
		} // end Update function

		// Adds a resource value and weight to their current values
		// This is called upon picking up a resource.
		public void AddResource( int resourceValue, int resourceWeight )
		{
			m_amount += resourceValue;
			m_weight += resourceWeight;
		} // end AddResource function

		// Subtracts a resource value and weight from their current values
		// This is called upon transferring a resource to an ally.
		public void RemoveResource( int resourceValue, int resourceWeight )
		{
			// Check if the operation will bring the resource value to zero or less.
			if ((m_amount - resourceWeight) <= 0)
			{
				// Clamp to zero.
				m_amount = 0;
			} // end if statement
			else
			{
				// Otherwise subtract the given value.
				m_amount -= resourceValue;
			} // end else statement

			// Check if the operation will bring the weight value to zero or less.
			if ((m_amount - resourceWeight) <= 0)
			{
				// Clamp to zero.
				m_weight = 0;
			} // end if statement
			else
			{
				// Otherwise subtract the given value.
				m_weight -= resourceWeight;
			} // end else statement
		} // end RemoveResource function

		// Clear the resources. This sets the values back to zero.
		// This is called after selling.
		public void ClearResources()
		{
			m_amount = 0;
			m_weight = 0;
		} // end ClearResources function
	} // end Resource class
} // end namespace
