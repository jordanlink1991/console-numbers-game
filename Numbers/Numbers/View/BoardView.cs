using System;
using System.Collections.Generic;

public static class BoardView
{
	public static string FormatInputRequest(Player player) => $"{player.Name}: ";

    public static string FormatPlayer(List<Player> players)
    {
        string msg = "";
        string msgName1 = "";
        string msgName2 = "";
        string msgTag1 = "";
        string msgTag2 = "";
        string msgNum1 = "";
        string msgNum2 = "";
        int counter = 1;

        msg += "****************************************\n";
        foreach (Player p in players)
        {
            int handNum = p.Hands.Count;
            if (counter%2 == 1)
            {
                msgName1 += p.Name + "       ";
                foreach (Hand h in p.Hands)
                {
                    msgTag1 += h.Tag + "|";
                    msgNum1 += h.Value.ToString() + "|";
                }
                msgTag1 = msgTag1.Substring(0, msgTag1.Length - 1);
                msgNum1 = msgNum1.Substring(0, msgNum1.Length - 1);

                msgTag1 += "       ";
                msgNum1 += "       ";
                //msgTag1 += "       ";
                //msgNum1 += "       ";
                msgTag1 = msgTag1.Substring(0, msgTag1.Length - 4);
                msgNum1 = msgNum1.Substring(0, msgNum1.Length - 4);
            }

            else
            {
                msgName2 += p.Name + "       ";
                foreach (Hand h in p.Hands)
                {
                    msgTag2 += h.Tag + "|";
                    msgNum2 += h.Value.ToString() + "|";
                }
                msgTag2 = msgTag2.Substring(0, msgTag2.Length - 1);
                msgNum2 = msgNum2.Substring(0, msgNum2.Length - 1);

                msgTag2 += "       ";
                msgNum2 += "       ";
                //msgTag2 += "       ";
                //msgNum2 += "       ";
                msgTag2 = msgTag2.Substring(0, msgTag2.Length - 4);
                msgNum2 = msgNum2.Substring(0, msgNum2.Length - 4);
            }
            counter++;
            
        }
        msg += msgName1 + "\n";
        msg += msgTag1 + "\n";
        msg += msgNum1 + "\n\n";
        msg += msgNum2 + "\n";
        msg += msgTag2 + "\n";
        msg += msgName2 + "\n";
        msg += "****************************************\n";

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
        return player.Name + " Wins!!!\n";
    }

    public static string FormatAction(Player currentPlayer, Player opponentPlayer, Results.Operations operation, Hand currentHand, Hand opponentHand){
        switch (operation){
            case Results.Operations.Plus:
                return currentPlayer.Name + "'s Hand " + currentHand.Tag + " PLUS " + opponentPlayer.Name + "'s Hand " + opponentHand.Tag + "\n";
            case Results.Operations.Minus:
                return currentPlayer.Name + "'s Hand " + currentHand.Tag + " MINUS " + opponentPlayer.Name + "'s Hand " + opponentHand.Tag + "\n";
            case Results.Operations.Mutiple:
                return currentPlayer.Name + "'s Hand " + currentHand.Tag + " MULTIPLES " + opponentPlayer.Name + "'s Hand " + opponentHand.Tag + "\n";
            case Results.Operations.Division:
                return currentPlayer.Name + "'s Hand " + currentHand.Tag + " DIVIDES " + opponentPlayer.Name + "'s Hand " + opponentHand.Tag + "\n";
            default:
                return "";
        }
    }   

    public static string FormatWelcome()
    {

        string manual = "********************\n";
        manual += "Welcome to Numbers\n";
        manual += "Goal: Try to get 8s on all of your hands.\n";
        manual += "How: Use your opponents hand values to change your hands. All basic operations (Addition, Subtraction, Multiplication and Division) are available.\n";
        manual += "Input: Specify the Hand Tag you want to change and the value. ex. A2 => Change my A hand to 2\n";
        manual += "The Application will let you know if your input is valid, but keep in mind that other players can use your hand to win.\n";
        manual += "Press any key to begin.\n";
        manual += "Have Fun!!!!\n";
        manual += "\n\n";
        manual += "Max total players: 6 \n";
        manual += "Number of Human player (min 0, max 6)\n";
        manual += "Number of Computer player (min 0, max 6)\n";
        manual += "Number of Hands (min 2, max 6)\n";
        manual += "Total number of players * number of hands cannot exceed 12";
        manual += "Level of Difficulty (Easy, Medium, Hard)\n";
        manual += "********************\n";
        return manual;
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
