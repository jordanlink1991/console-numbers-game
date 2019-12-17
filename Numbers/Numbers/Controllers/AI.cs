using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Numbers.Controllers
{
    public class AI
    {
        public static void BruteForce(Player currentPlayer, List<Player> otherPlayers)
        {
            List<Tuple<Results, int, int>> bruteResults = new List<Tuple<Results, int, int>>();

            // Clone player/hands to prevent update
            Player tempPlayer = new Player(currentPlayer);

            // Iterate through all opponents
            for (int i = 0; i < otherPlayers.Count; i++)
            {
                Player otherPlayer = otherPlayers[i];

                // Iterate through all current hands
                for (int j = 0; j < tempPlayer.Hands.Count; j++)
                {
                    Hand tempHand = tempPlayer.Hands[j];

                    // Initialize step counter
                    int steps = 0;

                    // Iterate through all other players' hands
                    for (int k = 0; k < otherPlayer.Hands.Count; k++)
                    {
                        Hand otherHand = otherPlayer.Hands[k];

                        // Iterate through all operators
                        foreach (Results.Operations op in Enum.GetValues(typeof(Results.Operations)))
                        {
                            int checkValue = -1;
                            if (ComputerInterpreter.ValidateMove(tempHand, otherHand, op, out checkValue))
                            {
                                tempHand.Value = checkValue;
                                if (run(tempPlayer, otherPlayers, steps, false))
                                {
                                    Results results = new Results();
                                    results.HandChanged = currentPlayer.Hands[j];
                                    results.HandUsed = otherHand;
                                    results.OperationType = op;
                                    results.OpponentUsed = otherPlayer;

                                    bruteResults.Add(new Tuple<Results, int, int>(results, checkValue, steps));
                                }
                            }
                        }
                    }
                }
            }

            // Find the move with the least steps
            Tuple<Results, int, int> bestResult = null;
            foreach (Tuple<Results, int, int> bruteResult in bruteResults)
                bestResult = bestResult == null ? bruteResult : bestResult.Item3 > bruteResult.Item3 ? bruteResult : bestResult;

            // Update hand!
            if (bestResult != null)
                bestResult.Item1.HandChanged.Value = bestResult.Item2;
        }

        public static void Aggressive(Player currentPlayer, List<Player> otherPlayers)
        {
            //try to get close to 8 as possible (number wise)


            //try to get close to 8 as possible (more options to 8)
        }

        public static void Defensive()
        {

        }

        private static bool run(Player currentPlayer, List<Player> otherPlayers, int steps, bool isCurrentPlayerTurn)
        {
            if (ComputerInterpreter.IsWinner(currentPlayer))
                return true;

            foreach (Player otherPlayer in otherPlayers)
                if (ComputerInterpreter.IsWinner(otherPlayer))
                    return false;

            // Add to steps
            steps++;

            // Clone player/hands to prevent update
            Player tempPlayer = new Player(currentPlayer);
            List<Player> tempOtherPlayers = new List<Player>();
            otherPlayers.ForEach(x => tempOtherPlayers.Add(new Player(x)));

            if (isCurrentPlayerTurn)
            {
                // Iterate through all opponents
                for (int i = 0; i < tempOtherPlayers.Count; i++)
                {
                    Player tempOtherPlayer = tempOtherPlayers[i];

                    // Iterate through all current hands
                    for (int j = 0; j < tempPlayer.Hands.Count; j++)
                    {
                        Hand tempHand = tempPlayer.Hands[j];

                        // Iterate through all other players' hands
                        for (int k = 0; k < tempOtherPlayer.Hands.Count; k++)
                        {
                            // Iterate through all operators
                            foreach (Results.Operations op in Enum.GetValues(typeof(Results.Operations)))
                            {
                                int result = -1;
                                if (ComputerInterpreter.ValidateMove(tempHand, tempOtherPlayer.Hands[k], op, out result))
                                {
                                    tempHand.Value = result;
                                    if (run(tempPlayer, tempOtherPlayers, steps, !isCurrentPlayerTurn))
                                        return true;
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                // Iterate through all opponents
                for (int i = 0; i < tempOtherPlayers.Count; i++)
                {
                    // Create a collection that contains the other players and the current player
                    List<Player> tempOtherAndCurrentPlayers = new List<Player>();
                    tempOtherPlayers.ForEach(x => tempOtherAndCurrentPlayers.Add(new Player(x)));
                    tempOtherAndCurrentPlayers.Add(new Player(currentPlayer)); // add "current" player
                    tempOtherAndCurrentPlayers.RemoveAt(i); // remove "current other" player

                    // Clone current player/hands to prevent update
                    Player tempOtherPlayer = tempOtherPlayers[i];

                    // Iterate through all current hands
                    for (int j = 0; j < tempOtherPlayer.Hands.Count; j++)
                    {
                        Hand tempOtherHand = tempOtherPlayer.Hands[j];

                        // Iterate through all other (and current) players
                        for (int k = 0; k < tempOtherAndCurrentPlayers.Count; k++)
                        {
                            Player tempOtherOrCurrentPlayer = tempOtherAndCurrentPlayers[k];

                            // Iterate through all other (and current) players' hands
                            for (int l = 0; l < tempOtherOrCurrentPlayer.Hands.Count; l++)
                            {
                                // Iterate through all operators
                                foreach (Results.Operations op in Enum.GetValues(typeof(Results.Operations)))
                                {
                                    int result = -1;
                                    if (ComputerInterpreter.ValidateMove(tempOtherHand, tempOtherOrCurrentPlayer.Hands[l], op, out result))
                                    {
                                        tempOtherHand.Value = result;
                                        if (!run(tempPlayer, tempOtherPlayers, steps, !isCurrentPlayerTurn))
                                            return false;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            // Unable to find path
            return false;
        }

        public static Results Random(Player currentPlayer, List<Player> otherPlayers)
        {
            Random randomPlayer = new Random();
            int randPlayer = randomPlayer.Next(0, otherPlayers.Count - 1);
            Random randomHand = new Random();
            int randHand = randomHand.Next(0, currentPlayer.Hands.Count - 1);
            int randHandComputer = randomHand.Next(0, currentPlayer.Hands.Count - 1);

            Player pickedPlayer = otherPlayers[randPlayer];
            Hand pickedHand = pickedPlayer.Hands[randHand];
            Hand computerHand = currentPlayer.Hands[randHandComputer];

            Array ops = Enum.GetValues(typeof(Results.Operations));
            Random randomOp = new Random();
            Results.Operations op = (Results.Operations)ops.GetValue(randomOp.Next(0, 3));

            //while ()
            Results r = new Results();
            r.OpponentUsed = pickedPlayer;
            r.HandUsed = pickedHand;
            r.HandChanged = computerHand;
            return r;
        }
    }
}
