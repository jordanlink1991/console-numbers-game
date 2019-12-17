﻿using System;

public class Results
{
    public bool Help { get; set; }
    public bool ValidMove { get; set; }
    public string Notification { get; set; }
    public Errors ErrorType { get; set; }
    public Operations OperationType { get; set; }
    public Player OpponentUsed { get; set; }
    public Hand HandUsed { get; set; }
    public Hand HandChanged { get; set; }
    public enum Errors { InvalidMoveLength, InvalidHand, InvalidNumber, InvalidCalculation, NotSet }
    public enum Operations {Plus, Minus, Mutiple, Division, NotSet }

    public Results()
    {
        ErrorType = Errors.NotSet;
        OperationType = Operations.NotSet;
    }

}
