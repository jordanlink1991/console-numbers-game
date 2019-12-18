using System;
using System.Collections.Generic;
using System.Text;

namespace Numbers.Model
{
    public class BruteResult
    {
        public Results MoveResult { get; set; }
        public int NewMoveValue { get; set; }
        public int Steps { get; set; }

        public BruteResult()
        {
            MoveResult = new Results();
            NewMoveValue = 0;
            Steps = 0;
        }
    }
}
