using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class teacher_TStartExamination : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void startTest_Click(object sender, EventArgs e)
    {
        SqlConnection conn = BaseClass.DBCon();//连接数据库
        conn.Open();//打开连接
        SqlCommand cmd = new SqlCommand("select * from tb_Teacher where TeacherNum='" + Session["teacher"].ToString() + "'", conn);
        SqlDataReader sdr = cmd.ExecuteReader();//创建记录集
        sdr.Read();
        int id = Convert.ToInt32(sdr["TeacherCourse"].ToString());//获取教师的授课编号
        sdr.Close();

        string strsql = "select Tested from tb_Lesson where ID='" + id + "'";
        cmd = new SqlCommand(strsql, conn);
        Boolean status = Convert.ToBoolean(cmd.ExecuteScalar());
        if (status == false)
        {
            strsql = "update  tb_Lesson set Tested = 'True' where ID='" + id + "'";
            cmd = new SqlCommand(strsql, conn);
            cmd.ExecuteNonQuery();
            MessageBox.Show("开放考试成功！");
        }
        else
        {
            MessageBox.Show("已经开放考试！");
        }
        
        conn.Close();//关闭连接
    }


    protected void stopTest_Click(object sender, EventArgs e)
    {
        SqlConnection conn = BaseClass.DBCon();//连接数据库
        conn.Open();//打开连接
        SqlCommand cmd = new SqlCommand("select * from tb_Teacher where TeacherNum='" + Session["teacher"].ToString() + "'", conn);
        SqlDataReader sdr = cmd.ExecuteReader();//创建记录集
        sdr.Read();
        int id = Convert.ToInt32(sdr["TeacherCourse"].ToString());//获取教师的授课编号
        sdr.Close();

        string strsql = "select Tested from tb_Lesson where ID='" + id + "'";
        cmd = new SqlCommand(strsql, conn);
        Boolean status = Convert.ToBoolean(cmd.ExecuteScalar());
        if (status == true)
        {
            strsql = "update  tb_Lesson set Tested = 'False' where ID='" + id + "'";
            cmd = new SqlCommand(strsql, conn);
            cmd.ExecuteNonQuery();
            MessageBox.Show("停止入考成功！");
        }
        else
        {
            MessageBox.Show("考试并未开放！");
        }

        conn.Close();//关闭连接
    }

}