using DAL.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AdminSite.UserControls
{
    public partial class ObrokUKombinacijiControl : System.Web.UI.UserControl
    {
        private int id;

        private ObrokUKombinaciji obrok = new ObrokUKombinaciji();

        public ObrokUKombinaciji Obrok
        {
            get
            {
                var UdioMasti = int.Parse(txtUdioMasti.Text);
                var UdioBjelancevina = int.Parse(txtUdioBjelancevina.Text);
                var UdioUgljikohidrata = int.Parse(txtUdioUgljikohidrata.Text);
                var DnevniUdio = int.Parse(txtDnevniUdio.Text);
                var NazivObrokaID = int.Parse(ddlNazivObroka.SelectedValue);

                obrok = new ObrokUKombinaciji()
                {
                    UdioMasti = UdioMasti,
                    UdioBjelancevina = UdioBjelancevina,
                    UdioUgljikohidrata = UdioUgljikohidrata,
                    DnevniUdio = DnevniUdio,
                    NazivObroka = DAL.DAL.DohvatiNaziveObroka().First(x => x.IDNazivObroka == NazivObrokaID)
                };

                return obrok;
            }
            set
            {
                obrok = value;

                if (obrok == null)
                {
                    txtUdioMasti.Text = string.Empty;
                    txtDnevniUdio.Text = string.Empty;
                    txtNazivObroka.Text = string.Empty;
                    txtUdioBjelancevina.Text = string.Empty;
                    txtUdioUgljikohidrata.Text = string.Empty;
                    return;
                }

                txtUdioMasti.Text = obrok.UdioMasti.ToString();
                txtDnevniUdio.Text = obrok.DnevniUdio.ToString();
                txtNazivObroka.Text = obrok.NazivObroka.Naziv;
                txtUdioBjelancevina.Text = obrok.UdioBjelancevina.ToString();
                txtUdioUgljikohidrata.Text = obrok.UdioUgljikohidrata.ToString();
            }
        }

        public IEnumerable<NazivObroka> naziviObroka
        {
            set
            {
                foreach (var no in value)
                {
                    ddlNazivObroka.Items.Add(new ListItem { Value = no.IDNazivObroka.ToString(), Text = no.Naziv });
                }
            }
        }

        public int IDUC
        {
            get => id;
            set
            {
                id = value;
                if (id % 2 == 1)
                {
                    Odd = true;
                }
            }
        }

        public bool Odd
        {
            set
            {
                if (value)
                {
                    SetBackColor(Color.FromArgb(255, 221, 221));
                    SetForeColor(Color.DarkRed);
                }
            }
        }
        public bool Editable
        {
            set
            {
                ddlNazivObroka.Visible = value;
                txtNazivObroka.Visible = !value;
                ddlNazivObroka.Enabled = value;
                txtUdioMasti.ReadOnly = !value;
                txtDnevniUdio.ReadOnly = !value;
                txtNazivObroka.ReadOnly = !value;
                txtUdioBjelancevina.ReadOnly = !value;
                txtUdioUgljikohidrata.ReadOnly = !value;
            }
        }
        public bool HeaderOnly
        {
            set
            {
                if (value)
                {
                    Editable = false;

                    SetFontBold(true);
                    SetValidators(false);

                    SetBackColor(Color.FromArgb(85, 0, 0));
                    SetForeColor(Color.White);

                    txtNazivObroka.Text = GetLocalResourceObject("mealNameTitle").ToString();
                    txtDnevniUdio.Text = GetLocalResourceObject("dailyShareTitle").ToString();
                    txtUdioMasti.Text = GetLocalResourceObject("fatProportionTitle").ToString();
                    txtUdioBjelancevina.Text = GetLocalResourceObject("proteinProportionTitle").ToString();
                    txtUdioUgljikohidrata.Text = GetLocalResourceObject("carbohydrateProportionTitle").ToString();
                }
                else
                {
                    txtNazivObroka.Visible = false;
                    ddlNazivObroka.Visible = true;
                }
            }
        }

        public string GetNazivObroka(int index)
        {
            var naziviObroka = DAL.DAL.DohvatiNaziveObroka();
            if (naziviObroka == null || naziviObroka.Count() == 0)
            {
                return string.Empty;
            }

            return naziviObroka.FirstOrDefault(x => x.IDNazivObroka == index)?.Naziv;
        }

        private void SetValidators(bool value)
        {
            rangeval1.Enabled = value;
            rangeval2.Enabled = value;
            rangeval3.Enabled = value;
            rangeval4.Enabled = value;
            regexval1.Enabled = value;
            regexval2.Enabled = value;
            regexval3.Enabled = value;
            regexval4.Enabled = value;
            reqval1.Enabled = value;
            reqval2.Enabled = value;
            reqval3.Enabled = value;
            reqval4.Enabled = value;

            CustomValidator1.Enabled = value;
        }
        private void SetFontBold(bool value)
        {
            txtUdioMasti.Font.Bold = value;
            txtDnevniUdio.Font.Bold = value;
            txtNazivObroka.Font.Bold = value;
            txtUdioBjelancevina.Font.Bold = value;
            txtUdioUgljikohidrata.Font.Bold = value;
        }
        private void SetBackColor(Color back)
        {
            ddlNazivObroka.BackColor = back;
            txtUdioMasti.BackColor = back;
            txtDnevniUdio.BackColor = back;
            txtNazivObroka.BackColor = back;
            txtUdioBjelancevina.BackColor = back;
            txtUdioUgljikohidrata.BackColor = back;
        }
        private void SetForeColor(Color fore)
        {
            ddlNazivObroka.ForeColor = fore;
            txtUdioMasti.ForeColor = fore;
            txtDnevniUdio.ForeColor = fore;
            txtNazivObroka.ForeColor = fore;
            txtUdioBjelancevina.ForeColor = fore;
            txtUdioUgljikohidrata.ForeColor = fore;
        }

        public void Clear()
        {
            txtUdioMasti.Text = string.Empty;
            txtDnevniUdio.Text = string.Empty;
            txtUdioBjelancevina.Text = string.Empty;
            txtUdioUgljikohidrata.Text = string.Empty;
        }
       
    }
}