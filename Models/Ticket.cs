using System.ComponentModel.DataAnnotations;

namespace HelpDesk.Models
{
    public class Ticket
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "عنوان تیکت الزامی است")]
        [Display(Name = "عنوان")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "توضیحات الزامی است")]
        [Display(Name = "توضیحات")]
        public string Description { get; set; } = string.Empty;

        [Display(Name = "اولویت")]
        public string Priority { get; set; } = "متوسط"; // کم / متوسط / بالا

        [Display(Name = "وضعیت")]
        public string Status { get; set; } = "باز"; // باز / درحال بررسی / بسته

        [Display(Name = "تاریخ ثبت")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public string UserId { get; set; } = string.Empty;
        public ApplicationUser? User { get; set; }
    }
}
