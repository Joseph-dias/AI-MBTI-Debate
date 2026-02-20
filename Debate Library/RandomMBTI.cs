using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Random;
using static Debate_Library.MBTI;

namespace Debate_Library
{
    public static class RandomMBTI
    {
        public static MbtiType ChooseFromAll()
        {
            Random random = new Random();
            return (MbtiType)random.Next(0, 16);
        }

        public static MbtiType ChooseFromExcluding(List<MbtiType> types)
        {
            Random random = new Random();
            List<MbtiType> toChooseFrom = Enum.GetValues<MbtiType>().Except(types).ToList();
            return toChooseFrom[random.Next(0, toChooseFrom.Count)];
        }
    }
}
