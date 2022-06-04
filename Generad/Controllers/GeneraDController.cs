using Generad.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Reflection;
using System.Runtime.Remoting.Contexts;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace Generad.Controllers
{
    public class GeneraDController : Controller
    {
        // GET: GeneraD
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Holding()
        {
            return View();
        }

        public ActionResult Nosotros()
        {
            return View();
        }

        public ActionResult Comercializacion()
        {
            var outPutDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase).ToString().Replace("bin", "");
            string serverpath = Path.Combine(outPutDirectory, "Content\\img\\generad\\Productos\\").Replace("file:\\", "");
            string[] files;
            int numFiles;
            List<string> ListBox1 = new List<string>();
            foreach (var d in System.IO.Directory.GetFiles(serverpath))
            {
                var dirName = new DirectoryInfo(d).Name;
                ListBox1.Add(dirName);
            }
            files = System.IO.Directory.GetFiles(serverpath);

            numFiles = files.Length;
            CatalogoModel catalogoModel = new CatalogoModel();
            catalogoModel.ImagesName = ListBox1;
            catalogoModel.ImagesCount = numFiles;
            return View(catalogoModel);
        }
        public ActionResult IntegracionPersonal()
        {
            return View();
        }

        public ActionResult Documental()
        {
            return View();
        }
        public JsonResult SetDocument(int id)
        {
            List<SelectListItem> listItems = new List<SelectListItem>();
            if (id != 0)
            {
                listItems.Add(new SelectListItem
                {
                    Text = "-- " + Resources.Documental_form.opcion + " --",
                    Value = "0",
                    Selected = true
                });


                if (id == 1)
                {
                    listItems.Add(new SelectListItem
                    {
                        Text = Resources.Documental_form.opcion_1,
                        Value = "1"
                    });

                    listItems.Add(new SelectListItem
                    {
                        Text = Resources.Documental_form.opcion_2,
                        Value = "2"
                    });
                }
                if (id == 2)
                {
                    listItems.Add(new SelectListItem
                    {
                        Text = Resources.Documental_form.opcion_3,
                        Value = "1"
                    });

                    listItems.Add(new SelectListItem
                    {
                        Text = Resources.Documental_form.opcion_4,
                        Value = "2"
                    });
                    listItems.Add(new SelectListItem
                    {
                        Text = Resources.Documental_form.opcion_5,
                        Value = "3"
                    });

                    listItems.Add(new SelectListItem
                    {
                        Text = Resources.Documental_form.opcion_6,
                        Value = "4"
                    });
                }
            }

            return Json(listItems, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Contacto()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Documetal_form(DocumentalModel documentalModel)
        {
            if (ModelState.IsValid)
            {
                if (documentalModel.Tipo_form != 0) 
                {
                    if (documentalModel.Tipo_form2 != 0) 
                    {
                        string correoPadreAsistencia = "";
                        string lang = Thread.CurrentThread.CurrentCulture.Name.ToString();
                        if (lang == "es")
                        {
                            correoPadreAsistencia = ConfigurationManager.AppSettings["CorreoPadreES"];
                        }
                        else if (lang == "en")
                        {
                            correoPadreAsistencia = ConfigurationManager.AppSettings["CorreoPadreEN"];
                        }
                        MailMessage mail2 = new MailMessage();
                        mail2.From = new MailAddress(correoPadreAsistencia);
                        mail2.To.Add(new MailAddress(correoPadreAsistencia));
                        mail2.Subject = "Te han solicitado documentación";
                        mail2.Body = BodyReceiverDocumental(documentalModel);
                        mail2.IsBodyHtml = true;
                        SmtpClient smtp2 = new SmtpClient();
                        smtp2.Host = "relay-hosting.secureserver.net";
                        smtp2.Port = 25;
                        smtp2.UseDefaultCredentials = false;
                        smtp2.Credentials = new System.Net.NetworkCredential(correoPadreAsistencia, ConfigurationManager.AppSettings["contrseñaPadre"]); // Enter seders User name and password       
                        smtp2.EnableSsl = false;
                        smtp2.Send(mail2);
                    }
                }
            }
            return View("Documental");
        }
        [HttpPost]
        public ActionResult CreateEmail(ContactoModel contactoModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string subject = "";
                    string correoPadreAsistencia = "";
                    string lang = Thread.CurrentThread.CurrentCulture.Name.ToString();
                    if (lang == "es")
                    {
                        correoPadreAsistencia = ConfigurationManager.AppSettings["CorreoPadreES"];
                        subject = Resources.Contacto.CorreoContacto_3;
                    }
                    else if (lang == "en")
                    {
                        correoPadreAsistencia = ConfigurationManager.AppSettings["CorreoPadreEN"];
                        subject = Resources.Contacto.CorreoContacto_3;
                    }
                    MailMessage mail = new MailMessage();
                    //string contactEmail = contactoModel.Email_form;
                    //mail.To.Add("tusnudesamicorreo@gmail.com");
                    //mail.From = new MailAddress("yournudestomyemail@gmail.com");
                    //mail.Subject = Resources.Contacto.Boton_form;
                    ////string Body = contactoModel.Mensaje_form;
                    //mail.Body = BodyContact(contactoModel);
                    ////mail.Body = Body;
                    //mail.IsBodyHtml = true;
                    //SmtpClient smtp = new SmtpClient();
                    //smtp.Host = "smtp.gmail.com";
                    //smtp.Port = 587;
                    //smtp.UseDefaultCredentials = false;
                    //smtp.Credentials = new System.Net.NetworkCredential("yournudestomyemail@gmail.com", "ubunto5@R"); // Enter seders User name and password       
                    //smtp.EnableSsl = true;
                    //smtp.Send(mail);
                    //smtp.Send(mail);
                    mail = new MailMessage();
                    mail.From = new MailAddress(correoPadreAsistencia);
                    mail.To.Add(new MailAddress(contactoModel.Email_form.ToString()));
                    mail.Subject = Resources.Contacto.CorreoContacto;
                    mail.Body = BodyContact(contactoModel);
                    mail.IsBodyHtml = true;
                    SmtpClient smtp2 = new SmtpClient();
                    smtp2.Host = "relay-hosting.secureserver.net";
                    smtp2.Port = 25;
                    smtp2.UseDefaultCredentials = false;
                    smtp2.Credentials = new System.Net.NetworkCredential(correoPadreAsistencia, ConfigurationManager.AppSettings["contraseñaPadre"]); // Enter seders User name and password       
                    smtp2.EnableSsl = false;
                    smtp2.Send(mail);
                    
                    mail = new MailMessage();
                    mail.From = new MailAddress(correoPadreAsistencia);
                    mail.To.Add(new MailAddress(correoPadreAsistencia));
                    mail.Subject = subject;
                    mail.Body = BodyReceiver(contactoModel);
                    mail.IsBodyHtml = true;
                    smtp2.Send(mail);
                }
                contactoModel = new ContactoModel();
                //return View("Contacto");
                //return Json("OK", JsonRequestBehavior.AllowGet);
                return RedirectToAction("Contacto", "GeneraD", contactoModel);
                // TODO: Add insert logic here

            }
            catch (Exception e)
            {
                //return View();
                return Json(e.Message.ToString());
            }
        }

        public string BodyReceiver(ContactoModel contactoModel)
        {
            #region correo
            string bodyhtml = @"
<!DOCTYPE html PUBLIC ' -//W3C//DTD XHTML 1.0 Transitional//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd'>
<html xmlns = 'http://www.w3.org/1999/xhtml' xmlns: o = 'urn:schemas-microsoft-com:office:office' >
    <head>
        <meta charset = 'UTF-8'>
         <meta content = 'width=device-width, initial-scale=1' name = 'viewport'>
            <meta name = 'x-apple-disable-message-reformatting'>
             <meta http - equiv = 'X-UA-Compatible' content = 'IE=edge'>
                  <meta content = 'telephone=no' name = 'format-detection'>
                     <title ></title>
                     <!--[if (mso 16)]>
                      <style type = 'text/css'>
                       a { text - decoration: none; }
    </style>
    <![endif]-- >
    <!--[if gte mso 9]>< style > sup { font - size: 100 % !important; }</style><![endif]-->
           <!--[if gte mso 9]>
        <xml>
            <o:OfficeDocumentSettings>
             <o:AllowPNG ></ o:AllowPNG>
                <o:PixelsPerInch> 96 </o:PixelsPerInch>
                     </o:OfficeDocumentSettings>
                  </xml>
                  <![endif]-- >
                  </head>
                  <body>
                      <div class='es-wrapper-color'>
        <!--[if gte mso 9]>
			<v:background xmlns:v='urn:schemas-microsoft-com:vml' fill='t'>
				<v:fill type = 'tile' color='#eeeeee'></v:fill>
			</v:background>
		<![endif]-->
        <table class='es-wrapper' width='100%' cellspacing='0' cellpadding='0'>
            <tbody>
                <tr>
                    <td class='esd-email-paddings' valign='top'>
                        <table class='es-content esd-header-popover' cellspacing='0' cellpadding='0' align='center'>
                            <tbody>
                                <tr></tr>
                                <tr>
                                    <td class='esd-stripe' esd-custom-block-id='7799' align='center'>
                                        <table class='es-header-body' style='background-color: #044767;' width='600' cellspacing='0' cellpadding='0' bgcolor='#044767' align='center'>
                                            <tbody>
                                                <tr>
                                                    <td class='esd-structure es-p35t es-p40b es-p35r es-p35l' align='left'>
                                                        <table width = '100%' cellspacing='0' cellpadding='0'>
                                                            <tbody>
                                                                <tr>
                                                                    <td class='esd-container-frame' width='530' valign='top' align='center'>
                                                                        <table width = '100%' cellspacing='0' cellpadding='0'>
                                                                            <tbody>
                                                                                <tr>
                                                                                    <td class='esd-block-text es-m-txt-c' align='center'>
                                                                                        <h1 style = 'color: #ffffff; line-height: 100%;' > " + Resources.Contacto.CorreoContacto + @" </h1>
                                                                                    </td>
                                                                                </tr>
                                                                            </tbody>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                        <table class='es-content' cellspacing='0' cellpadding='0' align='center'>
                            <tbody>
                                <tr>
                                    <td class='esd-stripe' align='center'>
                                        <table class='es-content-body' width='600' cellspacing='0' cellpadding='0' bgcolor='#ffffff' align='center'>
                                            <tbody>
                                                <tr>
                                                    <td class='esd-structure es-p35t es-p25b es-p35r es-p35l' esd-custom-block-id='7811' align='left'>
                                                        <table width = '100%' cellspacing='0' cellpadding='0'>
                                                            <tbody>
                                                                <tr>
                                                                    <td class='esd-container-frame' width='530' valign='top' align='center'>
                                                                        <table width = '100%' cellspacing='0' cellpadding='0'>
                                                                            <tbody>
                                                                                <tr>
                                                                                    <td class='esd-block-text es-p20t es-p5b' align='left'>
                                                                                        <h3 style = 'color: #333333;' >" + Resources.Contacto.CorreoContacto_3 + @"<br></h3>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class='esd-block-text es-p5t es-p10b' align='left'>
                                                                                        <p style = 'font-size: 16px; color: #777777;' >" + Resources.Contacto.CorreoContacto_4 + @": " + contactoModel.Nombre_form + @"</p>
                                                                                        <p style = 'font-size: 16px; color: #777777;' >" + Resources.Contacto.CorreoContacto_9 + @": " + contactoModel.NombreCompania_form + @"</p>
                                                                                        <p style = 'font-size: 16px; color: #777777;' >" + Resources.Contacto.CorreoContacto_7 + @": " + contactoModel.Mensaje_form + @"</p>
                                                                                        <p style = 'font-size: 16px; color: #777777;' >" + Resources.Contacto.CorreoContacto_6 + @": " + contactoModel.Email_form + @"</p>
                                                                                        <p style = 'font-size: 16px; color: #777777;' >" + Resources.Contacto.CorreoContacto_8 + @": " + contactoModel.Telefono_form + @"</p>
                                                                                    </td>
                                                                                </tr>
                                                                            </tbody>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                        <table class='es-content' cellspacing='0' cellpadding='0' align='center'>
                            <tbody>
                                <tr>
                                    <td class='esd-stripe' align='center'>
                                        <table class='es-content-body' width='600' cellspacing='0' cellpadding='0' bgcolor='#ffffff' align='center'>
                                            <tbody>
                                                <tr>
                                                    <td class='esd-structure es-p15t es-p35r es-p35l' align='left'>
                                                        <table width = '100%' cellspacing='0' cellpadding='0'>
                                                            <tbody>
                                                                <tr>
                                                                    <td class='esd-container-frame' width='530' valign='top' align='center'>
                                                                        <table width = '100%' cellspacing='0' cellpadding='0'>
                                                                            <tbody>
                                                                                <tr>
                                                                                    <td class='esd-block-image' align='center' style='font-size:0'><a target = '_blank'><img src='https://tlr.stripocdn.email/content/guids/CABINET_75694a6fc3c4633b3ee8e3c750851c02/images/18501522065897895.png' alt style = 'display: block;' width='46'></a></td>
                                                                                </tr>
                                                                            </tbody>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                        <table cellpadding = '0' cellspacing='0' class='es-content esd-footer-popover' align='center'>
                            <tbody>
                                <tr>
                                    <td class='esd-stripe' esd-custom-block-id='7766' align='center'>
                                        <table class='es-content-body' style='border-bottom:10px solid #48afb5;background-color: #1b9ba3;' width='600' cellspacing='0' cellpadding='0' bgcolor='#1b9ba3' align='center'>
                                            <tbody>
                                                <tr>
                                                    <td class='esd-structure' align='left'>
                                                        <table width = '100%' cellspacing='0' cellpadding='0'>
                                                            <tbody>
                                                                <tr>
                                                                    <td class='esd-container-frame' width='600' valign='top' align='center'>
                                                                        <table width = '100%' cellspacing='0' cellpadding='0'>
                                                                            <tbody>
                                                                                <tr>
                                                                                    <td align = 'left' class='esd-block-text'>
                                                                                    </td>
                                                                                </tr>
                                                                            </tbody>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
</body>
</html>";
            #endregion

            return bodyhtml;
        }
        public string BodyContact(ContactoModel contactoModel)
        {
            #region Correo
            string bodyhtml = @"
              <!DOCTYPE html PUBLIC '-//W3C//DTD XHTML 1.0 Transitional//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd'>
<html xmlns='http://www.w3.org/1999/xhtml' xmlns:o='urn:schemas-microsoft-com:office:office'>

<head>
    <meta charset='UTF-8'>
    <meta content='width=device-width, initial-scale=1' name='viewport'>
    <meta name='x-apple-disable-message-reformatting'>
    <meta http-equiv='X-UA-Compatible' content='IE=edge'>
    <meta content='telephone=no' name='format-detection'>
    <title></title>
    <!--[if (mso 16)]>
    <style type='text/css'>
    a {text-decoration: none;}
    </style>
    <![endif]-->
    <!--[if gte mso 9]><style>sup { font-size: 100% !important; }</style><![endif]-->
    <!--[if gte mso 9]>
<xml>
    <o:OfficeDocumentSettings>
    <o:AllowPNG></o:AllowPNG>
    <o:PixelsPerInch>96</o:PixelsPerInch>
    </o:OfficeDocumentSettings>
</xml>
<![endif]-->
</head>

<body>
    <div class='es-wrapper-color'>
        <!--[if gte mso 9]>
            <v:background xmlns:v='urn:schemas-microsoft-com:vml' fill='t'>
                <v:fill type='tile' color='#eeeeee'></v:fill>
            </v:background>
        <![endif]-->
        <table class='es-wrapper' width='100%' cellspacing='0' cellpadding='0'>
            <tbody>
                <tr>
                    <td class='esd-email-paddings' valign='top'>
                        <table class='es-content esd-header-popover' cellspacing='0' cellpadding='0' align='center'>
                            <tbody>
                                <tr></tr>
                                <tr>
                                    <td class='esd-stripe' esd-custom-block-id='7799' align='center'>
                                        <table class='es-header-body' style='background-color: #044767;' width='600' cellspacing='0' cellpadding='0' bgcolor='#044767' align='center'>
                                            <tbody>
                                                <tr>
                                                    <td class='esd-structure es-p35t es-p40b es-p35r es-p35l' align='left'>
                                                        <table width='100%' cellspacing='0' cellpadding='0'>
                                                            <tbody>
                                                                <tr>
                                                                    <td class='esd-container-frame' width='530' valign='top' align='center'>
                                                                        <table width='100%' cellspacing='0' cellpadding='0'>
                                                                            <tbody>
                                                                                <tr>
                                                                                    <td class='esd-block-text es-m-txt-c' align='center'>
                                                                                        <h1 style='color: #ffffff; line-height: 100%;'>Genera-D</h1>
                                                                                    </td>
                                                                                </tr>
                                                                            </tbody>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class='esd-structure es-p20t es-p35r es-p35l' align='left'>
                                                        <table cellpadding='0' cellspacing='0' width='100%'>
                                                            <tbody>
                                                                <tr>
                                                                    <td width='530' class='esd-container-frame' align='center' valign='top'>
                                                                        <table cellpadding='0' cellspacing='0' width='100%'>
                                                                            <tbody>
                                                                                <tr>
                                                                                    <td class='esd-block-menu'>
                                                                                        <table cellpadding='0' cellspacing='0' width='100%' class='es-menu'>
                                                                                            <tbody>
                                                                                             
                                                                                            </tbody>
                                                                                        </table>
                                                                                    </td>
                                                                                </tr>
                                                                            </tbody>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                        <table class='es-content' cellspacing='0' cellpadding='0' align='center'>
                            <tbody>
                                <tr>
                                    <td class='esd-stripe' align='center'>
                                        <table class='es-content-body' width='600' cellspacing='0' cellpadding='0' bgcolor='#ffffff' align='center'>
                                            <tbody>
                                                <tr>
                                                    <td class='esd-structure es-p20t es-p35r es-p35l' align='left'>
                                                        <table cellpadding='0' cellspacing='0' width='100%'>
                                                            <tbody>
                                                                <tr>
                                                                    <td width='530' class='esd-container-frame' align='center' valign='top'>
                                                                        <table cellpadding='0' cellspacing='0' width='100%'>
                                                                            <tbody>
                                                                                <tr>
                                                                                    <td align='center' class='esd-block-text'>
                                                                                        <h1 style='font-size: 36px;'>
                                                                                            <strong>" + Resources.Contacto.CorreoContacto_1 + @"
                                                                                            </strong>
                                                                                        </h1>
                                                                                    </td>
                                                                                </tr>
                                                                            </tbody>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class='esd-structure es-p35t es-p25b es-p35r es-p35l' esd-custom-block-id='7811' align='left'>
                                                        <table width='100%' cellspacing='0' cellpadding='0'>
                                                            <tbody>
                                                                <tr>
                                                                    <td class='esd-container-frame' width='530' valign='top' align='center'>
                                                                        <table width='100%' cellspacing='0' cellpadding='0'>
                                                                            <tbody>
                                                                                <tr>
                                                                                    <td class='esd-block-text es-p20t es-p5b' align='left'>
                                                                                        <h3 style='color: #333333;'>" + contactoModel.Nombre_form + @",<br></h3>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class='esd-block-text es-p15t es-p10b' align='left'>
                                                                                        <p style='font-size: 16px; color: #777777;'>" + Resources.Contacto.CorreoContacto_2 + @"</p>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class='esd-block-text es-p40t' align='left'>
                                                                                        <h3 style='color: #333333;'>Genera-D</h3>
                                                                                    </td>
                                                                                </tr>
                                                                            </tbody>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                        <table class='es-content' cellspacing='0' cellpadding='0' align='center'>
                            <tbody>
                                <tr>
                                    <td class='esd-stripe' align='center'>
                                        <table class='es-content-body' width='600' cellspacing='0' cellpadding='0' bgcolor='#ffffff' align='center'>
                                            <tbody>
                                                <tr>
                                                    <td class='esd-structure es-p15t es-p35r es-p35l' align='left'>
                                                        <table width='100%' cellspacing='0' cellpadding='0'>
                                                            <tbody>
                                                                <tr>
                                                                    <td class='esd-container-frame' width='530' valign='top' align='center'>
                                                                        <table width='100%' cellspacing='0' cellpadding='0'>
                                                                            <tbody>
                                                                                <tr>
                                                                                    <td class='esd-block-image' align='center' style='font-size:0'><a target='_blank'><img src='https://tlr.stripocdn.email/content/guids/CABINET_75694a6fc3c4633b3ee8e3c750851c02/images/18501522065897895.png' alt style='display: block;' width='46'></a></td>
                                                                                </tr>
                                                                            </tbody>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                        <table cellpadding='0' cellspacing='0' class='es-content esd-footer-popover' align='center'>
                            <tbody>
                                <tr>
                                    <td class='esd-stripe' esd-custom-block-id='7797' align='center'>
                                        <table class='es-content-body' style='background-color: #1b9ba3;' width='600' cellspacing='0' cellpadding='0' bgcolor='#1b9ba3' align='center'>
                                            <tbody>
                                                <tr>
                                                    <td class='esd-structure es-p40t es-p40b es-p35r es-p35l' align='left' esd-custom-block-id='7796'>
                                                        <!--[if mso]><table width='530' cellpadding='0' cellspacing='0'><tr><td width='255' valign='top'><![endif]-->
                                                        <table class='es-left' cellspacing='0' cellpadding='0' align='left'>
                                                            <tbody>
                                                                <tr>
                                                                    <td class='esd-container-frame es-m-p20b' width='255' align='left'>
                                                                        <table width='100%' cellspacing='0' cellpadding='0'>
                                                                            <tbody>
                                                                                <tr>
                                                                                    <td class='esd-block-text es-p15b' align='left'>
                                                                                        <h4>" + Resources.Menu.Contacto + @"</h4>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class='esd-block-text es-p10b' align='left'>
                                                                                        <p style='color: #ffffff;'>
                                                                                            <strong>
                                                                                                " + Resources.Contacto_div.Contacto_1 + @"
                                                                                            </strong>
                                                                                        </p>
                                                                                        <p style='color: #ffffff;'>
                                                                                            <strong>
                                                                                                 " + Resources.Contacto_div.Contacto_3 + @"
                                                                                            </strong>
                                                                                        </p>
                                                                                        <p style='color: #ffffff;'>
                                                                                            <strong>
                                                                                                " + Resources.Contacto_div.Contacto_4 + @"
                                                                                            </strong>
                                                                                        </p>
                                                                                    </td>
                                                                                </tr>
                                                                            </tbody>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                        <!--[if mso]></td><td width='20'></td><td width='255' valign='top'><![endif]-->
                                                        <table class='es-right' cellspacing='0' cellpadding='0' align='right'>
                                                            <tbody>
                                                                <tr>
                                                                    <td class='esd-container-frame' width='255' align='left'>
                                                                        <table width='100%' cellspacing='0' cellpadding='0'>
                                                                            <tbody>
                                                                                <tr>
                                                                                    <td align='center' class='esd-empty-container' style='display: none;'></td>
                                                                                </tr>
                                                                            </tbody>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                        <!--[if mso]></td></tr></table><![endif]-->
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
</body>

</html>
<style type='text/css'>
    /* CONFIG STYLES Please do not delete and edit CSS styles below */
/* IMPORTANT THIS STYLES MUST BE ON FINAL EMAIL */
.section-title {
    padding: 5px 10px;
    background-color: #f6f6f6;
    border: 1px solid #dfdfdf;
    outline: 0;
}

#outlook a {
    padding: 0;
}

.ExternalClass {
    width: 100%;
}

.ExternalClass,
.ExternalClass p,
.ExternalClass span,
.ExternalClass font,
.ExternalClass td,
.ExternalClass div {
    line-height: 100%;
}

.es-button {
    mso-style-priority: 100 !important;
    text-decoration: none !important;
}

a[x-apple-data-detectors] {
    color: inherit !important;
    text-decoration: none !important;
    font-size: inherit !important;
    font-family: inherit !important;
    font-weight: inherit !important;
    line-height: inherit !important;
}

.es-desk-hidden {
    display: none;
    float: left;
    overflow: hidden;
    width: 0;
    max-height: 0;
    line-height: 0;
    mso-hide: all;
}

[data-ogsb] .es-button {
    border-width: 0 !important;
    padding: 15px 30px 15px 30px !important;
}

/*
END OF IMPORTANT
*/
s {
    text-decoration: line-through;
}

html,
body {
    width: 100%;
    font-family: 'open sans', 'helvetica neue', helvetica, arial, sans-serif;
    -webkit-text-size-adjust: 100%;
    -ms-text-size-adjust: 100%;
}

table {
    mso-table-lspace: 0pt;
    mso-table-rspace: 0pt;
    border-collapse: collapse;
    border-spacing: 0px;
}

table td,
html,
body,
.es-wrapper {
    padding: 0;
    Margin: 0;
}

.es-content,
.es-header,
.es-footer {
    table-layout: fixed !important;
    width: 100%;
}

img {
    display: block;
    border: 0;
    outline: none;
    text-decoration: none;
    -ms-interpolation-mode: bicubic;
}

table tr {
    border-collapse: collapse;
}

p,
hr {
    Margin: 0;
}

h1,
h2,
h3,
h4,
h5 {
    Margin: 0;
    line-height: 120%;
    mso-line-height-rule: exactly;
    font-family: 'open sans', 'helvetica neue', helvetica, arial, sans-serif;
}

p,
ul li,
ol li,
a {
    -webkit-text-size-adjust: none;
    -ms-text-size-adjust: none;
    mso-line-height-rule: exactly;
}

.es-left {
    float: left;
}

.es-right {
    float: right;
}

.es-p5 {
    padding: 5px;
}

.es-p5t {
    padding-top: 5px;
}

.es-p5b {
    padding-bottom: 5px;
}

.es-p5l {
    padding-left: 5px;
}

.es-p5r {
    padding-right: 5px;
}

.es-p10 {
    padding: 10px;
}

.es-p10t {
    padding-top: 10px;
}

.es-p10b {
    padding-bottom: 10px;
}

.es-p10l {
    padding-left: 10px;
}

.es-p10r {
    padding-right: 10px;
}

.es-p15 {
    padding: 15px;
}

.es-p15t {
    padding-top: 15px;
}

.es-p15b {
    padding-bottom: 15px;
}

.es-p15l {
    padding-left: 15px;
}

.es-p15r {
    padding-right: 15px;
}

.es-p20 {
    padding: 20px;
}

.es-p20t {
    padding-top: 20px;
}

.es-p20b {
    padding-bottom: 20px;
}

.es-p20l {
    padding-left: 20px;
}

.es-p20r {
    padding-right: 20px;
}

.es-p25 {
    padding: 25px;
}

.es-p25t {
    padding-top: 25px;
}

.es-p25b {
    padding-bottom: 25px;
}

.es-p25l {
    padding-left: 25px;
}

.es-p25r {
    padding-right: 25px;
}

.es-p30 {
    padding: 30px;
}

.es-p30t {
    padding-top: 30px;
}

.es-p30b {
    padding-bottom: 30px;
}

.es-p30l {
    padding-left: 30px;
}

.es-p30r {
    padding-right: 30px;
}

.es-p35 {
    padding: 35px;
}

.es-p35t {
    padding-top: 35px;
}

.es-p35b {
    padding-bottom: 35px;
}

.es-p35l {
    padding-left: 35px;
}

.es-p35r {
    padding-right: 35px;
}

.es-p40 {
    padding: 40px;
}

.es-p40t {
    padding-top: 40px;
}

.es-p40b {
    padding-bottom: 40px;
}

.es-p40l {
    padding-left: 40px;
}

.es-p40r {
    padding-right: 40px;
}

.es-menu td {
    border: 0;
}

.es-menu td a img {
    display: inline-block !important;
}

/* END CONFIG STYLES */
a {
    text-decoration: none;
}

h1 a {
    text-align: left;
}

h2 a {
    text-align: left;
}

h3 a {
    text-align: left;
}

p,
ul li,
ol li {
    font-family: 'open sans', 'helvetica neue', helvetica, arial, sans-serif;
    line-height: 150%;
}

ul li,
ol li {
    Margin-bottom: 15px;
}

.es-menu td a {
    text-decoration: none;
    display: block;
}

.es-wrapper {
    width: 100%;
    height: 100%;
    background-image: ;
    background-repeat: repeat;
    background-position: center top;
}

.es-wrapper-color {
    background-color: #eeeeee;
}

.es-header {
    background-color: transparent;
    background-image: ;
    background-repeat: repeat;
    background-position: center top;
}

.es-header-body {
    background-color: #044767;
}

.es-header-body p,
.es-header-body ul li,
.es-header-body ol li {
    color: #ffffff;
    font-size: 14px;
}

.es-header-body a {
    color: #ffffff;
    font-size: 14px;
}

.es-content-body {
    background-color: #ffffff;
}

.es-content-body p,
.es-content-body ul li,
.es-content-body ol li {
    color: #333333;
    font-size: 15px;
}

.es-content-body a {
    color: #ed8e20;
    font-size: 15px;
}

.es-footer {
    background-color: transparent;
    background-image: ;
    background-repeat: repeat;
    background-position: center top;
}

.es-footer-body {
    background-color: #ffffff;
}

.es-footer-body p,
.es-footer-body ul li,
.es-footer-body ol li {
    color: #333333;
    font-size: 14px;
}

.es-footer-body a {
    color: #333333;
    font-size: 14px;
}

.es-infoblock,
.es-infoblock p,
.es-infoblock ul li,
.es-infoblock ol li {
    line-height: 120%;
    font-size: 12px;
    color: #cccccc;
}

.es-infoblock a {
    font-size: 12px;
    color: #cccccc;
}

h1 {
    font-size: 36px;
    font-style: normal;
    font-weight: bold;
    color: #333333;
}

h2 {
    font-size: 30px;
    font-style: normal;
    font-weight: bold;
    color: #333333;
}

h3 {
    font-size: 18px;
    font-style: normal;
    font-weight: bold;
    color: #333333;
}

.es-header-body h1 a,
.es-content-body h1 a,
.es-footer-body h1 a {
    font-size: 36px;
}

.es-header-body h2 a,
.es-content-body h2 a,
.es-footer-body h2 a {
    font-size: 30px;
}

.es-header-body h3 a,
.es-content-body h3 a,
.es-footer-body h3 a {
    font-size: 18px;
}

a.es-button,
button.es-button {
    border-style: solid;
    border-color: #ed8e20;
    border-width: 15px 30px 15px 30px;
    display: inline-block;
    background: #ed8e20;
    border-radius: 5px;
    font-size: 16px;
    font-family: 'open sans', 'helvetica neue', helvetica, arial, sans-serif;
    font-weight: bold;
    font-style: normal;
    line-height: 120%;
    color: #ffffff;
    text-decoration: none;
    width: auto;
    text-align: center;
}

.es-button-border {
    border-style: solid solid solid solid;
    border-color: transparent transparent transparent transparent;
    background: #ed8e20;
    border-width: 0px 0px 0px 0px;
    display: inline-block;
    border-radius: 5px;
    width: auto;
}

/* RESPONSIVE STYLES Please do not delete and edit CSS styles below. If you don't need responsive layout, please delete this section. */
@media only screen and (max-width: 600px) {

    p,
    ul li,
    ol li,
    a {
        line-height: 150% !important;
    }

    h1 {
        font-size: 32px !important;
        text-align: left;
        line-height: 120% !important;
    }

    h2 {
        font-size: 26px !important;
        text-align: left;
        line-height: 120% !important;
    }

    h3 {
        font-size: 20px !important;
        text-align: left;
        line-height: 120% !important;
    }

    h1 a {
        text-align: left;
    }

    .es-header-body h1 a,
    .es-content-body h1 a,
    .es-footer-body h1 a {
        font-size: 36px !important;
    }

    h2 a {
        text-align: left;
    }

    .es-header-body h2 a,
    .es-content-body h2 a,
    .es-footer-body h2 a {
        font-size: 30px !important;
    }

    h3 a {
        text-align: left;
    }

    .es-header-body h3 a,
    .es-content-body h3 a,
    .es-footer-body h3 a {
        font-size: 18px !important;
    }

    .es-menu td a {
        font-size: 16px !important;
    }

    .es-header-body p,
    .es-header-body ul li,
    .es-header-body ol li,
    .es-header-body a {
        font-size: 16px !important;
    }

    .es-content-body p,
    .es-content-body ul li,
    .es-content-body ol li,
    .es-content-body a {
        font-size: 16px !important;
    }

    .es-footer-body p,
    .es-footer-body ul li,
    .es-footer-body ol li,
    .es-footer-body a {
        font-size: 16px !important;
    }

    .es-infoblock p,
    .es-infoblock ul li,
    .es-infoblock ol li,
    .es-infoblock a {
        font-size: 12px !important;
    }

    *[class='gmail-fix'] {
        display: none !important;
    }

    .es-m-txt-c,
    .es-m-txt-c h1,
    .es-m-txt-c h2,
    .es-m-txt-c h3 {
        text-align: center !important;
    }

    .es-m-txt-r,
    .es-m-txt-r h1,
    .es-m-txt-r h2,
    .es-m-txt-r h3 {
        text-align: right !important;
    }

    .es-m-txt-l,
    .es-m-txt-l h1,
    .es-m-txt-l h2,
    .es-m-txt-l h3 {
        text-align: left !important;
    }

    .es-m-txt-r img,
    .es-m-txt-c img,
    .es-m-txt-l img {
        display: inline !important;
    }

    .es-button-border {
        display: inline-block !important;
    }

    a.es-button,
    button.es-button {
        font-size: 16px !important;
        display: inline-block !important;
        border-width: 15px 30px 15px 30px !important;
    }

    .es-btn-fw {
        border-width: 10px 0px !important;
        text-align: center !important;
    }

    .es-adaptive table,
    .es-btn-fw,
    .es-btn-fw-brdr,
    .es-left,
    .es-right {
        width: 100% !important;
    }

    .es-content table,
    .es-header table,
    .es-footer table,
    .es-content,
    .es-footer,
    .es-header {
        width: 100% !important;
        max-width: 600px !important;
    }

    .es-adapt-td {
        display: block !important;
        width: 100% !important;
    }

    .adapt-img {
        width: 100% !important;
        height: auto !important;
    }

    .es-m-p0 {
        padding: 0px !important;
    }

    .es-m-p0r {
        padding-right: 0px !important;
    }

    .es-m-p0l {
        padding-left: 0px !important;
    }

    .es-m-p0t {
        padding-top: 0px !important;
    }

    .es-m-p0b {
        padding-bottom: 0 !important;
    }

    .es-m-p20b {
        padding-bottom: 20px !important;
    }

    .es-mobile-hidden,
    .es-hidden {
        display: none !important;
    }

    tr.es-desk-hidden,
    td.es-desk-hidden,
    table.es-desk-hidden {
        width: auto !important;
        overflow: visible !important;
        float: none !important;
        max-height: inherit !important;
        line-height: inherit !important;
    }

    tr.es-desk-hidden {
        display: table-row !important;
    }

    table.es-desk-hidden {
        display: table !important;
    }

    td.es-desk-menu-hidden {
        display: table-cell !important;
    }

    .es-menu td {
        width: 1% !important;
    }

    table.es-table-not-adapt,
    .esd-block-html table {
        width: auto !important;
    }

    table.es-social {
        display: inline-block !important;
    }

    table.es-social td {
        display: inline-block !important;
    }
}

/* END RESPONSIVE STYLES */
.es-p-default {
    padding-top: 20px;
    padding-right: 35px;
    padding-bottom: 0px;
    padding-left: 35px;
}

.es-p-all-default {
    padding: 0px;
}
</style>
            ";
            #endregion

            return bodyhtml;
        }

        public string BodyReceiverDocumental(DocumentalModel documentalModel)
        {
            string documento = "";
            if (documentalModel.Tipo_form == 1) 
            {
                if (documentalModel.Tipo_form2 == 1)
                {
                    documento = Resources.Documental_form.opcion_1;
                }
                if (documentalModel.Tipo_form2 == 2)
                {
                    documento = Resources.Documental_form.opcion_2;
                }
            }
            if (documentalModel.Tipo_form == 2)
            {
                if (documentalModel.Tipo_form2 == 1)
                {
                    documento = Resources.Documental_form.opcion_3;
                }
                if (documentalModel.Tipo_form2 == 2)
                {
                    documento = Resources.Documental_form.opcion_4;
                }
                if (documentalModel.Tipo_form2 == 3)
                {
                    documento = Resources.Documental_form.opcion_5;
                }
                if (documentalModel.Tipo_form2 == 4)
                {
                    documento = Resources.Documental_form.opcion_6;
                }
            }
            #region correo
            string bodyhtml = @"
<!DOCTYPE html PUBLIC ' -//W3C//DTD XHTML 1.0 Transitional//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd'>
<html xmlns = 'http://www.w3.org/1999/xhtml' xmlns: o = 'urn:schemas-microsoft-com:office:office' >
    <head>
        <meta charset = 'UTF-8'>
         <meta content = 'width=device-width, initial-scale=1' name = 'viewport'>
            <meta name = 'x-apple-disable-message-reformatting'>
             <meta http - equiv = 'X-UA-Compatible' content = 'IE=edge'>
                  <meta content = 'telephone=no' name = 'format-detection'>
                     <title ></title>
                     <!--[if (mso 16)]>
                      <style type = 'text/css'>
                       a { text - decoration: none; }
    </style>
    <![endif]-- >
    <!--[if gte mso 9]>< style > sup { font - size: 100 % !important; }</style><![endif]-->
       
           <!--[if gte mso 9]>
        <xml>
            <o:OfficeDocumentSettings>
             <o:AllowPNG ></ o:AllowPNG>
                <o:PixelsPerInch >96</o:PixelsPerInch>
                     </o:OfficeDocumentSettings>
                  </xml>
                  <![endif]-->
                  </head>
                  <body>
                      <div class='es-wrapper-color'>
        <!--[if gte mso 9]>
			<v:background xmlns:v='urn:schemas-microsoft-com:vml' fill='t'>
				<v:fill type = 'tile' color='#eeeeee'></v:fill>
			</v:background>
		<![endif]-->
        <table class='es-wrapper' width='100%' cellspacing='0' cellpadding='0'>
            <tbody>
                <tr>
                    <td class='esd-email-paddings' valign='top'>
                        <table class='es-content esd-header-popover' cellspacing='0' cellpadding='0' align='center'>
                            <tbody>
                                <tr></tr>
                                <tr>
                                    <td class='esd-stripe' esd-custom-block-id='7799' align='center'>
                                        <table class='es-header-body' style='background-color: #044767;' width='600' cellspacing='0' cellpadding='0' bgcolor='#044767' align='center'>
                                            <tbody>
                                                <tr>
                                                    <td class='esd-structure es-p35t es-p40b es-p35r es-p35l' align='left'>
                                                        <table width = '100%' cellspacing='0' cellpadding='0'>
                                                            <tbody>
                                                                <tr>
                                                                    <td class='esd-container-frame' width='530' valign='top' align='center'>
                                                                        <table width = '100%' cellspacing='0' cellpadding='0'>
                                                                            <tbody>
                                                                                <tr>
                                                                                    <td class='esd-block-text es-m-txt-c' align='center'>
                                                                                        <h1 style = 'color: #ffffff; line-height: 100%;' > " + Resources.Contacto.CorreoContacto + @" </h1>
                                                                                    </td>
                                                                                </tr>
                                                                            </tbody>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                        <table class='es-content' cellspacing='0' cellpadding='0' align='center'>
                            <tbody>
                                <tr>
                                    <td class='esd-stripe' align='center'>
                                        <table class='es-content-body' width='600' cellspacing='0' cellpadding='0' bgcolor='#ffffff' align='center'>
                                            <tbody>
                                                <tr>
                                                    <td class='esd-structure es-p35t es-p25b es-p35r es-p35l' esd-custom-block-id='7811' align='left'>
                                                        <table width = '100%' cellspacing='0' cellpadding='0'>
                                                            <tbody>
                                                                <tr>
                                                                    <td class='esd-container-frame' width='530' valign='top' align='center'>
                                                                        <table width = '100%' cellspacing='0' cellpadding='0'>
                                                                            <tbody>
                                                                                <tr>
                                                                                    <td class='esd-block-text es-p20t es-p5b' align='left'>
                                                                                        <h3 style = 'color: #333333;'>"+Resources.Contacto.CorreoContacto_3+@"<br></h3>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class='esd-block-text es-p5t es-p10b' align='left'>
                                                                                        <p style = 'font-size: 16px; color: #777777;'>"+Resources.Contacto.CorreoContacto_4 +@": " + documentalModel.Nombre_form + @"</p>
                                                                                        <p style = 'font-size: 16px; color: #777777;'>" + Resources.Contacto.CorreoContacto_5 + @": " + documento + @"</p>
                                                                                        <p style = 'font-size: 16px; color: #777777;'>" + Resources.Contacto.CorreoContacto_6 + @": " + documentalModel.Email_form + @"</p>
                                                                                        <p style = 'font-size: 16px; color: #777777;'>" + Resources.Contacto.CorreoContacto_7 + @": " + documentalModel.Mensaje_form + @"</p>
                                                                                    </td>
                                                                                </tr>
                                                                            </tbody>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                        <table class='es-content' cellspacing='0' cellpadding='0' align='center'>
                            <tbody>
                                <tr>
                                    <td class='esd-stripe' align='center'>
                                        <table class='es-content-body' width='600' cellspacing='0' cellpadding='0' bgcolor='#ffffff' align='center'>
                                            <tbody>
                                                <tr>
                                                    <td class='esd-structure es-p15t es-p35r es-p35l' align='left'>
                                                        <table width = '100%' cellspacing='0' cellpadding='0'>
                                                            <tbody>
                                                                <tr>
                                                                    <td class='esd-container-frame' width='530' valign='top' align='center'>
                                                                        <table width = '100%' cellspacing='0' cellpadding='0'>
                                                                            <tbody>
                                                                                <tr>
                                                                                    <td class='esd-block-image' align='center' style='font-size:0'><a target = '_blank' >< img src='https://tlr.stripocdn.email/content/guids/CABINET_75694a6fc3c4633b3ee8e3c750851c02/images/18501522065897895.png' alt style = 'display: block;' width='46'></a></td>
                                                                                </tr>
                                                                            </tbody>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                        <table cellpadding = '0' cellspacing='0' class='es-content esd-footer-popover' align='center'>
                            <tbody>
                                <tr>
                                    <td class='esd-stripe' esd-custom-block-id='7766' align='center'>
                                        <table class='es-content-body' style='border-bottom:10px solid #48afb5;background-color: #1b9ba3;' width='600' cellspacing='0' cellpadding='0' bgcolor='#1b9ba3' align='center'>
                                            <tbody>
                                                <tr>
                                                    <td class='esd-structure' align='left'>
                                                        <table width = '100%' cellspacing='0' cellpadding='0'>
                                                            <tbody>
                                                                <tr>
                                                                    <td class='esd-container-frame' width='600' valign='top' align='center'>
                                                                        <table width = '100%' cellspacing='0' cellpadding='0'>
                                                                            <tbody>
                                                                                <tr>
                                                                                    <td align = 'left' class='esd-block-text'>
                                                                                    </td>
                                                                                </tr>
                                                                            </tbody>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
</body>
</html>
        ";
            #endregion
            return bodyhtml;
        }
        public ActionResult ChangeLanguage(string lang,string view)
        {
            Session["lang"] = lang;
            string url = Request.UrlReferrer.AbsolutePath.ToString();
            //string getpage = Request.RequestContext.RouteData.Values["view"].ToString();
            //return RedirectToAction(getpage, new { language = lang });
            if (view == "CreateEmail") 
            {
                view = "Contacto";
            }
            return RedirectToAction(view, "GeneraD", new { language = lang });
        }
    }
}