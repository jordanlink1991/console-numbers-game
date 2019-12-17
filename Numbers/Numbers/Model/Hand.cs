using System;
using System.Diagnostics;

[DebuggerDisplay("Tag: {Tag}, Value: {Value}")]
public class Hand
{
	/// <summary>
	/// Tag (or name) of the hand
	/// </summary>
	public string Tag { get; set; }
	
	/// <summary>
	/// Value of the hand
	/// </summary>
	public int Value { get; set; }

	public Hand()
	{
        Tag = "";
		Value = 0;
	}

	public Hand(string tag, int value)
	{
		Tag = tag ?? string.Empty;
		Value =  value;
	}

	public Hand(Hand h)
		: this(h.Tag, h.Value)
	{ }
}
