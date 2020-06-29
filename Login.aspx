<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html>
<head runat="server">
    <title>在线考试系统</title>
    <link href="Mystyle.css" rel="stylesheet" type="text/css" />

    
<style type="text/css">
    * {
        margin: 0;
        padding: 0;
    }


    #head {
        height: 120px;
        width: 100%;
        background-color: #66CCCC;
        text-align: center;
        position: relative;
    }

    #wrap .logGet {
        height: 408px;
        width: 368px;
        position: absolute;
        background-color: #FFFFFF;
        top: 29%;
        right: 14%;
    }

    .logGet .logD.logDtip .p1 {
        display: inline-block;
        font-size: 28px;
        margin-top: 30px;
        width: 86%;
    }

    #wrap .logGet .logD.logDtip {
        width: 86%;
        border-bottom: 1px solid #ee7700;
        margin-bottom: 40px;
        margin-top: 0px;
        margin-right: auto;
        margin-left: auto;
    }

    .logGet .lgD img {
        position: absolute;
        top: 11px;
        left: 8px;
    }

    #wrap .logGet .lgD {
        width: 86%;
        position: relative;
        margin-bottom: 30px;
        margin-top: 30px;
        margin-right: auto;
        margin-left: auto;
    }

    .logC {
        width: 86%;
        margin-top: 0px;
        margin-right: auto;
        margin-bottom: 0px;
        margin-left: auto;
    }


    .title {
        font-family: "宋体";
        color: #FFFFFF;
        position: absolute;
        top: 50%;
        left: 50%;
        transform: translate(-50%, -50%); /* 使用css3的transform来实现 */
        font-size: 36px;
        height: 40px;
        width: 30%;
    }

    
</style>

</head>
<body style="background-image:url(Image/bg.png)">

    <div class="header" id="head">
        <div class="title">在线考试系统</div>
    </div>


    <form runat="server" class="wrap" id="wrap">
        <div class="logGet">
            <div class="logD logDtip">
				<p class="p1">登录</p>
			</div>

                <div class="lgD">
                    <img src="Image/username.png" width="24" height="24" alt="" style=" position:absolute; right: 284px;"/>
                    <div style="width:300px; height:40px; margin-left:40px;">
                        <div style="width:300px; height:12px; "></div>
                         <asp:TextBox ID="txtNum" runat="server" Height="20px" Width="200px" Font-Size="Large"></asp:TextBox>   
                    </div>                          
                </div>

                <div style="float: left;">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtNum"
                        Display="Dynamic" ErrorMessage="请输入学生证号" ForeColor="DarkGray" Font-Size="10pt">*</asp:RequiredFieldValidator>
                </div>


                <div class="lgD">
                    <img src="Image/password.png" width="24" height="24" alt="" style=" position:absolute; right: 284px;"/>
                    <div style="width:300px; height:40px; margin-left:40px;">
                        <div style="width:300px; height:12px;"></div>
                            <asp:TextBox ID="txtPwd" runat="server" MaxLength="12" TextMode="Password" Width="200px" Height="20px"></asp:TextBox>
                    </div>                          
                </div>
               
                <div style="float: left;">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtPwd"
                        Display="Dynamic" ErrorMessage="密码不为空" ForeColor="DimGray" Font-Size="10pt">*</asp:RequiredFieldValidator>
                </div>


                <div style="width:300px; height:40px; margin-left:30px;">
                    <div style="float: left; margin-left: 5px;font-size:14px;">身份：</div>

                    <div style="float: left;">
                        <asp:DropDownList ID="ddlstatus" runat="server" Height="19px" Font-Size="12px">
                            <asp:ListItem Selected="True">学生</asp:ListItem>
                            <asp:ListItem>教师</asp:ListItem>
                            <asp:ListItem>管理员</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>

                <div style="width:300px; height:40px; margin-left:30px;">
                    <div style="float: left;  margin-top:3px;">验证码：</div>
                <div style="float: left;">
                    <asp:TextBox ID="txtCode" runat="server" Height="15px" Width="60px"></asp:TextBox>
                </div>
                <div style="float: left;">
                    <asp:Image ID="Image1" runat="server" Width="56px" BorderColor="Gray" BorderWidth="1px" Height="17px" ImageUrl="~/Image.aspx" />
                </div>
                </div>
                
                <div style="float: left;">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtCode"
                        Display="Dynamic" ErrorMessage="请输入验证码" ForeColor="DimGray" Font-Size="10pt">*</asp:RequiredFieldValidator>
                </div>
                <div style="float: left; width:300px;">
                    <asp:Button ID="btnlogin" runat="server" Text="登录" OnClick="btnlogin_Click" BackColor="#ee7700" Width="180px" Height="40px" ForeColor="White" BorderStyle="None" Style="margin-left:60px"/>
                </div>
            </div>

         
    </form>
</body>
</html>
