namespace Individuellt_Projekt_Bankomaten;

class Program
{
    static void Main(string[] args)
    {
        string[] usernames = { "Alexander", "Karin", "Johan" };
        string[] passwords = { "1234", "1111", "2222" };
        string username = "";
        string password = "";
        bool isRunning = true;
        while (isRunning)
        {
            Console.WriteLine("Välkommen till bankomaten!");
            Console.Write("Användarnamn:");
            username = Console.ReadLine();
            Console.Write("Lösenord:");
            password = Console.ReadLine();

            for (int i = 0; i < usernames.Length; i++)
            {
                if (username == usernames[i] && password == passwords[i])
                {
                    Console.WriteLine($"\nInloggning lyckades! Välkommen {username}!");
                    ;
                    isRunning = false;
                    break;
                }
                else
                {
                    Console.WriteLine("Fel användarnamn eller lösenord, försök igen!");
                    break;
                }
            }

        }
        bool loggedIn = true;
        while (loggedIn)
        {
            Console.WriteLine("Välj ett alternativ:");
            Console.WriteLine("1. Saldo & konton");
            Console.WriteLine("2. Överföring mellan konton");
            Console.WriteLine("3. Insättning & uttag");
            Console.WriteLine("4. Logga ut");
            int choice = int.Parse(Console.ReadLine());
            switch (choice)
            {
                case 1:
                    // metod för att se saldo och konton
                    break;
                case 2:
                    // metod för att överföra pengar mellan konton
                    break;
                case 3:
                    // metod för att sätta in och ta ut pengar
                    break;
                case 4:
                    Console.WriteLine("Tack för att du använde bankomaten!");
                    loggedIn = false;
                    break;
                default:
                    Console.WriteLine("Felaktigt val, försök igen!");
                    break;
            }
        }
    }
}