using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Generad.Models
{
    public class DocumentalModel
    {
        [Display(Name = "Nombre_form", ResourceType = typeof(Resources.Contacto))]
        [Required(ErrorMessageResourceType = typeof(Resources.Contacto), ErrorMessageResourceName = "Nombre_form_error")]
        public string Nombre_form
        {
            get;
            set;
        }
      
        [Display(Name = "Email_form", ResourceType = typeof(Resources.Contacto))]
        [Required(ErrorMessageResourceType = typeof(Resources.Contacto), ErrorMessageResourceName = "Email_form_error")]
        [EmailAddress(ErrorMessageResourceType = typeof(Resources.Contacto), ErrorMessageResourceName = "Email_no_es")]
        //[RegularExpression(@"^([0-9a-zA-Z]([\+\-_\.][0-9a-zA-Z]+)*)+@(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]*\.)+[a-zA-Z0-9]{2,3})$", ErrorMessageResourceType = typeof(Resources.Contacto), ErrorMessageResourceName = "Email_form_error")]
        public string Email_form
        {
            get;
            set;
        }

        [Display(Name = "Mensaje_form", ResourceType = typeof(Resources.Contacto))]
        [Required(ErrorMessageResourceType = typeof(Resources.Contacto), ErrorMessageResourceName = "Mensaje_form_error")]
        public string Mensaje_form
        {
            get;
            set;
        }
        
        [Required(ErrorMessageResourceType = typeof(Resources.Documental_form), ErrorMessageResourceName = "tipo_error")]
        public int Tipo_form
        {
            get;
            set;
        }
        [Required(ErrorMessageResourceType = typeof(Resources.Documental_form), ErrorMessageResourceName = "tipo_error_2")]
        public int Tipo_form2
        {
            get;
            set;
        }
    }
    public class Categorias
    {
        public int Id 
        {
            get;
            set;
        }
        public string Name 
        {
            set;
            get;
        }
    }
}