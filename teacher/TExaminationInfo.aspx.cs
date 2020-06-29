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
public partial class teacher_TExaminationInfo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["teacher"] == null)//禁止匿名登录
        {
            Response.Redirect("../Login.aspx");
        }
        else
        {
            if (!IsPostBack)
            {
                string strsql = "select * from tb_test where testCourse='" + Session["KCname"].ToString() + "'";
                BaseClass.BindDG(gvExaminationInfo, "ID", strsql, "ExaminationInfo");
            }
        }
    }
    public string Getstatus(string zt)
    {
        if (zt == "0")
            return "否";
        else
            return "是";
    }
    protected void btnserch_Click(object sender, EventArgs e)
    {
        string strsql = "select * from tb_test where testContent like '%" + txtstkey.Text.Trim() + "%' and testCourse='" + Session["KCname"].ToString() + "'";
        BaseClass.BindDG(gvExaminationInfo, "ID", strsql, "ExaminationInfo");
    }
    protected void gvExaminationInfo_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int id = (int)gvExaminationInfo.DataKeys[e.RowIndex].Value;//获取欲删除信息的编号
        string sql = "delete from tb_test where ID=" + id;//执行删除操作的SQL语句
        BaseClass.OperateData(sql);
        string strSql = "select * from tb_test where testCourse='" + Session["KCname"].ToString() + "' and testCourse='" + Session["KCname"].ToString() + "'";
        BaseClass.BindDG(gvExaminationInfo, "ID", strSql, "ExaminationInfo");
    }
    protected void gvExaminationInfo_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvExaminationInfo.PageIndex = e.NewPageIndex;
        string strsql = "select * from tb_test where testCourse='" + Session["KCname"].ToString() + "' and testCourse='" + Session["KCname"].ToString() + "'";
        BaseClass.BindDG(gvExaminationInfo, "ID", strsql, "ExaminationInfo");
    }

    
}
