using Numbers.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Numbers.Controllers
{
	public class AI
	{
        /// <summary>
        /// Iterate through all possible plays and determine the quickest route to victory
        /// </summary>
        /// <param name="currentPlayer"></param>
        /// <param name="otherPlayers"></param>
        /// <returns></returns>
		public static Results BruteForce(Player currentPlayer, List<Player> otherPlayers, int maxSteps)
		{
            int steps = 0;
            BruteResult results;
            if (BruteForce(currentPlayer, otherPlayers, ref steps, maxSteps, true, out results))
            {
                if (results != null)
                {
                    // Make the update to the hand
                    results.MoveResult.HandChanged = currentPlayer.Hands.Find(x => x.Tag == results.MoveResult.HandChanged.Tag);
                    results.MoveResult.HandUsed = otherPlayers.Find(x => x.Name == results.MoveResult.OpponentUsed.Name).Hands.Find(x => x.Tag == results.MoveResult.HandChanged.Tag);
					results.MoveResult.HandChangedOriginalValue = results.MoveResult.HandChanged.Value;
					results.MoveResult.HandChanged.Value = results.NewMoveValue;
                    return results.MoveResult;
                }
            }

            // Unable to find path - return random
            return Random(currentPlayer, otherPlayers);
        }

        /// <summary>
        /// Recursively play through all possible actions
        /// </summary>
        /// <param name="currentPlayer"></param>
        /// <param name="otherPlayers"></param>
        /// <param name="steps"></param>
        /// <param name="isCurrentPlayerTurn"></param>
        /// <param name="returnResult"></param>
        /// <returns></returns>
        private static bool BruteForce(Player currentPlayer, List<Player> otherPlayers, ref int steps, int maxSteps, bool isCurrentPlayerTurn, out BruteResult returnResult)
        {
            returnResult = null;

            if (isCurrentPlayerTurn)
            {
                foreach (Player otherPlayer in otherPlayers)
                    if (ComputerInterpreter.IsWinner(otherPlayer))
                        return false;
            }
            else if (ComputerInterpreter.IsWinner(currentPlayer))
                return true;

            if (steps > maxSteps)
                return false;

            Object locker = new object();
            List<BruteResult> bruteResults = new List<BruteResult>();
            List<Task> tasks = new List<Task>();

            if (!isCurrentPlayerTurn)
            {
                for (int i = 0; i < otherPlayers.Count; i++)
                {
                    // Create a collection that contains the other players and the current player
                    List<Player> tempOtherAndCurrentPlayers = new List<Player>();
                    otherPlayers.ForEach(x => tempOtherAndCurrentPlayers.Add(new Player(x)));
                    tempOtherAndCurrentPlayers.Add(new Player(currentPlayer)); // add "current" player
                    tempOtherAndCurrentPlayers.RemoveAt(i); // remove "current other" player

                    foreach (Hand hand in otherPlayers[i].Hands)
                    {
                        foreach (Player tempOtherPlayer in tempOtherAndCurrentPlayers)
                        {
                            foreach (Hand tempOtherHand in tempOtherPlayer.Hands)
                            {
                                foreach (Results.Operations op in Enum.GetValues(typeof(Results.Operations)))
                                {
                                    if (op == Results.Operations.NotSet)
                                        continue;

                                    // Clone player/hands to prevent update
                                    Player tempPlayer = new Player(otherPlayers[i]);
                                    Hand tempHand = tempPlayer.Hands.Find(x => x.Tag == hand.Tag);

                                    int result;
                                    if (ComputerInterpreter.ValidateMove(tempHand, tempOtherHand, op, out result))
                                    {
                                        tempHand.Value = result;

                                        // Winning move
                                        if (ComputerInterpreter.IsWinner(tempPlayer))
                                            return false;

                                        // Readd player to list of other players
                                        List<Player> newOtherPlayers = new List<Player>();
                                        otherPlayers.ForEach(x => newOtherPlayers.Add(new Player(x)));
                                        newOtherPlayers.RemoveAll(x => x.Name.Equals(tempPlayer.Name));
                                        newOtherPlayers.Add(tempPlayer);

                                        BruteResult bruteResult;
                                        int newSteps = steps;

                                        tasks.Add(Task.Run(() =>
                                        {
                                            if (BruteForce(currentPlayer, newOtherPlayers, ref newSteps, maxSteps, !isCurrentPlayerTurn, out bruteResult))
                                            {
                                                lock (locker)
                                                {
                                                    bruteResults.Add(bruteResult);
                                                }
                                            }
                                        }));
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                // Iterate steps
                steps++;

                foreach (Hand hand in currentPlayer.Hands)
                {
                    foreach (Player otherPlayer in otherPlayers)
                    {
                        foreach (Hand otherHand in otherPlayer.Hands)
                        {
                            foreach (Results.Operations op in Enum.GetValues(typeof(Results.Operations)))
                            {
                                if (op == Results.Operations.NotSet)
                                    continue;
                                
                                // Clone player/hands to prevent update
                                Player tempPlayer = new Player(currentPlayer);
                                Hand tempHand = tempPlayer.Hands.Find(x => x.Tag == hand.Tag);

                                int result;
                                if (ComputerInterpreter.ValidateMove(hand, otherHand, op, out result))
                                {
                                    tempHand.Value = result;
                                    BruteResult bruteResult;
                                    int newSteps = steps;
									
                                    tasks.Add(Task.Run(() =>
                                    {
                                        if (BruteForce(tempPlayer, otherPlayers, ref newSteps, maxSteps, !isCurrentPlayerTurn, out bruteResult))
                                        {
                                            if (bruteResult == null)
                                            {
                                                bruteResult = new BruteResult();
                                                bruteResult.MoveResult = new Results();
                                                bruteResult.Steps = newSteps;
                                            }

                                            // Update the result with the current move set
                                            bruteResult.MoveResult.HandChanged = currentPlayer.Hands.Find(x => x.Tag == hand.Tag);
                                            bruteResult.MoveResult.HandUsed = otherHand;
                                            bruteResult.MoveResult.OperationType = op;
                                            bruteResult.MoveResult.OpponentUsed = otherPlayer;
                                            bruteResult.NewMoveValue = result;
                                            bruteResults.Add(bruteResult);

                                            // Add result
                                            lock (locker)
                                            {
                                                bruteResults.Add(bruteResult);
                                            }
                                        }
                                    }));
                                }
                            }
                        }
                    }
                }
            }

            // Wait for tasks to complete
            Task.WaitAll(tasks.ToArray());

            if (bruteResults.Count == 0)
                return false;

            // Find the move with the least steps
            int minSteps = bruteResults.Min(x => x.Steps);
            List<BruteResult> bestBruteResults = bruteResults.FindAll(x => x.Steps == minSteps);
            returnResult = bestBruteResults.Count == 1 ? bestBruteResults[0] : bestBruteResults[(new Random()).Next(0, bestBruteResults.Count - 1)];
            
            return true;
        }
        
        public static void Aggressive()
		{


            //try to get close to 8 as possible (more options to 8)
        }

        public static void Defensive()
        {

        }

        public static Results Random(Player currentPlayer, List<Player> otherPlayers)
        {
            Random randomPlayer;
            int randPlayer;
            Random randomHand;
            int randHand;
            int randHandComputer;
            Player pickedPlayer;
            Hand pickedHand;
            Hand computerHand;
            Array ops;
            Random randomOp;
            Results.Operations op;

            randomPlayer = new Random();
            randPlayer = randomPlayer.Next(0, otherPlayers.Count - 1);
            randomHand = new Random();
            randHand = randomHand.Next(0, currentPlayer.Hands.Count - 1);
            randHandComputer = randomHand.Next(0, currentPlayer.Hands.Count - 1);

            pickedPlayer = otherPlayers[randPlayer];
            pickedHand = pickedPlayer.Hands[randHand];
            computerHand = currentPlayer.Hands[randHandComputer];

            ops = Enum.GetValues(typeof(Results.Operations));
            randomOp = new Random();
            op = (Results.Operations)ops.GetValue(randomOp.Next(0, 3));

            int result = 0;
            while (!ComputerInterpreter.ValidateMove(computerHand, pickedHand, op, out result))
            {
                randomPlayer = new Random();
                randPlayer = randomPlayer.Next(0, otherPlayers.Count - 1);
                randomHand = new Random();
                randHand = randomHand.Next(0, currentPlayer.Hands.Count - 1);
                randHandComputer = randomHand.Next(0, currentPlayer.Hands.Count - 1);

                pickedPlayer = otherPlayers[randPlayer];
                pickedHand = pickedPlayer.Hands[randHand];
                computerHand = currentPlayer.Hands[randHandComputer];

                ops = Enum.GetValues(typeof(Results.Operations));
                randomOp = new Random();
                op = (Results.Operations)ops.GetValue(randomOp.Next(0, 3));
            }

            Results r = new Results();
            r.OpponentUsed = pickedPlayer;
            r.HandUsed = pickedHand;
            r.HandChanged = computerHand;
			r.HandChangedOriginalValue = computerHand.Value;
            r.OperationType = op;

            computerHand.Value = result;

            return r;
        }
    }
}
