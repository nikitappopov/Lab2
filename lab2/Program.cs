using System;

class Program
{
    static void Main()
    {
        string message = Console.ReadLine();
        double scoreMin = double.MaxValue;
        string bestText = "";

        for (int i = 0; i < 26; i++)
        {
            string text = EncodeDecalage(message, i);
            double score = ProbaTextAnglais(text);

            if (score < scoreMin)
            {
                scoreMin = score;
                bestText = text;
            }
        }

        Console.WriteLine(bestText);
    }

    static string EncodeDecalage(string text, int decalage)
    {
        char[] result = new char[text.Length];

        for (int i = 0; i < text.Length; i++)
        {
            char currentChar = text[i];

            if (char.IsLetter(currentChar))
            {
                char baseChar = char.IsLower(currentChar) ? 'a' : 'A';
                char encodedChar = (char)(((currentChar - baseChar + decalage) % 26) + baseChar);
                result[i] = encodedChar;
            }
            else
            {
                result[i] = currentChar;
            }
        }

        return new string(result);
    }

    static double ProbaTextAnglais(string text)
    {
        int a = (int)'a';
        double[] proba = { 8.08, 1.67, 3.18, 3.99, 12.56, 2.17, 1.8, 5.27, 7.24, 0.14, 0.63, 4.04, 2.6, 7.38, 7.47, 1.91, 0.09, 6.42, 6.59, 9.15, 2.79, 1, 1.89, 0.21, 1.65, 0.07 };
        int[] frequence = new int[26];

        text = text.ToLower();

        foreach (char c in text)
        {
            int index = (int)c - a;
            if (index >= 0 && index <= 25)
            {
                frequence[index]++;
            }
        }

        int lenStr = text.Length;
        double score = 0;

        for (int i = 0; i < 26; i++)
        {
            double p = (frequence[i] / (double)lenStr) * 100;
            score += Math.Pow((proba[i] - p), 2);
        }

        return Math.Sqrt(score);
    }
}