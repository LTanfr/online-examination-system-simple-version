<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TStartExamination.aspx.cs" Inherits="teacher_TStartExamination" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div style="text-align: center">
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <fieldset style="width: 556px; text-align: center">
            <legend class="mailTitle">开始考试</legend>
            <br />

            <div>
                 <asp:Button id="startTest" runat="server" Text="开放考试" OnClick="startTest_Click" Width="100px" Height="40px" BackColor="#ee7700" style="margin-top:30px;" BorderStyle="None" ForeColor="White"/>
                 <asp:Button id="stopTest" runat="server" Text="停止入考" OnClick="stopTest_Click" Width="100px" Height="40px" BackColor="#2a5caa" style="margin-top:30px;" BorderStyle="None" ForeColor="White"/>
            </div>
            <br />
        </fieldset>
        </div>
    </form>
</body>
</html>
