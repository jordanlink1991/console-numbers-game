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
    public static bool CheckSubtract(Hand actionPlayerHand, Hand opponentHand, int handGoal)
    {
        int checkValue = Math.Abs(actionPlayerHand.Value - opponentHand.Value) % 10;
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
    public static bool CheckMultiply(Hand actionPlayerHand, Hand opponentHand, int handGoal)
    {
        int checkValue = (actionPlayerHand.Value * opponentHand.Value) % 10;
        return true;
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
        return true;
    }

    #endregion Hand Calculations

}
