using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProiectMedii.Data;
using ProiectMedii.Models;
using ProiectMedii.Models.MusicViewModels;
using Microsoft.AspNetCore.Authorization;

namespace ProiectMedii.Controllers
{
   [Authorize(Policy = "ResponsibleWithPublishers")]
    public class PublishersController : Controller
    {
        private readonly LibraryContext _context;

        public PublishersController(LibraryContext context)
        {
            _context = context;
        }

        // GET: Publishers
        public async Task<IActionResult> Index(int? id, int? albumID)
        {
            var viewModel = new PublisherIndexData
            {
                Publishers = await _context.Publishers
            .Include(i => i.PublishedAlbums)
            .ThenInclude(i => i.Album)
            .ThenInclude(i => i.Orders)
            .ThenInclude(i => i.Customer)
            .AsNoTracking()
            .OrderBy(i => i.PublisherName)
            .ToListAsync()
            };
            if (id != null)
            {
                ViewData["PublisherID"] = id.Value;
                Publisher publisher = viewModel.Publishers.Where(
                i => i.ID == id.Value).Single();
                viewModel.Albums = publisher.PublishedAlbums.Select(s => s.Album);
            }
            if (albumID != null)
            {
                ViewData["AlbumID"] = albumID.Value;
                viewModel.Orders = viewModel.Albums.Where(
                x => x.ID == albumID).Single().Orders;
            }
            return View(viewModel);
        }

        // GET: Publishers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var publisher = await _context.Publishers
                .FirstOrDefaultAsync(m => m.ID == id);
            if (publisher == null)
            {
                return NotFound();
            }

            return View(publisher);
        }

        // GET: Publishers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Publishers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,PublisherName,Adress")] Publisher publisher)
        {
            if (ModelState.IsValid)
            {
                _context.Add(publisher);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(publisher);
        }

        private void PopulatePublishedAlbumData(Publisher publisher)
        {
            var allAlbums = _context.Albums;
            var publisherAlbums = new HashSet<int>(publisher.PublishedAlbums.Select(c => c.AlbumID));
            var viewModel = new List<PublishedAlbumData>();
            foreach (var album in allAlbums)
            {
                viewModel.Add(new PublishedAlbumData
                {
                    AlbumID = album.ID,
                    Title = album.Title,
                    IsPublished = publisherAlbums.Contains(album.ID)
                });
            }
            ViewData["Albums"] = viewModel;
        }

        // GET: Publishers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var publisher = await _context.Publishers
            .Include(i => i.PublishedAlbums).ThenInclude(i => i.Album)
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.ID == id);
            if (publisher == null)
            {
                return NotFound();
            }
            PopulatePublishedAlbumData(publisher);
            return View(publisher);
        }
        

        // POST: Publishers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, string[] selectedAlbums)
        {
            if (id == null)
            {
                return NotFound();
            }
            var publisherToUpdate = await _context.Publishers
            .Include(i => i.PublishedAlbums)
            .ThenInclude(i => i.Album)
            .FirstOrDefaultAsync(m => m.ID == id);
            if (await TryUpdateModelAsync<Publisher>(
            publisherToUpdate,
            "",
            i => i.PublisherName, i => i.Adress))
            {
                UpdatePublishedAlbums(selectedAlbums, publisherToUpdate);
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException /* ex */)
                {

                    ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists, ");
                }
                return RedirectToAction(nameof(Index));
            }
            UpdatePublishedAlbums(selectedAlbums, publisherToUpdate);
            PopulatePublishedAlbumData(publisherToUpdate);
            return View(publisherToUpdate);
        }
        private void UpdatePublishedAlbums(string[] selectedAlbums, Publisher publisherToUpdate)
        {
            if (selectedAlbums == null)
            {
                publisherToUpdate.PublishedAlbums = new List<PublishedAlbum>();
                return;
            }
            var selectedAlbumsHS = new HashSet<string>(selectedAlbums);
            var publishedAlbums = new HashSet<int>
            (publisherToUpdate.PublishedAlbums.Select(c => c.Album.ID));
            foreach (var album in _context.Albums)
            {
                if (selectedAlbumsHS.Contains(album.ID.ToString()))
                {
                    if (!publishedAlbums.Contains(album.ID))
                    {
                        publisherToUpdate.PublishedAlbums.Add(new PublishedAlbum
                        {
                            PublisherID =
                       publisherToUpdate.ID,
                            AlbumID = album.ID
                        });
                    }
                }
                else
                {
                    if (publishedAlbums.Contains(album.ID))
                    {
                        PublishedAlbum albumToRemove = publisherToUpdate.PublishedAlbums.FirstOrDefault(i
                       => i.AlbumID == album.ID);
                        _context.Remove(albumToRemove);
                    }
                }
            }
        }

        // GET: Publishers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var publisher = await _context.Publishers
                .FirstOrDefaultAsync(m => m.ID == id);
            if (publisher == null)
            {
                return NotFound();
            }

            return View(publisher);
        }

        // POST: Publishers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var publisher = await _context.Publishers.FindAsync(id);
            _context.Publishers.Remove(publisher);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PublisherExists(int id)
        {
            return _context.Publishers.Any(e => e.ID == id);
        }
    }
}
