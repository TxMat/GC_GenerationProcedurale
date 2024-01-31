using System;


public enum Jobs
{
    MEDIC = 0,
    PAINTER = 1,
    ENGINEER = 2,
    STUDENT = 3,
    FARMER = 4,
    LAWYER = 5,
    MUSICIAN = 6,
    SCIENTIST = 7,
    SOLDIER = 8,
    ARTISAN = 9,
    VETERAN = 10,
    TEACHER = 11,
    PLUMBER = 16,
    CHEMIST = 17,
    COMPUTER_SCIENTIST = 19,
    LIBRARIAN = 20,
    ARCHITECT = 21,
    UNEMPLOYED = 22,
}

public enum Hobby
{
    BASKETBALL_PLAYER = 14,
    FOOTBALL_PLAYER = 15,
    BASEBALL_PLAYER = 16,
    TENNIS_PLAYER = 17,
    SWIMMER = 18,
    GOLFER = 19,
    BOXER = 20,
    CYCLIST = 21,
    RUNNER = 22,
    GYMNAST = 23,
    DANCER = 24,
    CHESS_PLAYER = 25,
    GAMER = 26,
}

public enum Personalities
{
    ADVENTUROUS = 0,
    SHY = 1,
    CHARISMATIC = 2,
    OPTIMISTIC = 4,
    PESSIMISTIC = 5,
    ALTRUISTIC = 6,
    EGOTIST = 7,
    ANGRY = 8,
    CALM = 9,
    REALIST = 10,
    DREAMER = 11,
    LAZY = 14,
    HARD_WORKER = 15,
    HUMBLE = 17,
    INTELLIGENT = 18,
    STUPID = 19,
    COWARD = 20,
    BRAVE = 21,
    SENSITIVE = 22,
    INSENSITIVE = 23,
    SADISTIC = 26,
    HONEST = 28,
    LIAR = 29,
    NAIVE = 33,
}

public enum Status
{
    ORPHAN = 0,
    EDUCATED = 1,
    ANALPHABET = 2,
    INJURED = 10,
    DISABLED = 11,
}

public enum RomanticStatus
{
    SINGLE = 3,
    MARRIED = 4,
    DIVORCED = 5,
    WIDOW = 6,
}

public enum LifeStyle
{
    VOYAGER = 0,
    CITY_HABITANT = 1,
    COUNTRY_HABITANT = 2,
    EXCENTRIC = 6,
    DRUG_ADDICT = 11,
    GAMBLER = 12,
    SMOKER = 13,
    GAMER = 15,
    MUSIC_LOVER = 16,
    ART_LOVER = 17,
    NATURE_LOVER = 18,
    ANIMAL_LOVER = 19,
    VEGETARIAN = 20,
    VEGAN = 21,
    RELIGIOUS = 22,
    ATHEIST = 23,
    POLITICIAN = 24,
    ACTIVIST = 25,
    CRIMINAL = 26,
}

