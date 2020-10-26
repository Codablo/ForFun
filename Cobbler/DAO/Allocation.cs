namespace Cobbler.DAO
{
    public class Allocation
    {
        public long Id { get; set; }
        public long PlanId { get; set; }
        public long Currency { get; set; }
        public string Project { get; set; }
        public string Person { get; set; }
    }
}