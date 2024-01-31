using System;


public enum Jobs
{
    MEDIC,
    PAINTER,
    ENGINEER,
    STUDENT,
    FARMER,
    LAWYER,
    MUSICIAN,
    SCIENTIST,
    SOLDIER,
    ARTISAN,
    VETERAN,
    TEACHER,
    PLUMBER,
    CHEMIST,
    COMPUTER_SCIENTIST,
    LIBRARIAN,
    ARCHITECT,
    UNEMPLOYED,
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
    CALM,
    REALIST,
    DREAMER,
    LAZY,
    HARD_WORKER,
    HUMBLE,
    INTELLIGENT,
    STUPID,
    COWARD,
    BRAVE,
    SENSITIVE,
    INSENSITIVE,
    SADISTIC,
    HONEST,
    LIAR,
    NAIVE,
}

public enum Status
{
    ORPHAN,
    EDUCATED,
    ANALPHABET,
    INJURED,
    DISABLED,
}

public enum RomanticStatus
{
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
    DRUG_ADDICT,
    GAMBLER,
    SMOKER,
    GAMER,
    MUSIC_LOVER,
    ART_LOVER,
    NATURE_LOVER,
    ANIMAL_LOVER,
    VEGETARIAN,
    VEGAN,
    RELIGIOUS,
    ATHEIST,
    POLITICIAN,
    ACTIVIST,
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
    RICH = 1 << 0,
    POOR = 1 << 1,
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
            Jobs.SCIENTIST => "Scientist",
            Jobs.SOLDIER => "Soldier",
            Jobs.ARTISAN => "Artisan",
            Jobs.VETERAN => "Veteran",
            Jobs.TEACHER => "Teacher",
            Jobs.PLUMBER => "Plumber",
            Jobs.CHEMIST => "Chemist",
            Jobs.COMPUTER_SCIENTIST => "Computer scientist",
            Jobs.LIBRARIAN => "Librarian",
            Jobs.ARCHITECT => "Architect",
            Jobs.UNEMPLOYED => "Unemployed",
            _ => "Unknown"
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
            Personalities.CALM => "Calm",
            Personalities.REALIST => "Realist",
            Personalities.DREAMER => "Dreamer",
            Personalities.LAZY => "Lazy",
            Personalities.HARD_WORKER => "Hard worker",
            Personalities.HUMBLE => "Humble",
            Personalities.INTELLIGENT => "Intelligent",
            Personalities.STUPID => "Stupid",
            Personalities.COWARD => "Coward",
            Personalities.BRAVE => "Brave",
            Personalities.SENSITIVE => "Sensitive",
            Personalities.INSENSITIVE => "Insensitive",
            Personalities.SADISTIC => "Sadistic",
            Personalities.HONEST => "Honest",
            Personalities.LIAR => "Liar",
            Personalities.NAIVE => "Naive",
            _ => "Unknown"
        };
    }

    public static string Name(this Status status)
    {
        return status switch
        {
            Status.ORPHAN => "Orphan",
            Status.EDUCATED => "Educated",
            Status.ANALPHABET => "Analphabet",
            Status.INJURED => "Injured",
            Status.DISABLED => "Disabled",
            _ => "Unknown"
        };
    }
    
    public static string Name(this RomanticStatus romanticStatus)
    {
        return romanticStatus switch
        {
            RomanticStatus.SINGLE => "Single",
            RomanticStatus.MARRIED => "Married",
            RomanticStatus.DIVORCED => "Divorced",
            RomanticStatus.WIDOW => "Widow",
            _ => "Unknown"
        };
    }
    
    public static string Name(this LifeStyle lifeStyle)
    {
        return lifeStyle switch
        {
            LifeStyle.VOYAGER => "Voyager",
            LifeStyle.CITY_HABITANT => "City habitant",
            LifeStyle.COUNTRY_HABITANT => "Country habitant",
            LifeStyle.EXCENTRIC => "Excentric",
            LifeStyle.DRUG_ADDICT => "Drug addict",
            LifeStyle.GAMBLER => "Gambler",
            LifeStyle.SMOKER => "Smoker",
            LifeStyle.GAMER => "Gamer",
            LifeStyle.MUSIC_LOVER => "Music lover",
            LifeStyle.ART_LOVER => "Art lover",
            LifeStyle.NATURE_LOVER => "Nature lover",
            LifeStyle.ANIMAL_LOVER => "Animal lover",
            LifeStyle.VEGETARIAN => "Vegetarian",
            LifeStyle.VEGAN => "Vegan",
            LifeStyle.RELIGIOUS => "Religious",
            LifeStyle.ATHEIST => "Atheist",
            LifeStyle.POLITICIAN => "Politician",
            LifeStyle.ACTIVIST => "Activist",
            LifeStyle.CRIMINAL => "Criminal",
            _ => "Unknown"
        };
    }
}