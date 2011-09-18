using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Karvis.Core;

namespace Karvis.Web
{
    public partial class sand : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int pageIndex = 0;
                var model = new JobModel();
                dgSand.DataSource = model.FindAll(string.Empty, string.Empty, AdSource.All, hdnSortExpression.Value, dgSand.PageSize, pageIndex);
                dgSand.VirtualItemCount = model.FindAllCount(string.Empty, string.Empty, AdSource.All);
                dgSand.CurrentPageIndex = pageIndex;
                dgSand.DataBind();
            }

        }

        protected void dgSand_SortCommand(object source, DataGridSortCommandEventArgs e)
        {
            const string DESC = " desc";
            int descLength = DESC.Length;
            string newValue = e.SortExpression;
            string oldValue = hdnSortExpression.Value;
            if (oldValue == newValue)
                newValue += DESC;
            if (oldValue.EndsWith(DESC))
                newValue = oldValue.Remove(oldValue.Length - descLength, descLength);
            hdnSortExpression.Value = newValue;

            int pageIndex = 0;
            var model = new JobModel();
            dgSand.DataSource = model.FindAll(string.Empty, string.Empty, AdSource.All, hdnSortExpression.Value, dgSand.PageSize, pageIndex);
            dgSand.VirtualItemCount = model.FindAllCount(string.Empty, string.Empty, AdSource.All);
            dgSand.CurrentPageIndex = pageIndex;
            dgSand.DataBind();

        }

        protected void dgSand_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            var model = new JobModel();
            var list = model.FindAll(string.Empty, string.Empty, AdSource.All, hdnSortExpression.Value,
                dgSand.PageSize, e.NewPageIndex * dgSand.PageSize);
            dgSand.DataSource = list;
            var count = model.FindAllCount(string.Empty, string.Empty, AdSource.All);
            dgSand.VirtualItemCount = count;
            dgSand.CurrentPageIndex = e.NewPageIndex;
            dgSand.DataBind();
        }
    }
}