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
        
        private static readonly Dictionary<int, List<string>> StatusSuffix = new()
        {
            { (int) StatusTags.ROMANTIC, new List<string> {
                "{m_Pronoun} is {1} for more than 5 years now.",
            }},
            { (int) StatusTags.PHYSICAL, new List<string> {
                "Due to {m_Pronoun} physical condition, {m_Pronoun} can't do some activities.",
            }},
            { (int) StatusTags.CHILDHOOD, new List<string> {
                "This is something that {m_Pronoun} has been dealing with since {m_Pronoun} was a child.",
            }},
            { (int) StatusTags.EDUCATION, new List<string> {
                "This is something that {m_Pronoun} has been dealing with since {m_Pronoun} was a child.",
            }},
        };

        
        private static string[] PersonalityPresentationTemplate = {
            "{m_Pronoun} can be described as {traits_Pronoun} {1} person.",
            "{m_Pronoun} is {traits_Pronoun} {1} person.",
        };
        
        private static readonly Dictionary<int, List<string>> PersonalitySuffix = new()
        {
            { (int) PersonalityTags.GOOD, new List<string> {
                "Some say that this is {m_Pronoun} best quality.",
                "This is what makes {m_Pronoun} a great person to be around.",
                "It's always a pleasure to be with {m_Pronoun}.",
                "This is what makes {m_Pronoun} unique."
            }},
            { (int) PersonalityTags.BAD, new List<string> {
                "It already happened that {m_Pronoun} attitude caused some problems.",
                "It's not always easy to deal with {m_Pronoun}.",
                "Sometimes, {m_Pronoun} can be a bit too much to handle.",
            }}
        };
        
        

        public static string GenerateText(TraitsMix traits, bool isMale)
        {
            var text = "";

            text += GenerateTemplatedText(traits.job, isMale, JobsPresentationTemplate);
            
            
            text += GenerateTemplatedText(traits.status, isMale, StatusPresentationTemplate, StatusSuffix);
            
            
            text += GenerateTemplatedText(traits.personnality, isMale, PersonalityPresentationTemplate, PersonalitySuffix);


            text += GeneratePredefinedText(traits.lifestyle, isMale);
            
            // Correct Capitalization
            Regex.Replace(text, @"(?<=\. )(.*?)(?=\b)", m => char.ToUpper(m.Value[0]) + m.Value.Substring(1));
            text = char.ToUpper(text[0]) + text.Substring(1);

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
                text += suffixes[traits.TextTags][UnityEngine.Random.Range(0, suffixes[traits.TextTags].Count)];
            }
            text = text.Replace("{m_Pronoun}", isMale ? "he" : "she");
            text = text.Replace("{traits_Pronoun}", vowels.Contains(traits.name[0].ToString().ToLower()) ? "an" : "a");
            text = text.Replace("{1}", traits.name.ToLower());
            
            return text;
        }
    }
}