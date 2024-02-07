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
                        "It's been like this for {rd_1} years now.",
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
                        "Due to {h_Pronoun} physical condition, {m_Pronoun} can't do some activities.",
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
                "Some say that this is {hs_Pronoun} best quality.",
                "This is what makes {hm_Pronoun} a great person to be around.",
                "It's always a pleasure to be with {hm_Pronoun}.",
                "This is what makes {hm_Pronoun} unique."
            }},
            { (int) GoodnessTags.BAD, new List<string> {
                "It already happened that {hs_Pronoun} attitude caused some problems.",
                "It's not always easy to deal with {hm_Pronoun}.",
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
            
            
            text += GenerateMEGATemplatedText(traits.status, isMale, StatusPresentationTemplate, StatusMEGATemplate);
            
            
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
            var text = "";
            text += primaryPresentationTemplate[UnityEngine.Random.Range(0, primaryPresentationTemplate.Count)];
            if (suffixes != null)
            {  
                var tags = traits.TextTags != -1 ? traits.TextTags : traits.TextGoodnessTags;
                text += suffixes[tags][UnityEngine.Random.Range(0, suffixes[tags].Count)];
            }

            return SanitizeText(text, isMale, traits);
        }
        
        private static string GenerateMEGATemplatedText(Traits traits, bool isMale, IReadOnlyList<string> primaryPresentationTemplate, Dictionary<int, Dictionary<int, List<string>>> statusMegaTemplate)
        {
            var text = "";
            text += primaryPresentationTemplate[UnityEngine.Random.Range(0, primaryPresentationTemplate.Count)];
            text += statusMegaTemplate[traits.TextTags][traits.TextGoodnessTags][UnityEngine.Random.Range(0, statusMegaTemplate[traits.TextTags][traits.TextGoodnessTags].Count)];

            return SanitizeText(text, isMale, traits);
        }
        
        private static string SanitizeText(string text, bool isMale, Traits traits)
        {
            const string vowels = "aeiou";
            text = text.Replace("{m_Pronoun}", isMale ? "he" : "she");
            text = text.Replace("{hs_Pronoun}", isMale ? "his" : "her");
            text = text.Replace("{hm_Pronoun}", isMale ? "him" : "her");
            text = text.Replace("{traits_Pronoun}", vowels.Contains(traits.Name[0].ToString().ToLower()) ? "an" : "a");
            text = text.Replace("{1}", traits.Name.ToLower());
            text = text.Replace("{rd_1}", UnityEngine.Random.Range(1, 20).ToString());
            return text;
        }
    }
}