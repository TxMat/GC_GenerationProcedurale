using System;
using System.Collections.Generic;

namespace _200_Dev
{
    public static class TextGenerator   
    {
        private static string[] JobsPresentationTemplaete = {
            "{M_Pronoun} works happily as {job_Pronoun} {1}.",
            "{M_Pronoun} always wanted to be {job_Pronoun} {1} since {m_Pronoun} was a child.",
        };
        
        private static string[] StatusPresentationTemplaete = {
            "{M_Pronoun} is {1}.",
        };
        
        private static string[] PersonalityPresentationTemplaete = {
            "{M_Pronoun} can be described as {1} person. Some say that this is {m_Pronoun} best quality.",
            "{M_Pronoun} is a {1} person. This is what makes {m_Pronoun} unique.",
        };

        public static string GenerateText(TraitsMix traits, bool isMale)
        {
            var text = "";

            text += GenerateTemplatedText(traits.job.name, isMale, JobsPresentationTemplaete);
            
            
            text += GenerateTemplatedText(traits.status.name, isMale, StatusPresentationTemplaete);
            
            
            text += GenerateTemplatedText(traits.personnality.name, isMale, PersonalityPresentationTemplaete);


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

        private static string GenerateTemplatedText(string jobName, bool isMale, IReadOnlyList<string> jobsPresentationTemplate)
        {
            const string vowels = "aeiou";
            var text = "";
            text += jobsPresentationTemplate[UnityEngine.Random.Range(0, jobsPresentationTemplate.Count)];
            text = text.Replace("{M_Pronoun}", isMale ? "He" : "She");
            text = text.Replace("{m_Pronoun}", isMale ? "he" : "she");
            text = text.Replace("{job_Pronoun}", vowels.Contains(jobName[0].ToString().ToLower()) ? "an" : "a");
            text = text.Replace("{1}", jobName);
            return text;
        }
    }
}