using Sibers.Objects;

namespace Sibers.Data
{
    public interface IData
    {
        public List<Person> getPerson();
        public void postPerson(Person person);
        public void putPerson(Person person);
        public void delPerson(int id);
        public Person findPerson(int id);

        public List<Project> getProject();
        public void postProject(Project project);
        public void putProject(Project project);
        public void delProject(int id);
        public Project findProject(int id);
        public void putProjectTask(int id, int idTask);

        public List<Sibers.Objects.Task> getTask();
        public void postTask(Sibers.Objects.Task task);
        public void putTask(Sibers.Objects.Task task);
        public void delTask(int id);
        public Sibers.Objects.Task findTask(int id);
    }
}
