namespace Individuellt_Projekt_Bankomaten;

class Program
{
    static void Main(string[] args)
    {
        string[] usernames = { "Alexander", "Karin", "Johan", "Fredrik", "Lisa" };
        string[] passwords = { "1234", "1111", "2222", "3333", "4444" };
        string[,] accounts =
        {
            { "Sparkonto", "Lönekonto", "Kreditkort", "Resekonto"},
            { "Sparkonto", "Matkonto", "Aktiekonto", "Filmkonto"},
            { "Gemensamt konto", "Autogirokonto", "Kreditkort", "E-sparkonto"},
            { "Godiskonto", "Autogirokonto", "Kreditkort", "Avanza"},
            { "Hundkonto", "Sparkonto PC", "Veterinärkostnader", "Nöjes konto"}
        };
        decimal[,] accountBalances =
        {
            { 100000m, 500000.69m, -1000m, 20000m },
            { 200000m, 10000m, 50000m, 1000m },
            { 1000000m, 50000m, -5000m, 100000m },
            { 4500m, 90000m, 9999999m, 5000.86m },
            { 3000m, 10000m, 3500m, 2000m }
        };
        string username = "";
        string password = "";
        int currentUserIndex = -1; // för att se vilken användare som är inloggad 
        int attempts = 0;
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
                attempts++;
                Console.WriteLine("Felaktigt användarnamn eller lösenord, försök igen!");
                if (attempts >= 3) 
                {
                    Console.WriteLine("För många felaktiga inloggningsförsök, försök igen senare!");
                    return;
                }
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
                        TransferMoney(accounts, accountBalances,currentUserIndex);
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
    }
    static void ShowAccounts(string[,] accounts, decimal[,] accountBalances, int userIndex, string[] usernames)
        {
            Console.WriteLine($"Konton och saldo för {usernames[userIndex]}:");
            for (int i = 0; i < accounts.GetLength(1); i++)
            {
                Console.WriteLine($"{accounts[userIndex, i]}: {accountBalances[userIndex, i].ToString("C")}");
            }
        }

        static void TransferMoney(string[,] accounts, decimal[,] accountBalances, int userIndex)
        {
            Console.WriteLine("Vilket konto vill du överföra pengar från?");
            for (int i = 0; i < accounts.GetLength(1); i++)
            {
                Console.WriteLine($"{i + 1}. {accounts[userIndex, i]} - Saldo: {accountBalances[userIndex, i]:C}");
            }

            
            if (!int.TryParse(Console.ReadLine(), out int fromAccount) || fromAccount < 1 || fromAccount > accounts.GetLength(1))
            {
                Console.WriteLine("Ogiltigt val, försök igen.");
                return;
            }
            
            fromAccount--;

            Console.WriteLine("Vilket konto vill du överföra pengar till?");
            for (int i = 0; i < accounts.GetLength(1); i++)
            {
                if (i != fromAccount) 
                {
                    Console.WriteLine($"{i + 1}. {accounts[userIndex, i]} - Saldo: {accountBalances[userIndex, i]:C}");
                }
            }
            
            if (!int.TryParse(Console.ReadLine(), out int toAccount) || toAccount < 1 || toAccount > accounts.GetLength(1) || toAccount == fromAccount + 1)
            {
                Console.WriteLine("Ogiltigt val, försök igen.");
                return;
            }

            toAccount--; 
            
            Console.WriteLine($"Hur mycket pengar vill du överföra från {accounts[userIndex, fromAccount]} till {accounts[userIndex, toAccount]}?");
            if (!decimal.TryParse(Console.ReadLine(), out decimal amount) || amount <= 0)
            {
                Console.WriteLine("Ogiltigt belopp, försök igen.");
                return;
            }
            
            if (accountBalances[userIndex, fromAccount] < amount)
            {
                Console.WriteLine("Du har inte tillräckligt med pengar på kontot för att genomföra överföringen.");
                return;
            }
            
            accountBalances[userIndex, fromAccount] -= amount;
            accountBalances[userIndex, toAccount] += amount;

            Console.WriteLine($"Överföringen lyckades! {amount:C} har överförts från {accounts[userIndex, fromAccount]} till {accounts[userIndex, toAccount]}.");
            Console.WriteLine($"Nytt saldo för {accounts[userIndex, fromAccount]}: {accountBalances[userIndex, fromAccount]:C}");
            Console.WriteLine($"Nytt saldo för {accounts[userIndex, toAccount]}: {accountBalances[userIndex, toAccount]:C}");
        }
}