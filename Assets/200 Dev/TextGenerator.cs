using System;

namespace _200_Dev
{
    public static class TextGenerator
    {
        private static String[] JobsPresentationTemplaete = {
            "{M_Pronoun} is a {1}.",
            "{M_Pronoun} is a {1} by profession.",
            "{M_Pronoun} always wanted to be a {1} since {m_Pronoun} was a child.",
            "{M_Pronoun} is a {1} by trade.",
            "{M_Pronoun} is a {1} by vocation.",
        };
        
        private static String[] StatusPresentationTemplaete = {
            "{M_Pronoun} is {1}.",
            "{M_Pronoun} is {1} by nature.",
            "{M_Pronoun} is {1} by temperament.",
            "{M_Pronoun} is {1} by character.",
            "{M_Pronoun} is {1} by personality.",
        };
        
        private static String[] PersonalityPresentationTemplaete = {
            "{1} by nature, tkt",
        };

        public static string GenerateText(TraitsMix traits, bool isMale)
        {
            string text = "";

            text += GenerateTemplatedText(traits.job.name, isMale, JobsPresentationTemplaete);
            
            
            text += GenerateTemplatedText(traits.status.name, isMale, StatusPresentationTemplaete);
            
            
            text += GenerateTemplatedText(traits.personnality.name, isMale, PersonalityPresentationTemplaete);


            text += GeneratePredifinedText(traits.lifestyle, isMale);

            return text;
        }

        private static string GeneratePredifinedText(Traits traits, bool isMale)
        {
            string text = "";
            
            text += traits.DescriptionTexts[UnityEngine.Random.Range(0, traits.DescriptionTexts.Count)];
            text = text.Replace("{M_Pronoun}", isMale ? "He" : "She");
            text = text.Replace("{m_Pronoun}", isMale ? "he" : "she");

            return text;
        }

        private static string GenerateTemplatedText(string jobName, bool isMale, string[] jobsPresentationTemplaete)
        {
            string text = "";
            text += jobsPresentationTemplaete[UnityEngine.Random.Range(0, jobsPresentationTemplaete.Length)];
            text = text.Replace("{M_Pronoun}", isMale ? "He" : "She");
            text = text.Replace("{m_Pronoun}", isMale ? "he" : "she");
            text = text.Replace("{1}", jobName);
            return text;
        }
    }
}