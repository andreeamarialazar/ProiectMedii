using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProiectMedii.Models
{
    public class PublishedAlbum
    {
        public int PublisherID { get; set; }
        public int AlbumID { get; set; }
        public Publisher Publisher { get; set; }
        public Album Album { get; set; }
    }
}
