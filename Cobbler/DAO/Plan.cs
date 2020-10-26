using System;

namespace Cobbler.DAO
{
    public class Plan
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long Currency { get; set; }
        public string CurrencyUnit { get; set; }
        
    }
}