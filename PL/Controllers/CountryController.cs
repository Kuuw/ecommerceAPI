﻿using Asp.Versioning;
using AutoMapper;
using BAL.Concrete;
using Entities.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ApiVersion("1.0")]
    public class CountryController : Controller
    {
        CountryService countryService = new CountryService();
        Mapper mapper = MapperConfig.InitializeAutomapper();

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Country()
        {
            return Ok();
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public IActionResult Country(int id)
        {
            return Ok();
        }

        [HttpPost]
        [Authorize(Roles="Admin")]
        public IActionResult Country(CountryDTO countryDTO)
        {
            return Ok();
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Country(int id, CountryDTO countryDTO)
        {
            return Ok();
        }
    }
}