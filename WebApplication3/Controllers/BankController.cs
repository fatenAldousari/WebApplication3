﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BankController : ControllerBase
    {
        BankContext _context;
        public BankController(BankContext context)
        {
            _context = context;
        }
        //[HttpGet]
        public List<BankBranch> GetAll()
        {
            return _context.BankBranches.Select(b=> new BankBranch
            {
                BranchManager = b.BranchManager,
                LocationURL = b.LocationURL,
                LocationName = b.LocationName
            }).ToList();
        }
        [HttpGet("{id}")]
        public ActionResult Details(int id)
        {
            var bank = _context.BankBranches.Find(id);
            if (bank == null) 
            { 
            return NotFound();
            }
            return Ok(new BankBranch
            {
                BranchManager = bank.BranchManager,
                LocationURL = bank.LocationURL,
                LocationName = bank.LocationName
            });
        }
        [HttpPatch("{id}")]
        public ActionResult Edit(int id, AddBankRequest req) 
        {
            var bank = _context.BankBranches.Find(id);
            bank.LocationName = req.LocationName;
            bank.LocationURL = req.LocationURL;
            bank.BranchManager = req.BranchManager;
            bank.EmployeeCount = req.EmployeeCount;
            _context.SaveChanges();
            return Created(nameof(Details), new { Id = bank.Id});
        }
        [HttpPost]
        public IActionResult Add(AddBankRequest req)
        {
            var bank = new BankBranch();
            bank.LocationName = req.LocationName;
            bank.LocationURL = req.LocationURL;
            bank.BranchManager = req.BranchManager;
            bank.EmployeeCount = req.EmployeeCount;
            _context.BankBranches.Add(bank);
            _context.SaveChanges();
            return Created(nameof(Details), new { Id = bank.Id });
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var bank = _context.BankBranches.Find(id);
            _context.BankBranches.Remove(bank);
            _context.SaveChanges();
            return Ok();
        }
    }
}
