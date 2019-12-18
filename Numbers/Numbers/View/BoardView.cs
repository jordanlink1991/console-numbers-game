using System;
using System.Collections.Generic;

public static class BoardView
{
	public static string FormatInputRequest(Player player) => $"{player.Name}: ";

	public static string FormatBoard(List<Player> players)
	{
		// Determine top and bottom players
		List<Player> topPlayers = new List<Player>();
		List<Player> bottomPlayers = new List<Player>();
		for (int i = 0; i < players.Count; i++)
			if (i == 0 || i % 2 == 0)
				topPlayers.Add(players[i]);
			else
				bottomPlayers.Add(players[i]);

		string response = string.Empty;
		const int lengthPerChunk = 25;

		response += "\n";

		// First line
		foreach (Player player in topPlayers)
		{
			string playerText = string.Empty;
			string name = player.Name;
			int fillerLength = lengthPerChunk - name.Length;
			int startPos = (lengthPerChunk / 2) - (name.Length / 2);
			playerText += " ";
			for (int i = 1; i < startPos; i++)
				playerText += "-";
			playerText += name;
			for (int i = playerText.Length; i < lengthPerChunk-1; i++)
				playerText += "-";
			response += playerText + " ";
		}

		// Second line
		response += "\n";
		foreach (Player player in topPlayers)
		{
			string index = string.Empty;
			for (int i = 0; i < player.Hands.Count; i++) {
				if (i > 0)
					index += " ";
				index += $" {player.Hands[i].Tag} ";
			}

			string indexText = string.Empty;
			int fillerLength = lengthPerChunk - index.Length;
			int startPos = (lengthPerChunk / 2) - (index.Length / 2);
			for (int i = 0; i < startPos; i++)
				indexText += " ";
			indexText += index;
			for (int i = indexText.Length; i < lengthPerChunk; i++)
				indexText += " ";
			response += indexText;
		}

		// Third line
		response += "\n";
		foreach (Player player in topPlayers)
		{
			string values = string.Empty;
			for (int i = 0; i < player.Hands.Count; i++)
			{
				if (i > 0)
					values += "|";
				values += $" {player.Hands[i].Value.ToString()} ";
			}

			string valueText = string.Empty;
			int fillerLength = lengthPerChunk - values.Length;
			int startPos = (lengthPerChunk / 2) - (values.Length / 2);
			for (int i = 0; i < startPos; i++)
				valueText += " ";
			valueText += values;
			for (int i = valueText.Length; i < lengthPerChunk; i++)
				valueText += " ";
			response += valueText;
		}

		// Filler
		response += "\n\n";

		// Fourth line
		response += "\n";
		foreach (Player player in bottomPlayers)
		{
			string values = string.Empty;
			for (int i = 0; i < player.Hands.Count; i++)
			{
				if (i > 0)
					values += "|";
				values += $" {player.Hands[i].Value.ToString()} ";
			}

			string valueText = string.Empty;
			int fillerLength = lengthPerChunk - values.Length;
			int startPos = (lengthPerChunk / 2) - (values.Length / 2);
			for (int i = 0; i < startPos; i++)
				valueText += " ";
			valueText += values;
			for (int i = valueText.Length; i < lengthPerChunk; i++)
				valueText += " ";
			response += valueText;
		}

		// Fifth line
		response += "\n";
		foreach (Player player in bottomPlayers)
		{
			string index = string.Empty;
			for (int i = 0; i < player.Hands.Count; i++)
			{
				if (i > 0)
					index += " ";
				index += $" {player.Hands[i].Tag} ";
			}

			string indexText = string.Empty;
			int fillerLength = lengthPerChunk - index.Length;
			int startPos = (lengthPerChunk / 2) - (index.Length / 2);
			for (int i = 0; i < startPos; i++)
				indexText += " ";
			indexText += index;
			for (int i = indexText.Length; i < lengthPerChunk; i++)
				indexText += " ";
			response += indexText;
		}

		// Sixth line
		response += "\n";
		foreach (Player player in bottomPlayers)
		{
			string playerText = string.Empty;
			string name = player.Name;
			int fillerLength = lengthPerChunk - name.Length;
			int startPos = (lengthPerChunk / 2) - (name.Length / 2);
			playerText += " ";
			for (int i = 1; i < startPos; i++)
				playerText += "-";
			playerText += name;
			for (int i = playerText.Length; i < lengthPerChunk-1; i++)
				playerText += "-";
			response += playerText + " ";
		}

		response += "\n";

		return response;
	}

	public static string FormatThinking(Player player) => $"{player.Name} is thinking...";

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

    public static string FormatAction(Player currentPlayer, Player opponentPlayer, Results.Operations operation, Hand currentHand, Hand opponentHand) {
        switch (operation){
            case Results.Operations.Plus:
                return "> " + currentPlayer.Name + "'s Hand " + currentHand.Tag + " PLUS " + opponentPlayer.Name + "'s Hand " + opponentHand.Tag + "\n";
            case Results.Operations.Minus:
                return "> " + currentPlayer.Name + "'s Hand " + currentHand.Tag + " MINUS " + opponentPlayer.Name + "'s Hand " + opponentHand.Tag + "\n";
            case Results.Operations.Mutiple:
                return "> " + currentPlayer.Name + "'s Hand " + currentHand.Tag + " MULTIPLES " + opponentPlayer.Name + "'s Hand " + opponentHand.Tag + "\n";
            case Results.Operations.Division:
                return "> " + currentPlayer.Name + "'s Hand " + currentHand.Tag + " DIVIDES " + opponentPlayer.Name + "'s Hand " + opponentHand.Tag + "\n";
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
