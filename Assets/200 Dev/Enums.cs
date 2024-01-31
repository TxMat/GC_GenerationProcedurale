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
}

public enum Personalities
{
    ADVENTUROUS = 0,
    SHY = 1,
    CHARISMATIC = 2,
    INTROVERT = 3,
    OPTIMISTIC = 4,
    PESSIMISTIC = 5,
    ALTRUISTIC = 6,
    EGOTIST = 7,
    ANGRY = 8,
    CALM = 9,
    REALIST = 10,
    DREAMER = 11,
}

public enum Status
{
    ORPHAN = 0,
    EDUCATED = 1,
    ANALPHABET = 2,
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
    DISABLED = 5,
    EXCENTRIC = 6,
}

public enum Category
{
    JOB = 0,
    STATUS = 1,
    PERSONNALITY = 2,
}

[Flags]
public enum TraitTags
{
    RICH = 1 << 0,
    POOR = 1 << 1,
}