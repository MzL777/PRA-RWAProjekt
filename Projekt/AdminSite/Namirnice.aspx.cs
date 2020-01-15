using DAL.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AdminSite
{
    public partial class Namirnice : BasePage
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
                var namirnice = DAL.DAL.DohvatiNamirnice();
                if (namirnice == null || namirnice.Count() == 0)
                {
                    gvNamirnice.DataSource = Get_EmptyDataTableNamirnice();
                    gvNamirnice.DataBind();
                    gvNamirnice.Rows[0].Cells.Clear();

                    LoadFooterTipoveNamirnica();
                }
                else
                {
                    gvNamirnice.DataSource = namirnice;
                    gvNamirnice.DataBind();

                    LoadEditTipoveNamirnica();
                    LoadFooterTipoveNamirnica();
                    LoadFooterMjerneJedinice();
                }
            }
            catch (Exception ex)
            {
                lblInfo.Text = ex.Message;
            }
        }

        private void LoadEditTipoveNamirnica()
        {
            foreach (GridViewRow row in gvNamirnice.Rows)
            {
                var ddlTipoviNamirnica = row.FindControl("ddlTipoviNamirnica") as DropDownList;
                var tipoviNamirnica = DAL.DAL.DohvatiTipoveNamirnica();

                foreach (var tipNamirnice in tipoviNamirnica)
                {
                    ddlTipoviNamirnica
                        ?.Items
                        ?.Add(new ListItem
                        {
                            Text = tipNamirnice.Naziv,
                            Value = tipNamirnice.IDTipNamirnice.ToString()
                        });
                }
            }
        }

        private void LoadFooterTipoveNamirnica()
        {
            var ddlTipoviNamirnica = gvNamirnice
                ?.FooterRow
                ?.FindControl("ddlTipoviNamirniceNove") as DropDownList;

            foreach (var tipNamirnice in DAL.DAL.DohvatiTipoveNamirnica())
            {
                ddlTipoviNamirnica
                    ?.Items
                    ?.Add(new ListItem
                    {
                        Text = tipNamirnice.Naziv,
                        Value = tipNamirnice.IDTipNamirnice.ToString()
                    });
            }
        }

        private void LoadFooterMjerneJedinice()
        {
            if (gvNamirnice.SelectedIndex != -1)
            {
                try
                {
                    lblInfo.Text = string.Empty;
                    var mjerneJediniceZaNamirnicu = DAL.DAL.DohvatiMjerneJediniceZaNamirnicu((int)gvNamirnice.DataKeys[gvNamirnice.SelectedIndex].Value);

                    if (mjerneJediniceZaNamirnicu == null || mjerneJediniceZaNamirnicu.Count() == 0)
                    {
                        gvMjerneJediniceZaNamirnicu.DataSource = Get_EmptyDataTableMjerneJedinice();
                        gvMjerneJediniceZaNamirnicu.DataBind();
                        gvMjerneJediniceZaNamirnicu.Rows[0].Cells.Clear();
                    }
                    else
                    {
                        gvMjerneJediniceZaNamirnicu.DataSource = mjerneJediniceZaNamirnicu;
                        gvMjerneJediniceZaNamirnicu.DataBind();
                    }

                    foreach (var mjernaJedinica in DAL.DAL.DohvatiMjerneJedinice().Except(mjerneJediniceZaNamirnicu))
                    {
                        (gvMjerneJediniceZaNamirnicu
                            .FooterRow
                            .FindControl("ddlMjernaJedinicaNove") as DropDownList)
                            .Items
                            .Add(new ListItem
                            {
                                Text = mjernaJedinica.Naziv,
                                Value = mjernaJedinica.IDMjerneJedinice.ToString()
                            });
                    }
                }
                catch (Exception ex)
                {
                    lblInfo.Text = ex.Message;
                }
            }
        }

        private DataTable Get_EmptyDataTableNamirnice()
        {
            DataTable dtEmpty = new DataTable();

            dtEmpty.Columns.Add("IDNamirnice", typeof(int));
            dtEmpty.Columns.Add("Naziv", typeof(string));
            dtEmpty.Columns.Add("TipNamirniceID", typeof(int));
            dtEmpty.Columns.Add("EnergetskaVrijednostKcalPoGramu", typeof(float));
            dtEmpty.Columns.Add("EnergetskaVrijednostKJPoGramu", typeof(float));
            DataRow datatRow = dtEmpty.NewRow();

            dtEmpty.Rows.Add(datatRow);
            return dtEmpty;
        }

        private DataTable Get_EmptyDataTableMjerneJedinice()
        {
            DataTable dtEmpty = new DataTable();

            dtEmpty.Columns.Add("IDMjerneJedinice", typeof(int));
            dtEmpty.Columns.Add("Naziv", typeof(string));
            dtEmpty.Columns.Add("TezinaUGramima", typeof(int));
            DataRow datatRow = dtEmpty.NewRow();

            dtEmpty.Rows.Add(datatRow);
            return dtEmpty;
        }

        public string GetTipNamirnice(string s)
        {
            if (int.TryParse(s, out int id))
            {
                return DAL.DAL.DohvatiTipoveNamirnica().FirstOrDefault(x => x.IDTipNamirnice == id)?.Naziv;
            }

            return string.Empty;
        }

        protected void gvNamirnice_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                gvNamirnice.SelectedIndex = -1;
                lblInfo.Text = string.Empty;
                var namirnice = DAL.DAL.DohvatiNamirnice();

                if (namirnice == null || namirnice.Count() == 0)
                {
                    e.Cancel = true;
                    return;
                }

                var selectedNamirnica = namirnice.First(x => x.IDNamirnice == (int)gvNamirnice.DataKeys[e.NewEditIndex].Value);

                gvNamirnice.EditIndex = e.NewEditIndex;
                GridViewDataBind();

                (gvNamirnice.Rows[e.NewEditIndex].FindControl("ddlTipoviNamirnica") as DropDownList).SelectedValue = selectedNamirnica.TipNamirniceID.ToString();
            }
            catch (Exception ex)
            {
                lblInfo.Text = ex.Message;
            }
        }

        protected void gvNamirnice_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                lblInfo.Text = string.Empty;
                DAL.DAL.UkloniNamirnicu((int)gvNamirnice.DataKeys[e.RowIndex].Value);
            }
            catch (Exception ex)
            {
                lblInfo.Text = ex.Message;
            }

            GridViewDataBind();
        }

        protected void gvNamirnice_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvNamirnice.EditIndex = -1;
            GridViewDataBind();
        }

        protected void gvNamirnice_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                lblInfo.Text = string.Empty;
                var updateRow = gvNamirnice.Rows[e.RowIndex];
                var naziv = (updateRow.FindControl("txtNaziv") as TextBox).Text;
                var tipNamirnice = int.Parse((updateRow.FindControl("ddlTipoviNamirnica") as DropDownList).SelectedItem.Value);
                var KcalPoGramu = float.Parse((updateRow.FindControl("txtKcalPoGramu") as TextBox).Text.Replace('.', ','));
                var KJPoGramu = float.Parse((updateRow.FindControl("txtKJPoGramu") as TextBox).Text.Replace('.', ','));

                DAL.DAL.UrediNamirnicu(new Namirnica()
                {
                    IDNamirnice = (int)gvNamirnice.DataKeys[e.RowIndex].Value,
                    Naziv = naziv,
                    EnergetskaVrijednostKcalPoGramu = KcalPoGramu,
                    EnergetskaVrijednostKJPoGramu = KJPoGramu,
                    TipNamirniceID = tipNamirnice,
                    MjerneJedinice = null
                });
            }
            catch (Exception ex)
            {
                lblInfo.Text = ex.Message;
            }

            gvNamirnice.EditIndex = -1;
            GridViewDataBind();
        }

        protected void btnDodaj_Click(object sender, EventArgs e)
        {
            try
            {
                lblInfo.Text = string.Empty;
                var naziv = (gvNamirnice.FooterRow.FindControl("txtNazivNove") as TextBox).Text;
                var tipNamirnice = int.Parse((gvNamirnice.FooterRow.FindControl("ddlTipoviNamirniceNove") as DropDownList).SelectedItem.Value);
                var KcalPoGramu = float.Parse((gvNamirnice.FooterRow.FindControl("txtKcalPoGramuNove") as TextBox).Text.Replace('.', ','));
                var KJPoGramu = float.Parse((gvNamirnice.FooterRow.FindControl("txtKJPoGramuNove") as TextBox).Text.Replace('.', ','));

                DAL.DAL.DodajNamirnicu(new Namirnica
                {
                    Naziv = naziv,
                    TipNamirniceID = tipNamirnice,
                    EnergetskaVrijednostKcalPoGramu = KcalPoGramu,
                    EnergetskaVrijednostKJPoGramu = KJPoGramu
                });
            }
            catch (Exception ex)
            {
                lblInfo.Text = ex.Message;
            }

            GridViewDataBind();
            gvNamirnice.FooterRow.FindControl("txtNazivNove").Focus();
        }

        protected void gvNamirnice_SelectedIndexChanged(object sender, EventArgs e)
        {
            gvNamirnice.EditIndex = -1;
            lblNamirnica.Text = GetLocalResourceObject("lblMeasuringUnitsForGrocery") + (gvNamirnice.SelectedRow.FindControl("lbNaziv") as Label).Text;
            GridViewDataBind();

            gvMjerneJediniceZaNamirnicu?.FooterRow.FindControl("ddlMjernaJedinicaNove")?.Focus();
        }

        protected void btnDodajMjernuJedinicu_Click(object sender, EventArgs e)
        {
            try
            {
                lblInfo.Text = string.Empty;
                var iDMjerneJedinice = int.Parse((gvMjerneJediniceZaNamirnicu.FooterRow.FindControl("ddlMjernaJedinicaNove") as DropDownList).SelectedItem.Value);
                var tezinaUGramima = int.Parse((gvMjerneJediniceZaNamirnicu.FooterRow.FindControl("txtTezinaGramaNove") as TextBox).Text);
                var idNamirnice = (int)gvNamirnice.DataKeys[gvNamirnice.SelectedIndex].Value;

                DAL.DAL.DodajMjernuJedinicuZaNamirnicu(
                    idNamirnice,
                    new MjernaJedinica
                    {
                        IDMjerneJedinice = iDMjerneJedinice,
                        TezinaUGramima = tezinaUGramima
                    });
            }
            catch (Exception ex)
            {
                lblInfo.Text = ex.Message;
            }

            GridViewDataBind();
            gvMjerneJediniceZaNamirnicu?.FooterRow.FindControl("ddlMjernaJedinicaNove")?.Focus();
        }

        protected void gvMjerneJediniceZaNamirnicu_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                lblInfo.Text = string.Empty;
                var mjernaJedinicaID = (int)gvMjerneJediniceZaNamirnicu.DataKeys[e.RowIndex].Value;
                int namirnicaID = (int)gvNamirnice.DataKeys[gvNamirnice.SelectedIndex].Value;
                DAL.DAL.UkloniMjernuJedinicuZaNamirnicu(namirnicaID, mjernaJedinicaID);
            }
            catch (Exception ex)
            {
                lblInfo.Text = ex.Message;
            }
            GridViewDataBind();
        }

        protected void gvMjerneJediniceZaNamirnicu_RowEditing(object sender, GridViewEditEventArgs e)
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

                gvMjerneJediniceZaNamirnicu.EditIndex = e.NewEditIndex;
                GridViewDataBind();

                gvMjerneJediniceZaNamirnicu.Rows[e.NewEditIndex].FindControl("txtTezinaGrama")?.Focus();
            }
            catch (Exception ex)
            {
                lblInfo.Text = ex.Message;
            }
        }

        protected void gvMjerneJediniceZaNamirnicu_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvMjerneJediniceZaNamirnicu.EditIndex = -1;
            GridViewDataBind();
            gvMjerneJediniceZaNamirnicu.FooterRow.FindControl("ddlMjernaJedinicaNove")?.Focus();
        }

        protected void gvMjerneJediniceZaNamirnicu_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                lblInfo.Text = string.Empty;
                GridViewRow updateRow = gvMjerneJediniceZaNamirnicu.Rows[e.RowIndex];
                var mjernaJedinica = (int)gvMjerneJediniceZaNamirnicu.DataKeys[e.RowIndex].Value;
                var tezinaUGramima = int.Parse((updateRow.FindControl("txtTezinaGrama") as TextBox).Text);
                var idNamirnice = (int)gvNamirnice.DataKeys[gvNamirnice.SelectedIndex].Value;

                DAL.DAL.UrediMjernuJedinicuZaNamirnicu(
                    idNamirnice,
                    new MjernaJedinica()
                    {
                        IDMjerneJedinice = mjernaJedinica,
                        TezinaUGramima = tezinaUGramima
                    });

                gvMjerneJediniceZaNamirnicu?.FooterRow.FindControl("ddlMjernaJedinicaNove")?.Focus();
            }
            catch (Exception ex)
            {
                lblInfo.Text = ex.Message;
            }

            gvMjerneJediniceZaNamirnicu.EditIndex = -1;
            GridViewDataBind();
        }

        protected override void OnError(EventArgs e)
        {
            var error = Server.UrlEncode(Server.GetLastError().Message);
            Response.Redirect($"Error.aspx?error={error}");
        }
    }
}