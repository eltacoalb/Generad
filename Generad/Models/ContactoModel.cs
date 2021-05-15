using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace Generad.Models
{
    public class ContactoModel
    {
        [Display(Name = "Nombre_form", ResourceType = typeof(Resources.Contacto))]
        [Required(ErrorMessageResourceType = typeof(Resources.Contacto), ErrorMessageResourceName = "Nombre_form_error")]
        public string Nombre_form
        {
            get;
            set;
        }

        [Display(Name = "NombreCompania_form", ResourceType = typeof(Resources.Contacto))]
        [Required(ErrorMessageResourceType = typeof(Resources.Contacto), ErrorMessageResourceName = "NombreCompania_form_error")]
        public string NombreCompania_form
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

        [Display(Name = "Telefono_form", ResourceType = typeof(Resources.Contacto))]
        [Required(ErrorMessageResourceType = typeof(Resources.Contacto), ErrorMessageResourceName = "Telefono_form_error")]
        [Phone(ErrorMessageResourceType = typeof(Resources.Contacto), ErrorMessageResourceName = "Telefono_no_es")]
        //[RegularExpression(@"0*[1-9][0-9]*", ErrorMessageResourceType = typeof(Resources.Contacto), ErrorMessageResourceName = "Telefono_form_error")]
        public int Telefono_form
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
        [Required(ErrorMessageResourceType = typeof(Resources.Contacto), ErrorMessageResourceName = "Mensaje_form_error")]
        public string Tipo_form
        {
            get;
            set;
        }
        [Required(ErrorMessageResourceType = typeof(Resources.Contacto), ErrorMessageResourceName = "Mensaje_form_error")]
        public string Tipo_form_1
        {
            get;
            set;
        }
    }
}