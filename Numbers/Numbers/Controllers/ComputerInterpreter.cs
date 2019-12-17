using System;

public class ComputerInterpreter : BaseInterpreter
{
	public ComputerInterpreter()
	{
	}

    public bool ValidateMove(Hand computerHand, Hand opponentHand, Results.Operations operations, out int result)
    {
        result = 0;
        switch (operations)
        {
            case Results.Operations.Plus:
                return CheckAdd(computerHand, opponentHand, out result);
            case Results.Operations.Minus:
                return CheckSubtract(computerHand, opponentHand, out result);
            case Results.Operations.Mutiple:
                return CheckMultiply(computerHand, opponentHand, out result);
            case Results.Operations.Division:
                return CheckDivide(computerHand, opponentHand, out result);
        }
        return true;
    }

}
