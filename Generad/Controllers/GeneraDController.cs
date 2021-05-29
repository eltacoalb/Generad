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
            int g = 0;
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
                { //checking model state
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
                    MailMessage mail = new MailMessage();
                    mail.From = new MailAddress(correoPadreAsistencia);
                    mail.To.Add(new MailAddress(contactoModel.Email_form));
                    mail.Subject = Resources.Contacto.CorreoContacto;
                    mail.Body = BodyContact(contactoModel);
                    mail.IsBodyHtml = true;
                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = "relay-hosting.secureserver.net";
                    smtp.Port = 25;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new System.Net.NetworkCredential(correoPadreAsistencia, ConfigurationManager.AppSettings["contrseñaPadre"]); // Enter seders User name and password       
                    smtp.EnableSsl = false;
                    smtp.Send(mail);

                    MailMessage mail2 = new MailMessage();
                    mail2.From = new MailAddress(correoPadreAsistencia);
                    mail2.To.Add(new MailAddress(correoPadreAsistencia));
                    mail2.Subject = "Te han contactado";
                    mail2.Body = BodyReceiver(contactoModel);
                    mail2.IsBodyHtml = true;
                    SmtpClient smtp2 = new SmtpClient();
                    smtp2.Host = "relay-hosting.secureserver.net";
                    smtp2.Port = 25;
                    smtp2.UseDefaultCredentials = false;
                    smtp2.Credentials = new System.Net.NetworkCredential(correoPadreAsistencia, ConfigurationManager.AppSettings["contrseñaPadre"]); // Enter seders User name and password       
                    smtp2.EnableSsl = false;
                    smtp2.Send(mail2);
                }
                contactoModel = new ContactoModel();
                return View("Contacto");
                //return Json("OK", JsonRequestBehavior.AllowGet);
                // TODO: Add insert logic here

            }
            catch (Exception e)
            {
                return View();
                //return Json(e.Message.ToString());
            }
        }

        public string BodyReceiver(ContactoModel contactoModel)
        {
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
                                                                                        <h3 style = 'color: #333333;' >"+Resources.Contacto.CorreoContacto_3+@"<br></h3>
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
</html>
        ";
            return bodyhtml;
        }
        public string BodyContact(ContactoModel contactoModel)
        {
            string bodyhtml = @"

<!DOCTYPE html PUBLIC ' -//W3C//DTD XHTML 1.0 Transitional//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd'>
<html xmlns = 'http://www.w3.org/1999/xhtml' xmlns: o = 'urn:schemas-microsoft-com:office:office' >
    <head>
        <meta charset = 'UTF-8'>
         <meta content = 'width=device-width, initial-scale=1' name = 'viewport'>
            <meta name = 'x-apple-disable-message-reformatting'>
             <meta http - equiv = 'X-UA-Compatible' content = 'IE=edge'>
                  <meta content = 'telephone=no' name = 'format-detection'>
                     <title ></title >
                     <!--[if (mso 16)]>
                      <style type = 'text/css' >
                       a { text - decoration: none; }
    </style>
    <![endif]-- >
    <!--[if gte mso 9]><style> sup { font - size: 100 % !important; }</style><![endif]-->
       
           <!--[if gte mso 9]>
        <xml>
            <o:OfficeDocumentSettings>
             <o:AllowPNG ></o:AllowPNG>
                <o:PixelsPerInch> 96 </o:PixelsPerInch>
                     </o:OfficeDocumentSettings>
                  </xml >
                  <![endif]-- >
                  </head >
                  <body>
                      <div class='es-wrapper-color'>
        <!--[if gte mso 9]>
			<v:background xmlns:v='urn:schemas-microsoft-com:vml' fill='t'>
				<v:fill type = 'tile' src='https://stripo.email/content/guids/CABINET_63fbbc11db6741389cc3292b09a63e6d/images/7711511856111535.png' color='#f6f6f6' origin='0.5, 0' position='0.5, 0'></v:fill>
			</v:background>
		<![endif]-->
        <table class='es-wrapper' width='100%' cellspacing='0' cellpadding='0' background='https://stripo.email/content/guids/CABINET_63fbbc11db6741389cc3292b09a63e6d/images/7711511856111535.png' style='background-position: center top;'>
            <tbody>
                <tr>
                    <td class='esd-email-paddings' valign='top'>
                        <table cellpadding = '0' cellspacing='0' class='es-header esd-header-popover' align='center'>
                            <tbody>
                                <tr>
                                    <td class='es-adaptive esd-stripe' esd-custom-block-id='4042' align='center'>
                                        <table class='es-header-body' width='600' cellspacing='0' cellpadding='0' align='center'>
                                            <tbody>
                                                <tr>
                                                    <td class='esd-structure es-p15' esd-general-paddings-checked='true' align='left'>
                                                        <table width = '100%' cellspacing='0' cellpadding='0'>
                                                            <tbody>
                                                                <tr>
                                                                    <td class='esd-container-frame' width='570' valign='top' align='center'>
                                                                        <table width = '100%' cellspacing='0' cellpadding='0'>
                                                                            <tbody>
                                                                                <tr>
                                                                                    <td class='esd-block-image es-m-p0l' align='center' style='font-size:0'><a href = 'https://generad.net' target='_blank'>" + Resources.Contacto.CorreoContacto + @"</a></td>
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
                                                    <td class='esd-structure' align='left'>
                                                        <table width = '100%' cellspacing='0' cellpadding='0'>
                                                            <tbody>
                                                                <tr>
                                                                    <td class='esd-container-frame' width='600' valign='top' align='center'>
                                                                        <table width = '100%' cellspacing='0' cellpadding='0'>
                                                                            <tbody>
                                                                                <tr>
                                                                                    <td class='esd-block-menu' esd-img-prev-h='16' esd-img-prev-w='16'>
                                                                                        <table class='es-menu' width='100%' cellspacing='0' cellpadding='0'>
                                                                                            <tbody>
                                                                                                <tr class='links'>
                                                                                                    <td class='es-p10t es-p10b es-p5r es-p5l' style='padding-bottom: 12px; padding-top: 12px; ' width='25.00%' bgcolor='#333333' align='center'><a target = '_blank' style='color: #ffffff;' href='https://generad.net/GeneraD/Holding'>" + Resources.Menu.Holding + @"</a></td>
                                                                                                    <td class='es-p10t es-p10b es-p5r es-p5l' style='padding-bottom: 12px; padding-top: 12px; ' width='25.00%' bgcolor='#333333' align='center'><a target = '_blank' style='color: #ffffff;' href='https://generad.net/GeneraD/Comercializacion'>" + Resources.Menu.Comercializacion + @"</a></td>
                                                                                                    <td class='es-p10t es-p10b es-p5r es-p5l' style='padding-bottom: 12px; padding-top: 12px; ' width='25.00%' bgcolor='#333333' align='center'><a target = '_blank' style='color: #ffffff;' href='https://generad.net/GeneraD/IntegracionPersonal'>" + Resources.Menu.Integracionpersonal + @"</a></td>
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
                        <table class='es-content' cellspacing='0' cellpadding='0' align='center'>
                            <tbody>
                                <tr>
                                    <td class='esd-stripe' esd-custom-block-id='4044' align='center'>
                                        <table class='es-content-body' style='background-color: #ffffff;' width='600' cellspacing='0' cellpadding='0' bgcolor='#ffffff' align='center'>
                                            <tbody>
                                                <tr>
                                                    <td class='esd-structure' style='background-color: #f3f3f3;' bgcolor='#f3f3f3' align='left'>
                                                        <table width = '100%' cellspacing='0' cellpadding='0'>
                                                            <tbody>
                                                                <tr>
                                                                    <td class='esd-container-frame' width='600' valign='top' align='center'>
                                                                        <table width = '100%' cellspacing='0' cellpadding='0'>
                                                                            <tbody>
                                                                                <tr>
                                                                                    <td class='esd-block-text es-p30t es-p15b es-p15r es-p15l' align='center'>
                                                                                        <h1 style = 'font-size: 38px;' >" + Resources.Contacto.CorreoContacto_1 + @"</h1>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class='esd-block-image es-p15b' align='center' style='font-size:0'><a target = '_blank'><img class='adapt-img' src='https://tlr.stripocdn.email/content/guids/CABINET_63fbbc11db6741389cc3292b09a63e6d/images/63541516368770627.png' alt='Handshake' title='Handshake' width='600'></a></td>
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
                                                    <td class='esd-structure es-p20t es-p10b es-p30r es-p30l' esd-general-paddings-checked='true' align='left'>
                                                        <table width = '100%' cellspacing='0' cellpadding='0'>
                                                            <tbody>
                                                                <tr>
                                                                    <td class='esd-container-frame' width='540' valign='top' align='center'>
                                                                        <table width = '100%' cellspacing='0' cellpadding='0'>
                                                                            <tbody>
                                                                                <tr>
                                                                                    <td class='esd-block-text es-p15t' align='left'>
                                                                                        <h3 style = 'line-height: 120%;' >" + contactoModel.Nombre_form + @",</h3>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class='esd-block-text es-m-txt-c es-p10t es-p10b' align='left'>
                                                                                        <p style = 'line-height: 150%;' >" + Resources.Contacto.CorreoContacto_2 + @"<br></p>
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
                                                    <td class='esd-structure es-p20b es-p30r es-p30l' esd-general-paddings-checked='true' align='left'>
                                                        <table width = '100%' cellspacing='0' cellpadding='0'>
                                                            <tbody>
                                                                <tr>
                                                                    <td class='esd-container-frame' width='540' valign='top' align='center'>
                                                                        <table width = '100%' cellspacing='0' cellpadding='0'>
                                                                            <tbody>
                                                                                <tr>
                                                                                    <td class='esd-block-text es-p15t es-m-txt-c' align='left'>
                                                                                        <p style = 'line-height: 150%;' ><span style='font-size: 14px; color: #24578e;'>" + Resources.Contacto_div.Contacto + @"</p>
                                                                                        <p style = 'line-height: 150%;' ><span style='font-size: 14px; color: #24578e;'>" + Resources.Contacto_div.Contacto_2 + @"</p>
                                                                                        <p style = 'line-height: 150%;' ><span style='font-size: 14px; color: #24578e;'>" + Resources.Contacto_div.Contacto_3 + @"</p>
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
                        <table cellpadding = '0' cellspacing='0' class='es-footer esd-footer-popover' align='center'>
                            <tbody>
                                <tr>
                                    <td class='esd-stripe' esd-custom-block-id='4061' align='center'>
                                        <table class='es-footer-body' width='600' cellspacing='0' cellpadding='0' align='center'>
                                            <tbody>
                                                <tr>
                                                    <td class='esd-structure es-p20t es-p20b es-p20r es-p20l' esd-general-paddings-checked='true' align='left'>
                                                        <table width = '100%' cellspacing='0' cellpadding='0'>
                                                            <tbody>
                                                                <tr>
                                                                    <td class='esd-container-frame' width='560' valign='top' align='center'>
                                                                        <table width = '100%' cellspacing='0' cellpadding='0'>
                                                                            <tbody>
                                                                                <tr>
                                                                                    <td class='esd-block-text es-p5t es-p5b' align='center' esd-links-underline='none'>
                                                                                        <p style = 'line-height: 21px;' ><a target='_blank' style='line-height: 21px; text-decoration: none;' href=''>" + Resources.Contacto_div.Contacto_1 + @"</a></p>
                                                                                        <p style = 'line-height: 21px;' ><a target='_blank' style='line-height: 21px; text-decoration: none;' href=''> " + Resources.Contacto_div.Contacto_3 + @"</a></p>
                                                                                        <p style = 'line-height: 21px;' ><a target='_blank' href='" + Resources.Contacto_div.Contacto_4 + @"' style='text-decoration: none;'>" + Resources.Contacto_div.Contacto_4 + @"</a></p>
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
            return bodyhtml;
        }
        public ActionResult ChangeLanguage(string lang,string view)
        {
            Session["lang"] = lang;
            string url = Request.UrlReferrer.AbsolutePath.ToString();
            //string getpage = Request.RequestContext.RouteData.Values["view"].ToString();
            //return RedirectToAction(getpage, new { language = lang });
            int g = 0;
            return RedirectToAction(view, "GeneraD", new { language = lang });
        }
    }
}