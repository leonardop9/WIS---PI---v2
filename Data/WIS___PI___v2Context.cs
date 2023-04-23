using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WIS_PI.Models;

namespace WIS___PI___v2.Data
{
    public class WIS___PI___v2Context : DbContext
    {
        public WIS___PI___v2Context (DbContextOptions<WIS___PI___v2Context> options)
            : base(options)
        {
        }

        public DbSet<WIS_PI.Models.Genero> Genero { get; set; } = default!;

        public DbSet<WIS_PI.Models.Usuario>? Usuario { get; set; }
    }
}
