using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebMySQL01
{
    public partial class index : System.Web.UI.Page
    {
        DBconnect ligacao = new DBconnect();
        protected void Page_Load(object sender, EventArgs e)
        {
            lblNFormandos.Text = ligacao.Count().ToString();
            lblMedia.Text=ligacao.Media().ToString();
            if(!Page.IsPostBack)
            {
                ligacao.Blind(ref GridView1);
                GridView1.DataBind();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("WebFormInsert.aspx");
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            Response.Redirect("WebFormDelete.aspx");
        }
    }
}