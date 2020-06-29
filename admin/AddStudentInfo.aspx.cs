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
public partial class admin_AddStudentInfo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["admin"] == null)
        {
            Response.Redirect("../Login.aspx");
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        long iloing = 0;
        if (!long.TryParse(txtNum.Text, out iloing))
        {
            MessageBox.Show("考生编号请输入数字");
            return;

        }
        if (txtName.Text == "" || txtNum.Text == "" || txtPwd.Text == "")//检查信息输入是否完整
        {
            MessageBox.Show("请将信息填写完整");//弹出提示信息
            return;
        }
        else
        {
            SqlConnection conn = BaseClass.DBCon();//连接数据库	
            conn.Open();//打开连接
            SqlCommand cmd = new SqlCommand("select count(*) from tb_Student where StudentNum='" + txtNum.Text + "'", conn);
            int i = Convert.ToInt32(cmd.ExecuteScalar());//获取返回值
            if (i > 0)//如果返回值大于0
            {
                MessageBox.Show("此学号已经存在");//提示学号已经存在
                return;
            }
            else
            {
                //将新增学生信息添加到数据库中
                cmd = new SqlCommand("insert into tb_Student(StudentNum,StudentName,StudentSex,StudentPwd) values('" + txtNum.Text.Trim() + "','" 
                    + txtName.Text.Trim() + "','" + rblSex.SelectedValue.ToString() + "','" + txtPwd.Text.Trim() + "')", conn);
                cmd.ExecuteNonQuery();
                conn.Close();//关闭连接
                MessageBox.Show("添加成功");//提示添加成功
                btnConcel_Click(sender, e);
            }
        }
    }
    protected void btnConcel_Click(object sender, EventArgs e)
    {
        txtName.Text = "";
        txtNum.Text = "";
        txtPwd.Text = "";
    }
}
