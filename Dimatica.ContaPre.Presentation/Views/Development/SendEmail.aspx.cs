namespace Dimatica.ContaPre.Presentation.Views.Develoment
{
    #region NameSpaces

    using System;
    using System.Text;
    using System.Net.Mail;

    using Dimatica.ContaPre.Presentation.Views.Shared;

    #endregion

    public partial class SendEmail : BasePage
    {
        #region Private Methods

        protected void Page_Load(object sender, EventArgs e) 
        {
            if (!this.IsPostBack)
            {
                this.Session["_currentPage"] = "EnviarEmail";
            }
        }


        protected void btnSendEmail_Click(object sender, EventArgs e)
        {

            string email = txtEmail.Text;
            string msg = $"Prueba de envío de correo realizada con éxito. Correo electrónico enviado a: {email} ";

            try
            {
                send(email);
                lblAlertMessage.Text = $"{msg} - {DateTime.Now}";
            }
            catch (Exception ex)
            {
                lblAlertMessage.Text = $"Error al enviar el correo electrónico: {ex.Message}";
            }
        }

        // Método privado para enviar correo
        private void send(string email)
        {
            // Dirección de correo electrónico del remitente
            string fromAddress  = "no_reply@uimp.es"; // Cambiar por tu dirección de correo electrónico
            MailMessage message = new MailMessage(fromAddress, email);

            message.Subject = "Prueba de correo electrónico";
            message.Body = "Este es un mensaje de prueba enviado desde una aplicación web ASP.NET.";

            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com"); 
            smtpClient.Port = 587; 
            smtpClient.Credentials = new System.Net.NetworkCredential("jositoyoyo@gmail.com", "jdqgrlzxgxiokcdk"); 
            smtpClient.EnableSsl = true; 
            smtpClient.Send(message);

        }



        #endregion
    }
}