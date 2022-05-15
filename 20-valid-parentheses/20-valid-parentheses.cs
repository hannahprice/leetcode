public class Solution {
    public bool IsValid(string s)
    {
        List<int> unmatchedCharacterIndexes = new List<int>();
        var chars = s.ToCharArray().ToList();
        
        for (int i = 0; i < chars.Count(); i++)
        {
            var currentCharacter = chars[i];

            if (FirstCharacter(i) && IsClosingBracket(currentCharacter) ||
                OddNumberOfBrackets(chars.Count())) 
            { 
                return false;
            }

            if (IsOpeningBracket(currentCharacter))
            {
                unmatchedCharacterIndexes.Add(i);
                continue;
            }
            else
            {
                if (ImmediateMatchedClose(chars[i - 1], chars[i]))
                {
                    unmatchedCharacterIndexes.RemoveAt(unmatchedCharacterIndexes.Count() - 1);
                    continue;
                } 
                else
                {
                    if (!unmatchedCharacterIndexes.Any())
                    {
                        return false;
                    }

                    var lastUnmatchedCharacterIndex = unmatchedCharacterIndexes.Last();
                    var lastUnmatchedCharacter = chars.ElementAt(lastUnmatchedCharacterIndex);
                    
                    if (ImmediateMatchedClose(lastUnmatchedCharacter, currentCharacter))
                    {
                        unmatchedCharacterIndexes.RemoveAt(unmatchedCharacterIndexes.Count() - 1);
                        continue;
                    }

                    return false;
                }
            }
        }

        if (unmatchedCharacterIndexes.Count > 0)
        {
            return false;
        }

        return true;
    }

    private bool FirstCharacter(int index)
    {
        return index == 0;
    }

    private bool OddNumberOfBrackets(int length)
    {
        return length % 2 != 0;
    }

    private bool IsOpeningBracket(char bracket)
    {
        var openingBrackets = new char[] { '(', '{', '[' };
        return openingBrackets.Contains(bracket);
    }

    private bool IsClosingBracket(char bracket)
    {
        var closingBrackets = new char[] { ')', '}', ']' };
        return closingBrackets.Contains(bracket);
    }

    private bool ImmediateMatchedClose(char prevBracket, char currentBracket)
    {
        var pairs = new List<KeyValuePair<char, char>>
        {
            new KeyValuePair<char, char>('[', ']'),
            new KeyValuePair<char, char>('{', '}'),
            new KeyValuePair<char, char>('(', ')')
        };

        var closingBracketPair = pairs.Single(x => x.Value == currentBracket);
        return prevBracket == closingBracketPair.Key;
    }
}