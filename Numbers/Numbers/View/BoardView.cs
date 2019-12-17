using System;
using System.Collections.Generic;

public static class BoardView
{
	public static string FormatInputRequest(Player player) => $"Player {player.Name}: ";

    public static string FormatPlayer(List<Player> players)
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
	public static string FormatError(int errorCode)
	{
        if (errorCode == 0)
            return "Invalid Input Length\n";
        else if (errorCode == 1)
            return "Invalid Hand\n";
        else if (errorCode == 2)
            return "Invalid Number\n";
        else if (errorCode == 3)
            return "Invalid Calculation\n";

        return ""; 
	}

    public static string FormatVictory(Player player)
    {
        return "Player " + player.Name + " Wins!!!\n";
    }

    public static string FormatAction(Player currentPlayer, Player opponentPlayer, Results.Operations operation, Hand currentHand, Hand opponentHand){
        switch (operation){
            case Results.Operations.Plus:
                return "Player " + currentPlayer.Name + "'s Hand " + currentHand.Tag + " PLUS " + "Player " + opponentPlayer.Name + "'s Hand " + opponentHand.Tag + "\n";
            case Results.Operations.Minus:
                return "Player " + currentPlayer.Name + "'s Hand " + currentHand.Tag + " MINUS " + "Player " + opponentPlayer.Name + "'s Hand " + opponentHand.Tag + "\n";
            case Results.Operations.Mutiple:
                return "Player " + currentPlayer.Name + "'s Hand " + currentHand.Tag + " MULTIPLES " + "Player " + opponentPlayer.Name + "'s Hand " + opponentHand.Tag + "\n";
            case Results.Operations.Division:
                return "Player " + currentPlayer.Name + "'s Hand " + currentHand.Tag + " DIVIDES " + "Player " + opponentPlayer.Name + "'s Hand " + opponentHand.Tag + "\n";
            default:
                return "";
        }
    }   


    public static string FormatHelp()
    {
        return "User Manual:xxxxxx";
    }
}
