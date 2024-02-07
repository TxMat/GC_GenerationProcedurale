using System;
using System.Collections.Generic;
using static UnityEngine.Rendering.DebugUI;


public enum Jobs
{
    MEDIC,
    PAINTER,
    ENGINEER,
    STUDENT,
    FARMER,
    LAWYER,
    MUSICIAN,
    //SCIENTIST,
    SOLDIER,
    ARTISAN,
    //VETERAN,
    //TEACHER,
    PLUMBER,
    CHEMIST,
    //COMPUTER_SCIENTIST,
    LIBRARIAN,
    ARCHITECT,
    //UNEMPLOYED,
}

public enum Hobby
{
    BASKETBALL_PLAYER,
    FOOTBALL_PLAYER,
    BASEBALL_PLAYER,
    TENNIS_PLAYER,
    SWIMMER,
    GOLFER,
    BOXER,
    CYCLIST,
    RUNNER,
    GYMNAST,
    DANCER,
    CHESS_PLAYER,
    GAMER,
}

public enum Personalities
{
    ADVENTUROUS,
    SHY,
    CHARISMATIC,
    OPTIMISTIC,
    PESSIMISTIC,
    ALTRUISTIC,
    EGOTIST,
    ANGRY,
    //CALM,
    //REALIST,
    DREAMER,
    LAZY,
    //HARD_WORKER,
    //HUMBLE,
    INTELLIGENT,
    STUPID,
    COWARD,
    BRAVE,
    //SENSITIVE,
    //INSENSITIVE,
    SADISTIC,
    //HONEST,
    LIAR,
    //NAIVE,
}

public enum Status
{
    ORPHAN,
    EDUCATED,
    ANALPHABET,
    INJURED,
    DISABLED,
    SINGLE,
    MARRIED,
    DIVORCED,
    WIDOW,
}
public enum LifeStyle
{
    VOYAGER,
    CITY_HABITANT,
    COUNTRY_HABITANT,
    EXCENTRIC,
    GAMER,
    ART_LOVER,
    CRIMINAL,
}

public enum Category
{
    JOB,
    STATUS,
    PERSONNALITY,
    LIFESTYLE,
}

[Flags]
public enum TraitTags
{
    NONE = 0,
    RICH = 1 << 0,
    POOR = 1 << 1,
    EDUCATED = 1 << 2,
    HAPPY = 1 << 3,
    DANGEROUS = 1 << 4,
    ARTISTIC = 1 << 5,
    STRANGE = 1 << 6,
    UNEDUCATED = 1 << 7,
    GOOD = 1 << 8,
    BAD = 1 << 9,
    UNHAPPY = 1 << 10,
    FREE = 1 << 11,
}


public enum GoodnessTags
{
    GOOD,
    BAD,
    NEUTRAL,
}

public enum StatusTags
{
    ROMANTIC,
    PHYSICAL,
    CHILDHOOD,
    EDUCATION,
}

public static class EnumExtensions
{
    public static string Name(this Jobs job)
    {
        return job switch
        {
            Jobs.MEDIC => "Medic",
            Jobs.PAINTER => "Painter",
            Jobs.ENGINEER => "Engineer",
            Jobs.STUDENT => "Student",
            Jobs.FARMER => "Farmer",
            Jobs.LAWYER => "Lawyer",
            Jobs.MUSICIAN => "Musician",
            Jobs.SOLDIER => "Soldier",
            Jobs.ARTISAN => "Artisan",
            Jobs.PLUMBER => "Plumber",
            Jobs.CHEMIST => "Chemist",
            Jobs.LIBRARIAN => "Librarian",
            Jobs.ARCHITECT => "Architect",
            _ => "Unknown"
        };
    }
    
    public static TraitTags Tags(this Jobs job)
    {
        return job switch
        {
            //Jobs.MEDIC => TraitTags.SMART | TraitTags.RICH,
            Jobs.PAINTER => TraitTags.ARTISTIC | TraitTags.POOR,
            //Jobs.ENGINEER => TraitTags.SMART | TraitTags.RICH,
            //Jobs.STUDENT => TraitTags.SMART,
            Jobs.FARMER => TraitTags.POOR | TraitTags.UNEDUCATED,
            //Jobs.LAWYER => TraitTags.SMART | TraitTags.RICH,
            Jobs.MUSICIAN => TraitTags.ARTISTIC | TraitTags.POOR,
            Jobs.SOLDIER => TraitTags.DANGEROUS,
            Jobs.ARTISAN => TraitTags.ARTISTIC | TraitTags.POOR,
            Jobs.PLUMBER => TraitTags.POOR,
            //Jobs.CHEMIST => TraitTags.SMART | TraitTags.RICH,
            //Jobs.LIBRARIAN => TraitTags.SMART | TraitTags.RICH,
            //Jobs.ARCHITECT => TraitTags.SMART | TraitTags.RICH,
            _ => TraitTags.RICH
        };
    }

    public static string Name(this Hobby hobby)
    {
        return hobby switch
        {
            Hobby.BASKETBALL_PLAYER => "Basketball player",
            Hobby.FOOTBALL_PLAYER => "Football player",
            Hobby.BASEBALL_PLAYER => "Baseball player",
            Hobby.TENNIS_PLAYER => "Tennis player",
            Hobby.SWIMMER => "Swimmer",
            Hobby.GOLFER => "Golfer",
            Hobby.BOXER => "Boxer",
            Hobby.CYCLIST => "Cyclist",
            Hobby.RUNNER => "Runner",
            Hobby.GYMNAST => "Gymnast",
            Hobby.DANCER => "Dancer",
            Hobby.CHESS_PLAYER => "Chess player",
            Hobby.GAMER => "Gamer",
            _ => "Unknown"
        };
    }
    
