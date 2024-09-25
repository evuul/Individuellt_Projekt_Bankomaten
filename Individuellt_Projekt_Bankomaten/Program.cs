namespace Individuellt_Projekt_Bankomaten;

class Program
{
    static void Main(string[] args)
    {
        string[] usernames = { "Alexander", "Karin", "Johan" };
        string[] passwords = { "1234", "1111", "2222" };
        string[,] accounts =
        {
            { "Sparkonto", "Lönekonto", "Kreditkort" },
            { "Sparkonto", "Lönekonto", "Kreditkort" },
            { "Sparkonto", "Lönekonto", "Kreditkort" }
        };
        decimal[,] accountBalances =
        {
            { 10000m, 5000m, -1000m },
            { 20000m, 10000m, -2000m },
            { 30000m, 15000m, -3000m }
        };
        string username = "";
        string password = "";
        int currentUserIndex = -1; // för att se vilken användare som är inloggad 
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
                    currentUserIndex = i; // sparar index för inloggad användare
                    isRunning = false;
                    break;
                }
            }

            if (currentUserIndex == -1)
            {
                Console.WriteLine("Felaktigt användarnamn eller lösenord, försök igen!");
            }
        }

        bool loggedIn = true;
        while (loggedIn && currentUserIndex != -1)
        {
            Console.WriteLine("Välj ett alternativ:");
            Console.WriteLine("1. Saldo & konton");
            Console.WriteLine("2. Överföring mellan konton");
            Console.WriteLine("3. Insättning & uttag");
            Console.WriteLine("4. Logga ut");
            if (int.TryParse(Console.ReadLine(), out int choice))
            {
                switch (choice)
                {
                    case 1:
                        ShowAccounts(accounts, accountBalances, currentUserIndex, usernames);
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
            else
            {
                Console.WriteLine("Felaktigt val, försök igen!");
            }
        }

        static void ShowAccounts(string[,] accounts, decimal[,] accountBalances, int userIndex, string[] usernames)
        {
            Console.WriteLine($"Konton och saldo för {usernames[userIndex]}:");
            for (int i = 0; i < accounts.GetLength(1); i++)
            {
                Console.WriteLine($"{accounts[userIndex, i]}: {accountBalances[userIndex, i]} kr");
            }
        }
    }
}