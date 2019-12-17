using System;

public class Results
{
    public bool Help { get; set; }
    public bool ValidMove { get; set; }
    public string Notification { get; set; }
    public Errors ErrorType { get; set; }
    public enum Errors { InvalidMoveLength, InvalidHand, InvalidNumber, InvalidCalculation }

}
