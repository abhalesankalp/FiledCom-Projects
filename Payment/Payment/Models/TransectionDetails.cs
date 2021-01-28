using System;
using System.ComponentModel.DataAnnotations;

namespace Payment.Models
{
    public class TransectionDetails
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [CreditCard]
        public string CreditCardNumber { get; set; }
        [Required]
        public string CardHolder { get; set; }
        [Required]
        public DateTime ExpirationDate { get; set; }
        [Required]
        public string SecurityCode { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Only positive number allowed")]
        [StringLength(3, ErrorMessage ="Number should include only three digits")]
        public Decimal Amount { get; set; }
        public PaymentState PaymentState { get; set; }
    }
}
