using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;

namespace CoinRepresentation
{
    //create the class to associate the position (the given number) with the value on the Stern sequence
    public class CoinRepresentation
    {
        private class Check
        {
            //accessable properties
            public long Position { get; } //declare the position which is read-only
            public long Value { get; } //declare the value which is read-only
            //constructor
            public Check(long position, long value)
            {
                Position = position; //asign positon from the given
                Value = value; //assin value from the given
            }
        }
        //find the result using binary search on Stern diatomic sequence
        private static long Find(long sum, Check start, Check end)
        {
            if (sum == start.Position) return start.Value; //value of the sum is calculated, return it
            else if (sum == end.Position) return end.Value; //value of the sum is calculated, return it
            else
            {
                //calculate the mid point
                long mid_point = start.Position + ((end.Position - start.Position) / 2); //calculate the position of mid on the sequence which is in the middle of the given range
                long mid_value = start.Value + end.Value; //calculate the value associate with the position with will be the sum of the 2 current end points (Stern sequence property)
                Check mid = new Check(mid_point, mid_value); //create the Check object with the calculated position and value
                //recursion to calculate the value of the sum position
                if (sum > mid.Position) return Find(sum, mid, end); //binary search continues on the right hand side
                else return Find(sum, start, mid); //binary search continues on the left hand side
            }
        }
        //main method to solve the problem
        public static long Solve(long sum)
        {
            //calculate the start point
            int value1 = Convert.ToInt32(Math.Floor(Math.Log(sum, 2))); //calculate the nearest k of the 2^k on the left hand side
            long position1 = (long)(Math.Pow(2, value1)) - 1; //calculate the nearesr (2^k - 1) on the left hand side, this will be the start position
            Check start = new Check(position1, 1); //create the check object from the calculated position and assign value 1 (Stern sequence property)
            //calculate the end point
            int value2 = Convert.ToInt32(Math.Ceiling(Math.Log(sum, 2))); //calculate the nearest k of the 2^k on the right hand side
            long position2 = (long)(Math.Pow(2, value2)) - 1; //calculate the nearesr (2^k - 1) on the right hand side, this will be the end position
            Check end = new Check(position2, 1); //create the check object from the calculated position and assign value 1 (Stern sequence property)
            //Check to find the result
            if (start.Position == end.Position) return value1 + 1; //if the sum is already a 2^k, the result will be (k + 1) (Stern sequence property)
            else return Find(sum, start, end); //enter the binary search on the Stern sequence with the calculated starting range
        }
    }
}