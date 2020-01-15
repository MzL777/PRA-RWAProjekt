using DAL;
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
    public partial class MjerneJedinice : BasePage
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
                var mjerneJedinice = DAL.DAL.DohvatiMjerneJedinice();
                if (mjerneJedinice != null && mjerneJedinice.Count() != 0)
                {
                    gvMjerneJedinice.DataSource = mjerneJedinice;
                    gvMjerneJedinice.DataBind();
                }
                else
                {
                    gvMjerneJedinice.DataSource = Get_EmptyDataTable();
                    gvMjerneJedinice.DataBind();
                    gvMjerneJedinice.Rows[0].Cells.Clear();
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

            dtEmpty.Columns.Add("IDMjerneJedinice", typeof(int));
            dtEmpty.Columns.Add("Naziv", typeof(string));
            DataRow datatRow = dtEmpty.NewRow();

            dtEmpty.Rows.Add(datatRow);
            return dtEmpty;
        }

        protected void gvMjerneJedinice_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                lblInfo.Text = string.Empty;
                var mjerneJedinice = DAL.DAL.DohvatiMjerneJedinice();

                if (mjerneJedinice == null || mjerneJedinice.Count() == 0)
                {
                    e.Cancel = true;
                    return;
                }

                gvMjerneJedinice.EditIndex = e.NewEditIndex;
                GridViewDataBind();
                gvMjerneJedinice.Rows[e.NewEditIndex].FindControl("txtNaziv").Focus();
            }
            catch (Exception ex)
            {
                lblInfo.Text = ex.Message;
            }
        }

        protected void gvMjerneJedinice_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvMjerneJedinice.EditIndex = -1;
            GridViewDataBind();
        }

        protected void gvMjerneJedinice_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                lblInfo.Text = string.Empty;
                var updateRow = gvMjerneJedinice.Rows[e.RowIndex];
                var naziv = (updateRow.FindControl("txtNaziv") as TextBox).Text;

                DAL.DAL.UrediMjernuJedinicu(new MjernaJedinica
                {
                    Naziv = naziv,
                    IDMjerneJedinice = (int)gvMjerneJedinice.DataKeys[e.RowIndex].Value
                });
            }
            catch (Exception ex)
            {
                lblInfo.Text = ex.Message;
            }

            gvMjerneJedinice.EditIndex = -1;
            GridViewDataBind();
        }

        protected void gvMjerneJedinice_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                lblInfo.Text = string.Empty;
                DAL.DAL.UkloniMjernuJedinicu((int)gvMjerneJedinice.DataKeys[e.RowIndex].Value);
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
                var nazivNove = (gvMjerneJedinice.FooterRow.FindControl("txtNazivNove") as TextBox)?.Text;
                DAL.DAL.DodajMjernuJedinicu(
                    new MjernaJedinica
                    {
                        Naziv = nazivNove
                    });
            }
            catch (Exception ex)
            {
                lblInfo.Text = ex.Message;
            }

            GridViewDataBind();
            gvMjerneJedinice.FooterRow.FindControl("txtNazivNove").Focus();
        }

        protected override void OnError(EventArgs e)
        {
            var error = Server.UrlEncode(Server.GetLastError().Message);
            Response.Redirect($"Error.aspx?error={error}");
        }
    }
}