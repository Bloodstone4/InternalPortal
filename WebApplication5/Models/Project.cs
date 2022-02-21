using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication5.Models
{
    public class Project
    {
        public int Id { get; set; }
        [Required (ErrorMessage ="Заполните внутренний номер проекта.")]
        public string InternalNum { get; set; }
        public string ContractNumber { get; set; }
        public string ShortName { get; set; }
        [Required(ErrorMessage = "Заполните наименование проекта.")]
        public string FullName { get; set; }
        public User Manager { get; set; }
        public List<Corrections> Corrections { get; set; }
    }
}
