using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Mail;
using System.Data;
using System.Reflection;
using System.Linq;

namespace Service
{
    public static class Mailing
    {
        static string smtpAddress = "smtp.gmail.com";
        static int portNumber = 587;
        static bool enableSSL = true;
        static DataTable dtCredientials = new DataTable();

        static Mailing()
        {
            dtCredientials.Columns.Add("Email");
            dtCredientials.Columns.Add("Password");
            List<DataRow> drRows = new List<DataRow>();

            DataRow dr = dtCredientials.NewRow();
            dr[0] = "info@travoware.com";
            //dr[1] = "Mahanayak@1253";
            dr[1] = "vcudpveiiiqfwybt";
            drRows.Add(dr);

            dr = dtCredientials.NewRow();
            dr[0] = "trackTeamwork@gmail.com";
            dr[1] = "Mahanayak@1253";
            drRows.Add(dr);

            foreach (var rw in drRows)
            {
                dtCredientials.Rows.Add(rw);
            }
        }

        public static void SendMail(string mailBody, string subject, List<string> toRecipients, List<string> ccRecipients = null, List<string> bccRecipients = null)
        {
            try
            {
                bool isMailSend = false;
                using (MailMessage mail = new MailMessage())
                {
                    foreach (DataRow row in dtCredientials.Rows)
                    {
                        mail.From = new MailAddress(row["Email"].ToString());
                        foreach (var recipt in toRecipients)
                        {
                            mail.To.Add(recipt);
                        }
                        if (ccRecipients != null && ccRecipients.Any())
                        {
                            foreach (var recipt in ccRecipients)
                            {
                                mail.CC.Add(recipt);
                            }
                        }
                        if (bccRecipients != null && bccRecipients.Any())
                        {
                            foreach (var recipt in bccRecipients)
                            {
                                mail.Bcc.Add(recipt);
                            }
                        }

                        mail.Subject = subject;
                        mail.Body = mailBody;
                        mail.IsBodyHtml = true;
                        using (SmtpClient smtp = new SmtpClient(smtpAddress, portNumber))
                        {
                            smtp.Credentials = new NetworkCredential(row["Email"].ToString(), row["Password"].ToString());
                            smtp.EnableSsl = enableSSL;
                            try
                            {
                                smtp.Send(mail);
                                isMailSend = true;
                                break;
                            }
                            catch (Exception exc)
                            {
                                Logger.Log(System.Reflection.Assembly.GetEntryAssembly().GetName().Name + " " + MethodBase.GetCurrentMethod().Name + exc.Message, Serilog.Events.LogEventLevel.Error);
                            }
                        }

                        if (!isMailSend)
                        {
                            Logger.Log(System.Reflection.Assembly.GetEntryAssembly().GetName().Name + " " + MethodBase.GetCurrentMethod().Name + "All Mails Credientials exhausted.", Serilog.Events.LogEventLevel.Error);
                        }
                    }
                }
            }
            catch (Exception mailExc)
            {
                Logger.Log(System.Reflection.Assembly.GetEntryAssembly().GetName().Name + " " + MethodBase.GetCurrentMethod().Name + mailExc.Message, Serilog.Events.LogEventLevel.Error);
            }
        }
    }
}
