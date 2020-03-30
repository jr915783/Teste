using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CrudPaginacao.Models
{
    public class Base:DbContext
    {
        public DbSet<Pessoa> Pessoas { get; set; }

    }
}