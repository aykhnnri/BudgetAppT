using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BudgetApp.Areas.Identity.Data
{
    public class Transaction
    {
        [Key]
        public int TransactionId { get; set; }
        [ForeignKey("BudgetAppUser")]
        public string? Id { get; set; }
        public string? Category { get; set; }
		[Required]
		public decimal Amount { get; set; }
        public string? Description { get; set; }
        public DateTime Date { get; set; }
        public BudgetAppUser? User { get; set; }
    }
}
