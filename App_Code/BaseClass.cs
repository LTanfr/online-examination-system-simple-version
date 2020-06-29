using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
/// <summary>
/// BaseClass 的摘要说明
/// </summary>
public class BaseClass
{
	public BaseClass()
	{
		
	}
    //建立连接数据库的公共方法
    public static SqlConnection DBCon()
    {
        return new SqlConnection("server=WIN-RC0II76JD0v\\MRSQLSERVER;database=db_ExamOnline;uid=sa;pwd=tanjie");
    }

    //建立绑定GridView控件的方法
    public static void BindDG(GridView gv, string id, string strSql, string name)
    {
        //连接数据库
        SqlConnection conn = DBCon();
        //执行传入的查询语句
        SqlDataAdapter sda = new SqlDataAdapter(strSql, conn);
        //创建数据集对象
        DataSet ds = new DataSet();
        //填充数据集
        sda.Fill(ds, name);
        //设置绑定数据源
        gv.DataSource = ds.Tables[name];
        //设置绑定的主键字段
        gv.DataKeyNames = new string[] { id };
        //绑定控件
        gv.DataBind();

    }

    public static void OperateData(string strsql)//建立一个执行SQL语句的方法
    {
        SqlConnection conn = DBCon();//连接数据库
        conn.Open();//打开数据库
        SqlCommand cmd = new SqlCommand(strsql, conn);//指定要执行的SQL语句
        cmd.ExecuteNonQuery();//执行SQL语句
        conn.Close();//关闭连接
    }

    //================如果是学生登录=========================================
    public static bool CheckStudent(string studentNum, string studentPwd)
    {
        //连接数据库
        SqlConnection conn = DBCon();
        //打开数据库
        conn.Open();
        //根据填写的表单中对应的账号和密码来查询学生信息
        SqlCommand cmd = new SqlCommand("select count(*) from tb_Student where StudentNum='" + studentNum + "' and StudentPwd='" + studentPwd + "'", conn);
        //返回执行结果
        int i = Convert.ToInt32(cmd.ExecuteScalar());
        //关闭连接
        conn.Close();
        //判断结果值，并返回相应的bool值
        if (i > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    //================如果是教师登录=========================================
    public static bool CheckTeacher(string teacherNum, string teacherPwd)
    {
        SqlConnection conn = DBCon();//连接数据库
        conn.Open();//打开数据库
        //根据账号和密码查询教师信息
        SqlCommand cmd = new SqlCommand("select count(*) from tb_Teacher where TeacherNum='" + teacherNum + "' and TeacherPwd='" + teacherPwd + "'", conn);
        int i = Convert.ToInt32(cmd.ExecuteScalar());//返回执行结果
        conn.Close();//关闭连接
        if (i > 0)//判断结果值，并返回相应的bool值
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    //================如果是管理员登录=========================================
    public static bool CheckAdmin(string adminNum, string adminPwd)
    {
        SqlConnection conn = DBCon();//连接数据库
        conn.Open();//打开数据库
        //根据账号和密码查询管理员信息
        SqlCommand cmd = new SqlCommand("select count(*) from tb_Admin where AdminNum='" + adminNum + "' and adminPwd='" + adminPwd + "'", conn);
        int i = Convert.ToInt32(cmd.ExecuteScalar());//返回执行结果
        conn.Close();//关闭连接
        if (i > 0)//判断结果值，并返回相应的bool值
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
