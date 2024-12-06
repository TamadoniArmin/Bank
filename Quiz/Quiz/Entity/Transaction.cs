using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Entity
{
    public class TransAction
    {
        [Key]
        public int TransactionId { get; set; }
        [MaxLength(16)]
        public string SourceCardNumber { get; set; }
        public int SourceCardId { get; set; }
        public Card SourceCard { get; set; }
        [MaxLength (16)]
        public string DestinationCardNumber { get; set; }
        public int DestinationCardId { get; set; }
        public string? TransactionDetails { get; set; }
        public Card DestinationCard { get; set; }
        public double Amount { get; set; }
        public DateTime TransactionDate { get; set; }
        public bool isSuccessful { get; set; }
    }
}
