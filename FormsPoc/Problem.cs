using System;
namespace FormsPoc
{
    public class Problem
    {
       static string givenSentence = "I tried to use the back navigation by overriding OnBackButtonPressed, but somehow it wasn't get called at all. I am using the ContentPage and the latest 1.4.2 release";
       static string matchSentence = "I tried to $ the back";

        string givenSentence2 = "I tried to use the back navigation by overriding OnBackButtonPressed, but somehow it wasn't get called at all. I am using the ContentPage and the latest 1.4.2 release";
        string matchSentence2 = "You tried to $ the back";

        static char changeElement = '$';

        //static void Main(String[] args)
        //{
            //string output = FindMatch(givenSentence, matchSentence);
        //}

        public void RemoveWordFromString(string text, int stratingIndex, int count)
        {
            matchSentence.Remove(stratingIndex, count);
            Console.WriteLine(text);
            matchSentence = matchSentence.Remove(stratingIndex, count);
            Console.WriteLine(text);
        }

        // Find Match
        public string FindMatch(string givenSentence, string matchSentence)
        {
             int count = 0;
             for(int i = 0; i < matchSentence.Length; i++)
             {
                for (int j = 0; j < givenSentence.Length; j++)
                {
                    if(count == matchSentence.Length)
                    {
                        return matchSentence;
                    }
                    else if(matchSentence[i].Equals(changeElement))
                    {
                        int pos = i;
                        //while(!givenSentence[pos].Equals(" "))
                        while(!char.IsWhiteSpace(givenSentence[pos]))
                        {
                            pos++; 
                        }
                        string replaceWord = givenSentence.Substring(i, pos - i);
                        //matchSentence.Insert(i, "");
                        matchSentence = matchSentence.Remove(i, 1);
                        matchSentence = matchSentence.Insert(i, replaceWord);
                        i = i + replaceWord.Length;
                        j = i - 1;
                        count += replaceWord.Length;
                    }
                    else if(i < givenSentence.Length && matchSentence[i].Equals(givenSentence[j]) )
                    {
                        i++;
                        count++;
                    }
                    else
                    {
                        i++;
                        j = 0;
                    }
                }
            }
            return matchSentence;
        }

        // Find Character Count in a Sentence
        void FindCharacterCount(string sentence)
        {
            int start = 0;
            int count = 0;
            for (int i = 0; i< sentence.Length; i++)
            {
                if(sentence[start].Equals(sentence[i]))
                {
                    count++;
                    //sentence.Remove(i);
                    //i--;
                }

                if(i == sentence.Length - 1)
                {
                    Console.WriteLine(sentence[start] +  "-" + count);
                    count = 0;
                    start++;
                    i = -1;
                }
            }
        }

        // Sort Sentence
        void SortString(string sentence)
        {
            for (int i = 0; i < sentence.Length; i++)
            {
                 
            }
        }

        void SortNumbers(int[] nums)
        { 
            for(int i = 0; i < nums.Length; i++)
            {
                 
            }
        }
    }
}
