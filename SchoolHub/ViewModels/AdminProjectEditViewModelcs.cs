using System.ComponentModel.DataAnnotations;

namespace SchoolHub.ViewModels
{
    public class AdminProjectEditViewModelcs
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Введите название проекта")]

        public string Title { get; set; }

        [Required(ErrorMessage = "Введите описание проекта")]

        public string Description { get; set; }

        [Required(ErrorMessage = "Введите категорию ")]

        public string Category {  get; set; } = string.Empty;

        [Required(ErrorMessage = "Введите статус проекта")]

        public string Status { get; set; } = string.Empty;



    }
}
