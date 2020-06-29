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
public partial class student_StartExam : System.Web.UI.Page
{
    public int num = 20;//选择题数量
    public string Ans = null; //建立存储正确答案的公共变量
    public int tNUM;//记录考题数量
    protected void Page_Load(object sender, EventArgs e)
    {
        lblEndtime.Text = "考试时间为120分钟，每道选择题5分，倒计时："; //显示考试提示
        lblStuNum.Text = Session["ID"].ToString(); //显示考生编号
        lblStuName.Text = Session["name"].ToString();//显示考生姓名
        lblStuSex.Text = Session["sex"].ToString();//显示考生性别
        lblStuKM.Text = "[" + Session["KM"].ToString() + "]" + "考试试题"; //显示考试科目
        int i = 1;//初始化变量
        SqlConnection conn = BaseClass.DBCon();//连接数据库
        conn.Open();//打开连接
        SqlCommand cmd = new SqlCommand("select top " +num +" * from tb_test where testCourse='" + Session["KM"].ToString() + "' order by newid()", conn);
        SqlDataReader sdr = cmd.ExecuteReader();//创建记录集
        while (sdr.Read())
        {
            Literal littxt = new Literal();//创建Literal控件
            Literal litti = new Literal();//创建Literal控件
            RadioButtonList cbk = new RadioButtonList();//创建RadioButtonList控件
            cbk.ID = "cbk" + i.ToString();
            littxt.Text = i.ToString() + "、" + Server.HtmlEncode(sdr["testContent"].ToString()) + "<br>";
            litti.Text = "</Blockquote>";
            cbk.Items.Add("A. " + Server.HtmlEncode(sdr["testAns1"].ToString()));//添加选项A
            cbk.Items.Add("B. " + Server.HtmlEncode(sdr["testAns2"].ToString()));//添加选项B
            cbk.Items.Add("C. " + Server.HtmlEncode(sdr["testAns3"].ToString()));//添加选项C
            cbk.Items.Add("D. " + Server.HtmlEncode(sdr["testAns4"].ToString()));//添加选项D
            cbk.Font.Size = 11;//设置文字大小
            for (int j = 1; j <= 4; j++)
            {
                cbk.Items[j - 1].Value = j.ToString();
            }
            Ans += sdr[6].ToString();//获取试题的正确答案
            if (Session["a"] == null)//判断是否第一次加载
            {
                //如果第一次加载则将正确答案赋值给Session["Ans"]
                Session["Ans"] = Ans;
            }
            Panel1.Controls.Add(littxt);//将控件添加到Panel中
            Panel1.Controls.Add(cbk);//将控件添加到Panel中
            Panel1.Controls.Add(litti);//将控件添加到Panel中
            i++;//使i递增
            tNUM++;//使tNUM递增
        }
        sdr.Close();
        conn.Close();//关闭连接
        Session["a"] = 1;
    }

    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        string msc = "";//建立变量msc存储考生答案
        for (int i = 1; i <= num; i++)
        {
            RadioButtonList list = (RadioButtonList)Panel1.FindControl("cbk" + i.ToString());
            if (list != null)
            {
                if (list.SelectedValue.ToString() != "")
                    msc += list.SelectedValue.ToString();//存储考生答案
                else
                    msc += "0";//如果没有选择则为0			
            }
        }
        Session["Sans"] = msc;//考生答案
        //更新考试结果数据表
        string sql = "update tb_score set RigthAns='" + Ans + "',StudentAns='" + msc + "' where StudentID='" + lblStuNum.Text + "'";
        BaseClass.OperateData(sql);
        Response.Redirect("result.aspx?BInt=" + tNUM.ToString());
    }
}
