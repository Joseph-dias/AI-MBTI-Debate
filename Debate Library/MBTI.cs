using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Debate_Library
{
    public static class MBTI
    {
        public enum MbtiType
        {
            ISTJ,
            ISFJ,
            INFJ,
            INTJ,
            ISTP,
            ISFP,
            INFP,
            INTP,
            ESTP,
            ESFP,
            ENFP,
            ENTP,
            ESTJ,
            ESFJ,
            ENFJ,
            ENTJ
        }

        public static readonly Dictionary<MbtiType, string> MbtiDescriptions = new()
        {
            { MbtiType.ISTJ, "Quiet, serious, thorough, and dependable. Practical, realistic, responsible, and fact-oriented. Value traditions, loyalty, and order; work steadily to fulfill commitments." },
            { MbtiType.ISFJ, "Quiet, friendly, responsible, and conscientious. Committed to obligations, steady, and loyal. Strive for harmony, remember details about people, and create orderly environments." },
            { MbtiType.INFJ, "Seek meaning and connection in ideas, relationships, and the world. Insightful, principled, and idealistic. Develop clear visions for the common good and organize decisively to implement them." },
            { MbtiType.INTJ, "Have original minds and great drive to implement ideas and achieve goals. Quickly see patterns, develop long-range perspectives, and are skeptical, independent, and decisive." },
            { MbtiType.ISTP, "Tolerant, flexible, and quiet observers until a problem appears—then act quickly to find workable solutions. Analyze causes and effects logically; value efficiency and freedom." },
            { MbtiType.ISFP, "Quiet, friendly, sensitive, and kind. Enjoy the present moment, value harmony, and dislike conflicts. Loyal and committed to close relationships; seek to serve others' needs." },
            { MbtiType.INFP, "Idealistic, loyal to their values, and curious. Seek to understand people and serve humanity. Adaptable, flexible, and helpful; dislike rigid structures that conflict with their ideals." },
            { MbtiType.INTP, "Seek logical explanations for everything that interests them. Theoretical, abstract thinkers who value intelligence and ingenuity. Quiet, contained, flexible, and tolerant." },
            { MbtiType.ESTP, "Flexible, tolerant, pragmatic, and focused on immediate results. Enjoy material comforts and action. Learn best through doing; spontaneous, energetic, and resourceful problem-solvers." },
            { MbtiType.ESFP, "Outgoing, friendly, accepting, exuberant lovers of life. Enjoy working with others to make things happen. Bring common sense and realism to any project; flexible and spontaneous." },
            { MbtiType.ENFP, "Warm, enthusiastic, imaginative, and creative. See life as full of possibilities. Make connections between events/ideas and inspire others; flexible and open-minded." },
            { MbtiType.ENTP, "Quick, ingenious, stimulating, resourceful, and adept at possibilities. Good at reading people; debate almost anything for intellectual stimulation. Value competence and ingenuity." },
            { MbtiType.ESTJ, "Practical, realistic, matter-of-fact, decisive, and quick to implement plans. Organize projects and people to get things done; focus on efficiency, tradition, and clear standards." },
            { MbtiType.ESFJ, "Warmhearted, conscientious, and cooperative. Want harmony in their environment and work with determination to establish it. Loyal, notice others' needs, and provide practical care." },
            { MbtiType.ENFJ, "Warm, empathetic, responsive, and responsible. Highly attuned to others' emotions and needs. Provide inspiring leadership and facilitate growth in people and groups." },
            { MbtiType.ENTJ, "Frank, decisive, assume leadership readily. See inefficiencies quickly and devise systems to solve problems. Enjoy long-term planning and forceful implementation of goals." }
        };
    }
}
