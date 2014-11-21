using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour 
{
	public Vector3 MoveLeft(Vector3 position)
	{
		if(position.x > -20)
		{
			return new Vector3(-1, 0, 0);
		} //end if
		else
		{
			return new Vector3(0, 0, 0);
		} //end else
	} //end MoveLeft(Vector3 position)

	public Vector3 MoveRight(Vector3 position)
	{
		if(position.x < 20)
		{
			return new Vector3(1, 0, 0);
		} //end if
		else
		{
			return new Vector3(0, 0, 0);
		} //end else
	} //end MoveRight(Vector3 position)

	public Vector3 MoveUp(Vector3 position)
	{
		if(position.y > -20)
		{
			return new Vector3(0, 1, 0);
		} //end if
		else
		{
			return new Vector3(0, 0, 0);
		} //end else
	} //end MoveUp(Vector3 position)

	public Vector3 MoveDown(Vector3 position)
	{
		if(position.y < 20)
		{
			return new Vector3(0, -1, 0);
		} //end if
		else
		{
			return new Vector3(0, 0, 0);
		} //end else
	} //end MoveDown(Vector3 position)
}
