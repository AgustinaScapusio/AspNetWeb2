using AspNetWeb2.Models;
using Microsoft.AspNetCore.Mvc;


namespace AspNetWeb2.Controllers
{
    public class MusicController : Controller
    {
        private static readonly List<Album> _albums = new List<Album>
        {
            new Album
            {
            Id = 1,
            Name = "...And Justice for All",
            ReleaseDate = new DateTime(1988, 8, 25)
            },
            new Album
            {
            Id = 2,
            Name = "Hardwired... to Self-Destruct",
            ReleaseDate = new DateTime(2016, 11, 18)
            }
        };

       
        [HttpGet("/")]
        public IActionResult Index()
        {
            return Json(_albums);
        }

        [HttpPost("albums/add")]
        public IActionResult AddAlbum(Album album)
        {
            var newId=_albums.Max(i=>i.Id)+1;
            album.Id = newId;
            _albums.Add(album);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost("albums/delete/{id?}")]
        public IActionResult DeleteAlbum(int id)
        {
            var album = _albums.FirstOrDefault(x => x.Id == id);
            if (album == null)
            {
                NotFound();
            }
            _albums.Remove(album!);
            return RedirectToAction("Index");
        }
        [HttpPost("albums/edit")]
        public IActionResult EditAlbum(Album editableAlbum)
        {
            var index = _albums.FindIndex(album => album.Id == editableAlbum.Id);
            _albums[index] = editableAlbum;
            return RedirectToAction("Index");
        }

    }
}
