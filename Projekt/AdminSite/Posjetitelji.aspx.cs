using DAL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace AdminSite
{
    public partial class Posjetitelji : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!DAL.DAL.JeLiAdminAutenticiran(Session, Server))
            {
                Session["Site"] = HttpContext.Current.Request.Url.AbsolutePath;
                Response.Redirect("Login.aspx");
            }

            if (!IsPostBack)
            {
                try
                {
                    var posjetitelji = DAL.DAL.DohvatiSveKorisnike();
                    rpPosjetitelji.DataSource = posjetitelji;
                    rpPosjetitelji.DataBind();

                    if (posjetitelji.Count() > 0)
                    {
                        btnSaveToCsv.Enabled = true;
                    }
                }
                catch (Exception ex)
                {
                    lblInfo.Text = ex.Message;
                }
            }

            MaintainScrollPositionOnPostBack = true;
        }

        public string GetSpol(int value) => DAL.DAL.DohvatiSpolove().FirstOrDefault(x => x.IDSpol == value)?.Naziv;
        public string GetTipDijabetesa(int value) => DAL.DAL.DohvatiTipoveDijabetesa().FirstOrDefault(x => x.IDTipDijabetesa == value)?.Naziv;
        public string GetRazinaFizickeAktivnosti(int value) => DAL.DAL.DohvatiRazineFizickeAktivnosti().FirstOrDefault(x => x.IDRazinaFizickeAktivnosti == value)?.Naziv;

        protected void btnSaveToCsv_Click(object sender, EventArgs e)
        {
            string path = DAL.DAL.serverTempFilePath;

            if (!DAL.DAL.IspisiSveKorisnikeUDatoteku(path, ";"))
            {
                return;
            }
            FileInfo file = new FileInfo(path);
            if (file.Exists)
            {
                Response.Clear();
                Response.ClearHeaders();
                Response.ClearContent();
                Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name.Replace(' ', '_'));
                Response.AddHeader("Content-Length", file.Length.ToString());
                Response.ContentType = "text/plain";
                Response.Flush();
                Response.TransmitFile(file.FullName);
                Response.End();
            }
        }

        protected override void OnError(EventArgs e)
        {
            var error = Server.UrlEncode(Server.GetLastError().Message);
            Response.Redirect($"Error.aspx?error={error}");
        }
    }
}