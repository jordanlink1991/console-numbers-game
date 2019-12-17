using System;
using System.Collections.Generic;

public class Interpreter
{
    
	public Interpreter()
	{
	}
    

    /// <summary>
    /// Method to Validate the move from the Action Player
    /// </summary>
    /// <param name="actionPlayer">Player performing action</param>
    /// <param name="opponents">List of all players except for the action Player</param>
    /// <param name="move">String of the Move from Player 1</param>
    /// <returns></returns>
    public static Results ValidateMove(Player actionPlayer, List<Player> opponents, string move)
    {
        Results results = new Results();
        Hand actionPlayerHand = null;
        int handGoal;
        
        //First Check to see if the player needs help
        if (move.ToLower().Equals("help"))
        {
            results.Help = true;
            return results;
        }

        //Check to see if input length is proper.
        char[] splitMove = move.ToCharArray();
        if (splitMove.Length != 2)
        {
            results.ValidMove = false;
            results.ErrorType = Results.Errors.InvalidMoveLength;
            return results;
        }

        
        //Check to see if Hand Tag exists for current player
        foreach (Hand hand in actionPlayer.Hands)
            if (splitMove[0].Equals(hand.Tag))
                actionPlayerHand = hand;
        
        if(actionPlayerHand == null)
        {
            results.ErrorType = Results.Errors.InvalidHand;
            return results;
        }

        //Check to see if number is a valid number and if its inside the bounds of 0 and 10
        if (!int.TryParse(splitMove[1].ToString(), out handGoal) || handGoal >= 10 || handGoal < 0)
        {
            results.ErrorType = Results.Errors.InvalidNumber;
            return results;
        }

        //Check to see if hand and number can be completed with the hands of the Opponents
        foreach(Player opponent in opponents)
        {
            if (results.OperationType != Results.Operations.NotSet)
                break;

            foreach (Hand hand in opponent.Hands)
            {
                if (results.OperationType != Results.Operations.NotSet)
                    break;

                if (CheckAdd(actionPlayerHand, hand))
                    results.OperationType = Results.Operations.Plus;
                else if (CheckSubtract(actionPlayerHand, hand))
                    results.OperationType = Results.Operations.Minus;
                else if (CheckMultiply(actionPlayerHand, hand))
                    results.OperationType = Results.Operations.Mutiple;
                else if (CheckDivide(actionPlayerHand, hand))
                    results.OperationType = Results.Operations.Division;
            }
        }

        if (results.OperationType == Results.Operations.NotSet)
            results.ErrorType = Results.Errors.InvalidCalculation;
        
        return results;
    }

    public static bool CheckAdd(Hand actionPlayerHand, Hand opponentHand)
    {
        return true;
    }

    public static bool CheckSubtract(Hand actionPlayerHand, Hand opponentHand)
    {
        return true;
    }

    public static bool CheckMultiply(Hand actionPlayerHand, Hand opponentHand)
    {
        return true;
    }
    public static bool CheckDivide(Hand actionPlayerHand, Hand opponentHand)
    {
        return true;
    }
    
}
