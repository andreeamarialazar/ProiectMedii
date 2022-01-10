using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
namespace ProiectMedii.Models
{
    public class Album
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string SongsWriter { get; set; }
        [Column(TypeName = "decimal(6, 2)")]
        public decimal Price { get; set; }
        
        public ICollection<Order> Orders { get; set; }
        public ICollection<PublishedAlbum> PublishedAlbums { get; set; }

    }
}
