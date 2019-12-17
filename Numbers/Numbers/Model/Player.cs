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

	/// <summary>
	/// Flag for human players
	/// </summary>
	public bool IsHuman { get; set;  }

	public Player()
	{
		Name = "Player - Master";
		Hands = new List<Hand>();
		IsHuman = true;
	}

	public Player(string name, List<Hand> hands)
	{
		Name = name ?? string.Empty;
		Hands = hands ?? new List<Hand>();
		IsHuman = true;
	}

	public Player(string name, List<Hand> hands, bool isHuman)
		: this(name, hands)
	{
		IsHuman = isHuman;
	}

	public Player(Player p)
		: this()
	{
		Name = p.Name;
		p.Hands.ForEach(x => Hands.Add(new Hand(x)));
		IsHuman = p.IsHuman;
	}
}
