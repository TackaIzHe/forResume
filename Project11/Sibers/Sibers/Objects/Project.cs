namespace Sibers.Objects
{
    public class Project
    { 
        public int id { get; set; }
        public string name { get; set; }
        public string custCompany { get; set; }
        public string execCompany { get; set; }
        public List<int> idPersons { get; set; }
        public int idProjManager { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }
        public int priority { get; set; }
        public List<int> idTasks { get; set; }
    }
}
