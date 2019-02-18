using SecretSanta.Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Domain.Services
{
    public class RandomService : IRandomService
    {        
        private static readonly Random GlobalRandom = new Random();
        [ThreadStatic] private static Random  LocalRandom;
        public RandomService()
        {
            if (LocalRandom == null)
            {
                lock (GlobalRandom)
                {
                    LocalRandom = new Random(GlobalRandom.Next());
                }
            }
        }
        public int Next()
        {
            if(LocalRandom == null)
            {
                lock(GlobalRandom)
                {
                    LocalRandom = new Random(GlobalRandom.Next());
                }
            }
            return LocalRandom.Next();
        }

        public int Next(int max)
        {
            if (LocalRandom == null)
            {
                lock (GlobalRandom)
                {
                    LocalRandom = new Random(GlobalRandom.Next());
                }
            }
            return LocalRandom.Next(max);
        }
    }
}
