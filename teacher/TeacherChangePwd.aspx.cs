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
public partial class teacher_TeacherChangePwd : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["teacher"] == null)
        {
            Response.Redirect("../Login.aspx");
        }
    }
    protected void btnchange_Click(object sender, EventArgs e)
    {
        if (txtNewPwd.Text == "" || txtNewPwdA.Text == "" || txtOldPwd.Text == "")//检查信息输入是否完整
        {
            MessageBox.Show("请将信息填写完整");//弹出提示信息
            return;
        }
        else
        {
            //检查旧密码输入是否正确
            if (BaseClass.CheckTeacher(Session["teacher"].ToString(), txtOldPwd.Text.Trim()))
            {
                if (txtNewPwd.Text.Trim() != txtNewPwdA.Text.Trim())//检查两次输入的新密码是否相等
                {
                    MessageBox.Show("两次密码不一致");
                    return;
                }
                else
                {
                    string strsql = "update tb_Teacher set TeacherPwd='" + txtNewPwdA.Text.Trim() + "' where TeacherNum='" + Session["teacher"].ToString() + "'";
                    BaseClass.OperateData(strsql);//更新数据表
                    MessageBox.Show("密码修改成功");
                    txtNewPwd.Text = ""; //清空文本框
                    txtNewPwdA.Text = "";//清空文本框
                    txtOldPwd.Text = "";//清空文本框
                }
            }
            else
            {
                MessageBox.Show("旧密码输入错误");
                return;
            }
        }
    }
}
