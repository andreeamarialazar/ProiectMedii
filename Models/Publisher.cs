using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace ProiectMedii.Models
{
    public class Publisher
    {
        public int ID { get; set; }
        [Required]
        [Display(Name = "Numele Producatorului")]
        [StringLength(50)]
        public string PublisherName { get; set; }

        [StringLength(70)]
        [Display(Name = "Adresa Producatorului")]

        public string Adress { get; set; }
        public ICollection<PublishedAlbum> PublishedAlbums { get; set; }
    }
}
