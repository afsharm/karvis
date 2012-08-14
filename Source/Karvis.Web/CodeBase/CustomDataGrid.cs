using System;
using System.Linq;
using System.Reflection;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Karvis.Web.CodeBase
{
    public class CustomDataGrid : DataGrid
    {
        //protected override void OnPreRender(EventArgs e)

        //{
        //    var table = Controls[0] as Table;


        //    if (table != null && table.Rows.Count > 0)
        //    {
        //        table.Rows[0].TableSection = TableRowSection.TableHeader;

        //        table.Rows[table.Rows.Count - 1].TableSection = TableRowSection.TableFooter;


        //        FieldInfo field = typeof (WebControl).GetField("tagKey", BindingFlags.Instance | BindingFlags.NonPublic);


        //        foreach (TableCell cell in table.Rows[0].Cells.Cast<TableCell>().Where(cell => field != null))
        //        {
        //            if (field != null) field.SetValue(cell, HtmlTextWriterTag.Th);
        //        }
        //    }

        //        base.OnPreRender(e);
        //}
    }
}