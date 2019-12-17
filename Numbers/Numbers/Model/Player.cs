using System;
using System.Collections.Generic;

public class Player
{
	public List<Hand> Hands { get; set; }
    public string Name { get; set; }

	public Player()
	{
		Hands = new List<Hand>();
        Name = "Player - Master";
	}
}
