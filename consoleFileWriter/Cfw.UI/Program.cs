namespace Cfw.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            ConsoleReader reader = new ConsoleReader();
            if (reader.CreateSession())
            {
                reader.ReadInput();
            }
        }
    }
}