public enum Category
{
    JOB = 0,
    STATUS = 1,
    PERSONNALITY = 2,
    LIFESTYLE = 3,
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
        switch (job)
        {
            case Jobs.MEDIC: return "Medic";
            case Jobs.PAINTER: return "Painter";
            case Jobs.ENGINEER: return "Engineer";
            case Jobs.STUDENT: return "Student";
            case Jobs.FARMER: return "Farmer";
            case Jobs.LAWYER: return "Lawyer";
            case Jobs.MUSICIAN: return "Musician";
            case Jobs.SCIENTIST: return "Scientist";
            case Jobs.SOLDIER: return "Soldier";
            case Jobs.ARTISAN: return "Artisan";
            case Jobs.VETERAN: return "Veteran";
            case Jobs.TEACHER: return "Teacher";
            case Jobs.PLUMBER: return "Plumber";
            case Jobs.CHEMIST: return "Chemist";
            case Jobs.COMPUTER_SCIENTIST: return "Computer scientist";
            case Jobs.LIBRARIAN: return "Librarian";
            case Jobs.ARCHITECT: return "Architect";
            case Jobs.UNEMPLOYED: return "Unemployed";
        }
        return null;
    }
    
    public static string Name(this Hobby hobby)
    {
        switch (hobby)
        {
            case Hobby.BASKETBALL_PLAYER: return "Basketball player";
            case Hobby.FOOTBALL_PLAYER: return "Football player";
            case Hobby.BASEBALL_PLAYER: return "Baseball player";
            case Hobby.TENNIS_PLAYER: return "Tennis player";
            case Hobby.SWIMMER: return "Swimmer";
            case Hobby.GOLFER: return "Golfer";
            case Hobby.BOXER: return "Boxer";
            case Hobby.CYCLIST: return "Cyclist";
            case Hobby.RUNNER: return "Runner";
            case Hobby.GYMNAST: return "Gymnast";
            case Hobby.DANCER: return "Dancer";
            case Hobby.CHESS_PLAYER: return "Chess player";
            case Hobby.GAMER: return "Gamer";
        }
        return null;
    }
    
    public static string Name(this Personalities personality)
    {
        switch (personality)
        {
            case Personalities.ADVENTUROUS: return "Adventurous";
            case Personalities.SHY: return "Shy";
            case Personalities.CHARISMATIC: return "Charismatic";
            case Personalities.OPTIMISTIC: return "Optimistic";
            case Personalities.PESSIMISTIC: return "Pessimistic";
            case Personalities.ALTRUISTIC: return "Altruistic";
            case Personalities.EGOTIST: return "Egotist";
            case Personalities.ANGRY: return "Angry";
            case Personalities.CALM: return "Calm";
            case Personalities.REALIST: return "Realist";
            case Personalities.DREAMER: return "Dreamer";
            case Personalities.LAZY: return "Lazy";
            case Personalities.HARD_WORKER: return "Hard worker";
            case Personalities.HUMBLE: return "Humble";
            case Personalities.INTELLIGENT: return "Intelligent";
            case Personalities.STUPID: return "Stupid";
            case Personalities.COWARD: return "Coward";
            case Personalities.BRAVE: return "Brave";
            case Personalities.SENSITIVE: return "Sensitive";
            case Personalities.INSENSITIVE: return "Insensitive";
            case Personalities.SADISTIC: return "Sadistic";
            case Personalities.HONEST: return "Honest";
            case Personalities.LIAR: return "Liar";
            case Personalities.NAIVE: return "Naive";
        }
        return null;
    }

    public static string Name(this Status status)
    {
        switch (status)
        {
            case Status.ORPHAN: return "Orphan";
            case Status.EDUCATED: return "Educated";
            case Status.ANALPHABET: return "Analphabet";
            case Status.INJURED: return "Injured";
            case Status.DISABLED: return "Disabled";
        }
        return null;
    }
    
    public static string Name(this RomanticStatus romanticStatus)
    {
        switch (romanticStatus)
        {
            case RomanticStatus.SINGLE: return "Single";
            case RomanticStatus.MARRIED: return "Married";
            case RomanticStatus.DIVORCED: return "Divorced";
            case RomanticStatus.WIDOW: return "Widow";
        }
        return null;
    }
    
    public static string Name(this LifeStyle lifeStyle)
    {
        switch (lifeStyle)
        {
            case LifeStyle.VOYAGER: return "Voyager";
            case LifeStyle.CITY_HABITANT: return "City habitant";
            case LifeStyle.COUNTRY_HABITANT: return "Country habitant";
            case LifeStyle.EXCENTRIC: return "Excentric";
            case LifeStyle.DRUG_ADDICT: return "Drug addict";
            case LifeStyle.GAMBLER: return "Gambler";
            case LifeStyle.SMOKER: return "Smoker";
            case LifeStyle.GAMER: return "Gamer";
            case LifeStyle.MUSIC_LOVER: return "Music lover";
            case LifeStyle.ART_LOVER: return "Art lover";
            case LifeStyle.NATURE_LOVER: return "Nature lover";
            case LifeStyle.ANIMAL_LOVER: return "Animal lover";
            case LifeStyle.VEGETARIAN: return "Vegetarian";
            case LifeStyle.VEGAN: return "Vegan";
            case LifeStyle.RELIGIOUS: return "Religious";
            case LifeStyle.ATHEIST: return "Atheist";
            case LifeStyle.POLITICIAN: return "Politician";
            case LifeStyle.ACTIVIST: return "Activist";
            case LifeStyle.CRIMINAL: return "Criminal";
        }
        return null;
    }
}