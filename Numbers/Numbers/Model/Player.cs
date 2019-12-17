using System;
using System.Collections.Generic;

public class Player
{
	public List<Hand> Hands { get; set; }

	public Player()
	{
		Hands = new List<Hand>();   
	}
}
