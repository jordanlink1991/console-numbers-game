using System;
using System.Collections.Generic;

public class BoardView
{
    public string FormatPlayer(List<Player> players)
    {
        string msg = "";
        foreach (Player p in players)
        {
            msg += "******************\n";
            msg += "Player: " + p.Name + "\n";
            int handIndex = 1;
            foreach (Hand h in p.Hands)
            {
                msg += "Hand: " + handIndex.ToString() + "|" + h.Value.ToString() + "\n";
                handIndex++;
            }
            msg += "******************\n";
        }
        return msg;
    }
	public string FormatError(int errorCode)
	{
        if (errorCode == 1)
            return "Invalid Input Length";
        else if (errorCode == 2)
            return "Invalid Hand";
        else if (errorCode == 3)
            return "Invalid Calculation";

        return ""; 
	}

    public string FormatVictory(Player player)
    {
        return "Player " + player.Name + " Wins!!!";
    }

    public string FormatHelp()
    {
        return "User Manual:xxxxxx";
    }
}
