using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Entity
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(30)]
        public string Name { get; set; }
        public string? Email { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public List<Card>? cards { get; set; }
    }
}
