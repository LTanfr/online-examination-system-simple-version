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
public partial class admin_Subject : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["admin"] == null)//禁止匿名登录
        {
            Response.Redirect("../Login.aspx");
        }
        if (!IsPostBack)
        {
            SqlConnection conn = BaseClass.DBCon();//连接数据库
            conn.Open();//打开连接
            SqlCommand cmd = new SqlCommand("select * from tb_Lesson", conn);
            SqlDataReader sdr = cmd.ExecuteReader();
            while (sdr.Read())
            {
                ListBox1.Items.Add(sdr["LessonName"].ToString());
            }
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (txtKCName.Text == "")//判断是否输入课程名称
        {
            MessageBox.Show("请输入课程名称");//弹出提示信息
            return;
        }
        else
        {
            string systemTime = DateTime.Now.ToString();//获取当前系统时间
            //将信息插入数据库中的课程信息表中
            string strsql = "insert into tb_Lesson(LessonName,LessonDataTime,Tested) values('" + txtKCName.Text.Trim() + "','" + systemTime + "','False')";
            BaseClass.OperateData(strsql);//执行SQL语句
            txtKCName.Text = "";
            Response.Write("<script>alert('添加成功');location='Subject.aspx'</script>");
        }
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (ListBox1.SelectedValue.ToString() == "")//判断是否有选中项
        {
            MessageBox.Show("请选择删除项目后删除");//弹出提示
            return;
        }
        else
        {
            //删除指定的信息
            string strsql = "delete from tb_Lesson where LessonName='" + ListBox1.SelectedItem.Text + "'";
            BaseClass.OperateData(strsql);//执行SQL语句
            Response.Write("<script>alert('删除成功');location='Subject.aspx'</script>");
        }
    }
}
