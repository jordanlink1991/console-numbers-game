using System;
using System.Collections.Generic;

namespace Numbers
{
	/// <summary>
	/// Starting point for the program 
	/// </summary>
	public class Board
	{
		private static string HAND_TAGS_TWO = "LR";
		private static string HAND_TAGS_MORE = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

		static void Main(string[] args)
		{
			// var result = Interpretor.ValidateGame(args)
			// if(result.IsInvalid)
			//		return;

			// BoardView.FormatWelcome();			

			// Initialize players
			Player currentPlayer;
			List<Player> otherPlayers;
			InitializePlayers(2, 0, 2, out currentPlayer, out otherPlayers);			

			// Run forever!
			while (true)
			{
				// BoardView.FormatInput(currentPlayer);
				Console.Write("Input (e for exit): ");
				string input = Console.ReadLine();
				// var result = Interpretor.Interpret(currentPlayer, otherPlayers, input);
				// if(result.IsHelp)
				//     BoardView.FormatHelp();
				// else if(result.IsInvalid)
				//		BoardView.FormatError(result.ErrorCode);
				//
				// BoardView.FormatAction(currentPlayer, result.ActionPlayer, result.Operation, result.EndResult);
				// 
				// if (result.IsVictory) 
				// {
				//     BoardView.FormatVictory(currentPlayer);
				//     break;
				// }

				if (input == "e")
					break;

				// Push and pop players
				otherPlayers.Add(currentPlayer);
				currentPlayer = otherPlayers[0];
				otherPlayers.RemoveAt(0);
			}

			// Wait for exit
			Console.ReadKey();
		}

		/// <summary>
		/// Initialize the players
		/// </summary>
		/// <param name="humanPlayers"></param>
		/// <param name="computerPlayers"></param>
		/// <param name="handsPerPlayer"></param>
		/// <param name="currentPlayer"></param>
		/// <param name="otherPlayers"></param>
		public static void InitializePlayers(int humanPlayers, int computerPlayers, int handsPerPlayer, out Player currentPlayer, out List<Player> otherPlayers)
		{
			otherPlayers = new List<Player>();

			// Initialize the hands
			string tag = handsPerPlayer <= 2 ? HAND_TAGS_TWO : HAND_TAGS_MORE;
			List<Hand> hands = new List<Hand>();
			for (int i = 0; i < handsPerPlayer; i++)
				hands.Insert(0, new Hand(tag[i].ToString(), 1));

			// Add humans
			for (int i = 0; i < humanPlayers; i++)
				otherPlayers.Add(new Player(i.ToString(), new List<Hand>(hands)));

			// Add computer
			for (int i = 0; i < humanPlayers; i++)
				otherPlayers.Add(new Player(i.ToString(), new List<Hand>(hands), false));

			// Pop the first player
			currentPlayer = otherPlayers[0];
			otherPlayers.RemoveAt(0);
		}
	}
}
