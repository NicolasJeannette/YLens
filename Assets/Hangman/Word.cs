namespace HangmanCS;

public class Word
{
    public string PrintedWord { get; set; }

    public string ToFind { get; set; }
    private bool _isAscii = false;

    public Word(string toFind)
    {
        this.ToFind = toFind;
        this.RevealFirstLetter();
    }

    private void RevealFirstLetter ()
    {
        if (this.ToFind.Length >= 6)
        {
            this.PrintedWord += this.ToFind[0];
        }
        else
        {
            this.PrintedWord += "_";
        }
        for (var i = 0; i < this.ToFind.Length - 1; i++)
        {
            this.PrintedWord += "_";
        }
    }

    public void PrintWord()
    {
        if (this._isAscii)
        {
            Console.WriteLine("To be implemented later");
        }
        else
        {
            foreach (var t in this.PrintedWord!)
            {
                Console.Write(t + " ");
            }
        }
        Console.WriteLine();
    }

    public void RevealInputLetter(char c)
    {
        var wordLetters = PrintedWord.ToCharArray();
        var i = 0;
        foreach (var j in ToFind)
        {
            if (c == j)
            {
                wordLetters[i] = ToFind[i];
            }
            i++;
        }

        PrintedWord = new string(wordLetters);
    }
}