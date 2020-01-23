using System;

public class BaseInterpreter
{
	public BaseInterpreter()
	{
	}

    #region Hand Calculations
    /// <summary>
    /// Adds the Player and Opponent Hands, gets the Value Modulo 10 and compares with the Hand goal
    /// </summary>
    /// <param name="actionPlayerHand"></param>
    /// <param name="opponentHand"></param>
    /// <param name="handGoal">Value of the actionPlayerHand that is trying to be achieved</param>
    /// <returns></returns>
    public static bool CheckAdd(Hand actionPlayerHand, Hand opponentHand, out int checkValue)
    {
        checkValue = (actionPlayerHand.Value + opponentHand.Value) % 10;
        return true;

    }

    /// <summary>
    /// Subtracts the Player and Opponent Hands, gets the absolute value Modulo 10 and compares with the Hand goal
    /// </summary>
    /// <param name="actionPlayerHand"></param>
    /// <param name="opponentHand"></param>
    /// <param name="handGoal">Value of the actionPlayerHand that is trying to be achieved</param>
    /// <returns></returns>
    public static bool CheckSubtract(Hand actionPlayerHand, Hand opponentHand, out int checkValue)
    {
        checkValue = Math.Abs(actionPlayerHand.Value - opponentHand.Value) % 10;
        return true;
        //return UpdateHand(actionPlayerHand, checkValue, handGoal, results);
    }

    /// <summary>
    /// Multiplies the Player and Opponent Hands, gets the Modulo 10 value and compares with the Hand goal
    /// </summary>
    /// <param name="actionPlayerHand"></param>
    /// <param name="opponentHand"></param>
    /// <param name="handGoal">Value of the actionPlayerHand that is trying to be achieved</param>
    /// <returns></returns>
    public static bool CheckMultiply(Hand actionPlayerHand, Hand opponentHand, out int checkValue)
    {
        checkValue = (actionPlayerHand.Value * opponentHand.Value) % 10;
        return true;
    }

    /// <summary>
    /// Divides the Player and Opponent Hands, gets the Modulo 10 value and compares with the Hand goal
    /// </summary>
    /// <param name="actionPlayerHand"></param>
    /// <param name="opponentHand"></param>
    /// <param name="handGoal">Value of the actionPlayerHand that is trying to be achieved</param>
    /// <returns></returns>
    public static bool CheckDivide(Hand actionPlayerHand, Hand opponentHand, out int checkValue)
    {
        checkValue = 0;
        if (opponentHand.Value == 0  || actionPlayerHand.Value % opponentHand.Value != 0)
            return false;

        checkValue = (actionPlayerHand.Value / opponentHand.Value) % 10;
        return true;
    }

    #endregion Hand Calculations

    #region Update Hand

    /// <summary>
    /// Method to be used by the Person Interpreter Class
    /// </summary>
    /// <param name="actionPlayerHand"></param>
    /// <param name="checkValue"></param>
    /// <param name="handGoal"></param>
    /// <returns></returns>
    public static bool UpdateHand(Hand actionPlayerHand, int checkValue, int handGoal)
    {
        if (checkValue == handGoal)
        {
            actionPlayerHand.Value = handGoal;
            return true;
        }
        return false;
    }


    #endregion Update Hand

    #region Winning Hand Check

    public static bool IsWinner(Player player)
    {
        foreach (Hand hand in player.Hands)
            if (!IsWinningHand(hand))
                return false;

        return true;
    }


    public static bool IsWinningHand(Hand hand)
    {
        if (hand.Value == 8)
            return true;

        return false;
    }

    #endregion Winning Hand Check

}
