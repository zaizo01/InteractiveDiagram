using AutoMapper;
using DIGEIG.Api.Dto;
using DIGEIG.Api.Validator;
using DIGEIG.Application.Filter;
using DIGEIG.Application.Interfaces.Core;
using DIGEIG.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DIGEIG.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SysInstitutionController : ControllerBase
    {
        private IRepositoryServices<Sys_Tb_Institutions> _sysInstitutionService;
        private readonly IMapper _mapper;

        public SysInstitutionController(IRepositoryServices<Sys_Tb_Institutions> institutionService, IMapper mapper)
        {
            _sysInstitutionService = institutionService;
            _mapper = mapper;
        }

        [HttpGet("SearchRegistry")]
        public async Task<IActionResult> SearchRegistry(string filter)
        {
            try
            {
                var list = _mapper.Map<List<Sys_Tb_InstitutionsWithIdDto>>(await _sysInstitutionService.SearchRegistryAsync(filter));
               
                return Ok(list);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Response", ex.GetBaseException().Message);
                return StatusCode(404, ModelState);
            }
        }

        [HttpGet("filter")]
        public async Task<IActionResult> GetAll([FromQuery] PaginationFilter filter)
        {
            try
            {
                var list = _mapper.Map<List<Sys_Tb_InstitutionsWithIdDto>>(await _sysInstitutionService.GetFilterRecords(filter));
          
                return Ok(list);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Response", ex.GetBaseException().Message);
                return StatusCode(404, ModelState);
            }
        }

        [HttpGet()]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
               
                var list = _mapper.Map<List<Sys_Tb_InstitutionsWithIdDto>>(await _sysInstitutionService.GetAllAsync());
                return Ok(list);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Response", ex.GetBaseException().Message);
                return StatusCode(404, ModelState);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
               
                var obj = _mapper.Map<Sys_Tb_InstitutionsWithIdDto>(await _sysInstitutionService.GetAsync(id));
                if (obj == null) return NotFound();
                return Ok(obj);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Response", ex.GetBaseException().Message);
                return StatusCode(404, ModelState);
            }
        }

        [HttpGet("GetForReference")]
        public async Task<IActionResult> GetForReference(int referenceId)
        {
            try
            {
                var obj = _mapper.Map<List<Sys_Tb_InstitutionsWithIdDto>>(await _sysInstitutionService.FindAsync(t => t.ReferenceID == referenceId));

                //var obj = await _sysInstitutionService.FindAsync(t => t.ReferenceID == referenceId);
                if (obj == null || obj.Count == 0) return NotFound();
                return Ok(obj.FirstOrDefault());
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Response", ex.GetBaseException().Message);
                return StatusCode(404, ModelState);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Sys_Tb_InstitutionsDto institution)
        {
            try
            {
                var tb_Institution = _mapper.Map<Sys_Tb_Institutions>(institution);

                InstitutionsValidator validationRules = new InstitutionsValidator();
                var result = validationRules.Validate(tb_Institution);
                if (result.IsValid)
                {

                    if (await _sysInstitutionService.ExistsAsync(t => t.Name == tb_Institution.Name))
                    {
                        ModelState.AddModelError("Response", $"ya existe un registro con el nombre {tb_Institution.Name}");
                        return StatusCode(404, ModelState);
                    }

                    if (await _sysInstitutionService.InsertAsync(tb_Institution))
                    {
                        return StatusCode(200, new { Id = tb_Institution.InstitutionId });
                    }
                    else
                    {
                        ModelState.AddModelError("Response", $"Ha ocurrido un error al intentar guardar el registro {tb_Institution.Name}");

                        return StatusCode(500, ModelState);
                    }
                }
                else
                {

                    foreach (var failure in result.Errors)
                    { 
                        ModelState.AddModelError("Response", " Propiedad " + failure.PropertyName + " validación fallida. El error fue: " + failure.ErrorMessage);

                    }

                    return StatusCode(500, ModelState);
                }

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Response", ex.GetBaseException().Message);
                return StatusCode(404, ModelState);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Sys_Tb_InstitutionsWithIdDto institution)
        {
            try
            {
                var tb_Institution = _mapper.Map<Sys_Tb_Institutions>(institution);

                InstitutionsValidator validationRules = new InstitutionsValidator();
                var result = validationRules.Validate(tb_Institution);
                if (result.IsValid)
                {
                  
                    if (!await _sysInstitutionService.UpdateAsync(tb_Institution))
                    {
                        ModelState.AddModelError("Response", $"Ha ocurrido un error al intentar actualizar el registro {tb_Institution.Name}");
                        return StatusCode(500, ModelState);
                    }

                    return StatusCode(200, new { Id = tb_Institution.InstitutionId });
                }
                else
                {

                    foreach (var failure in result.Errors)
                    {
                        ModelState.AddModelError("Response", " Propiedad " + failure.PropertyName + " validación fallida. El error fue: " + failure.ErrorMessage);

                    }

                    return StatusCode(500, ModelState);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Response", ex.GetBaseException().Message);
                return StatusCode(404, ModelState);
            }

        }

        [HttpDelete("{id}/{useEmail}")]
        public async Task<IActionResult> Delete(Guid id, string useEmail)
        {
            try
            {
                if (!await _sysInstitutionService.ExistsAsync(t=>t.Id == id)) return NotFound();

                var obj = await _sysInstitutionService.GetAsync(id);

                obj.IsActive = false;

                obj.LastModifyBy = useEmail;

                if (!await _sysInstitutionService.UpdateAsync(obj))
                {
                    ModelState.AddModelError("Response", $"Ha ocurrido un error al intentar eliminar el registro {obj.Name}");
                    return StatusCode(500, ModelState);
                }

                return Ok();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Response", ex.GetBaseException().Message);
                return StatusCode(404, ModelState);
            }

        }



    }
}
