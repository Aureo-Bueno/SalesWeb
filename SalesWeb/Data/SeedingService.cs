using SalesWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesWeb.Models.Enums;

namespace SalesWeb.Data
{
    public class SeedingService
    {
        private SalesWebContext _context;


        public SeedingService(SalesWebContext context)
        {
            _context = context;
        }

        public void Seed()
        {
            if (_context.Department.Any() || _context.Seller.Any() || _context.SalesRecord.Any())
            {
                return; // O BANCO DE DADOS JA FOI POPULADO
            }


            _context.SaveChanges();
        }
    }
}
