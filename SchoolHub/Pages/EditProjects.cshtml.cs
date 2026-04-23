using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SchoolHub.Data;
using SchoolHub.Service;

namespace SchoolHub.Pages
{
    public class EditProjectsModel : PageModel
    {

        private readonly IProjectService _projectService;
        private readonly ICurrentUserService _currentUserService;

        public EditProjectsModel(IProjectService projectService, ICurrentUserService currentUserService) 
        {
            _projectService = projectService;
            _currentUserService = currentUserService;
        }
        [BindProperty]
        public int Id { get; set; }
        [BindProperty]
        public string Title { get; set; } = string.Empty;
        [BindProperty]
        public string Description { get; set; } = string.Empty;
        [BindProperty]
        public string Status { get; set; } = "Идея";
        [BindProperty]
        public string Category { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public List<string> Categories { get; } = new()
        {
            "Программирование",
            "Роботехника",
            "Игры",
            "Сайт",
            "Мобильные Приложения",
            "Дизайн",
            "Другое"
        };


        public List<string> Statuses { get; set; } = new()
        {
            "Идея",
            "В разработке",
            "Завершен"
        };
        public IActionResult OnGet(int id)
        {
            var userId = _currentUserService.GetCurrentUser(HttpContext);

            if (userId == null) 
            {
                return RedirectToPage("/Index");
            }
            var project = _projectService.GetProjectById(id);
            if (project == null)
            {
                return RedirectToPage("/MyProjects");
            }

            if (project.AuthorId != userId.Value)
            {
                return RedirectToPage("/Projects");
            }   

            if (project.Status == "Завершён")
            {
                return RedirectToPage("/MyProjects");
            }

            Id = userId.Value;
            Title = project.Title;
            Description = project.Description;
            Category = project.Category;
            Status = project.Status;

            return Page();
        }

        public IActionResult OnPost()
        {
            var userId = _currentUserService.GetCurrentUserId(HttpContext);
            if (userId == null)
            {
                return RedirectToPage("/Index");
            }
            if(string.IsNullOrWhiteSpace(Title) ||
                string.IsNullOrWhiteSpace(Description) ||
                string.IsNullOrWhiteSpace(Category) ||
                string.IsNullOrWhiteSpace(Status))
            {
                Message = "Заполните все поля" + Title + "|" + Description + "|" + Category + "|" + Status;
                return Page();
            }
            var project = _projectService.GetProjectsById(Id);
            if (project == null)
            {
                return RedirectToPage("/MyProjects");
            }

            if (project.AuthorId != userId.Value)
            {
                return RedirectToPage("/Projects");
            }
            if (project.Status == "Завершён")
            {
                return RedirectToPage("/MyProjects");
            }

            project.Title = Title;
            project.Description = Description;
            project.Status = Status;
            project.Category = Category;
            _projectService.UpdateProject(project);

            return RedirectToPage("/MyProjects");
        }
    }

    }
