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
public partial class student_result : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string Rans = Session["Ans"].ToString();//获取正确答案
        int j = Convert.ToInt32(Request.QueryString["BInt"]);//获取试题数量
        string Sans = Session["Sans"].ToString();//获取考生答案
        int StuScore = 0;//将考试成绩初始化为0
        for (int i = 0; i < j; i++)
        {
            if (Rans.Substring(i, 1).Equals(Sans.Substring(i, 1)))//将考生答案与正确答案作比较
            {
                StuScore += 5;//如果答案正确加5分
            }
        }
        this.lblResult.Text = StuScore.ToString();//显示考试成绩
        this.lblkm.Text = Session["KM"].ToString();//显示考试科目
        this.lblnum.Text = Session["ID"].ToString();//显示考生编号
        this.lblname.Text = Session["name"].ToString();//显示考生姓名
        //更新考试结果数据表
        string strsql = "update tb_score set score='" + StuScore.ToString() + "' where StudentID='" + Session["ID"].ToString() + "' and LessonName='" + Session["KM"].ToString() + "'";
        BaseClass.OperateData(strsql);
    }
}
