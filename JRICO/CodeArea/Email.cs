using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SendGridMail;
using SendGridMail.Transport;
using System.Net.Mail;
using System.Net;

namespace JRICO.CodeArea
{
    public class Email
    {
        public string SendEmail(string EmailTo, string EmailSubject, string EmailContent, int ContractID)
        {
            string result;
            try
            {
                // Create the email object first, then add the properties.
                SendGrid myMessage = SendGrid.GetInstance();
                List<String> EmailToInList = new List<String>(EmailTo.Split(';'));
                myMessage.AddTo(EmailToInList);
                //myMessage.AddBcc("cathal.reddin@gmail.com");
                myMessage.From = new MailAddress("cathal_r@yahoo.com", "JRI");
                myMessage.Subject = EmailSubject;
                myMessage.Text = EmailContent;

                // Create credentials, specifying your user name and password.
                var credentials = new NetworkCredential("4cad11dc-27e7-4d92-8b48-b5335424d368@apphb.com", "htmy0jzx");

                // Create an SMTP transport for sending email.
                var transportSMTP = SMTP.GetInstance(credentials);

                // Send the email.
                transportSMTP.Deliver(myMessage);
                result = "Email Sent";
            }
            catch (Exception ex)
            {
                result = "Error: " + ex.Message;
                result = "<br />: " + ex.InnerException;
                return result;
            }
            return result;
        }
        //Send Email when Email Details Updated or Inserted
        public string SendEmailToCathal(string EmailTo, string EmailSubject, string EmailContent, DateTime SendDate, int ContractID = 0)
        {
            string result;
            try
            {
                // Create the email object first, then add the properties.
                EmailTo = "cathal.reddin@bt.com;cathal.reddin@gmail.com";
                SendGrid myMessage = SendGrid.GetInstance();
                List<String> EmailToInList = new List<String>(EmailTo.Split(';'));
                myMessage.AddTo(EmailToInList);
                //myMessage.AddBcc("cathal.reddin@gmail.com"); 
                myMessage.From = new MailAddress("cathal_r@yahoo.com", "JRI");
                myMessage.Subject = EmailSubject;
                myMessage.Text = EmailContent;

                var credentials = new NetworkCredential("4cad11dc-27e7-4d92-8b48-b5335424d368@apphb.com", "htmy0jzx");

                // Create an SMTP transport for sending email.
                var transportSMTP = SMTP.GetInstance(credentials);

                // Send the email.
                transportSMTP.Deliver(myMessage);
                result = "Email Sent";
            }
            catch (Exception ex)
            {
                result = "Error: " + ex.Message;
                result = "<br />: " + ex.InnerException;
                return result;
            }
            return result;
        }
    }
}