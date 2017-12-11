<%@ Page Language="C#" CodeBehind="~/Views/Shared/Error.aspx.cs" 
    Inherits="ASPNetMVCDemo.Views.Shared.Error" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN"
    "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>File Upload Example by ASP.NET in MVC Pattern</title>
    <asp:Literal ID="litLinkTojQuery" runat="server" />
    <asp:Literal ID="litAppStyle" runat="server" />
    <script language="javascript" type="text/javascript">
        $(document).ready(function() {
            $("#spanBackToMain").hover(function() {
                this.style.color = "red";
            }, function() {
                this.style.color = "blue";
            });

            $("#spanBackToMain").click(function(e) {
                e.preventDefault();
                document.location.replace($("#hidHomeURL").val());
            });
        });
    </script>
</head>

<body class="DocumentDefault">
<input type="hidden" id="hidHomeURL" runat="server" />
<div class="DocumentTitle">
    <asp:Literal id="litApplicationName" runat="server" />
</div>

<div class="DocumentAuthor">
    <asp:Literal id="litAuthorInformation" runat="server" />
</div>

<div style="text-align: left">
    <span id="spanBackToMain" class="BoldLink">
        Click to go back to the main page</span>
</div>
    
<div id="MainContent" class="MainContent" style="text-align: center">
    <div style="font-weight: bold; color: Maroon; margin-top: 30px">
        <asp:Literal ID="litErrorMessage" runat="server" />
    </div>
</div>
<div class="Copyright">Copy right: The Code Project Open License (CPOL)</div>

</body>
</html>
