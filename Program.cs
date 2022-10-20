

namespace tagfinder
{
    class App
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Telegram JSON tag extractor");
            Console.WriteLine();
            if (args.Length == 0)
            {
                Console.WriteLine("Usage: app.exe <path to settings file>");
                return;
            }
            Tagger magic = new Tagger();
            magic.Start(args[0]);


        }
    }
}