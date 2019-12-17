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
    /// <param name="players">List of all players except for the action Player</param>
    /// <param name="move">String of the Move from Player 1</param>
    /// <returns></returns>
    public static Results ValidateMove(Player actionPlayer, List<Player> players, string move)
    {
        Results results = new Results();

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
        

        //Check to see if Valid hand and number has been inputted
        foreach(Player player in players)
        {
            
        }

        
        
        return results;
    }

    public static bool CheckAdd()
    {
        return true;
    }
    
}
