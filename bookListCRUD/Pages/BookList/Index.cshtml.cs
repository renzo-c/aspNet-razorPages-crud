using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bookListCRUD.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace bookListCRUD.Pages.BookList
{
    public class IndexModel : PageModel
    {

        private readonly ApplicationDbContext _db;

        public IndexModel(ApplicationDbContext db) // context obtained by using dependency injection, once dbcontext is declared in startup file, which means it is already in the pipeline
        {
            _db = db;
        }

        public IEnumerable<Book> Books { get; set; }
        public async Task OnGet()
        {
            Books = await _db.Book.ToListAsync();
        }

        public async Task<IActionResult> OnPostDelete(int id)
        {
            var book = await _db.Book.FindAsync(id);
            if(book == null)
            {
                return NotFound();
            }
            _db.Book.Remove(book);
            await _db.SaveChangesAsync();
            return RedirectToPage("Index");
        }
    }
}
