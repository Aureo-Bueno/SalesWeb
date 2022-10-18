using Microsoft.EntityFrameworkCore;
using SalesWeb.Data;
using SalesWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWeb.Services
{
    public class SalesRecordService
    {
        private readonly SalesWebContext _context;

        public SalesRecordService(SalesWebContext context)
        {
            _context = context;
        }

        public async Task<List<SalesRecord>> FindByDateAsync(DateTime? minDate, DateTime? maxDate)
        {
            var result = from obj in _context.SalesRecord select obj;
            if (minDate.HasValue)
            {
                result = result.Where(w => w.Date >= minDate.Value);
            }

            if (maxDate.HasValue)
            {
                result = result.Where(w => w.Date <= maxDate.Value);
            }

            return await result.Include(i => i.Seller).Include(i => i.Seller.Department).OrderByDescending(o => o.Date).ToListAsync();
        }
    }
}
