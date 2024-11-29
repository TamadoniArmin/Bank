﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Entity
{
    public class Card
    {
        [Key]
        [MaxLength(16)]
        public string CardNumber { get; set; }
        [MaxLength(30)]
        public string HolderName { get; set; }
        public float Balance { get; set; }
        public bool IsActice { get; set; }=true;
        [MaxLength(20)]
        public string Password { get; set; }
        public float Daylitransaction { get; set; }
        public DateTime SetedLimitationDate { get; set; }
        public int InsertingPasswordWrongly { get; set; } = 0;
        public List<TransAction>  SourceTransactions { get; set; }
        public List<TransAction> DestinationTransactions { get; set; }
    }
}
