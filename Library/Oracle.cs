using System;

namespace Library
{
    public class Oracle : IOracle
    {
        Random _oracleRandom;

        public Oracle()
        {
            _oracleRandom = new Random();
        }

       

        public int MakeChoice(int min, int max)
        {
            return _oracleRandom.Next(min,max+1);
        }
    }

    public class TestOracle: IOracle
    {
        int next;
        private int[] numbers;
        public TestOracle(int[] array)
        {
            numbers = array;
            next = 0;
        }
        public int MakeChoice(int min, int max)
        {
            int choice;

            if (next >= numbers.Length)
            {
                next = 0;
            }
                
            choice = numbers[next];
            next++;
                      
            return choice;
        }
    }

}
