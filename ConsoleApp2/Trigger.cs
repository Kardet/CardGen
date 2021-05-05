using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp2
{
    class Trigger
    {
        public enum TriggerEff
        {
            ON_DRAW_CARD,
            ON_DISCARD,
            ON_ATTACK,
            ON_DESTROY,
            ON_LIFEGAIN,
            ON_LIFELOSS,
            ON_SPELL,
            ON_SUMMON
        }
        public enum TriggerInputPlayer
        {
            USER,
            OPPONENT,
            ANYONE
        }
        
        public enum TriggerInputCard
        {
            THIS,
            TARGET
        }

        public static string[] TriggerDesc = new string[] {
            $"When this card is played",
            $"When a card is drawn by [[player]], ",
            $"When a card is discarded by [[player]], ",
            $"When [[player]]'s monster attacks, ",
            $"When [[player]]'s card is destroyed, ",
            $"When [[player]]'s gains life, ",
            $"When [[player]]'s loses life, ",
            $"When a spell is used by [[player]], ",
            $"When a monster is summoned by [[player]], "
        };

        public static double[] TriggerPower = new double[]
        {
            1,
            .75,
            1,
            .5,
            .5,
            .5,
            .5,
            .75,
            .75,
        };

        public static string TriggerEffString;
        public static int TriggerEffInt;
    }
}
