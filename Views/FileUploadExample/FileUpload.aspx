<%@ Page Language="C#" 
    CodeBehind="~/Views/FileUploadExample/FileUpload.aspx.cs"
    AutoEventWireup="true" EnableViewState="false"
    Inherits="ASPNetMVCDemo.Views.FileUploadExample.FileUpload" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN"
    "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>File Upload Example by ASP.NET in MVC Pattern</title>
    <asp:Literal ID="litLinkTojQuery" runat="server" />
    <asp:Literal ID="litAppStyle" runat="server" />
    <script language="javascript" type="text/javascript">
        $(document).ready(function() {
            $("a").hover(function() {
                this.style.color = "red";
            }, function() {
                this.style.color = "";
            });

            $.mouseX = function(e) {
                if (e.pageX) return e.pageX;
                else if (e.clientX)
                    return e.clientX + (document.documentElement.scrollLeft ?
                    document.documentElement.scrollLeft :
                    document.body.scrollLeft);
                else return null;
            };

            $.mouseY = function(e) {
                if (e.pageY) return e.pageY;
                else if (e.clientY)
                    return e.clientY + (document.documentElement.scrollTop ?
                    document.documentElement.scrollTop :
                    document.body.scrollTop);
                else return null;
            };

            $("#spanStartUpload").hover(function() {
                this.style.color = "red";
            }, function() {
                this.style.color = "blue";
            });

            $("#spanStartUpload").click(function(e) {
                e.preventDefault();
                $("#divFileUpload").slideToggle("fast");
                $file = $("#spanFileUpload");
                $file.html($file.html());
            });

            $("#btnCancel").click(function(e) {
                $file = $("#spanFileUpload");
                $file.html($file.html());
                $("#divFileUpload").css("display", "none");
            });

            $("#btnUpload").click(function(e) {
                $file = $("#FileToLoad");
                var $filePath = $.trim($file.val());
                if ($filePath == "") {
                    alert("Please browse a file to upload");
                    return;
                }

                var $ext = $filePath.split(".").pop().toLowerCase();
                var $allow = new Array("gif", "png", "jpg", "jpeg");
                if ($.inArray($ext, $allow) == -1) {
                    alert("Only image files are accepted, please browse a image file");
                    return;
                }

                var $uploadaction = $("#hidFileUploadAction").val();
                $("#MainForm").attr("action", $uploadaction);

                try {
                    $("#MainForm").submit();
                } catch (err) {
                    alert("Upload file failed, make sure to browse a valid file path, and make sure the file is not openned by any programs.");
                }

            });

            $(".ImagePopLink").click(function(e) {
                e.preventDefault();
                var $ID = $(this).html();
                var $imagebase = $("#hidImageBase").val();
                var $imagesource = $imagebase + "?ID=" + $ID;
                var $imagelink = "<img style=\"width: 400px\" src=\"" + $imagesource + "\" />";
                $("#divImg").html($imagelink);
                $("#divImg").load();
                $("#PopWindow").css({ "left": $.mouseX(e), "top": $.mouseY(e) + 5 });
                $("#PopWindow").show();
            });

            $("#Close").click(function(e) {
                e.preventDefault();
                $("#PopWindow").css({ "left": "0px", "top": "0px" });
                $("#PopWindow").hide();
            });

        });
    </script>
</head>

<body class="DocumentDefault">
<input type="hidden" id="hidFileUploadAction" runat="server" />
<input type="hidden" id="hidImageBase" runat="server" />
<form name="MainForm" method="post" action="" id="MainForm"
    enctype="multipart/form-data" runat="server">

    <div class="DocumentTitle">
        <asp:Literal id="litApplicationName" runat="server" />
    </div>
    
    <div class="DocumentAuthor">
        <asp:Literal id="litAuthorInformation" runat="server" />
    </div>

    <div style="text-align: left">
        <span id="spanStartUpload" class="BoldLink">
            Click to upload a new file</span>
    </div>
    
    <div id="divFileUpload" style="display: none; text-align: left">
        <span style="font-weight: bold">Please browse a file to upload&nbsp;</span>
        <span id="spanFileUpload">
            <input type="file" name="FileToLoad" id="FileToLoad" style="width:350px;" />
        </span>
        <span>
            <input type="button" id="btnUpload" value="Submit" style="width: 75px; height: 21px" />
        </span>
        <span>
            <input type="button" id="btnCancel" value="Cancel" style="width: 75px; height: 21px" />
        </span>
    </div>

<div id="MainContent" class="MainContent">
    <asp:Literal ID="litExistingFiles" runat="server" />
</div>
<div class="Copyright">Copy right: The Code Project Open License (CPOL)</div>

<div id="PopWindow" class="HiddenPopup">
<div>
    <div style="background-color: Transparent; position:absolute; top: 1px; left: 380px">
        <a id="Close" style="font-weight: bold" href="#">X</a></div>
    <div id="divImg"></div>
</div>
</div>

</div>
</form>
</body>
</html>
