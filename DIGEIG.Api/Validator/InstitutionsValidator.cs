using DIGEIG.Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DIGEIG.Api.Validator
{
    public class InstitutionsValidator : AbstractValidator<Sys_Tb_Institutions>
    {
        public InstitutionsValidator()
        {
            RuleFor(x => x.ReferenceID).NotEmpty().GreaterThan(99).WithMessage("Por favor especifique el una referencia de 3 digitos"); 
            RuleFor(x => x.Name).NotEmpty().WithMessage("Por favor especifique el nombre la la institucion").Length(3, 250); 
        }
 
    }
}
