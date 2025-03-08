using Sibers.Data;
using Sibers.Objects;
using System.Text.Json;


namespace Sibers.Services
{
    public static class EndPoints
    {
        public static void map(this IEndpointRouteBuilder app)
        {
            app.MapPost("/UserPages/NewProject", (context) =>
            {
                Project obj = context.Request.ReadFromJsonAsync<Project>().Result;
                
                new DBcontext().postProject(obj);
                context.Response.WriteAsJsonAsync(obj);
                return System.Threading.Tasks.Task.CompletedTask;
            });
            app.MapPut("/UserPages/PutProject", (context) =>
            {
                Project obj = context.Request.ReadFromJsonAsync<Project>().Result;

                new DBcontext().putProject(obj);
                context.Response.WriteAsJsonAsync(obj);
                return System.Threading.Tasks.Task.CompletedTask;
            });
            app.MapPut("/UserPages/PutPerson", (context) =>
            {
                Person obj = context.Request.ReadFromJsonAsync<Person>().Result;
                Person person = new DBcontext().findPerson(obj.id);
                obj.email = person.email;
                obj.password = person.password;

                new DBcontext().putPerson(obj);
                context.Response.WriteAsJsonAsync(obj);
                return System.Threading.Tasks.Task.CompletedTask;
            });
            app.MapPost("/UserPages/NewTask", (context) =>
            {
            Sibers.Objects.Task task = context.Request.ReadFromJsonAsync<Sibers.Objects.Task>().Result;
                string SprojId = context.Request.Headers.ToList().Find(x => x.Key == "project").Value;
                int projId;
                int.TryParse(SprojId, out projId);
                new DBcontext().postTask(task);
                Project proj = new DBcontext().findProject(projId);
                proj.idTasks.Add(task.id);
                new DBcontext().putProject(proj);
                context.Response.WriteAsJsonAsync(task);
                return System.Threading.Tasks.Task.CompletedTask;
            });
            app.MapPut("/UserPages/Task", (context) =>
            {
                var task = context.Request.ReadFromJsonAsync<Objects.Task>().Result;
                
                Objects.Task oldTask = new DBcontext().findTask(task.id);
                oldTask.statusTask = task.statusTask;
                new DBcontext().putTask(oldTask);
                context.Response.WriteAsJsonAsync(oldTask);
                return System.Threading.Tasks.Task.CompletedTask;
            });

        }
    }
}
