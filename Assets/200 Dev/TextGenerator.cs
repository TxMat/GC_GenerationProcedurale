using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace _200_Dev
{
    public static class TextGenerator   
    {
        private static string[] JobsPresentationTemplate = {
            "{m_Pronoun} works happily as {traits_Pronoun} {1}.",
            "{m_Pronoun} always wanted to be {traits_Pronoun} {1} since {m_Pronoun} was a child.",
        };
        
        private static string[] StatusPresentationTemplate = {
            "{m_Pronoun} is {1}.",
        };
        
        private static readonly Dictionary<int, Dictionary<int, List<string>>> StatusMEGATemplate = new()
        {
            { (int) StatusTags.ROMANTIC, new()
                {
                    { (int) GoodnessTags.GOOD, new List<string> {
                        "this is something that {m_Pronoun} is proud of.",
                    }},
                    { (int) GoodnessTags.BAD, new List<string> {
                        "It's been like this for 5 years now.",
                    }},
                    { (int) GoodnessTags.NEUTRAL, new List<string> {
                        "",
                    }}
                }
            },
            { (int) StatusTags.PHYSICAL, new ()
                {
                    { (int) GoodnessTags.GOOD, new List<string> {
                        ""
                    }},
                    { (int) GoodnessTags.BAD, new List<string> {
                        "Due to {m_Pronoun} physical condition, {m_Pronoun} can't do some activities.",
                    }},
                    { (int) GoodnessTags.NEUTRAL, new List<string> {
                        "",
                    }}
                }
            },
            { (int) StatusTags.CHILDHOOD, new ()
            {
                { (int) GoodnessTags.GOOD, new List<string> {
                    ""
                }},
                { (int) GoodnessTags.BAD, new List<string> {
                    "This is something that {m_Pronoun} has been dealing with since {m_Pronoun} was a child.",
                }},
                { (int) GoodnessTags.NEUTRAL, new List<string> {
                    "",
                }}
            }
            },
            { (int) StatusTags.EDUCATION, new ()
            {
                { (int) GoodnessTags.GOOD, new List<string> {
                    "",
                }},
                { (int) GoodnessTags.BAD, new List<string> {
                    "This is something that {m_Pronoun} has been dealing with since {m_Pronoun} was a child.",
                }},
                { (int) GoodnessTags.NEUTRAL, new List<string> {
                    "",
                }}
            }
            },
        };

        
        private static string[] PersonalityPresentationTemplate = {
            "{m_Pronoun} can be described as {traits_Pronoun} {1} person.",
            "{m_Pronoun} is {traits_Pronoun} {1} person.",
        };
        
        private static readonly Dictionary<int, List<string>> PersonalitySuffix = new()
        {
            { (int) GoodnessTags.GOOD, new List<string> {
                "Some say that this is {m_Pronoun} best quality.",
                "This is what makes {m_Pronoun} a great person to be around.",
                "It's always a pleasure to be with {m_Pronoun}.",
                "This is what makes {m_Pronoun} unique."
            }},
            { (int) GoodnessTags.BAD, new List<string> {
                "It already happened that {m_Pronoun}'s attitude caused some problems.",
                "It's not always easy to deal with {m_Pronoun}.",
                "Sometimes, {m_Pronoun} can be a bit too much to handle.",
            }},
            { (int) GoodnessTags.NEUTRAL, new List<string> {
                "",
            }}
        };
        
        

        public static string GenerateText(TraitsMix traits, bool isMale)
        {
            var text = "";

            text += GenerateTemplatedText(traits.job, isMale, JobsPresentationTemplate);
            
            
            text += GenerateMEGATemplatedText(traits.status, isMale, StatusMEGATemplate);
            
            
            text += GenerateTemplatedText(traits.personnality, isMale, PersonalityPresentationTemplate, PersonalitySuffix);


            text += GeneratePredefinedText(traits.lifestyle, isMale);
            
            // Correct Capitalization
            text = Regex.Replace(text, @"([!?.]\s*)([a-zA-Z])", m => m.Groups[1].Value + " " + char.ToUpper(m.Groups[2].Value[0]));
            text = char.ToUpper(text[0]) + text[1..];

            return text;
        }


        private static string GeneratePredefinedText(Traits traits, bool isMale)
        {
            var text = "";
            
            text += traits.DescriptionTexts[UnityEngine.Random.Range(0, traits.DescriptionTexts.Count)];
            text = text.Replace("{m_Pronoun}", isMale ? "He" : "She");
            text = text.Replace("{m_Pronoun}", isMale ? "he" : "she");

            return text;
        }

        private static string GenerateTemplatedText(Traits traits, bool isMale, IReadOnlyList<string> primaryPresentationTemplate, Dictionary<int, List<string>> suffixes = null)
        {
            const string vowels = "aeiou";
            var text = "";
            text += primaryPresentationTemplate[UnityEngine.Random.Range(0, primaryPresentationTemplate.Count)];
            if (suffixes != null)
            {  
                var tags = traits.TextTags != -1 ? traits.TextTags : traits.TextGoodnessTags;
                text += suffixes[tags][UnityEngine.Random.Range(0, suffixes[tags].Count)];
            }
            text = text.Replace("{m_Pronoun}", isMale ? "he" : "she");
            text = text.Replace("{traits_Pronoun}", vowels.Contains(traits.Name[0].ToString().ToLower()) ? "an" : "a");
            text = text.Replace("{1}", traits.Name.ToLower());
            
            return text;
        }
        
        private static string GenerateMEGATemplatedText(Traits traits, bool isMale, Dictionary<int, Dictionary<int, List<string>>> statusMegaTemplate)
        {
            const string vowels = "aeiou";
            var text = "";
            text += statusMegaTemplate[traits.TextTags][traits.TextGoodnessTags][UnityEngine.Random.Range(0, statusMegaTemplate[traits.TextTags][traits.TextGoodnessTags].Count)];
            text = text.Replace("{m_Pronoun}", isMale ? "he" : "she");
            text = text.Replace("{traits_Pronoun}", vowels.Contains(traits.Name[0].ToString().ToLower()) ? "an" : "a");
            text = text.Replace("{1}", traits.Name.ToLower());
            return text;
        }
    }
}