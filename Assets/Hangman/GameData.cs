namespace HangmanCS;

public class GameData
{
    private int NbTry { get; set; } = 10;
    private string UsedLetters;
    private string[] UsedWords;
    private string[] Positions;

    public GameData()
    {
        Positions = new string[10];
    }

    private string[] ReadFileLIneByLine (string file)
    {
        StreamReader sr = new StreamReader("textFiles/" + file + ".txt");
        string line = sr.ReadLine();
        string[] fileLines = Array.Empty<string>();
        while (line != null)
        {
            fileLines = fileLines.Append(line).ToArray();
            line = sr.ReadLine();
        }
        return fileLines;
    }

    private string Choose()
    {
        Console.Write("Choose : ");
        var choose = Console.ReadLine();
        if (choose != "STOP" && choose != "--startWith" && string.IsNullOrEmpty(choose))
        {
            choose = choose.ToLower();
        }
        return choose;
    }

    private bool InWord(char letter, string word)
    {
        return word.Any(c => letter == c);
    }

    private void SetPositions()
    {
        string[] hangmanPositions = ReadFileLIneByLine("hangman");
        for (int i = 0; i < 10; i++)
        {
            string aPosition = "";
            for (int j = 0; j < 8;j++)
            {
                aPosition += hangmanPositions[8 * i + j] + "\n";
            }

            Positions[i] = aPosition;
        }
    }

    public Word Start()
    {
        var aleatory = new Random();
        string[] wordList = ReadFileLIneByLine("words");
        Word word = new Word(wordList[aleatory.Next(wordList.Length)]);
        SetPositions();
        Console.Clear();
        Console.WriteLine("Bonne chance, vous avez 10 essais");
        return word;
    }

    public void Game(Word word)
    {
        while (word.PrintedWord != word.ToFind && NbTry > 0)
        {
            word.PrintWord();
            if (NbTry < 10)
            {
                Console.WriteLine(Positions[9 - NbTry]);
            }
            string choose = Choose();
            Console.Clear();
            while (choose.Length == 1 && InWord(choose[0],UsedLetters))
            {
                if (choose.Length == 1)
                {
                    Console.WriteLine("Lettre déjà utilisée");
                }
                else
                {
                    Console.WriteLine("Mot déjà utilisé");
                }
            }
            if (choose.Length == 1)
            {
                char c = choose[0];
                if (InWord(c, word.ToFind))
                {
                    word.RevealInputLetter(c);
                }
                else
                {
                    NbTry--;
                    Console.WriteLine("Not in the word, " + NbTry + " attempts remaining");
                }
            }
            else
            {
                if (choose == word.ToFind)
                {
                    word.PrintedWord = word.ToFind;
                }
                else
                {
                    NbTry -= 2;
                    Console.WriteLine("This is not the word !");
                    Console.WriteLine(NbTry + " attempts remaining");
                }
            }
        }
    }

    public void End(Word word)
    {
        Console.Clear();
        if (NbTry == 0)
        {
            Console.WriteLine("You lost ! The word was " + word.ToFind);
        }
        else
        {
            word.PrintWord();
            Console.WriteLine("Congrats !");
        }
    }

    public bool Replay()
    {
        Console.WriteLine("Do you want to play again ?");
        if (Choose() == "yes")
        {
            return true;
        }

        return false;
    }
}