using AdminSite.UserControls;
using DAL.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AdminSite
{
    public partial class Kombinacije : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!DAL.DAL.JeLiAdminAutenticiran(Session, Server))
            {
                Session["Site"] = HttpContext.Current.Request.Url.AbsolutePath;
                Response.Redirect("Login.aspx");
            }

            GridViewDataBind();

            MaintainScrollPositionOnPostBack = true;
        }

        private void GridViewDataBind()
        {
            try
            {
                btnSave.Enabled = false;
                lblInfo.Text = string.Empty;
                txtDateValidFrom.Text = DateTime.Now.ToShortDateString();

                var selectedObroka = int.Parse(ddlBrojObroka.SelectedValue);
                var kombinacije = DAL.DAL.DohvatiKombinacijuZaBrojObroka(selectedObroka);
                var naziviObroka = DAL.DAL.DohvatiNaziveObroka();

                if (kombinacije == null || kombinacije.Obroci == null || kombinacije.Obroci.Count() == 0)
                {
                    phObroci.Controls.Clear();

                    // Kreiraj prazne kontrole kako bi se mogle upisivati vrijednosti.
                    var headerRow = Page.LoadControl("~/UserControls/ObrokUKombinacijiControl.ascx") as ObrokUKombinacijiControl;
                    headerRow.HeaderOnly = true;

                    phObroci.Controls.Add(headerRow);

                    for (int i = 0; i < selectedObroka; i++)
                    {
                        var obrokUserControl = Page.LoadControl("~/UserControls/ObrokUKombinacijiControl.ascx") as ObrokUKombinacijiControl;

                        obrokUserControl.Editable = true;
                        obrokUserControl.IDUC = i;
                        obrokUserControl.naziviObroka = naziviObroka;

                        phObroci.Controls.Add(obrokUserControl);
                    }

                    btnDisable.Enabled = false;
                    btnSave.Enabled = true;
                    lblInfo.Text = GetLocalResourceObject("infoAdd").ToString();
                }
                else
                {
                    phObroci.Controls.Clear();

                    // Popuni kontrole s dohvaćenim podacima (read-only).
                    var headerRow = Page.LoadControl("~/UserControls/ObrokUKombinacijiControl.ascx") as ObrokUKombinacijiControl;
                    headerRow.HeaderOnly = true;

                    phObroci.Controls.Add(headerRow);

                    for (int i = 0; i < selectedObroka; i++)
                    {
                        var obrokUserControl = Page.LoadControl("~/UserControls/ObrokUKombinacijiControl.ascx") as ObrokUKombinacijiControl;

                        obrokUserControl.Editable = false;
                        obrokUserControl.IDUC = i;
                        obrokUserControl.Obrok = kombinacije.Obroci[i];

                        phObroci.Controls.Add(obrokUserControl);
                    }

                    btnDisable.Enabled = true;
                    btnSave.Enabled = false;
                    kombinacijaIDhidden.Value = kombinacije.IDKombinacijeObroka.ToString();
                    lblInfo.Text = GetLocalResourceObject("infoView").ToString();
                }
            }
            catch (Exception ex)
            {
                lblInfo.Text = ex.ToString();
            }
        }

        protected void ddlBrojObroka_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (ObrokUKombinacijiControl ctrl in phObroci.Controls)
            {
                if (ctrl == phObroci.Controls[0])
                {
                    continue;
                }

                if (int.TryParse(ddlBrojObroka.SelectedValue, out int selected))
                {
                    var kombinacije = DAL.DAL.DohvatiKombinacijuZaBrojObroka(selected);

                    if (kombinacije == null || kombinacije.Obroci == null || kombinacije.Obroci.Count() == 0)
                    {
                        ctrl.Clear();
                    }
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                lblInfo.Text = "";
                DateTime vrijediOd, vrijedoDo;
                DateTime.TryParse(txtDateValidFrom.Text, out vrijediOd);
                DateTime.TryParse(txtDateValidTo.Text, out vrijedoDo);

                var obroci = new List<ObrokUKombinaciji>();

                foreach (var ctrl in phObroci.Controls)
                {
                    if (ctrl.Equals(phObroci.Controls[0]))
                    {
                        continue;
                    }

                    if (ctrl is ObrokUKombinacijiControl)
                    {
                        obroci.Add((ctrl as ObrokUKombinacijiControl).Obrok);
                        lblInfo.Text += (ctrl as ObrokUKombinacijiControl).Obrok + Environment.NewLine;
                    }
                }

                DAL.DAL.DodajKombinaciju(new KombinacijaObroka()
                {
                    VrijediOd = vrijediOd,
                    Obroci = obroci
                });

                lblInfo.Text = GetLocalResourceObject("comboAdded").ToString();
            }
            catch (Exception ex)
            {
                lblInfo.Text = ex.ToString();
            }
            Response.Redirect(HttpContext.Current.Request.Url.AbsolutePath);
        }

        protected void btnDisable_Click(object sender, EventArgs e)
        {
            txtDateValidTo.Text = DateTime.Now.ToShortDateString();
            DAL.DAL.UkloniKombinaciju(new KombinacijaObroka {
                IDKombinacijeObroka = int.Parse(kombinacijaIDhidden.Value),
                VrijediDo = DateTime.Now });
            Response.Redirect(HttpContext.Current.Request.Url.AbsolutePath);
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(HttpContext.Current.Request.Url.AbsolutePath);
        }

        protected override void OnError(EventArgs e)
        {
            var error = Server.UrlEncode(Server.GetLastError().Message);
            Response.Redirect($"Error.aspx?error={error}");
        }
    }
}