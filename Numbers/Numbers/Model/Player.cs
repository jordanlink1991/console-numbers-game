using System;
using System.Collections.Generic;

public class Player
{
	/// <summary>
	/// Name of the player
	/// </summary>
    public string Name { get; set; }

	/// <summary>
	/// Set of Hands of the player
	/// </summary>
	public List<Hand> Hands { get; set; }

	public Player()
	{
		Hands = new List<Hand>();
        Name = "Player - Master";
	}

	public Player(string name, List<Hand> hands)
	{
		Name = name ?? string.Empty;
		Hands = hands ?? new List<Hand>();
	}
}
