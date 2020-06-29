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
public partial class teacher_TExaminationDetail : System.Web.UI.Page
{
    private static int id;//记录试题ID
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["teacher"] == null)//判断老师是否登录
        {
            Response.Redirect("../Login.aspx");
        }
        if (!IsPostBack)
        {
            id = Convert.ToInt32(Request.QueryString["Eid"]);//获取试题ID
            SqlConnection conn = BaseClass.DBCon();//创建数据库连接对象
            conn.Open();//打开数据库连接            
            SqlCommand cmd = new SqlCommand("select * from tb_test where ID=" + id, conn);//根据ID查询试题信息
            SqlDataReader sdr = cmd.ExecuteReader();//将查询结果存储到SqlDataReader中
            sdr.Read();//读取数据
            txtsubject.Text = sdr["testContent"].ToString();//获取试题题目
            txtAnsA.Text = sdr["testAns1"].ToString();//获取答案A
            txtAnsB.Text = sdr["testAns2"].ToString();//获取答案B
            txtAnsC.Text = sdr["testAns3"].ToString();//获取答案C
            txtAnsD.Text = sdr["testAns4"].ToString();//获取答案D
            rblRightAns.SelectedValue = sdr["rightAns"].ToString();//获取正确答案
            lblkm.Text = sdr["testCourse"].ToString();//获取所属科目
            sdr.Close();//关闭读取器
            conn.Close();//关闭数据库连接
        }
    }

    protected void btnconfirm_Click(object sender, EventArgs e)
    {
        if (txtsubject.Text == "" || txtAnsA.Text == "" || txtAnsB.Text == "" || txtAnsC.Text == "" || txtAnsD.Text == "")
        {
            MessageBox.Show("请将信息填写完整");
            return;
        }
        else
        {
            //string isfb = "";
            //if (cbFB.Checked == true)
            //    isfb = "1";
            //else
            //    isfb = "0";
            string str = "update tb_test set testContent='" + txtsubject.Text.Trim() + "',testAns1='" + txtAnsA.Text.Trim() + "',testAns2='" + txtAnsB.Text.Trim() + "',testAns3='" + txtAnsC.Text.Trim() + "',testAns4='" + txtAnsD.Text + "',rightAns='" + rblRightAns.SelectedValue.ToString() + "' where ID=" + id;
            BaseClass.OperateData(str);
            Response.Redirect("TExaminationInfo.aspx");
        }
    }
    protected void btnconcel_Click(object sender, EventArgs e)
    {
        Response.Redirect("TExaminationInfo.aspx");
    }
}
