using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManagment.Models
{
    public class Attendence
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "Required*")]
        public int EmpID { get; set; }

        [Required(ErrorMessage = "Required*")]
        public string Attandence { get; set; }

        [Required(ErrorMessage = "Required*")]
        //[DisplayFormat(DataFormatString = "{0:dd/mm/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [NotMapped]
        public string Employee { get; set; }
    }
}
