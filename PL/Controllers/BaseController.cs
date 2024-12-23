﻿using Entities.Models;
using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public abstract class BaseController : ControllerBase
    {
        protected IActionResult HandleServiceResult<T>(ServiceResult<T> result)
        {
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return StatusCode(result.StatusCode, result.ErrorMessage);
        }
    }
}