using System.Data;
using System.IO;
using System.Web;
using ClosedXML.Excel;

public class logicaExportarExcel
{
    public static void ExportarMicrosoftExcel(DataTable pDataTable, string pNombreArchivo)
    {
        using (XLWorkbook libro = new XLWorkbook())
        {
            var hoja = libro.Worksheets.Add(pDataTable);
            hoja.ColumnsUsed().AdjustToContents();
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.Charset = "";
            HttpContext.Current.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            HttpContext.Current.Response.AddHeader("content-disposition", $"attachment;filename={pNombreArchivo}.xlsx");
            using (MemoryStream MyMemoryStream = new MemoryStream())
            {
                libro.SaveAs(MyMemoryStream);
                HttpContext.Current.Response.BinaryWrite(MyMemoryStream.ToArray());
                HttpContext.Current.Response.Flush();
                HttpContext.Current.Response.End();
            }
        }
    }
}
