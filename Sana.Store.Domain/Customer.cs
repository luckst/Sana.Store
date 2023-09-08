namespace Sana.Store.Domain
{
    public class Customer: BaseEntity
    {
        public string DocumentNumber { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
