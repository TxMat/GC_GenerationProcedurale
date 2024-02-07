﻿using System;
using System.Collections.Generic;

namespace _200_Dev
{
    public static class TextGenerator   
    {
        private static string[] JobsPresentationTemplate = {
            "{M_Pronoun} works happily as {traits_Pronoun} {1}.",
            "{M_Pronoun} always wanted to be {traits_Pronoun} {1} since {m_Pronoun} was a child.",
        };
        
        private static string[] StatusPresentationTemplate = {
            "{M_Pronoun} is {1}.",
        };
        
        private static string[] PersonalityPresentationTemplate = {
            "{M_Pronoun} can be described as {traits_Pronoun} {1} person. Some say that this is {m_Pronoun} best quality.",
            "{M_Pronoun} is a {1} person. This is what makes {m_Pronoun} unique.",
        };

        public static string GenerateText(TraitsMix traits, bool isMale)
        {
            var text = "";

            text += GenerateTemplatedText(traits.job.name, isMale, JobsPresentationTemplate);
            
            
            text += GenerateTemplatedText(traits.status.name, isMale, StatusPresentationTemplate);
            
            
            text += GenerateTemplatedText(traits.personnality.name, isMale, PersonalityPresentationTemplate);


            text += GeneratePredefinedText(traits.lifestyle, isMale);

            return text;
        }

        private static string GeneratePredefinedText(Traits traits, bool isMale)
        {
            var text = "";
            
            text += traits.DescriptionTexts[UnityEngine.Random.Range(0, traits.DescriptionTexts.Count)];
            text = text.Replace("{M_Pronoun}", isMale ? "He" : "She");
            text = text.Replace("{m_Pronoun}", isMale ? "he" : "she");

            return text;
        }

        private static string GenerateTemplatedText(string traitsName, bool isMale, IReadOnlyList<string> jobsPresentationTemplate)
        {
            const string vowels = "aeiou";
            var text = "";
            text += jobsPresentationTemplate[UnityEngine.Random.Range(0, jobsPresentationTemplate.Count)];
            text = text.Replace("{M_Pronoun}", isMale ? "He" : "She");
            text = text.Replace("{m_Pronoun}", isMale ? "he" : "she");
            text = text.Replace("{traits_Pronoun}", vowels.Contains(traitsName[0].ToString().ToLower()) ? "an" : "a");
            text = text.Replace("{1}", traitsName);
            return text;
        }
    }
}