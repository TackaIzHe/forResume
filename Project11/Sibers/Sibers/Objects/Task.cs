namespace Sibers.Objects
{
    public class Task
    {
        public int id {  get; set; }
        public string name { get; set; }
        public int idAuthorTask { get; set; }
        public int idExecTask { get; set; }
        public StatusTask statusTask { get; set; }
        public string comment {  get; set; }
        public int priority { get; set; }

    }
    public enum StatusTask
    {
        ToDo,
        InProgress,
        Done
    }
}
