using Microsoft.AspNetCore.Mvc;
using QRCoder;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Qrcode.Models;
using Qrcode.Service;

public class HomeController : Controller
{
    private readonly VietQRService _vietQRService;
    private readonly IConfiguration _configuration;

    public HomeController(VietQRService vietQRService, IConfiguration configuration)
    {
        _vietQRService = vietQRService;
        _configuration = configuration;
    }

    public IActionResult Index()
    {
        return View(new InvoiceViewModel());
    }

    [HttpPost]
    public async Task<IActionResult> Create(InvoiceViewModel model)
    {
     
            var qrCodeUrl = await _vietQRService.GenerateQRCodeAsync(model.Amount);

            model.AccountNo = _configuration["VietQR:AccountNo"];
            model.AccountName = _configuration["VietQR:AccountName"];
            model.Description = _configuration["VietQR:Description"];
            model.QRCodeUrl = qrCodeUrl;

            return View("Invoice", model);
     
    }
}