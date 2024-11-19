namespace RepairPC1.Data
{
    public class Order
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Tel { get; set; }
        public string Stat { get; set; }
        public int Price { get; set; }
        public int MasterId { get; set; }
        public string Adress { get; set; }
        public string[] Services { get; set; }
    }
}
