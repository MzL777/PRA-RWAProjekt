using DAL.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AdminSite
{
    public partial class NaziviObroka : BasePage
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
                GridViewDataBind();
            }

            MaintainScrollPositionOnPostBack = true;
        }

        private void GridViewDataBind()
        {
            try
            {
                lblInfo.Text = string.Empty;
                var naziviObroka = DAL.DAL.DohvatiNaziveObroka();
                if (naziviObroka != null && naziviObroka.Count() != 0)
                {
                    gvNaziviObroka.DataSource = naziviObroka;
                    gvNaziviObroka.DataBind();
                }
                else
                {
                    gvNaziviObroka.DataSource = Get_EmptyDataTable();
                    gvNaziviObroka.DataBind();
                    gvNaziviObroka.Rows[0].Cells.Clear();
                }
            }
            catch (Exception ex)
            {
                lblInfo.Text = ex.Message;
            }
        }

        private DataTable Get_EmptyDataTable()
        {
            DataTable dtEmpty = new DataTable();

            dtEmpty.Columns.Add("IDNazivObroka", typeof(int));
            dtEmpty.Columns.Add("Naziv", typeof(string));
            DataRow datatRow = dtEmpty.NewRow();

            dtEmpty.Rows.Add(datatRow);
            return dtEmpty;
        }

        protected void gvNaziviObroka_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                lblInfo.Text = string.Empty;
                var naziviObroka = DAL.DAL.DohvatiNaziveObroka();

                if (naziviObroka == null || naziviObroka.Count() == 0)
                {
                    e.Cancel = true;
                    return;
                }

                gvNaziviObroka.EditIndex = e.NewEditIndex;
                GridViewDataBind();
                gvNaziviObroka.Rows[e.NewEditIndex].FindControl("txtNaziv").Focus();
            }
            catch (Exception ex)
            {
                lblInfo.Text = ex.Message;
            }
        }

        protected void gvNaziviObroka_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvNaziviObroka.EditIndex = -1;
            GridViewDataBind();
        }

        protected void gvNaziviObroka_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                lblInfo.Text = string.Empty;
                var updateRow = gvNaziviObroka.Rows[e.RowIndex];
                var naziv = (updateRow.FindControl("txtNaziv") as TextBox).Text;

                DAL.DAL.UrediNazivObroka(new NazivObroka
                {
                    Naziv = naziv,
                    IDNazivObroka = (int)gvNaziviObroka.DataKeys[e.RowIndex].Value
                });
            }
            catch (Exception ex)
            {
                lblInfo.Text = ex.Message;
            }

            gvNaziviObroka.EditIndex = -1;
            GridViewDataBind();
        }

        protected void gvNaziviObroka_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                lblInfo.Text = string.Empty;
                DAL.DAL.UkloniNazivObroka((int)gvNaziviObroka.DataKeys[e.RowIndex].Value);
            }
            catch (Exception ex)
            {
                lblInfo.Text = ex.Message;
            }
            GridViewDataBind();
        }

        protected void btnDodaj_Click(object sender, EventArgs e)
        {
            try
            {
                lblInfo.Text = string.Empty;
                var nazivNovog = (gvNaziviObroka.FooterRow.FindControl("txtNazivNovog") as TextBox)?.Text;

                DAL.DAL.DodajNazivObroka(
                    new NazivObroka
                    {
                        Naziv = nazivNovog
                    });
            }
            catch (Exception ex)
            {
                lblInfo.Text = ex.Message;
            }

            GridViewDataBind();
            gvNaziviObroka.FooterRow.FindControl("txtNazivNovog").Focus();
        }

        protected override void OnError(EventArgs e)
        {
            var error = Server.UrlEncode(Server.GetLastError().Message);
            Response.Redirect($"Error.aspx?error={error}");
        }
    }
}