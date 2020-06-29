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
using System.Data.SqlClient;
public partial class admin_StudentInfo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["admin"] == null)//禁止匿名登录
        {
            Response.Redirect("../Login.aspx");
        }
        if (!IsPostBack)
        {
            string strsql = "select * from tb_Student order by ID desc";//检索所有学生信息
            BaseClass.BindDG(gvStuInfo, "ID", strsql, "stuinfo");//绑定控件
        }
    }
    protected void btnserch_Click(object sender, EventArgs e)
    {
        if (txtKey.Text == "")//检查是否输入了关键字
        {
            string strsql = "select * from tb_Student order by ID desc";//检索所有学生信息
            BaseClass.BindDG(gvStuInfo, "ID", strsql, "stuinfo");//绑定控件
        }
        else
        {
            string stype = ddlType.SelectedItem.Text;//获取查询范围
            string strsql = "";
            switch (stype)
            {
                case "学号"://如果查询范围是“学号”
                    strsql = "select * from tb_Student where StudentNum like '%" + txtKey.Text.Trim() + "%'";
                    BaseClass.BindDG(gvStuInfo, "ID", strsql, "stuinfo"); ;
                    break;
                case "姓名"://如果查询范围是“姓名”
                    strsql = "select * from tb_Student where StudentName like '%" + txtKey.Text.Trim() + "%'";
                    BaseClass.BindDG(gvStuInfo, "ID", strsql, "stuinfo");
                    break;
            }
        }
    }
    protected void gvStuInfo_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int id = (int)gvStuInfo.DataKeys[e.RowIndex].Value;//记录要删除的学生编号
        string str = "delete from tb_Student where ID=" + id;//定义删除学生信息的SQL语句
        BaseClass.OperateData(str);//执行删除学生信息操作
        string strsql = "select * from tb_Student order by ID desc";//获取学生信息表中的最新记录
        BaseClass.BindDG(gvStuInfo, "ID", strsql, "stuinfo");//显示最新的学生信息
    }
    protected void gvStuInfo_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvStuInfo.PageIndex = e.NewPageIndex;
        string strsql = "select * from tb_Student order by ID desc";
        BaseClass.BindDG(gvStuInfo, "ID", strsql, "stuinfo");
    }
}
