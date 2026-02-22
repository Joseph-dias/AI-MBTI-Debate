using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Random;
using static Debate_Library.Personality;

namespace Debate_Library
{
    public static class RandomExtensions
    {
        public static MbtiType ChooseFromAllMBTI(this Random rand)
        {
            return (MbtiType)rand.Next(0, 16);
        }

        public static MbtiType ChooseFromExcludingMBTI(this Random rand, List<MbtiType> types)
        {
            List<MbtiType> toChooseFrom = Enum.GetValues<MbtiType>().Except(types).ToList();
            return toChooseFrom[rand.Next(0, toChooseFrom.Count)];
        }

        /// <summary>
        /// Choose a number of traits
        /// </summary>
        /// <param name="rand"></param>
        /// <param name="num">number of traits to choose</param>
        /// <returns></returns>
        public static List<Trait> ChooseTraits(this Random rand, int num = 3)
        {
            List<Trait> toChooseFrom = Enum.GetValues<Trait>().ToList();

            if (num > toChooseFrom.Count) throw new IndexOutOfRangeException("Too many traits to choose"); //Error checking

            List<int> nums = new List<int>();

            List<Trait> toReturn = new List<Trait>();

            for (int i = 0; i < num; i++) //Pick the traits
            {
                int x;
                do
                {
                    x = rand.Next(0, toChooseFrom.Count);
                } while (nums.Contains(x));  //Choose different numbers
                toReturn.Add(toChooseFrom[x]);
            }

            return toReturn;
        }

        public static Vocation ChooseVocation(this Random rand)
        {
            List<Vocation> toChooseFrom = Enum.GetValues<Vocation>().ToList();
            int x = rand.Next(0, toChooseFrom.Count);
            return toChooseFrom[x];
        }

        public static string ChooseExperiences(this Random rand)
        {
            return Experiences[rand.Next(0, Experiences.Count)];
        }
    }
}
