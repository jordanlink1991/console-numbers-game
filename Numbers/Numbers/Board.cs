using System;
using System.Collections.Generic;
using Numbers.Controllers;

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

			Console.WriteLine(BoardView.FormatWelcome());

            //Read user config
            string humanPlayerCountInput, computerPlayerCountInput, handsPerPlayerCountInput, computerPlayerDifficultyLevelInput = string.Empty;
			int humanPlayerCount = 0, computerPlayerCount = 0, handsPerPlayerCount = 0;

			bool okToStart = false;
			string errorMessage = string.Empty;
            while (!okToStart)
            {
				if (!string.IsNullOrEmpty(errorMessage))
				{
					Console.WriteLine();
					Console.WriteLine("-------------------------------------------");
					Console.WriteLine("ERROR - " + errorMessage);
					Console.WriteLine("-------------------------------------------\n");
				}

                Console.Write("Number of Human Players (0-6): ");
                humanPlayerCountInput = Console.ReadLine();
                Console.Write("Number of Computer Players (0-6): ");
                computerPlayerCountInput = Console.ReadLine();
                Console.Write("Number of Hands (2-6): ");
                handsPerPlayerCountInput = Console.ReadLine();

				int.TryParse(humanPlayerCountInput, out humanPlayerCount);
				int.TryParse(computerPlayerCountInput, out computerPlayerCount);
				int.TryParse(handsPerPlayerCountInput, out handsPerPlayerCount);

				if (computerPlayerCount > 0)
				{
					Console.Write("Computer Difficulty Level (easy, medium, hard): ");
					computerPlayerDifficultyLevelInput = Console.ReadLine();
					errorMessage = BaseInterpreter.CheckConfig(humanPlayerCountInput, computerPlayerCountInput, handsPerPlayerCountInput, computerPlayerDifficultyLevelInput ?? string.Empty);
				}
				else
				{
					errorMessage = BaseInterpreter.CheckConfig(humanPlayerCountInput, computerPlayerCountInput, handsPerPlayerCountInput);
				}

				if(string.IsNullOrEmpty(errorMessage))
					okToStart = true;
			}

            Console.Clear();

			// Initialize players
			List<Player> players = InitializePlayers(humanPlayerCount, computerPlayerCount, handsPerPlayerCount);

			// Pop the first player
			Player currentPlayer = players[0];
			List<Player> otherPlayers = new List<Player>(players);
			otherPlayers.RemoveAt(0);

			// Run game
			while (true)
			{
				// Print state of Board
				//Console.WriteLine(BoardView.FormatPlayer(players));
				Console.WriteLine(BoardView.FormatBoard(players));

				Results result;
				if (currentPlayer.IsHuman)
				{
					// Read input
					Console.Write(BoardView.FormatInputRequest(currentPlayer));
					string input = Console.ReadLine();

                    // Clear existing input
                    Console.Clear();

                    result = PersonInterpreter.ValidateMove(currentPlayer, otherPlayers, input);
					if (result.Help)
					{
						Console.WriteLine(BoardView.FormatHelp());
						continue;
					}
					else if (!result.ValidMove)
					{
						Console.WriteLine(BoardView.FormatError(result.ErrorType));
						continue;
					}
				}
				else
                {
                    Console.WriteLine(BoardView.FormatThinking(currentPlayer));

					int thinkTimerMilliseconds = 1500;

					// Determine move
					DateTime thinkStart = DateTime.Now;
                    if (computerPlayerDifficultyLevelInput.ToLower() == "medium")
                        result = AI.BruteForce(currentPlayer, otherPlayers, 1);
                    else if (computerPlayerDifficultyLevelInput.ToLower() == "hard")
                        result = AI.BruteForce(currentPlayer, otherPlayers, 2);
                    else
                        result = AI.Random(currentPlayer, otherPlayers);
					DateTime thinkEnd = DateTime.Now;

					// Prevent screen flashing
					int thinkTime = (thinkEnd - thinkStart).Milliseconds;
					if (thinkTime < thinkTimerMilliseconds)
						System.Threading.Thread.Sleep(thinkTimerMilliseconds - thinkTime);

					// Clear existing input
					Console.Clear();
                }

                // Write action
				Console.WriteLine(BoardView.FormatAction(currentPlayer, result.OpponentUsed, result.OperationType, result.HandChanged, result.HandUsed, result.HandChangedOriginalValue));

                // Detect winner
				if (BaseInterpreter.IsWinner(currentPlayer))
				{
					// Print state of Board
					Console.WriteLine(BoardView.FormatBoard(players));
					Console.WriteLine(BoardView.FormatVictory(currentPlayer));
					break;
				}

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
		private static List<Player> InitializePlayers(int humanPlayers, int computerPlayers, int handsPerPlayer)
		{
			List<Player> players = new List<Player>();

			// Initialize the hands
			string tag = (handsPerPlayer <= 2 ? HAND_TAGS_TWO : HAND_TAGS_MORE);
			
			List<Hand> hands = new List<Hand>();
			for (int i = 0; i < handsPerPlayer; i++)
				hands.Add(new Hand(tag[i].ToString(), 1));

			// Add humans
			for (int i = 0; i < humanPlayers; i++)
			{
				List<Hand> playerHands = new List<Hand>();
				hands.ForEach(x => playerHands.Add(new Hand(x)));
				players.Add(new Player($"Player {(i + 1).ToString()}", new List<Hand>(playerHands)));
			}

			// Add computer
			for (int i = 0; i < computerPlayers; i++)
			{
				List<Hand> playerHands = new List<Hand>();
				hands.ForEach(x => playerHands.Add(new Hand(x)));
				players.Add(new Player($"Computer {(i + 1).ToString()}", new List<Hand>(playerHands), false));
			}

			return players;
		}
	}
}
