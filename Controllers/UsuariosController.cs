using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WIS_PI.Models;
using WIS___PI___v2.Data;
using Firebase.Auth.Providers;
using Firebase.Auth.Repository;
using Firebase.Auth;

namespace WIS___PI___v2.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly WIS___PI___v2Context _context;

        public UsuariosController(WIS___PI___v2Context context)
        {
            _context = context;
        }

        // GET: Usuarios
        public async Task<IActionResult> Index()
        {
            var wIS___PI___v2Context = _context.Usuario.Include(u => u.Genero);
            return View(await wIS___PI___v2Context.ToListAsync());
        }

        // GET: Usuarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Usuario == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuario
                .Include(u => u.Genero)
                .FirstOrDefaultAsync(m => m.UsuarioId == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // GET: Usuarios/Create
        public IActionResult Create()
        {
            ViewData["GeneroId"] = new SelectList(_context.Genero, "GeneroId", "NomeGenero");
            return View();
        }

        // POST: Usuarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UsuarioId,Nome,Email,Senha,DataNascimento,GeneroId")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var config = new FirebaseAuthConfig
                    {
                        ApiKey = "AIzaSyBLkFa3c41FS2P-vWCZNWAKPi25McMeqbE",
                        AuthDomain = "wis---pi.firebaseapp.com",
                        Providers = new FirebaseAuthProvider[]
                   {
                    new EmailProvider()
                   },
                        UserRepository = new FileUserRepository("NossosDados")
                    };
                    var cliente = new FirebaseAuthClient(config);
                    await cliente.CreateUserWithEmailAndPasswordAsync(usuario.Email, usuario.Senha); ;

                    _context.Add(usuario);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    if (ex.Message.Contains("The user with the provided email already exists (EMAIL_EXISTS)"))
                    {
                        Console.WriteLine("Já existe o e-mail cadastrado.");
                    }
                }
            }
            ViewData["GeneroId"] = new SelectList(_context.Set<Genero>(), "GeneroId", "NomeGenero", usuario.GeneroId);
            return View(usuario);
        }

        // GET: Usuarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Usuario == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuario.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            ViewData["GeneroId"] = new SelectList(_context.Genero, "GeneroId", "GeneroId", usuario.GeneroId);
            return View(usuario);
        }

        // POST: Usuarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UsuarioId,Nome,Email,Senha,DataNascimento,GeneroId")] Usuario usuario)
        {
            if (id != usuario.UsuarioId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(usuario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuarioExists(usuario.UsuarioId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["GeneroId"] = new SelectList(_context.Genero, "GeneroId", "GeneroId", usuario.GeneroId);
            return View(usuario);
        }

        // GET: Usuarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Usuario == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuario
                .Include(u => u.Genero)
                .FirstOrDefaultAsync(m => m.UsuarioId == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Usuario == null)
            {
                return Problem("Entity set 'WIS___PI___v2Context.Usuario'  is null.");
            }
            var usuario = await _context.Usuario.FindAsync(id);
            if (usuario != null)
            {
                _context.Usuario.Remove(usuario);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsuarioExists(int id)
        {
          return (_context.Usuario?.Any(e => e.UsuarioId == id)).GetValueOrDefault();
        }

        public IActionResult Login()
        {
            return View();
        }
    }
}
