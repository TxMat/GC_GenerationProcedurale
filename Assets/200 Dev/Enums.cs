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
    BASKETBALL_PLAYER = 14,
    FOOTBALL_PLAYER = 15,
    PLUMBER = 16,
    CHEMIST = 17,
    MAGE = 18,
    COMPUTER_SCIENTIST = 19,
    LIBRARIAN = 20,
    ARCHITECT = 21,
    UNEMPLOYED = 22,
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