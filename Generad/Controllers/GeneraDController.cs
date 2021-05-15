using Generad.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
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
            return View();
        }
        public ActionResult IntegracionPersonal()
        {
            return View();
        }

        public ActionResult Documental()
        {
            return View();
        }
        public ActionResult Contacto()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateEmail(ContactoModel contactoModel)
        {
            try
            {
                if (ModelState.IsValid)
                { //checking model state
                    string correoPadreAsistencia = ConfigurationManager.AppSettings["CorreoPadre"];
                    MailMessage mail = new MailMessage();
                    mail.To.Add(contactoModel.Email_form);
                    string contactEmail = contactoModel.Email_form;
                    //mail.To.Add("tusnudesamicorreo@gmail.com");
                    //mail.From = new MailAddress("yournudestomyemail@gmail.com");
                    mail.From = new MailAddress(correoPadreAsistencia);
                    mail.Subject = Resources.Contacto.CorreoContacto;
                    //string Body = contactoModel.Mensaje_form;
                    mail.Body = BodyContact(contactoModel);
                    //mail.Body = Body;
                    mail.IsBodyHtml = true;
                    SmtpClient smtp = new SmtpClient();
                    string host = ConfigurationManager.AppSettings["host"];
                    int port = Convert.ToInt32(ConfigurationManager.AppSettings["port"]);
                    string correoPadre = ConfigurationManager.AppSettings["CorreoPadre"];
                    string ContraseñaPadre = ConfigurationManager.AppSettings["ClientId"];
                    //smtp.Host = "smtp.gmail.com";
                    //smtp.Port = 587; 
                    smtp.Host = host;
                    smtp.Port = port;
                    smtp.UseDefaultCredentials = false;
                    //smtp.Credentials = new System.Net.NetworkCredential("yournudestomyemail@gmail.com", "ubunto5@R"); // Enter seders User name and password    
                    smtp.Credentials = new System.Net.NetworkCredential(correoPadre, ContraseñaPadre); // Enter seders User name and password    
                    smtp.EnableSsl = true;
                    smtp.Send(mail);

                    mail = new MailMessage();
                    //mail.To.Add(contactoModel.Email_form);
                    //string contactEmail = contactoModel.Email_form;

                    mail.To.Add(correoPadreAsistencia);
                    mail.From = new MailAddress(correoPadreAsistencia);
                    mail.Subject = Resources.Contacto.CorreoContacto;
                    //string Body = contactoModel.Mensaje_form;
                    mail.Body = BodyReceiver(contactoModel);
                    //mail.Body = Body;
                    mail.IsBodyHtml = true;
                    smtp = new SmtpClient();
                    //smtp.Host = "smtp.gmail.com";
                    //smtp.Port = 587;
                    //smtp.UseDefaultCredentials = false;
                    //smtp.Credentials = new System.Net.NetworkCredential("yournudestomyemail@gmail.com", "ubunto5@R"); // Enter seders User name and password      
                    smtp.Host = host;
                    smtp.Port = port;
                    smtp.UseDefaultCredentials = false;
                    //smtp.Credentials = new System.Net.NetworkCredential("yournudestomyemail@gmail.com", "ubunto5@R"); // Enter seders User name and password    
                    smtp.Credentials = new System.Net.NetworkCredential(correoPadre, ContraseñaPadre); // Enter seders User name and password    
                    smtp.EnableSsl = true;
                    smtp.Send(mail);


                }


                return View("contacto");
                // TODO: Add insert logic here

            }
            catch (Exception e)
            {
                return View();
            }
        }

        public string BodyReceiver(ContactoModel contactoModel)
        {
            string bodyhtml = @"
<td class='esd-stripe' align='center'>
    <table class='es-content-body' style='background-color: transparent;' width='640' cellspacing='0' cellpadding='0' align='center'>
        <tbody>
            <tr>
                <td class='esd-structure es-p25t es-p20b es-p20r es-p20l' align='left'>
                    <table width='100%' cellspacing='0' cellpadding='0'>
                        <tbody>
                            <tr>
                                <td class='esd-container-frame' width='600' valign='top' align='center'>
                                    <table style='background-color: #ffffff; border-radius: 3px; border-collapse: separate;' width='100%' cellspacing='0' cellpadding='0' bgcolor='#ffffff'>
                                        <tbody>
                                            <tr>
                                                <td class='esd-block-text es-p40t es-p10b es-p40r es-p40l' align='left' bgcolor='transparent'>
                                                    <p style='color: #999999;'>Te han contactado</p>
                                                    <p style='color: #999999;'><br></p>
                                                    <p style='color: #999999;'>" + contactoModel.Nombre_form + @"</p>
                                                    <p style='color: #999999;'><br></p>
                                                    <p style='color: #999999;'>" + contactoModel.NombreCompania_form + @"</p>
                                                    <p style='color: #999999;'><br></p>
                                                    <p style='color: #999999;'>" + contactoModel.Mensaje_form + @"</p>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class='esd-block-text es-p40b es-p40r es-p40l' align='left'>
                                                    <p>" + contactoModel.Email_form + @"</p>
													<p>" + contactoModel.Telefono_form + @"</p>
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
        ";
            return bodyhtml;
        }
        public string BodyContact(ContactoModel contactoModel)
        {
            string bodyhtml = @"

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
				<v:fill type='tile' src='https://stripo.email/content/guids/CABINET_63fbbc11db6741389cc3292b09a63e6d/images/7711511856111535.png' color='#f6f6f6' origin='0.5, 0' position='0.5, 0'></v:fill>
			</v:background>
		<![endif]-->
        <table class='es-wrapper' width='100%' cellspacing='0' cellpadding='0' background='https://stripo.email/content/guids/CABINET_63fbbc11db6741389cc3292b09a63e6d/images/7711511856111535.png' style='background-position: center top;'>
            <tbody>
                <tr>
                    <td class='esd-email-paddings' valign='top'>
                        <table cellpadding='0' cellspacing='0' class='es-content esd-header-popover' align='center'>
                            <tbody>
                                <tr>
                                    <td class='es-adaptive esd-stripe' esd-custom-block-id='4041' align='center'>
                                        <table class='es-content-body' width='600' cellspacing='0' cellpadding='0' align='center'>
                                            <tbody>
                                                <tr>
                                                    <td class='esd-structure es-p10 esd-checked' style='background-image:url(https://tlr.stripocdn.email/content/guids/CABINET_63fbbc11db6741389cc3292b09a63e6d/images/7711511856111535.png);background-position: left top; background-repeat: repeat;' align='left' background='https://tlr.stripocdn.email/content/guids/CABINET_63fbbc11db6741389cc3292b09a63e6d/images/7711511856111535.png'>
                                                        <!--[if mso]><table width='580'><tr><td width='280' valign='top'><![endif]-->
                                                        <table class='es-left' cellspacing='0' cellpadding='0' align='left'>
                                                            <tbody>
                                                                <tr>
                                                                    <td class='esd-container-frame' width='280' align='left'>
                                                                        <table width='100%' cellspacing='0' cellpadding='0'>
                                                                            <tbody>
                                                                                <tr>
                                                                                    <td class='es-infoblock esd-block-text' align='left'>
                                                                                        <p></p>
                                                                                    </td>
                                                                                </tr>
                                                                            </tbody>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                        <!--[if mso]></td><td width='20'></td><td width='280' valign='top'><![endif]-->
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                        <table cellpadding='0' cellspacing='0' class='es-header' align='center'>
                            <tbody>
                                <tr>
                                    <td class='es-adaptive esd-stripe' esd-custom-block-id='4042' align='center'>
                                        <table class='es-header-body' width='600' cellspacing='0' cellpadding='0' align='center'>
                                            <tbody>
                                                <tr>
                                                    <td class='esd-structure es-p15' esd-general-paddings-checked='true' align='left'>
                                                        <table width='100%' cellspacing='0' cellpadding='0'>
                                                            <tbody>
                                                                <tr>
                                                                    <td class='esd-container-frame' width='570' valign='top' align='center'>
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
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class='esd-structure' align='left'>
                                                        <table width='100%' cellspacing='0' cellpadding='0'>
                                                            <tbody>
                                                                <tr>
                                                                    <td class='esd-container-frame' width='600' valign='top' align='center'>
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
                                                        <table width='100%' cellspacing='0' cellpadding='0'>
                                                            <tbody>
                                                                <tr>
                                                                    <td class='esd-container-frame' width='600' valign='top' align='center'>
                                                                        <table width='100%' cellspacing='0' cellpadding='0'>
                                                                            <tbody>
                                                                                <tr>
                                                                                    <td class='esd-block-text es-p30t es-p15b es-p15r es-p15l' align='center'>
                                                                                        <h1 style='font-size: 38px;'>" + Resources.Contacto.CorreoContacto_1 + @"</h1>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class='esd-block-image es-p15b' align='center' style='font-size:0'><a target='_blank'><img class='adapt-img' src='https://tlr.stripocdn.email/content/guids/CABINET_63fbbc11db6741389cc3292b09a63e6d/images/63541516368770627.png' alt='Handshake' title='Handshake' width='600'></a></td>
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
                                                        <table width='100%' cellspacing='0' cellpadding='0'>
                                                            <tbody>
                                                                <tr>
                                                                    <td class='esd-container-frame' width='540' valign='top' align='center'>
                                                                        <table width='100%' cellspacing='0' cellpadding='0'>
                                                                            <tbody>
                                                                                <tr>
                                                                                    <td class='esd-block-text es-p15t' align='left'>
                                                                                        <h3 style='line-height: 120%;'>" + contactoModel.Nombre_form + @",</h3>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class='esd-block-text es-m-txt-c es-p10t es-p10b' align='left'>
                                                                                        <p style='line-height: 150%;'>" + Resources.Contacto.CorreoContacto_2 + @"</p>
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
                                                    <td class='esd-structure es-p10b es-p30r es-p30l' esd-general-paddings-checked='true' align='left'>
                                                        <table width='100%' cellspacing='0' cellpadding='0'>
                                                            <tbody>
                                                                <tr>
                                                                    <td class='esd-container-frame' width='540' valign='top' align='center'>
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
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class='esd-structure es-p20b es-p30r es-p30l' esd-general-paddings-checked='true' align='left'>
                                                        <table width='100%' cellspacing='0' cellpadding='0'>
                                                            <tbody>
                                                                <tr>
                                                                    <td class='esd-container-frame' width='540' valign='top' align='center'>
                                                                        <table width='100%' cellspacing='0' cellpadding='0'>
                                                                            <tbody>
                                                                                <tr>
                                                                                    <td class='esd-block-text es-m-txt-c' align='left'>
                                                                                        <p style='line-height: 150%;'><br></p>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class='esd-block-text es-p15t es-m-txt-c' align='left'>
                                                                                        <p style='line-height: 150%;'>" + Resources.Contacto.Nombre_form + @"</p>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class='esd-block-text es-p15t es-m-txt-c' align='left'>
                                                                                        <p style='line-height: 150%;'><span style='font-size: 14px; color: #24578e;'>:</span> +525547392178</p>
                                                                                        <p style='line-height: 150%;'><span style='font-size: 14px; color: #24578e;'>Whatsapp:</span>+525548334915</p>
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
                                                    <td class='esd-structure es-p10t es-p20b es-p30r es-p30l' esd-general-paddings-checked='true' align='left'>
                                                        <table width='100%' cellspacing='0' cellpadding='0'>
                                                            <tbody>
                                                                <tr>
                                                                    <td class='esd-container-frame' width='540' valign='top' align='center'>
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
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class='esd-structure es-p20t es-p30b es-p30r es-p30l' align='left'>
                                                        <!--[if mso]><table width='540' cellpadding='0'
                            cellspacing='0'><tr><td width='100' valign='top'><![endif]-->
                                                        <table class='es-left' cellspacing='0' cellpadding='0' align='left'>
                                                            <tbody>
                                                                <tr>
                                                                    <td class='es-m-p0r es-m-p20b esd-container-frame' width='100' valign='top' align='center'>
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
                                                        <!--[if mso]></td><td width='20'></td><td width='420' valign='top'><![endif]-->
                                                        <table cellspacing='0' cellpadding='0' align='right'>
                                                            <tbody>
                                                                <tr>
                                                                    <td class='esd-container-frame' width='420' align='left'>
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
                        <table cellpadding='0' cellspacing='0' class='es-footer' align='center'>
                            <tbody>
                                <tr>
                                    <td class='esd-stripe' esd-custom-block-id='4061' align='center'>
                                        <table class='es-footer-body' width='600' cellspacing='0' cellpadding='0' align='center'>
                                            <tbody>
                                                <tr>
                                                    <td class='esd-structure es-p20t es-p20b es-p20r es-p20l' esd-general-paddings-checked='true' align='left'>
                                                        <table width='100%' cellspacing='0' cellpadding='0'>
                                                            <tbody>
                                                                <tr>
                                                                    <td class='esd-container-frame' width='560' valign='top' align='center'>
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
        public ActionResult ChangeLanguage(string lang)
        {
            Session["lang"] = lang;
            //string url = Request.UrlReferrer.AbsolutePath.ToString();
            //string getpage = Request.RequestContext.RouteData.Values["view"].ToString();
            //return RedirectToAction(getpage, new { language = lang });
            return RedirectToAction("Index", "GeneraD", new { language = lang });
        }
    }
}