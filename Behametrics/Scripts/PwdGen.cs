using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PwdGen
{
    readonly char[] smallLetters = "abcdefghjkmnopqrstuvwxyz".ToCharArray();
    readonly char[] capitalLetters = "ABCDEFGHJKMNOPQRSTUVWXYZ".ToCharArray();
    readonly char[] numbers = "1234567890".ToCharArray();
    List<string> facts=new List<string>()
    {
        "In Switzerland it is illegal to own just one guinea pig.",
        "So far, two diseases have successfully been eradicated: smallpox and rinderpest.",
        "Cherophobia is an irrational fear of fun or happiness..",
        "If you lift a kangaroo’s tail off the ground it can’t hop.",
        "Bananas are curved because they grow towards the sun.",
        "Billy goats urinate on their own heads to smell more attractive to females.",
        "The inventor of the Frisbee was cremated and made into a Frisbee after he died.",
        "Polar bears could eat as many as 86 penguins in a single sitting…",
        "Movie trailers were originally shown after the movie, which is why they were called “trailers”.",
        "The top six foods that make your fart are beans, corn, bell peppers, cauliflower, cabbage and milk.",
        "Saint Lucia is the only country in the world named after a woman.",
        "In Uganda, around 48% of the population is under 15 years of age.",
        "The average male gets bored of a shopping trip after 26 minutes.",
        "Approximately 12% of U.S. power outages are caused by squirrels.",
        "Facebook, Instagram and Twitter are all banned in China",
        "Nearly 3% of the ice in Antarctic glaciers is penguin urine.",
        "A small child could swim through the veins of a blue whale.",
        "There is a total of 1,710 steps in the Eiffel Tower.",
        "Birds don’t urinate.",
        "Vincent van Gogh only sold one painting in his lifetime.",
        "The world record for stuffing drinking straws into your mouth at once is 459.",
        "Squirrels forget where they hide about half of their nuts."
    };

    public string GeneratePassword(int smallLettersCount, int capitalLettersCount, int numbersCount, int pwdLength)
    {
        string pwd = "";
        List<string> types = new List<string>();
        
        //Init Types
        for (int i=0; i < smallLettersCount;i++)
        {
            types.Add("small");
        }
        for (int i = 0; i < capitalLettersCount; i++)
        {
            types.Add("cap");
        }
        for (int i = 0; i < numbersCount; i++)
        {
            types.Add("num");
        }


        for (int i = 0; i < pwdLength; i++)
        {
            char charToAdd= 'a';
            int randomId = Random.Range(0, types.Count - 1);
            string type=types[randomId];
            types.RemoveAt(randomId);

            switch (type)
            {
                case "small" : charToAdd = smallLetters[Random.Range(0, smallLetters.Length - 1)]; break;
                case "cap" : charToAdd = capitalLetters[Random.Range(0, capitalLetters.Length - 1)]; break;
                case "num": charToAdd = numbers[Random.Range(0, numbers.Length - 1)]; break;
            }
            if(i==0) 
                pwd+= smallLetters[Random.Range(0, smallLetters.Length - 1)];
            else
            pwd += charToAdd;

        }
        return pwd;
    }
    public string RandomFact() {
        string fact = facts[Random.Range(0, facts.Count - 1)];
        facts.Remove(fact);
        return "<u>Fun fact</u>: "+fact;
    }
}
