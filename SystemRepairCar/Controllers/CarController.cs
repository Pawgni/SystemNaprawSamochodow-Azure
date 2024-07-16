using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SystemRepairCar.Models;
using SystemRepairCar.Repository;
using SystemRepairCar.Events;
using SystemRepairCar.Services;
using SystemRepairCar.Interfaces;
using System;
using System.IO;
using Microsoft.EntityFrameworkCore;

namespace SystemRepairCar.Controllers
{
    public class CarController : Controller
    {
        private readonly ICarRepository _carRepository;
        private readonly CarEventDispatcher _eventDispatcher;
        private readonly BlobService _blobService;
        private readonly ICarFactory _carFactory;

        public CarController(ICarRepository carRepository, CarEventDispatcher eventDispatcher, BlobService blobService, ICarFactory carFactory)
        {
            _carRepository = carRepository;
            _eventDispatcher = eventDispatcher;
            _blobService = blobService;
            _carFactory = carFactory;
        }

        public async Task<IActionResult> Index(string searchString)
        {
            var cars = await _carRepository.GetAllCarsAsync();

            if (!String.IsNullOrEmpty(searchString))
            {
                cars = cars.Where(c => c.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase) ||
                                       c.Model.Contains(searchString, StringComparison.OrdinalIgnoreCase) ||
                                       c.Year.ToString().Contains(searchString, StringComparison.OrdinalIgnoreCase));
            }

            ViewData["CurrentFilter"] = searchString;
            return View(cars);
        }


        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _carRepository.GetCarByIdAsync(id.Value);
            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Model,Year,ImageFile,Type,DamageReportFiles")] Car car)
        {
            if (ModelState.IsValid)
            {
                Car newCar = null;
                var carType = Request.Form["Type"];
                var batteryCapacity = Request.Form["BatteryCapacity"];
                var fuelCapacity = Request.Form["FuelCapacity"];

                if (carType == "electric")
                {
                    newCar = new ElectricCar
                    {
                        Name = car.Name,
                        Model = car.Model,
                        Year = car.Year,
                        Type = car.Type,
                        BatteryCapacity = int.Parse(batteryCapacity)
                    };
                }
                else if (carType == "gasoline")
                {
                    newCar = new GasolineCar
                    {
                        Name = car.Name,
                        Model = car.Model,
                        Year = car.Year,
                        Type = car.Type,
                        FuelCapacity = int.Parse(fuelCapacity)
                    };
                }

                var uploadBlobs = new List<(string containerName, string blobName, Stream fileStream)>();

                if (car.ImageFile != null && car.ImageFile.Length > 0)
                {
                    var blobName = $"{Guid.NewGuid()}{Path.GetExtension(car.ImageFile.FileName)}";
                    uploadBlobs.Add(("cars", blobName, car.ImageFile.OpenReadStream()));
                }

                if (car.DamageReportFiles != null && car.DamageReportFiles.Count > 0)
                {
                    foreach (var damageReportFile in car.DamageReportFiles)
                    {
                        var reportName = $"{Guid.NewGuid()}{Path.GetExtension(damageReportFile.FileName)}";
                        uploadBlobs.Add(("damage-reports", reportName, damageReportFile.OpenReadStream()));
                    }
                }

                var blobUrls = await _blobService.UploadBlobsAsync(uploadBlobs.ToArray());

                if (car.ImageFile != null && car.ImageFile.Length > 0)
                {
                    newCar.Image = blobUrls[0];
                }

                if (car.DamageReportFiles != null && car.DamageReportFiles.Count > 0)
                {
                    newCar.DamageReports = blobUrls.Skip(1).ToList();
                }

                newCar.CreatedAt = DateTime.Now;
                await _carRepository.AddCarAsync(newCar);
                await _eventDispatcher.CarCreatedAsync(newCar);
                return RedirectToAction(nameof(Index));
            }
            return View(car);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _carRepository.GetCarByIdAsync(id.Value);
            if (car == null)
            {
                return NotFound();
            }
            return View(car);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Model,Year,Image,CreatedAt,ImageFile,Type,BatteryCapacity,FuelCapacity,DamageReportFiles")] Car car)
        {
            if (id != car.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var existingCar = await _carRepository.GetCarByIdAsync(car.Id);
                    var uploadBlobs = new List<(string containerName, string blobName, Stream fileStream)>();

                    if (car.ImageFile != null && car.ImageFile.Length > 0)
                    {
                        var blobName = $"{Guid.NewGuid()}{Path.GetExtension(car.ImageFile.FileName)}";
                        uploadBlobs.Add(("cars", blobName, car.ImageFile.OpenReadStream()));
                    }

                    if (car.DamageReportFiles != null && car.DamageReportFiles.Count > 0)
                    {
                        foreach (var damageReportFile in car.DamageReportFiles)
                        {
                            var reportName = $"{Guid.NewGuid()}{Path.GetExtension(damageReportFile.FileName)}";
                            uploadBlobs.Add(("damage-reports", reportName, damageReportFile.OpenReadStream()));
                        }
                    }

                    var blobUrls = await _blobService.UploadBlobsAsync(uploadBlobs.ToArray());

                    if (car.ImageFile != null && car.ImageFile.Length > 0)
                    {
                        car.Image = blobUrls[0];
                    }
                    else
                    {
                        car.Image = existingCar.Image;
                    }

                    if (car.DamageReportFiles != null && car.DamageReportFiles.Count > 0)
                    {
                        car.DamageReports = blobUrls.Skip(1).ToList();
                    }
                    else
                    {
                        car.DamageReports = existingCar.DamageReports;
                    }

                    car.CreatedAt = car.CreatedAt.ToLocalTime();

                    await _carRepository.UpdateCarAsync(car);
                    await _eventDispatcher.CarUpdatedAsync(car);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _carRepository.CarExistsAsync(car.Id))
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
            return View(car);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _carRepository.GetCarByIdAsync(id.Value);
            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _carRepository.DeleteCarAsync(id);
            await _eventDispatcher.CarDeletedAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
