using System;

namespace _200_Dev
{
    public static class TextGenerator
    {
        private static String[] JobsPresentationTemplaete = new string[]
        {
            "{M_Pronoun} is a {1}.",
            "{M_Pronoun} is a {1} by profession.",
            "{M_Pronoun} always wanted to be a {1} since {m_Pronoun} was a child.",
            "{M_Pronoun} is a {1} by trade.",
            "{M_Pronoun} is a {1} by vocation.",
        };
        
        private static String[] StatusPresentationTemplaete = new string[]
        {
            "{M_Pronoun} is {1}.",
            "{M_Pronoun} is {1} by nature.",
            "{M_Pronoun} is {1} by temperament.",
            "{M_Pronoun} is {1} by character.",
            "{M_Pronoun} is {1} by personality.",
        };
        
        private static String[] PersonalityPresentationTemplaete = new string[]
        {
            "{M_Pronoun} is {1}.",
            "{M_Pronoun} is {1} by nature.",
            "{M_Pronoun} is {1} by temperament.",
            "{M_Pronoun} is {1} by character.",
            "{M_Pronoun} is {1} by personality.",
        };
        
        private static String[] LifestylePresentationTemplaete = new string[]
        {
            "{M_Pronoun} is {1}.",
            "{M_Pronoun} is {1} by nature.",
            "{M_Pronoun} is {1} by temperament.",
            "{M_Pronoun} is {1} by character.",
            "{M_Pronoun} is {1} by personality.",
        };

        public static string GenerateText(TraitsMix traits, bool isMale)
        {
            string text = "";

            text += JobsPresentationTemplaete[UnityEngine.Random.Range(0, JobsPresentationTemplaete.Length)];
            text = text.Replace("{M_Pronoun}", isMale ? "He" : "She");
            text = text.Replace("{m_Pronoun}", isMale ? "he" : "she");
            text = text.Replace("{1}", traits.jobTrait.Name);
            
            
            text += StatusPresentationTemplaete[UnityEngine.Random.Range(0, StatusPresentationTemplaete.Length)];
            text = text.Replace("{M_Pronoun}", isMale ? "He" : "She");
            text = text.Replace("{m_Pronoun}", isMale ? "he" : "she");
            text = text.Replace("{1}", traits.Status.Name);
            
            
            text += PersonalityPresentationTemplaete[UnityEngine.Random.Range(0, PersonalityPresentationTemplaete.Length)];
            text = text.Replace("{M_Pronoun}", isMale ? "He" : "She");
            text = text.Replace("{m_Pronoun}", isMale ? "he" : "she");
            text = text.Replace("{1}", traits.Personality.Name);
            
            
            text += LifestylePresentationTemplaete[UnityEngine.Random.Range(0, LifestylePresentationTemplaete.Length)];
            text = text.Replace("{M_Pronoun}", isMale ? "He" : "She");
            text = text.Replace("{m_Pronoun}", isMale ? "he" : "she");
            text = text.Replace("{1}", traits.Lifestyle.Name);

            return text;
        }

    }
}