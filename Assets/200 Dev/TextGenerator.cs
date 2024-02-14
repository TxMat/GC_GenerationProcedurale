using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace _200_Dev
{
    public static class TextGenerator   
    {
        private static string[] JobsPresentationTemplate = {
            "{m_Name} works happily as {traits_Pronoun} {1}.",
            "{m_Name} always wanted to be {traits_Pronoun} {1} since {m_Pronoun} was a child.",
                "For {m_Name}, being {traits_Pronoun} {1} is a dream come true.",
            "{m_Name} holds a steady job as {traits_Pronoun} {1}.",
            "{m_Name} has found stability in {hs_Pronoun} career as {traits_Pronoun} {1}.",
            "{m_Name} reluctantly works as {traits_Pronoun} {1}, struggling to find fulfillment.",
            "{m_Name} feels trapped in {hs_Pronoun} job as {traits_Pronoun} {1}, longing for something more.",
            "{m_Name} endures {hs_Pronoun} role as {traits_Pronoun} {1} with a sense of resignation and dissatisfaction."
        };

        private static readonly Dictionary<int, Dictionary<int, List<string>>> StatusMEGATemplate = new()
        {
            { (int) StatusTags.ROMANTIC, new()
                {
                    { (int) GoodnessTags.GOOD, new List<string> {
                        "{m_Pronoun} has been happily {1} for {rd_1} years and it's something that {m_Pronoun} is proud of.",
                        "{hs_Pronoun} romantic nature brings joy to {hs_Pronoun} life.",
                        "{m_Pronoun} cherishes {!hs_Pronoun} romantic side.",
                    }},
                    { (int) GoodnessTags.BAD, new List<string> {
                        "{m_Pronoun} sometimes struggles with {hs_Pronoun} romantic feelings.",
                        "{m_Pronoun} has faced challenges due to {hs_Pronoun} romantic tendencies.",
                        "{m_Pronoun} has been {1} for {rd_1} years now. it hase been pretty tough for {hm_Pronoun}.",
                        "{m_Pronoun} has been {1} for {rd_1} years now. but it doesn't seem to bother {hm_Pronoun}."
                    }},
                    { (int) GoodnessTags.NEUTRAL, new List<string> {
                        "{m_Pronoun} doesn't think much about romance.",
                        "Romance is not something important for {hm_Pronoun} currently."
                    }}
                }
            },
            { (int) StatusTags.PHYSICAL, new ()
                {
                    { (int) GoodnessTags.GOOD, new List<string> {
                        "Despite some physical challenges, {m_Pronoun} excels in many activities.",
                        "{m_Pronoun} has overcome physical obstacles with determination and resilience.",
                        "{m_Pronoun} is an inspiration to others with {hs_Pronoun} positive attitude towards physical health."
                    }},
                    { (int) GoodnessTags.BAD, new List<string> {
                        "Due to {hs_Pronoun} physical condition, {m_Pronoun} can't do some activities.",
                        "{m_Pronoun} faces daily struggles due to {hs_Pronoun} physical limitations.",
                        "Physical discomfort has been a constant challenge for {m_Pronoun}."
                    }},
                    { (int) GoodnessTags.NEUTRAL, new List<string> {
                        "Physical health has never been a major concern for {m_Pronoun}.",
                        "Despite being in average health, {m_Pronoun} leads an active lifestyle.",
                        "{m_Pronoun} maintains a balanced approach to physical well-being."
                    }}
                }
            },
            { (int) StatusTags.CHILDHOOD, new ()
                {
                    { (int) GoodnessTags.GOOD, new List<string> {
                        "{m_Pronoun}'s childhood was filled with happy memories and positive experiences.",
                        "Growing up, {m_Pronoun} enjoyed a loving and supportive environment.",
                        "{m_Pronoun} has fond memories of {hs_Pronoun} childhood, filled with laughter and joy."
                    }},
                    { (int) GoodnessTags.BAD, new List<string> {
                        "This is something that {m_Pronoun} has been dealing with since {m_Pronoun} was a child.",
                        "Childhood trauma has had a lasting impact on {hs_Pronoun} life.",
                        "{m_Pronoun} faced significant challenges during {hs_Pronoun} upbringing."
                    }},
                    { (int) GoodnessTags.NEUTRAL, new List<string> {
                        "Childhood memories for {m_Pronoun} are a mix of good and bad experiences.",
                        "{m_Pronoun} had a relatively average childhood, with ups and downs like anyone else.",
                        "Looking back, {m_Pronoun} recalls {hs_Pronoun} childhood with a sense of nostalgia and reflection."
                    }}
                }
            },
            { (int) StatusTags.EDUCATION, new ()
                {
                    { (int) GoodnessTags.GOOD, new List<string> {
                        "Education has always been a priority for {hm_Pronoun}, and {hs_Pronoun} hard work has paid off.",
                        "{m_Pronoun} values education as a cornerstone of personal and professional growth.",
                        "{m_Pronoun} is proud of {hs_Pronoun} achievements in education, which have opened many doors."
                    }},
                    { (int) GoodnessTags.BAD, new List<string> {
                        "Educational challenges have shaped {m_Pronoun}'s journey in life.",
                        "{m_Pronoun} has faced obstacles in {hs_Pronoun} educational path, but continues to persevere."
                    }},
                    { (int) GoodnessTags.NEUTRAL, new List<string> {
                        "Education has played a modest role in {m_Pronoun}'s life.",
                        "For {m_Pronoun}, education is neither a strength nor a weakness.",
                        "{m_Pronoun} approaches education with a balanced perspective."
                    }}
                }
            },

        };

        
        private static string[] PersonalityPresentationTemplate = {
            "{m_Pronoun} can be described as {traits_Pronoun} {1} {suffix}",
            "{m_Pronoun} is {traits_Pronoun} {1} {suffix}",
            "People often describe {hm_Pronoun} as {traits_Pronoun} {1} {suffix}"
        };
        
        private static readonly Dictionary<int, List<string>> PersonalitySuffix = new()
        {
            { (int) GoodnessTags.GOOD, new List<string> {
                " and some say that this is {hs_Pronoun} best quality.",
                ", this is what makes {hm_Pronoun} a great person to be around.",
                ", it's always a pleasure to be with {hm_Pronoun}.",
                " and this is what makes {hm_Pronoun} unique."
            }},
            { (int) GoodnessTags.BAD, new List<string> {
                " and it already happened that {hs_Pronoun} attitude caused some problems.",
                ", it's not always easy to deal with {hm_Pronoun}.",
                " and sometimes, {m_Pronoun} can be a bit too much to handle.",
                " and needs to work on overcoming some negative traits."
            }},
            { (int) GoodnessTags.NEUTRAL, new List<string> {
                ", {m_Pronoun} has a balanced personality.",
                ", it's hard to say if {m_Pronoun} is a good or a bad person.",
                " but when it comes to {hs_Pronoun} personality, {m_Pronoun} is a bit of a mystery."
            }}
        };
        
        

        public static string GenerateText(TraitsMix traits, bool isMale, string name)
        {
            var text = "";

            text += GenerateTemplatedText(traits.job, isMale, JobsPresentationTemplate, name);
            
            
            text += GenerateMEGATemplatedText(traits.status, isMale, StatusMEGATemplate , name);
            
            
            text += GenerateTemplatedText(traits.personnality, isMale, PersonalityPresentationTemplate, name, PersonalitySuffix);


            text += GeneratePredefinedText(traits.lifestyle, isMale, name);
            
            // Correct Capitalization
            text = Regex.Replace(text, @"([!?.]\s*)([a-zA-Z])", m => m.Groups[1].Value + " " + char.ToUpper(m.Groups[2].Value[0]));
            text = char.ToUpper(text[0]) + text[1..];
            
            // correct spaces
            text = Regex.Replace(text, @"\s+(?=[.,?!])", "");   
            
            // correct multiple spaces
            text = Regex.Replace(text, @"\s+", " ");

            return text;
        }


        private static string GeneratePredefinedText(Traits traits, bool isMale, string name)
        {
            var text = "";
            
            text += traits.DescriptionTexts[UnityEngine.Random.Range(0, traits.DescriptionTexts.Count)];

            return SanitizeText(text, isMale, traits, name);
        }

        private static string GenerateTemplatedText(Traits traits, bool isMale, IReadOnlyList<string> primaryPresentationTemplate, string name, Dictionary<int, List<string>> suffixes = null)
        {
            var text = "";
            text += primaryPresentationTemplate[UnityEngine.Random.Range(0, primaryPresentationTemplate.Count)];
            if (suffixes != null)
            {  
                var tags = traits.TextTags != -1 ? traits.TextTags : traits.TextGoodnessTags;
                text += suffixes[tags][UnityEngine.Random.Range(0, suffixes[tags].Count)];
            }

            return SanitizeText(text, isMale, traits, name);
        }
        
        private static string GenerateMEGATemplatedText(Traits traits, bool isMale, Dictionary<int, Dictionary<int, List<string>>> statusMegaTemplate, string name)
        {
            var text = "";
            
            text += statusMegaTemplate[traits.TextTags][traits.TextGoodnessTags][UnityEngine.Random.Range(0, statusMegaTemplate[traits.TextTags][traits.TextGoodnessTags].Count)];

            return SanitizeText(text, isMale, traits, name);
        }
        
        private static string SanitizeText(string text, bool isMale, Traits traits, string name)
        {
            const string vowels = "aeiou";
            text = text.Replace("{m_Pronoun}", isMale ? "he" : "she");
            text = text.Replace("{hs_Pronoun}", isMale ? "his" : "her");
            text = text.Replace("{!hs_Pronoun}", isMale ? "her" : "his");
            text = text.Replace("{hm_Pronoun}", isMale ? "him" : "her");
            text = text.Replace("{m_Name}", name);
            text = text.Replace("{traits_Pronoun}", vowels.Contains(traits.Name[0].ToString().ToLower()) ? "an" : "a");
            text = text.Replace("{1}", traits.Name.ToLower());
            text = text.Replace("{suffix}", traits.NeedsSuffix ? UnityEngine.Random.Range(0, 2) == 0 ? "person" : "individual" : "");
            text = text.Replace("{rd_1}", UnityEngine.Random.Range(1, 20).ToString());
            return text;
        }
    }
}