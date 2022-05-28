using System.Collections.Generic;

namespace BreachChecker
{
    public class Account
    {
        public string Email { get; set; }
        public List<Breach> Breaches { get; set; }
    }
}