    public static string Name(this Personalities personality)
    {
        return personality switch
        {
            Personalities.ADVENTUROUS => "Adventurous",
            Personalities.SHY => "Shy",
            Personalities.CHARISMATIC => "Charismatic",
            Personalities.OPTIMISTIC => "Optimistic",
            Personalities.PESSIMISTIC => "Pessimistic",
            Personalities.ALTRUISTIC => "Altruistic",
            Personalities.EGOTIST => "Egotist",
            Personalities.ANGRY => "Angry",
            Personalities.DREAMER => "Dreamer",
            Personalities.LAZY => "Lazy",
            Personalities.INTELLIGENT => "Intelligent",
            Personalities.STUPID => "Stupid",
            Personalities.COWARD => "Coward",
            Personalities.BRAVE => "Brave",
            Personalities.SADISTIC => "Sadistic",
            Personalities.LIAR => "Liar",
            _ => "Unknown"
        };
    }
    
    public static GoodnessTags GoodnessTag(this Personalities personality)
    {
        return personality switch
        {
            Personalities.ADVENTUROUS => GoodnessTags.GOOD,
            Personalities.SHY => GoodnessTags.NEUTRAL,
            Personalities.CHARISMATIC => GoodnessTags.GOOD,
            Personalities.OPTIMISTIC => GoodnessTags.GOOD,
            Personalities.PESSIMISTIC => GoodnessTags.BAD,
            Personalities.ALTRUISTIC => GoodnessTags.GOOD,
            Personalities.EGOTIST => GoodnessTags.BAD,
            Personalities.ANGRY => GoodnessTags.BAD,
            Personalities.DREAMER => GoodnessTags.NEUTRAL,
            Personalities.LAZY => GoodnessTags.BAD,
            Personalities.INTELLIGENT => GoodnessTags.GOOD,
            Personalities.STUPID => GoodnessTags.BAD,
            Personalities.COWARD => GoodnessTags.BAD,
            Personalities.BRAVE => GoodnessTags.GOOD,
            Personalities.SADISTIC => GoodnessTags.BAD,
            Personalities.LIAR => GoodnessTags.BAD,
            _ => GoodnessTags.NEUTRAL
        };
    }

    public static string Name(this Status status)
    {
        return status switch
        {
            Status.ORPHAN => "Orphan",
            Status.EDUCATED => "Educated",
            Status.ANALPHABET => "An-alphabet",
            Status.INJURED => "Injured",
            Status.DISABLED => "Disabled",
            Status.SINGLE => "Single",
            Status.MARRIED => "Married",
            Status.DIVORCED => "Divorced",
            Status.WIDOW => "Widow",
            _ => "Unknown"
        };
    }
    
    public static StatusTags TextTags(this Status status)
    {
        return status switch
        {
            Status.ORPHAN => StatusTags.CHILDHOOD,
            Status.EDUCATED => StatusTags.EDUCATION,
            Status.ANALPHABET => StatusTags.EDUCATION,
            Status.INJURED => StatusTags.PHYSICAL,
            Status.DISABLED => StatusTags.PHYSICAL,
            Status.SINGLE => StatusTags.ROMANTIC,
            Status.MARRIED => StatusTags.ROMANTIC,
            Status.DIVORCED => StatusTags.ROMANTIC,
            Status.WIDOW => StatusTags.ROMANTIC,
            _ => StatusTags.CHILDHOOD
        };
    }

    public static GoodnessTags GoodnessTag(this Status status)
    {
        return status switch
        {
            Status.ORPHAN => GoodnessTags.BAD,
            Status.EDUCATED => GoodnessTags.GOOD,
            Status.ANALPHABET => GoodnessTags.BAD,
            Status.INJURED => GoodnessTags.BAD,
            Status.DISABLED => GoodnessTags.BAD,
            Status.SINGLE => GoodnessTags.GOOD,
            Status.MARRIED => GoodnessTags.GOOD,
            Status.DIVORCED => GoodnessTags.BAD,
            Status.WIDOW => GoodnessTags.BAD,
            _ => GoodnessTags.NEUTRAL
        };
    }
    
    public static string Name(this LifeStyle lifeStyle)
    {
        return lifeStyle switch
        {
            LifeStyle.VOYAGER => "Voyager",
            LifeStyle.CITY_HABITANT => "City inhabitant",
            LifeStyle.COUNTRY_HABITANT => "Country inhabitant",
            LifeStyle.EXCENTRIC => "Eccentric",
            LifeStyle.GAMER => "Gamer",
            LifeStyle.ART_LOVER => "Art lover",
            LifeStyle.CRIMINAL => "Criminal",
            _ => "Unknown"
        };
    }


    public static bool Any(this TraitTags thisTags, TraitTags tags)
    {
        return (thisTags & tags) != 0;
    }

    public static int HammingWeight(this TraitTags tags)
    {
        int n = (int)tags;
        int count = 0;
        while (n > 0)
        {           
            if ((n & 1) == 1)
                count++;
            n >>= 1;
        }
        return count;
    }
}