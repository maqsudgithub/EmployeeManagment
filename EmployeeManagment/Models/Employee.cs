using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManagment.Models
{
    public class Employee
    {
        [Key]
        public int ID { get; set; }


        [Required(ErrorMessage = "Required*")]
        public string Name { get; set; }


        [Required(ErrorMessage = "Required*")]
        public string FName { get; set; }


        [Required(ErrorMessage = "Required*")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }


        [Required(ErrorMessage = "Required*")]
        public string Contact { get; set; }


        [Required(ErrorMessage = "Required*")]
        public string Designation { get; set; }



        [Required(ErrorMessage = "Required*")]
        //[DisplayFormat(DataFormatString="{0:dd//mm/yyyy}",ApplyFormatInEditMode =true)]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }


        public string? Image { get; set; }

        //[NotMapped]
        //public string File { get; set; }
    }
}
