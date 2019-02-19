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
        public int MakeChoice(int min, int max)
        {
            return min;
        }
    }

}
