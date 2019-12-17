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
        if (!int.TryParse(splitMove[1].ToString(), out int handGoal) || handGoal >= 10 || handGoal < 0)
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

                if (CheckAdd(actionPlayerHand, hand, handGoal))
                    results.OperationType = Results.Operations.Plus;
                else if (CheckSubtract(actionPlayerHand, hand, handGoal))
                    results.OperationType = Results.Operations.Minus;
                else if (CheckMultiply(actionPlayerHand, hand, handGoal))
                    results.OperationType = Results.Operations.Mutiple;
                else if (CheckDivide(actionPlayerHand, hand, handGoal))
                    results.OperationType = Results.Operations.Division;
            }
        }

        if (results.OperationType == Results.Operations.NotSet)
            results.ErrorType = Results.Errors.InvalidCalculation;
        
        return results;
    }


    #region Hand Calculations
    /// <summary>
    /// Adds the Player and Opponent Hands, gets the Value Modulo 10 and compares with the Hand goal
    /// </summary>
    /// <param name="actionPlayerHand"></param>
    /// <param name="opponentHand"></param>
    /// <param name="handGoal">Value of the actionPlayerHand that is trying to be achieved</param>
    /// <returns></returns>
    public static bool CheckAdd(Hand actionPlayerHand, Hand opponentHand, int handGoal)
    {
        int checkValue = (actionPlayerHand.Value + opponentHand.Value) % 10;
        return UpdateHand(actionPlayerHand, checkValue, handGoal);
    }

    /// <summary>
    /// Subtracts the Player and Opponent Hands, gets the absolute value Modulo 10 and compares with the Hand goal
    /// </summary>
    /// <param name="actionPlayerHand"></param>
    /// <param name="opponentHand"></param>
    /// <param name="handGoal">Value of the actionPlayerHand that is trying to be achieved</param>
    /// <returns></returns>
    public static bool CheckSubtract(Hand actionPlayerHand, Hand opponentHand, int handGoal)
    {
        int checkValue = Math.Abs(actionPlayerHand.Value - opponentHand.Value) % 10;
        return UpdateHand(actionPlayerHand, checkValue, handGoal);
    }

    /// <summary>
    /// Multiplies the Player and Opponent Hands, gets the Modulo 10 value and compares with the Hand goal
    /// </summary>
    /// <param name="actionPlayerHand"></param>
    /// <param name="opponentHand"></param>
    /// <param name="handGoal">Value of the actionPlayerHand that is trying to be achieved</param>
    /// <returns></returns>
    public static bool CheckMultiply(Hand actionPlayerHand, Hand opponentHand, int handGoal)
    {
        int checkValue = (actionPlayerHand.Value * opponentHand.Value) % 10;
        return UpdateHand(actionPlayerHand, checkValue, handGoal);
    }

    /// <summary>
    /// Divides the Player and Opponent Hands, gets the Modulo 10 value and compares with the Hand goal
    /// </summary>
    /// <param name="actionPlayerHand"></param>
    /// <param name="opponentHand"></param>
    /// <param name="handGoal">Value of the actionPlayerHand that is trying to be achieved</param>
    /// <returns></returns>
    public static bool CheckDivide(Hand actionPlayerHand, Hand opponentHand, int handGoal)
    {
        if (actionPlayerHand.Value % opponentHand.Value != 0)
            return false;

        int checkValue = (actionPlayerHand.Value / opponentHand.Value) % 10;
        return UpdateHand(actionPlayerHand, checkValue, handGoal);
    }


    public static bool UpdateHand(Hand actionPlayerHand, int checkValue, int handGoal)
    {
        if (checkValue == handGoal)
        {
            actionPlayerHand.Value = handGoal;
            return true;
        }
        return false;
    }
    #endregion Hand Calculations


}
