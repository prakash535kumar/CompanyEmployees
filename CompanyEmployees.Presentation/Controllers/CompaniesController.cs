﻿using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyEmployees.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly IServiceManager _service;

        public CompaniesController(IServiceManager service) => _service = service;

        //[HttpGet]
        //public IActionResult GetCompanies()
        //{
        //    try
        //    {
        //        IEnumerable<CompanyDto> companies =
        //        _service.CompanyService.GetAllCompanies(trackChanges: false);
        //        return Ok(companies);
        //    }
        //    catch
        //    {
        //        return StatusCode(500, "Internal server error");
        //    }
        //}

        [HttpGet]
        public IActionResult GetCompanies()
        {
            //throw new Exception("Exception");   // For Testing Purpose

            var companies = _service.CompanyService.GetAllCompanies(trackChanges: false);
            return Ok(companies);
        }

        [HttpGet("{id:guid}")]
        public IActionResult GetCompany(Guid id)
        {
            var company = _service.CompanyService.GetCompany(id, trackChanges: false);
            return Ok(company);
        }
    }
}
