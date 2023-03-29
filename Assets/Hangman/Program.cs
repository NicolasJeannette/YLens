namespace HangmanCS;

class Program
{
    static void Main()
    {
        bool replay = true;
        while (replay)
        {
            GameData data = new GameData();
            Word word = data.Start();
            data.Game(word);
            data.End(word);
            replay = data.Replay();
            Console.Clear();
        }
    }
}