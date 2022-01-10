using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProiectMedii.Models.MusicViewModels
{
    public class PublisherIndexData
    {
        public IEnumerable<Publisher> Publishers { get; set; }
        public IEnumerable<Album> Albums { get; set; }
        public IEnumerable<Order> Orders { get; set; }
    }
}
