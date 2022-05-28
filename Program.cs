using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Text.Json;

namespace BreachChecker
{
    class Program
    {
        private static List<Account> accounts = new List<Account>();
        private static readonly string outputBreach = "breach.json";
        private static readonly string outputPasswordBreach = "pbreach.json";
        private static readonly string api = "https://haveibeenpwned.com/api/v3/breachedaccount/";
        private static HttpClient client = new HttpClient();

        static async Task Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine("You must provide a path to a list of emails to check and your api key. The file must have a newline separated list of email addresses.");
                Console.WriteLine("Example call: `./BreachChecker.exe C:/accountlist.txt <api key>");
            }
            else if (!File.Exists(args[0]))
            {
                Console.WriteLine($"The provided file does not exist: `{args[0]}`");
            }
            else
            {
                await GetBreaches(args[0], args[1]);
            }
        }

        static async Task GetBreaches(string filename, string key)
        {
            List<Account> pb = new List<Account>();
            List<Account> b = new List<Account>();

            client.DefaultRequestHeaders.Add("User-Agent", "Breach Checker");
						client.DefaultRequestHeaders.Add("hibp-api-key", key);

            await GetAccountInfo(filename);

            Console.WriteLine("Starting...");
            int count = 0;

            foreach (Account account in accounts)
            {
                count++;
                Console.WriteLine($"Evaluating account {count}/{accounts.Count}: {account.Email}");
                string url = api + account.Email + "?truncateResponse=false";
                using (HttpResponseMessage response = await client.GetAsync(url))
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        List<Breach> breaches = JsonSerializer.Deserialize<List<Breach>>((await response.Content.ReadAsStringAsync()));
                        account.Breaches = breaches;
                        if (breaches.Any(x => x.DataClasses.Any(y => y == "Passwords")))
                        {
                            pb.Add(account);
                        }
                        else if (breaches.Any())
                        {
                            b.Add(account);
                        }
                    }
                }

                //API ratelimits any request made within 1500 MS from the last request
                Thread.Sleep(1500);
            }

            await OutputToFile(outputPasswordBreach, pb);
            await OutputToFile(outputBreach, b);
        }

        static async Task<List<Account>> GetPopulatedAccountInfo(string filename)
        {
            string file = await File.ReadAllTextAsync(filename);

            return JsonSerializer.Deserialize<List<Account>>(file);
        }

        static async Task GetAccountInfo(string filename)
        {
            string[] emails = await File.ReadAllLinesAsync(filename);
            accounts.AddRange(emails.Select(x => new Account { Email = x }));
        }

        static async Task OutputToFile(string filename, List<Account> accounts)
        {
            await File.AppendAllLinesAsync(filename, new List<string> { JsonSerializer.Serialize(accounts) });
        }
    }
}
