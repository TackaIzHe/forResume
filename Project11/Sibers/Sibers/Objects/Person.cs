namespace Sibers.Objects
{
    public class Person
    {
        public int id { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string patronymic { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public Role role { get; set; }
    }
    public enum Role
    {
        Director,
        Manager,
        Employee
    }
}
