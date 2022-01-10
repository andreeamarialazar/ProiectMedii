using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProiectMedii.Models
{
    public class Customer
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CustomerID { get; set; }

        [Display(Name = "Numele Clientului")]

        public string Name { get; set; }
        [Display(Name = "Adresa")]

        public string Adress { get; set; }
        [Display(Name = "Data de nastere")]

        public DateTime BirthDate { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
