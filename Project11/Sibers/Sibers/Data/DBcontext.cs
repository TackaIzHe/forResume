using Microsoft.EntityFrameworkCore;
using Sibers.Objects;
using Sibers.Services;

namespace Sibers.Data
{
    public class DBcontext:DbContext,IData
    {

        public DBcontext()
        {
            if(!Database.CanConnect())
            {
                Database.EnsureDeleted();
                Database.EnsureCreated();
                new UsersServices().Register(new Person {
                    name = "root",
                    surname = "root",
                    patronymic = "root",
                    email = "root@root",
                    password = "root",
                    role = Role.Director
                });
            }    
        }

        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=DbList;Trusted_connection=True");
        }

        DbSet<Person> persons => Set<Person>();
        DbSet<Project> projects => Set<Project>();
        DbSet<Objects.Task> tasks => Set<Objects.Task>();

        /////
        public List<Person> getPerson()
        {
            return persons.ToList();
        }

        public void postPerson(Person person)
        {
            persons.Add(person);
            SaveChanges();
        }

        public void putPerson(Person person) 
        {
            persons.Update(person);
            SaveChanges();
        }

        public void delPerson(int id)
        {
            persons.Remove(findPerson(id));
            SaveChanges();
        }

        public Person findPerson(int id)
        {
            return persons.Find(id);
        }
        public Person findPersonEmail(string email)
        {
            foreach (Person person in persons)
            {
                if(person.email == email)
                {
                    return person;
                }
            }
            return null;
        }

        ////
        public List<Project> getProject()
        {
            return projects.ToList();
        }

        public void postProject(Project project)
        {
            projects.Add(project);
            SaveChanges();
        }

        public void putProject(Project project)
        {
            projects.Update(project);
            SaveChanges();
        }

        public void delProject(int id)
        {
            projects.Remove(findProject(id));
            SaveChanges();
        }
        
        public Project findProject(int id)
        {
            return projects.Find(id);
        }

        public void putProjectTask(int id, int idTask)
        {
            findProject(id).idTasks.Add(idTask);
            SaveChanges();
        }

        ////
        public List<Objects.Task> getTask()
        {
            return tasks.ToList();
        }

        public void postTask(Objects.Task task) 
        {
            tasks.Add(task);
            SaveChanges();
        }

        public void putTask(Objects.Task task)
        {
            tasks.Update(task);
            SaveChanges();
        }

        public void delTask(int id)
        {
            tasks.Remove(findTask(id));
            SaveChanges();
        }

        public Objects.Task findTask(int id)
        {
            return tasks.Find(id);
        }
    }
}
