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
using System.IO;
using System.Drawing.Imaging;
using System.Drawing;
public partial class Image : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //获取生成的验证码
        string tmp = RndNum();
        //将验证码存储在Session中
        Session["verify"] = tmp;
        //绘制验证码
        ValidateCode(tmp);
    }

    //绘制验证码
    private void ValidateCode(string code)
    {
        //创建位图对象，用来存储生成的验证码图片
        Bitmap image = null;
        //绘制验证码的对象
        Graphics g = null;
        //内存流对象，用来缓存验证码图片
        MemoryStream ms = null;
        //设置验证码宽度
        int width = code.Length * 8;
        //创建位图对象
        image = new Bitmap(width, 18);
        //从位图对象创建绘图对象
        g = Graphics.FromImage(image);
        //设置背景颜色
        g.Clear(Color.WhiteSmoke);
        //设置文字字体
        Font font = new Font("Tahoma", 9);
        //设置文字颜色
        SolidBrush fontBrush = new SolidBrush(Color.Black);
        //绘制验证码
        g.DrawString(code, font, fontBrush, 0, 3);
        //创建内存流对象
        ms = new MemoryStream();
        //将验证码保存到内存流中
        image.Save(ms, ImageFormat.Jpeg);
        //清空页面内容
        Response.ClearContent();
        //设置输出格式
        Response.ContentType = "image/Jpeg";
        //输出验证码
        Response.BinaryWrite(ms.ToArray());
        //手动释放非托管类型资源
        g.Dispose();
        image.Dispose();
        //终止响应
        Response.End();
    }

    //生成验证码
    private string RndNum()
    {
        //最小值
        string MinNum = "1000";
        //最大值
        string MaxNum = "";
        //控制验证码生成的位数为4位
        for (int i = 0; i < 4; i++)
        {
            MaxNum = MaxNum + "6";
        }
        //定义随机数生成器
        Random random = new Random();
        //生成随机的4位验证码
        string VNum = Convert.ToString(random.Next(Convert.ToInt32(MinNum), Convert.ToInt32(MaxNum)));
        //返回生成的验证码
        return VNum;
    }
}
