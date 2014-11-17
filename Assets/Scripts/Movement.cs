using UnityEngine;
using System.Collections;

/*
 * Note:
 * Currently returns a value to move by.
 * Change "Vector3" function type to "void"
 * and delete active line and uncomment
 * the line above it to directly move
 * the player object. Uncomment the
 * GameObject m_player line too.
 */ 

public class Movement : MonoBehaviour 
{
	//GameObject m_player = GameObject.FindGameObjectWithTag( "Player" );
	public Vector3 MoveLeft()
	{
		//m_player.transform.position = Vector3 (-1, 0, 0);
		return new Vector3(-1, 0, 0);
	} //end MoveLeft()

	public Vector3 MoveRight()
	{
		//m_player.transform.positon = Vector3 (1, 0, 0);
		return new Vector3(1, 0, 0);
	} //end MoveRight()

	public Vector3 MoveUp()
	{
		//m_player.transform.position = Vector3 (0, -1, 0);
		return new Vector3(0, -1, 0);
	} //end MoveUp()

	public Vector3 MoveDown()
	{
		//m_player.transform.position = Vector3 (0, 1, 0);
		return new Vector3(0, 1, 0);
	} //end MoveDown()
}
