using Microsoft.AspNetCore.Mvc;
using SalesWeb.Models;
using SalesWeb.Models.ViewModels;
using SalesWeb.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWeb.Controllers
{
    public class SalesRecordsController : Controller
    {
        private readonly SalesRecordService _salesRecordService;
        private readonly DepartmentService _departmentService;
        private readonly SellerService _sellerService;

        public SalesRecordsController(SalesRecordService salesRecordService, DepartmentService departmentService, SellerService sellerService)
        {
            _salesRecordService = salesRecordService;
            _departmentService = departmentService;
            _sellerService = sellerService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> SimpleSearch(DateTime? minDate, DateTime? maxDate)
        {
            if (!minDate.HasValue)
            {
                minDate = new DateTime(DateTime.Now.Year, 1, 1);
            }

            if (!maxDate.HasValue)
            {
                maxDate = DateTime.Now;
            }

            ViewData["minDate"] = minDate.Value.ToString("yyyy-MM-dd");
            ViewData["maxDate"] = maxDate.Value.ToString("yyyy-MM-dd");

            var salesList = await _salesRecordService.FindByDateAsync(minDate, maxDate);
            return View(salesList);
        }

        public async Task<IActionResult> GroupingSearch(DateTime? minDate, DateTime? maxDate)
        {
            if (!minDate.HasValue)
            {
                minDate = new DateTime(DateTime.Now.Year, 1, 1);
            }

            if (!maxDate.HasValue)
            {
                maxDate = DateTime.Now;
            }

            ViewData["minDate"] = minDate.Value.ToString("yyyy-MM-dd");
            ViewData["maxDate"] = maxDate.Value.ToString("yyyy-MM-dd");

            var salesList = await _salesRecordService.FindByDateGroupingAsync(minDate, maxDate);
            return View(salesList);
        }

        public async Task<IActionResult> Create()
        {
            var sellers = await _sellerService.FindAllAsync();
            var viewModel = new SalesRecordViewModel { Sellers = sellers };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SalesRecord salesRecord)
        {
            if (!ModelState.IsValid)
            {
                var sellers = await _sellerService.FindAllAsync();
                var viewModel = new SalesRecordViewModel { SalesRecord = salesRecord, Sellers = sellers };
                return View(viewModel);
            }
            await _salesRecordService.InsertAsync(salesRecord);
            return RedirectToAction(nameof(Create));
        }
    }
}
