using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public record BookDtoForInsertion : BookDtoForManipulation
    {
        [Display(Name = "Category")]
        [Required(ErrorMessage = "{0} is required")]
        public int CategoryId { get; init; }
    }
}
