using UnityEngine;
using System.Collections;
using System; //This allows the IComparable Interface

//This is the class you will be storing
//in the different collections. In order to use
//a collection's Sort() method, this class needs to
//implement the IComparable interface.
public class Command
{
	public string name;
	public int power;

	public Command(string newName, int newPower)
	{
		name = newName;
		power = newPower;
	}

	public string GetTitle(){
		return name;
	}

	public int GetPower(){
		return power;
	}

	//This method is required by the IComparable
	//interface. 
	public int CompareTo(Command other)
	{
		if(other == null)
		{
			return 1;
		}

		//Return the difference in power.
		return power - other.power;
	}
}