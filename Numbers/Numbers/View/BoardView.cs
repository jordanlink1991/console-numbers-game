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
            msg += "********************\n";
            msg += "Player: " + p.Name + "\n";
            foreach (Hand h in p.Hands)
                msg += "Hand: " + h.Tag + "|" + h.Value.ToString() + "\n";

            msg += "********************\n";
        }
        return msg;
    }
	public static string FormatError(Results.Errors errorCode)
	{
        switch (errorCode)
        {
            case Results.Errors.InvalidMoveLength:
                return "Invalid Input Length - Please use one letter plus one digit\n";
            case Results.Errors.InvalidHand:
                return "Invalid Hand - Please use your proper hand tag\n";
            case Results.Errors.InvalidNumber:
                return "Invalid Number - Please use last digit only if your end result is a two digits number\n";
            case Results.Errors.InvalidCalculation:
                return "Invalid Calculation - Please check your calculation\n";
            default:
                return "";
        }
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
        string manual = "********************\n";
        manual += "User Manual:\n";
        manual += "End Goal: Achive 8 on all hands\n";
        manual += "Rule #1: Can only do operations against other players' hands\n";
        manual += "Rule #2: Operations allowed: Plus, Minus, Mulplication, Division\n";
        manual += "Rule #3: If result after operation is a two digits number, only take the last digit (0-9)\n";
        manual += "********************\n";
        return manual;
    }
}
