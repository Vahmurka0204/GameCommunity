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
        public TestOracle()
        {
            next = 0;
        }
        public int MakeChoice(int min, int max)
        {
            int choice;

            if (next > max)
            {
                next = 0;
            }
                
            choice = next;
            next++;
                      
            return choice;
        }
    }

}
