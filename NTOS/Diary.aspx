<%@ Page Title="Diary" MasterPageFile="~/Site.Master" Language="C#" EnableViewState="true" AutoEventWireup="true" CodeBehind="Diary.aspx.cs" Inherits="NTOS.Dairy" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <%--<link type="text/css" rel="Stylesheet" href="Content/jquery.ui.tinytbl.css" />--%>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <style type="text/css">
        #tab {
                
            
          
            
                
        }
            
        #tab th {
            text-transform: capitalize;
            padding-left :10px;
            color: #9999A2;
            border-spacing:0px;
            
        }
        #tab th {
            background-color: #FFFFFF;
            color: #9999A2;
            text-align: center ;
            white-space: nowrap;
        }

      
        #DivRoot {
            padding: 0px;
            margin: 0px;
            height: 500px;
        }
        #tab td {
            text-align:right ;
            font-size :11px;
              padding-left :15px;
                padding-left :15px;
             
        }
        table td, th {
            /*width:250px;*/
            /*width: auto;*/
        }
        #tab td a {
            
             color: #9999A2!important;
             font-size :11px;


              padding: 0 3px 0 3px;
                width: 140px !important;
                overflow: hidden;
                display: inline-block;
                white-space: pre-wrap ;
                text-decoration: none;

        }
          .clr{
            word-spacing:10px;
            padding:3px;
        }
    </style>
   
    <style type="text/css">
        a.dp-choose-date {
            float: left;
            height: 30px;
            width: 30px;
            padding: 0;
            margin: 5px 3px 0;
            display: block;
            text-indent: -2000px;
            overflow: hidden;
            background: url('/images/calendar.png') no-repeat;
        }

            a.dp-choose-date.dp-disabled {
                background-position: 0 -20px;
                cursor: default;
            }

        input.dp-applied {
            width: 140px;
            float: left;
        }

        #diarypar {
           width:500px;
            margin-left: 23px;
            margin-bottom: 420px;
            position: absolute;
            z-index: 50;
            border: solid 0px;
            top: 105px;
            left: 0;
        }
    </style>
    <script type="text/javascript">
        var pageNumber = 1;

        
    </script>

    <asp:UpdatePanel ID="uplpnl" runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="hfdiaryhide" runat="server" />
            <asp:UpdateProgress ID="UpdateProgress" runat="server">
                <ProgressTemplate>
                    <asp:Image ID="Image1" Width="100px" Height="100px" ImageUrl="~/Images/process1.gif"
                        AlternateText="Processing" runat="server" />
                </ProgressTemplate>
            </asp:UpdateProgress>
            <ajaxToolkit:ModalPopupExtender ID="modalPopup" runat="server" TargetControlID="UpdateProgress"
                PopupControlID="UpdateProgress" BackgroundCssClass="modalPopup" />
            <asp:HiddenField ID="hdndateval" runat="server" />
            <div style="display: none">
                <asp:Button ID="btn" runat="server" OnClick="btn_Click" />
            </div>
            <div id="datepicker" style="margin: 150px 0 0px 20px; position: fixed; z-index: 1;"></div>
            <div id="diarypar">
                <div style="font-weight: normal; float: left; font-size: 14px; padding: 0 0 0 10px;">
                <%--    <label class="label-blue">Date</label>--%>
                    &nbsp;&nbsp;&nbsp;
                                        <asp:TextBox Width="100px" ID="txtDatepicker" runat="server" OnTextChanged="btn_Click" AutoPostBack="true"></asp:TextBox>
                </div>
                <div style="font-weight: bold; width: 150px; height: 24px; cursor: pointer; margin-left: 20px; float: left; display: none" title="Show/Hide Calender">
                    <ajaxToolkit:CalendarExtender ID="cedate" runat="server" TargetControlID="txtDatepicker"></ajaxToolkit:CalendarExtender>
                    <asp:HiddenField ID="hdfdate" runat="server" />
                </div>
                <div style="font-weight: normal; float: left; font-size: 14px; padding: 0 0 0 10px;">
                  <%--  <label class="label-blue">Season</label>--%>
                    &nbsp;&nbsp;&nbsp;
                                        <asp:DropDownList ID="ddlSeason" EnableViewState="true" OnSelectedIndexChanged="ddlSeason_SelectedIndexChanged" AutoPostBack="true" runat="server"></asp:DropDownList>&nbsp;&nbsp;
                </div>
                <div style="font-weight: normal; float: left; font-size: 14px; padding: 0 0 0 10px;">
                  
                    <asp:LinkButton ID="lnkbtnlastyears" runat ="server"  Text="Last Five Years"  OnClick ="lnkbtnlastyears_Click"  ></asp:LinkButton>
                    &nbsp;&nbsp;&nbsp;
                                        <asp:CheckBox AutoPostBack="true" ID="chkbxFiveyears" Visible ="false"  runat="server" OnCheckedChanged="chkbxFiveyears_CheckedChanged" />
                </div>

            </div>
            <table  cellpadding="0" cellspacing="0" align="left" style="z-index: 0;">
                <tr>
                    <td class="contentpadding">
                        <table   cellpadding="0" cellspacing="0">
                            <tr>
                                <td align="center" valign="middle" height="33"
                                    colspan="8">

                                    <div style="font-weight: bold; width: 150px; height: 24px; cursor: pointer; margin-left: 20px; float: left; display: none" title="Show/Hide Calender">
                                    </div>


                                </td>
                            </tr>
                            <tr style="padding-top: 10px">
                                <td align="left">
                                    <div id="divmsg" runat="server" class="msgbox" style="width: 100%;">
                                    </div>
                                    <div runat="server" id="divrep" style="margin-top: 5px;">
                                        <div id="DivRoot" align="left">
                                            <div style="overflow: hidden;" id="DivHeaderRow">
                                            </div>
                                            <div style="overflow: scroll;width:1250px;height:450px"   onscroll="OnScrollDiv(this)" id="DivMainContent">
                                               <asp:PlaceHolder ID = "PlaceHolder1" runat="server" />
                                                
                                                   
                                            </div>
                                        </div>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                 <tr>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
   
     <script async type="text/javascript">
         function scroll() {
             var DivHR = document.getElementById('DivHeaderRow');
             var DivMC = document.getElementById('DivMainContent');
             if ($("#<%=hfdiaryhide.ClientID%>").val() == "") {
                 var id = 0;
                 if ($("#id")[0] == undefined) {
                     id = 0;
                 } else {
                     id = $("#id").offset().top;
                 }
                 $("#DivMainContent").scrollTop(id - 240);
             }
         }
    </script>

      <script async type="text/javascript">
          var prm = Sys.WebForms.PageRequestManager.getInstance();
          //Raised before processing of an asynchronous postback starts and the postback request is sent to the server.
          prm.add_beginRequest(BeginRequestHandler);
          // Raised after an asynchronous postback is finished and control has been returned to the browser.
          prm.add_endRequest(EndRequestHandler);
          function BeginRequestHandler(sender, args) {
              //Shows the modal popup - the update progress
              var popup = $find('<%= modalPopup.ClientID %>');
              if (popup != null) {
                  popup.show();
              }
          }

          function EndRequestHandler(sender, args) {
              //Hide the modal popup - the update progress
              var popup = $find('<%= modalPopup.ClientID %>');
            if (popup != null) {
                popup.hide();
            }
        }
    </script>
    <script async language="javascript" type="text/javascript">
        var pageNumber = 1;
        function MakeStaticHeader(gridId, height, width, headerHeight, isFooter) {
            
            $("#tab tr:even").css('background-color', '#FFFFFF');
            $("#tab tr:odd").css('background-color', '#F5F7FA');
            $('#tab tr').find('th:first, td:first').remove();
            width = 1250; height = 450;
            $('#tab tr').find('th:first, td:first').addClass('clr');
            scroll();
            var tbl = document.getElementById(gridId);
            if (tbl) {
                var DivHR = document.getElementById('DivHeaderRow');
                var DivMC = document.getElementById('DivMainContent');

                //*** Set divheaderRow Properties ****
                DivHR.style.height = headerHeight + 'px';
                DivHR.style.width = (parseInt(width) - 16) + 'px';
                DivHR.style.position = 'relative';
                DivHR.style.top = '0px';
                DivHR.style.zIndex = '10';
                DivHR.style.verticalAlign = 'top';

                //*** Set divMainContent Properties ****
                DivMC.style.width = width + 'px';
                DivMC.style.height = height + 'px';
                DivMC.style.position = 'relative';
                DivMC.style.top = -headerHeight + 'px';

                DivMC.style.zIndex = '1';



                var DivRoot = document.getElementById('DivRoot');
                DivRoot.style.height = height + 'px';

                //****Copy Header in divHeaderRow****
                DivHR.appendChild(tbl.cloneNode(true));
                //$('#tab')[0].rows[1].style.display = "none";
            }
            scroll();
        }
        function OnScrollDiv(Scrollablediv) {

        document.getElementById('DivHeaderRow').scrollLeft = Scrollablediv.scrollLeft;
           
        
           
        }


        function BindTable(data)
        {
            alert(data);
            $('#tab').append(data);
        }

    </script>

</asp:Content>









