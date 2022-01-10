using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProiectMedii.Models;

namespace ProiectMedii.Data
{
    public class DbInitializer
    {
        public static void Initialize(LibraryContext context)
        {
            context.Database.EnsureCreated();
            if (context.Albums.Any())
            {
                return; // BD a fost creata anterior
            }
            var albums= new Album[]
            {
 new Album{Title="PatchWork",SongsWriter="Passenger",Price=Decimal.Parse("55.7")},
 new Album{Title="Shades of Black",SongsWriter="Kovacs",Price=Decimal.Parse("30.5")},
 new Album{Title="Carnival of Souls",SongsWriter="Kiss",Price=Decimal.Parse("49.2")},
  new Album{Title="Monsters",SongsWriter="Kiss",Price=Decimal.Parse("49")}
            };
            foreach (Album s in albums)
            {
                context.Albums.Add(s);
            }
            context.SaveChanges();
            var customers = new Customer[]
            {

 new Customer{CustomerID=1050,Name="Lazar Adina",BirthDate=DateTime.Parse("1980-05-14")},
 new Customer{CustomerID=1045,Name="Lazar Gheorghe",BirthDate=DateTime.Parse("1967-07-08")},
 new Customer{CustomerID=1050,Name="Lazar Mihaela",BirthDate=DateTime.Parse("2001-11-24")},
 new Customer{CustomerID=1045,Name="Lazar Valentina",BirthDate=DateTime.Parse("2006-03-07")},
 new Customer{CustomerID=1045,Name="Lazar Luca",BirthDate=DateTime.Parse("2008-10-06")},
 new Customer{CustomerID=1045,Name="Lazar Andreea",BirthDate=DateTime.Parse("2000-06-06")},

 };
            foreach (Customer c in customers)
            {
                context.Customers.Add(c);
            }
            context.SaveChanges();



            var orders = new Order[]
            {
 new Order{AlbumID=1,CustomerID=1050, OrderDate=DateTime.Parse("02-25-2021")},
 new Order{AlbumID=3,CustomerID=1045, OrderDate=DateTime.Parse("03-27-2021")},
 new Order{AlbumID=1,CustomerID=1045, OrderDate=DateTime.Parse("04-29-2021")},
 new Order{AlbumID=2,CustomerID=1050, OrderDate=DateTime.Parse("09-15-2021")},
  new Order{AlbumID=4,CustomerID=1050, OrderDate=DateTime.Parse("09-15-2021")},
   new Order{AlbumID=5,CustomerID=1050, OrderDate=DateTime.Parse("09-15-2021")},
            };
            foreach (Order e in orders)
            {
                context.Orders.Add(e);
            }
            context.SaveChanges();




            var publishers = new Publisher[]
 {

 new Publisher{PublisherName="Hahaproduction",Adress="Str. Aviatorilor, nr. 40,Bucuresti"},
 new Publisher{PublisherName="Smiley",Adress="Str. Plopilor, nr. 35,Ploiesti"},
 new Publisher{PublisherName="MTV",Adress="Str. Cascadelor, nr.22, Cluj-Napoca"},
 };
            foreach (Publisher p in publishers)
            {
                context.Publishers.Add(p);
            }
            context.SaveChanges();



            var publishedalbums = new PublishedAlbum[]
            {
 new PublishedAlbum {
 AlbumID = albums.Single(c => c.Title == "Carnival of Souls" ).ID,
 PublisherID = publishers.Single(i => i.PublisherName ==
"Hahaproduction").ID
 },
 new PublishedAlbum {
 AlbumID = albums.Single(c => c.Title == "Monsters" ).ID,
PublisherID = publishers.Single(i => i.PublisherName ==
"MTV").ID
 },
 new PublishedAlbum {
 AlbumID = albums.Single(c => c.Title == "PatchWork" ).ID,
 PublisherID = publishers.Single(i => i.PublisherName ==
"Smiley").ID
 },
 new PublishedAlbum {
 AlbumID = albums.Single(c => c.Title == "Shades of black" ).ID,
PublisherID = publishers.Single(i => i.PublisherName == "MTV").ID
 },

            };
            foreach (PublishedAlbum pb in publishedalbums)
            {
                context.PublishedAlbums.Add(pb);
            }
            context.SaveChanges();
        }
    }
}
