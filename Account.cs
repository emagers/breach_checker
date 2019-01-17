using System.Collections.Generic;

namespace LoginChecker
{
    public class Account
    {
        public string Email { get; set; }
        public List<Breach> Breaches { get; set; }
    }
}
