using DIGEIG.Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DIGEIG.Api.Validator
{
    public class InstitutionsStructureValidator : AbstractValidator<Sys_Tb_InstitutionsStructure>
    {
        public InstitutionsStructureValidator()
        {
            RuleFor(x => x.InstitutionId).NotEmpty().WithMessage("Por favor especifique la insitucion "); 
            RuleFor(x => x.Name).NotEmpty().WithMessage("Por favor especifique el nombre  de el departamento");
            RuleFor(x => x.Name).MaximumLength(200).WithMessage("No puede ser mayor a 250 caracteres");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Por favor especifique una descripción");
        }
 
    }
}
