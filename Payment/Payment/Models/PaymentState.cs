using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Payment.Models
{
    public class PaymentState
    {
        [Key]
        public int ID { get; set; }
        [ForeignKey("TransectionDetails")]
        public int TransectionID { get; set; }
        public int PaymentStatusID { get; set; }
    }
}
