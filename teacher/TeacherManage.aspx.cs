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
public partial class teacher_TeacherManage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["teacher"] == null)//禁止匿名登录
        {
            Response.Redirect("../Login.aspx");
        }
        else
        {
            lblwz.Text = Session["teacher"].ToString();//教师编号
            SqlConnection conn = BaseClass.DBCon();//连接数据库
            conn.Open();//打开连接
            SqlCommand cmd = new SqlCommand("select * from tb_Teacher where TeacherNum='" + lblwz.Text + "'", conn);
            SqlDataReader sdr = cmd.ExecuteReader();//创建记录集
            sdr.Read();
            lblname.Text = sdr["TeacherName"].ToString();//显示教师姓名
            int id = Convert.ToInt32(sdr["TeacherCourse"].ToString());//获取教师的授课编号
            sdr.Close();
            cmd = new SqlCommand("select LessonName from tb_Lesson where ID=" + id, conn);
            lblkc.Text = cmd.ExecuteScalar().ToString();//获取教师授课科目名称
            Session["KCname"] = lblkc.Text;
            conn.Close();//关闭连接
        }
    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Response.Redirect("TLogout.aspx");
    }
}
