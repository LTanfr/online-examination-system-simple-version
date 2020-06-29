using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Text;
using System.Data.SqlClient;

using NPOI;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.HSSF.Util;

public partial class teacher_TExaminationResult : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["teacher"] == null)
        {
            Response.Redirect("../Login.aspx");
        }
    }

    protected void btnserch_Click(object sender, EventArgs e)
    {
        string type = ddltype.SelectedItem.Text;//获取查询的范围
        if (type == "学号")//如果选择“学号”
        {
            string resultstr = "select * from tb_score where StudentID like '%" + txtkey.Text.Trim() + "%' and LessonName ='" + Session["KCname"].ToString() + "'";
            BaseClass.BindDG(gvExaminationresult, "ID", resultstr, "result");//在学号范围内查找
            Session["num"] = "学号";
        }
        if (type == "姓名")//如果选择“姓名”
        {
            string resultstr = "select * from tb_score where StudentName like '%" + txtkey.Text.Trim() + "%' and LessonName='" + Session["KCname"].ToString() + "'";
            BaseClass.BindDG(gvExaminationresult, "ID", resultstr, "result");//在姓名范围内查找
            Session["num"] = "姓名";
        }
    }

    protected void gvExaminationInfo_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int id = (int)gvExaminationresult.DataKeys[e.RowIndex].Value;//获取欲删除信息的id
        string strsql = "delete from tb_score where ID=" + id;//执行删除操作的SQL语句
        BaseClass.OperateData(strsql);
        if (Session["num"].ToString() == "学号")//判断当前查询的范围
        {
            string resultstr = "select * from tb_score where StudentID like '%" + txtkey.Text.Trim() + "%' and LessonName='" + Session["KCname"].ToString() + "'";
            BaseClass.BindDG(gvExaminationresult, "ID", resultstr, "result");
        }
        else
        {
            string resultstr = "select * from tb_score where StudentName like '%" + txtkey.Text.Trim() + "%' and LessonName='" + Session["KCname"].ToString() + "'";
            BaseClass.BindDG(gvExaminationresult, "ID", resultstr, "result");
        }
    }

    protected void gvExaminationresult_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        if (Session["num"].ToString() == "学号")
        {
            gvExaminationresult.PageIndex = e.NewPageIndex;//判断当前查询范围
            string resultstr = "select * from tb_score where StudentID like '%" + txtkey.Text.Trim() + "%' and LessonName='" + Session["KCname"].ToString() + "'";
            BaseClass.BindDG(gvExaminationresult, "ID", resultstr, "result");
        }
        else
        {
            gvExaminationresult.PageIndex = e.NewPageIndex;
            string resultstr = "select * from tb_score where StudentName like '%" + txtkey.Text.Trim() + "%' and LessonName='" + Session["KCname"].ToString() + "'";
            BaseClass.BindDG(gvExaminationresult, "ID", resultstr, "result");
        }
    }

    protected void below60btn_Click(object sender, EventArgs e)
    {
        string resultstr = "select * from tb_score where score < 60 and LessonName ='" + Session["KCname"].ToString() + "'";
        BaseClass.BindDG(gvExaminationresult, "ID", resultstr, "result");//在学号范围内查找
        Session["num"] = "学号";
    }

    protected void beyond85btn_Click(object sender, EventArgs e)
    {
        string resultstr = "select * from tb_score where score >= 85 and LessonName ='" + Session["KCname"].ToString() + "'";
        BaseClass.BindDG(gvExaminationresult, "ID", resultstr, "result");//在学号范围内查找
        Session["num"] = "学号";
    }


    protected void output_Click(object sender, EventArgs e)
    {
        string sqlstr = "select StudentID, StudentName, LessonName, score from tb_score";
        SqlConnection conn = BaseClass.DBCon();//创建数据库连接对象
        conn.Open();//打开数据库连接
        SqlDataAdapter adapter = new SqlDataAdapter(sqlstr, conn);
        DataSet ds = new DataSet();
        adapter.Fill(ds);
        DataTable dt = ds.Tables[0];
        NpoiExcel(dt, "output");
        conn.Close();//关闭数据库
        conn.Dispose();
        adapter.Dispose();
        ds.Dispose();
    }

    public void NpoiExcel(DataTable dt, string title)
    {
        HSSFWorkbook book = new HSSFWorkbook();
        ISheet sheet = book.CreateSheet("Sheet1");
        //表头格式
        IRow headerrow = sheet.CreateRow(0);
        ICellStyle style = book.CreateCellStyle();
        style.Alignment = HorizontalAlignment.Center;
        style.VerticalAlignment = VerticalAlignment.Center;
        IFont font = book.CreateFont();
        font.Boldweight = 20;
        font.FontHeightInPoints = 14;
        //单元格格式
        ICellStyle bodyStyle = book.CreateCellStyle();
        bodyStyle.Alignment = HorizontalAlignment.Center;
        bodyStyle.VerticalAlignment = VerticalAlignment.Center;
        IFont font1 = book.CreateFont();
        font1.Boldweight = 20;
        bodyStyle.SetFont(font1);
        //列宽
        sheet.SetColumnWidth(0, 20 * 256);
        sheet.SetColumnWidth(1, 20 * 256);
        sheet.SetColumnWidth(2, 20 * 256);
        sheet.SetColumnWidth(3, 20 * 256);
        //表头
        ICell icell1top = headerrow.CreateCell(0);
        icell1top.CellStyle = style;
        icell1top.SetCellValue("学号");

        ICell icell2top = headerrow.CreateCell(1);
        icell2top.CellStyle = style;
        icell2top.SetCellValue("姓名");

        ICell icell3top = headerrow.CreateCell(2);
        icell3top.CellStyle = style;
        icell3top.SetCellValue("科目");

        ICell icell4top = headerrow.CreateCell(3);
        icell4top.CellStyle = style;
        icell4top.SetCellValue("成绩");
        //表内容
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            IRow row = sheet.CreateRow(i + 1);
            for (int j = 0; j < dt.Columns.Count; j++)
            {
                ICell cell = row.CreateCell(j);
                cell.CellStyle = bodyStyle;
                cell.SetCellValue(dt.Rows[i][j].ToString());
            }
        }
        MemoryStream ms = new MemoryStream();
        book.Write(ms);
        Response.AddHeader("Content-Disposition", string.Format("attachment; filename={0}.xls", 
            HttpUtility.UrlEncode(title + "_" + DateTime.Now.ToString("yyyy-MM-dd"), System.Text.Encoding.UTF8)));
        Response.BinaryWrite(ms.ToArray());
        Response.End();
        book = null;
        ms.Close();
        ms.Dispose();
    }
}
