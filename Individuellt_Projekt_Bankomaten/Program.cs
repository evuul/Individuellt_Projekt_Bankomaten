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
            Console.WriteLine("1. Saldo");
            Console.WriteLine("2. Uttag");
            Console.WriteLine("3. Insättning");
            Console.WriteLine("4. Avsluta");
            int choice = int.Parse(Console.ReadLine());
            switch (choice)
            {
                case 1:
                    Console.WriteLine("Ditt saldo är 1000kr");
                    break;
                case 2:
                    Console.WriteLine("Hur mycket vill du ta ut?");
                    int withdraw = int.Parse(Console.ReadLine());
                    if (withdraw > 1000)
                    {
                        Console.WriteLine("Du har inte tillräckligt med pengar på kontot");
                    }
                    else
                    {
                        Console.WriteLine($"Du har tagit ut {withdraw}kr");
                    }
                    break;
                case 3:
                    Console.WriteLine("Hur mycket vill du sätta in?");
                    int deposit = int.Parse(Console.ReadLine());
                    Console.WriteLine($"Du har satt in {deposit}kr");
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