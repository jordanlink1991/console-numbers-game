using System;
using System.Collections.Generic;
using System.Text;

namespace Numbers.Controllers
{
    public class AI: BaseInterpreter
    {
        public static void BruteForce()
        {

        }

        public static void Aggressive(Player currentPlayer, List<Player> otherPlayers)
        {
            //try to get close to 8 as possible (number wise)


            //try to get close to 8 as possible (more options to 8)
        }

        public static void Defensive()
        {

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
