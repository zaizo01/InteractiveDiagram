using AutoMapper;
using DIGEIG.Api.Dto;
using DIGEIG.Api.Validator;
using DIGEIG.Application.Filter;
using DIGEIG.Application.Interfaces;
using DIGEIG.Application.Interfaces.Core;
using DIGEIG.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DIGEIG.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SysInstitutionsStructure : ControllerBase
    {
        private ISysInstitutionsStructureService<Sys_Tb_InstitutionsStructure> _sysStructureService;
        private IRepositoryServices<Sys_Tb_Institutions> _sysInstitutionService;
        private readonly IMapper _mapper;

        public SysInstitutionsStructure(IRepositoryServices<Sys_Tb_Institutions> sysInstitutionService, ISysInstitutionsStructureService<Sys_Tb_InstitutionsStructure> institutionService, IMapper mapper)
        {
            _sysStructureService = institutionService;
            _sysInstitutionService = sysInstitutionService;
            _mapper = mapper;
        }

        [HttpGet("SearchRegistry")]
        public async Task<IActionResult> SearchRegistry(string filter)
        {
            try
            {
                var list = _mapper.Map<List<Sys_Tb_InstitutionsStructureWithIdDto>>(await _sysStructureService.SearchRegistryAsync(filter));

                return Ok(list);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Response", ex.GetBaseException().Message);
                return StatusCode(404, ModelState);
            }
        }
        [HttpGet("GetRecordsByInstitutionId")]
        public async Task<IActionResult> GetRecordsByInstitutionId(int InstitutionId)
        {
            try
            {
                var list = await _sysStructureService.GetRecordsByInstitutionIdAsync(InstitutionId);
           
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

                var list = _mapper.Map<List<Sys_Tb_InstitutionsStructureWithIdDto>>(await _sysStructureService.GetFilterRecords(filter));
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
                var list = _mapper.Map<List<Sys_Tb_InstitutionsStructureWithIdDto>>(await _sysStructureService.GetAllAsync());
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
                var obj = _mapper.Map<Sys_Tb_InstitutionsStructureWithIdDto>(await _sysStructureService.GetAsync(id));

                if (obj == null) return NotFound();
                return Ok(obj);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Response", ex.GetBaseException().Message);
                return StatusCode(404, ModelState);
            }
        }
        [HttpGet("GetDiagramRecords/{InstitutionId}")]
        public async Task<IActionResult> GetDiagramRecords(int InstitutionId)
        {
            try
            {
              
                var obj = JsonConvert.SerializeObject(await _sysStructureService.GetDiagramRecordsByInstitutionId(InstitutionId));

                if (obj == null) return NotFound();
                return Ok(obj);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Response", ex.GetBaseException().Message);
                return StatusCode(404, ModelState);
            }
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Sys_Tb_InstitutionsStructureDto institution)
        {
            try
            {
                if (!await _sysInstitutionService.ExistsAsync(t => t.InstitutionId == institution.InstitutionId))                 
                {
                    ModelState.AddModelError("Response", $"Esta institución no existe");
                    return StatusCode(404, ModelState);
                }
                var tb_Institution = _mapper.Map<Sys_Tb_InstitutionsStructure>(institution);

                InstitutionsStructureValidator validationRules = new InstitutionsStructureValidator();
                var result = validationRules.Validate(tb_Institution);
                if (result.IsValid)
                {
                    var institutionFound = await _sysStructureService.FindAsync(t => t.InstitutionId == institution.InstitutionId && tb_Institution.MainInstitutionStructureId == 0);
                    if (institutionFound.Count == 1 && tb_Institution.InstitutionStructureId == 0)
                    {
                        ModelState.AddModelError("Response", $"este nivel de departamento existe");
                        return StatusCode(404, ModelState);
                    }
                    if (await _sysStructureService.ExistsAsync(t => t.Name == tb_Institution.Name 
                    && t.InstitutionId == institution.InstitutionId))
                    {
                        ModelState.AddModelError("Response", $"ya existe un registro con el nombre {tb_Institution.Name}");
                        return StatusCode(404, ModelState);
                    }
            

                    if (await _sysStructureService.InsertAsync(tb_Institution))
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
        public async Task<IActionResult> Update([FromBody] Sys_Tb_InstitutionsStructureWithIdDto institution)
        {
            try
            {
                var tb_Institution = _mapper.Map<Sys_Tb_InstitutionsStructure>(institution);
                if (!await _sysInstitutionService.ExistsAsync(t => t.InstitutionId == institution.InstitutionId))
                {
                    ModelState.AddModelError("Response", $"Esta institución no existe");
                    return StatusCode(404, ModelState);
                }
                InstitutionsStructureValidator validationRules = new InstitutionsStructureValidator();
                var result = validationRules.Validate(tb_Institution);
                if (result.IsValid)
                {
                    var institutionFound = await _sysStructureService.FindAsync(t => t.InstitutionId == institution.InstitutionId && t.MainInstitutionStructureId == 0);
                    if (institutionFound.Count == 1 && tb_Institution.InstitutionStructureId == 0)
                    {
                        ModelState.AddModelError("Response", $"este nivel de departamento existe");
                        return StatusCode(404, ModelState);
                    }
                    var instritucte = await _sysStructureService.FindAsync(t => t.Name == tb_Institution.Name);
                    foreach (var item in instritucte)
                    {
                        if (item.Id != institution.Id)
                        {
                            ModelState.AddModelError("Response", $"ya existe un registro con el nombre {tb_Institution.Name}");
                            return StatusCode(404, ModelState);
                        }
                    }
                   
                    var insititucionStructurUpdate = await _sysStructureService.GetAsync(institution.Id);
                    insititucionStructurUpdate.InstitutionStructureId = tb_Institution.InstitutionStructureId;
                    insititucionStructurUpdate.InstitutionId = tb_Institution.InstitutionId;
                    insititucionStructurUpdate.Name = tb_Institution.Name;
                    insititucionStructurUpdate.Description = tb_Institution.Description;
                    insititucionStructurUpdate.MainInstitutionStructureId = tb_Institution.MainInstitutionStructureId;
                    insititucionStructurUpdate.ListInstitutionsStructure = tb_Institution.ListInstitutionsStructure;
                    if (!await _sysStructureService.UpdateAsync(insititucionStructurUpdate))
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
                if (!await _sysStructureService.ExistsAsync(t => t.Id == id)) return NotFound();
              
                var obj = await _sysStructureService.GetAsync(id);
                var _childrens = await _sysStructureService.FindAsync(x => x.MainInstitutionStructureId == obj.InstitutionStructureId);
                if (_childrens.Count > 0)
                {
                    ModelState.AddModelError("Response", $"Este departamento tiene dependientes ");
                    return StatusCode(500, ModelState);
                }
                obj.IsActive = false;

                obj.LastModifyBy = useEmail;

                if (!await _sysStructureService.UpdateAsync(obj))
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
