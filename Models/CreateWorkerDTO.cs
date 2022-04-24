using System.ComponentModel.DataAnnotations;

namespace WorkManagementSystem.Models
{
    public class CreateWorkerDTO
    {
        [Required(ErrorMessage = "First Name is required")]
        public string name { get; set; }
        [Required(ErrorMessage = "Last Name is required")]
        public string lastName { get; set; }
        [Required(ErrorMessage = "Login is required")]
        public string login { get; set; }
        [Required(ErrorMessage ="Password is required")]
        public string password { get; set; }
        [Required(ErrorMessage = "Role is required")]
        public string roleName { get; set; }
    }
}
