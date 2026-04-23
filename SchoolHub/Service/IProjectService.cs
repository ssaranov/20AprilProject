using SchoolHub.Models;

namespace SchoolHub.Service
{
    public interface IProjectService
    {
        List<Project> GetProjects();
        List<Project> GetProjectsByAuthorId(int authorId);  

        Project? GetProjectById(int id);

        void AddProject(Project project);
        void UpdateProject(Project project);
        void DeleteProject(Project project);


    }
}
