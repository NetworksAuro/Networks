<%@ Page Title="SearchAll" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SearchAll.aspx.cs" Inherits="NTOS.SearchAll" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    
    <%@ Register Assembly="DropDownCheckBoxes" Namespace="Saplin.Controls" TagPrefix="asp" %> 

    <%--<style type ="text/css"
>

        .CSSTableGenerator {
	margin:0px;padding:0px;
	width:100%;
	box-shadow: 10px 10px 5px #888888;
	border:1px solid #000000;
	
	-moz-border-radius-bottomleft:14px;
	-webkit-border-bottom-left-radius:14px;
	border-bottom-left-radius:14px;
	
	-moz-border-radius-bottomright:14px;
	-webkit-border-bottom-right-radius:14px;
	border-bottom-right-radius:14px;
	
	-moz-border-radius-topright:14px;
	-webkit-border-top-right-radius:14px;
	border-top-right-radius:14px;
	
	-moz-border-radius-topleft:14px;
	-webkit-border-top-left-radius:14px;
	border-top-left-radius:14px;
}.CSSTableGenerator table{
    border-collapse: collapse;
        border-spacing: 0;
	width:100%;
	height:100%;
	margin:0px;padding:0px;
}.CSSTableGenerator tr:last-child td:last-child {
	-moz-border-radius-bottomright:14px;
	-webkit-border-bottom-right-radius:14px;
	border-bottom-right-radius:14px;
}
.CSSTableGenerator table tr:first-child td:first-child {
	-moz-border-radius-topleft:14px;
	-webkit-border-top-left-radius:14px;
	border-top-left-radius:14px;
}
.CSSTableGenerator table tr:first-child td:last-child {
	-moz-border-radius-topright:14px;
	-webkit-border-top-right-radius:14px;
	border-top-right-radius:14px;
}.CSSTableGenerator tr:last-child td:first-child{
	-moz-border-radius-bottomleft:14px;
	-webkit-border-bottom-left-radius:14px;
	border-bottom-left-radius:14px;
}.CSSTableGenerator tr:hover td{
	
}
.CSSTableGenerator tr:nth-child(odd){ background-color:#aad4ff; }
.CSSTableGenerator tr:nth-child(even)    { background-color:#ffffff; }.CSSTableGenerator td{
	vertical-align:middle;
	
	
	border:1px solid #000000;
	border-width:0px 1px 1px 0px;
	text-align:left;
	padding:7px;
	font-size:10px;
	font-family:Arial;
	font-weight:normal;
	color:#000000;
}.CSSTableGenerator tr:last-child td{
	border-width:0px 1px 0px 0px;
}.CSSTableGenerator tr td:last-child{
	border-width:0px 0px 1px 0px;
}.CSSTableGenerator tr:last-child td:last-child{
	border-width:0px 0px 0px 0px;
}
.CSSTableGenerator tr:first-child td{
		background:-o-linear-gradient(bottom, #005fbf 5%, #003f7f 100%);	background:-webkit-gradient( linear, left top, left bottom, color-stop(0.05, #005fbf), color-stop(1, #003f7f) );
	background:-moz-linear-gradient( center top, #005fbf 5%, #003f7f 100% );
	filter:progid:DXImageTransform.Microsoft.gradient(startColorstr="#005fbf", endColorstr="#003f7f");	background: -o-linear-gradient(top,#005fbf,003f7f);

	background-color:#005fbf;
	border:0px solid #000000;
	text-align:center;
	border-width:0px 0px 1px 1px;
	font-size:14px;
	font-family:Arial;
	font-weight:bold;
	color:#ffffff;
}
.CSSTableGenerator tr:first-child:hover td{
	background:-o-linear-gradient(bottom, #005fbf 5%, #003f7f 100%);	background:-webkit-gradient( linear, left top, left bottom, color-stop(0.05, #005fbf), color-stop(1, #003f7f) );
	background:-moz-linear-gradient( center top, #005fbf 5%, #003f7f 100% );
	filter:progid:DXImageTransform.Microsoft.gradient(startColorstr="#005fbf", endColorstr="#003f7f");	background: -o-linear-gradient(top,#005fbf,003f7f);

	background-color:#005fbf;
}
.CSSTableGenerator tr:first-child td:first-child{
	border-width:0px 0px 1px 0px;
}
.CSSTableGenerator tr:first-child td:last-child{
	border-width:0px 0px 1px 1px;
}
    </style> --%> 
     <style type="text/css">
       .CSSTableGenerator {
	margin:0px;padding:0px;
	width:100%;
	box-shadow: 10px 10px 5px #888888;
	border:1px solid #cccccc;
	
	-moz-border-radius-bottomleft:0px;
	-webkit-border-bottom-left-radius:0px;
	border-bottom-left-radius:0px;
	
	-moz-border-radius-bottomright:0px;
	-webkit-border-bottom-right-radius:0px;
	border-bottom-right-radius:0px;
	
	-moz-border-radius-topright:0px;
	-webkit-border-top-right-radius:0px;
	border-top-right-radius:0px;
	
	-moz-border-radius-topleft:0px;
	-webkit-border-top-left-radius:0px;
	border-top-left-radius:0px;
}.CSSTableGenerator table{
    border-collapse: collapse;
        border-spacing: 0;
	width:100%;
	height:100%;
	margin:0px;padding:0px;
}.CSSTableGenerator tr:last-child td:last-child {
	-moz-border-radius-bottomright:0px;
	-webkit-border-bottom-right-radius:0px;
	border-bottom-right-radius:0px;
}
.CSSTableGenerator table tr:first-child td:first-child {
	-moz-border-radius-topleft:0px;
	-webkit-border-top-left-radius:0px;
	border-top-left-radius:0px;
}
.CSSTableGenerator table tr:first-child td:last-child {
	-moz-border-radius-topright:0px;
	-webkit-border-top-right-radius:0px;
	border-top-right-radius:0px;
}.CSSTableGenerator tr:last-child td:first-child{
	-moz-border-radius-bottomleft:0px;
	-webkit-border-bottom-left-radius:0px;
	border-bottom-left-radius:0px;
}.CSSTableGenerator tr:hover td{
	background-color:#e5e5e5;
		

}
.CSSTableGenerator td{
	vertical-align:middle;
		background:-o-linear-gradient(bottom, #ffffff 5%, #e5e5e5 100%);	background:-webkit-gradient( linear, left top, left bottom, color-stop(0.05, #ffffff), color-stop(1, #e5e5e5) ); 
	background:-moz-linear-gradient( center top, #ffffff 5%, #e5e5e5 100% );
	filter:progid:DXImageTransform.Microsoft.gradient(startColorstr="#ffffff", endColorstr="#e5e5e5");	background: -o-linear-gradient(top,#ffffff,e5e5e5);

	background-color:#ffffff;

	border:1px solid #cccccc;
	border-width:0px 1px 1px 0px;
	text-align:left;
	padding:7px;
	font-size:10px;
	font-family:Arial;
	font-weight:normal;
	color:#000000;
}.CSSTableGenerator tr:last-child td{
	border-width:0px 1px 0px 0px;
}.CSSTableGenerator tr td:last-child{
	border-width:0px 0px 1px 0px;
}.CSSTableGenerator tr:last-child td:last-child{
	border-width:0px 0px 0px 0px;
}
.CSSTableGenerator tr:first-child td{
		background:-o-linear-gradient(bottom, #cccccc 5%, #b2b2b2 100%);	background:-webkit-gradient( linear, left top, left bottom, color-stop(0.05, #cccccc), color-stop(1, #b2b2b2) );
	background:-moz-linear-gradient( center top, #cccccc 5%, #b2b2b2 100% );
	filter:progid:DXImageTransform.Microsoft.gradient(startColorstr="#cccccc", endColorstr="#b2b2b2");	background: -o-linear-gradient(top,#cccccc,b2b2b2);

	background-color:#cccccc;
	border:0px solid #cccccc;
	text-align:center;
	border-width:0px 0px 1px 1px;
	font-size:14px;
	font-family:Arial;
	font-weight:bold;
	color:#000000;
}
.CSSTableGenerator tr:first-child:hover td{
	background:-o-linear-gradient(bottom, #cccccc 5%, #b2b2b2 100%);	background:-webkit-gradient( linear, left top, left bottom, color-stop(0.05, #cccccc), color-stop(1, #b2b2b2) );
	background:-moz-linear-gradient( center top, #cccccc 5%, #b2b2b2 100% );
	filter:progid:DXImageTransform.Microsoft.gradient(startColorstr="#cccccc", endColorstr="#b2b2b2");	background: -o-linear-gradient(top,#cccccc,b2b2b2);

	background-color:#cccccc;
}
.CSSTableGenerator tr:first-child td:first-child{
	border-width:0px 0px 1px 0px;
}
.CSSTableGenerator tr:first-child td:last-child{
	border-width:0px 0px 1px 1px;
}
   </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <asp:HiddenField ID="hdncityid" runat="server" />
        <asp:HiddenField ID="hdncountry" Value="0" runat="server" />
        <asp:HiddenField ID="hdnzipcode" Value="0" runat="server" />
        <asp:HiddenField ID="hdnstate" Value="0" runat="server" />
        
        <table width="100%" cellpadding="0" cellspacing="0" border="0" style="margin-top:20px;">
            <tr><td width="100%">  
         <asp:Panel ID="pnlpresenter"  runat="server" Visible="false">
            <table>
              <tr >
                  <td colspan="3" style="text-align:left;">
                      <label>
                      Presenter Search
                          </label>
                  </td>
              </tr>
                        
             

                <tr >
                    
                    <td style="text-align:right">
                         <%if (mode == "Presentation")
                           { %>
                       
                        Presenter 
                     <%}else{ %>
                     
                        Venue 
                    <%} %>
&nbsp;
                   
                         <%if (mode == "Presentation")
                           { %>
                        <asp:TextBox ID="txtpresentername" MaxLength="100" Columns="30"  CssClass="txtmixcaps" runat="server"  AutoPostBack="false"></asp:TextBox>
                        <ajaxToolkit:AutoCompleteExtender ID="acepresentername" runat="server" TargetControlID="txtpresentername"
                                            CompletionListElementID="divacewidth" DelimiterCharacters="," CompletionListCssClass="AutoExtender" CompletionListItemCssClass="AutoExtenderList"
                                            CompletionListHighlightedItemCssClass="AutoExtenderHighlight" ShowOnlyCurrentWordInCompletionListItem="true"
                                            MinimumPrefixLength="1" EnableCaching="true"  CompletionSetCount="1" CompletionInterval="100"
                                            ServiceMethod="Getpresentername"  FirstRowSelected="true">
                        </ajaxToolkit:AutoCompleteExtender>

                         <%}else{ %>
                         <asp:TextBox ID="txtvenuname"  MaxLength="100" CssClass="txtmixcaps" TabIndex="1" runat="server"  Columns="30" ></asp:TextBox>
                        <ajaxToolkit:AutoCompleteExtender ID="aceVenuename" runat="server" TargetControlID="txtvenuname" DelimiterCharacters=","
                                CompletionListElementID="divacewidth" CompletionListCssClass="AutoExtender" CompletionListItemCssClass="AutoExtenderList"
                                CompletionListHighlightedItemCssClass="AutoExtenderHighlight"
                                FirstRowSelected="true" MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="1"
                                CompletionInterval="60" 
                                ServiceMethod="GetVenueName">
                            </ajaxToolkit:AutoCompleteExtender>
                             <%}%>
                    </td>
                    
                   
                      
                    <td > &nbsp;&nbsp; City/State
             
                     <%--   <asp:DropDownCheckBoxes ID="ddchkCountry" runat="server"
                    AddJQueryReference="True" UseButtons="True" UseSelectAllNode="True" OnSelectedIndexChanged="ddchkCountry_SelectedIndexChanged"
                   SelectBoxWidth="500" DropDownBoxBoxWidth="200"  DropDownBoxBoxHeight="130" style="top: 0px; left: 0px; width:85px; height: 10px;" >
                        </asp:DropDownCheckBoxes>--%>
                         <asp:TextBox ID="txtPVCityState" OnTextChanged="txtPVCityState_TextChanged" runat="server"></asp:TextBox>
                         <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender4" runat="server" TargetControlID="txtPVCityState"
                              CompletionListHighlightedItemCssClass="AutoExtenderHighlight"
                          DelimiterCharacters="," CompletionListCssClass="AutoExtender" CompletionListItemCssClass="AutoExtenderList"
                        FirstRowSelected="true" MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="1"
                        CompletionInterval="100"
                        ServiceMethod="Getcityname">
                                                </ajaxToolkit:AutoCompleteExtender>

                       &nbsp;&nbsp;&nbsp;
                          <asp:TextBox ID="txtstatePV" runat="server"></asp:TextBox>
                         <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender9" runat="server" TargetControlID="txtstatePV"
                              CompletionListHighlightedItemCssClass="AutoExtenderHighlight"
                          DelimiterCharacters="," CompletionListCssClass="AutoExtender" CompletionListItemCssClass="AutoExtenderList"
                        FirstRowSelected="true" MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="1"
                        CompletionInterval="100"
                        ServiceMethod="GetstATEname">
                                                </ajaxToolkit:AutoCompleteExtender>

                   &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;ZipCode
             
                   <%--     <asp:DropDownCheckBoxes ID="drpdwnckbxZipCode" runat="server"
                    AddJQueryReference="True" UseButtons="True" UseSelectAllNode="True" OnSelectedIndexChanged="ddchkCountry_SelectedIndexChanged"
                   SelectBoxWidth="500" DropDownBoxBoxWidth="200"  DropDownBoxBoxHeight="130" style="top: 0px; left: -31px; width:100px" >
                        </asp:DropDownCheckBoxes>--%>
                         <asp:TextBox ID="txtPVZipCode" runat="server" Columns="10"></asp:TextBox>
                                            <ajaxToolkit:AutoCompleteExtender ID="aceZipcode" runat="server" TargetControlID="txtPVZipCode"
                                                 DelimiterCharacters="," CompletionListCssClass="AutoExtender" CompletionListItemCssClass="AutoExtenderList"
                        FirstRowSelected="true" MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="1"
                        CompletionInterval="100"
                        ServiceMethod="GetZipcode">
                                                </ajaxToolkit:AutoCompleteExtender>

                         &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;
                    Country
            
                     
                        <asp:TextBox ID="txtCountry" MaxLength="100" Columns="20"  CssClass="txtmixcaps" runat="server"  AutoPostBack="true"></asp:TextBox>
                         <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender8" runat="server" TargetControlID="txtCountry"
                              CompletionListHighlightedItemCssClass="AutoExtenderHighlight"
                          DelimiterCharacters=",
                             " CompletionListCssClass="AutoExtender" CompletionListItemCssClass="AutoExtenderList"
                        FirstRowSelected="true" MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="1"
                        CompletionInterval="100"
                        ServiceMethod="Getcountryname">
                                                </ajaxToolkit:AutoCompleteExtender>
                        &nbsp;</td>
                    </tr>
                    <%if(mode=="Venue"){ %>
                   <tr>
                    <td style="text-align:right">
                        <label>
                        Capacity</label> </td>
                    <td>
                        <asp:TextBox ID="txtCapacity1" MaxLength="15" Columns="30"  CssClass="txtmixcaps" runat="server"  AutoPostBack="true" ></asp:TextBox>
                        </td>
                       <td>
                           &nbsp;
                       </td>
                       </tr>
                    <%} %>
               
                
                <tr style="align:left">
                    <td colspan="3">
                        <label style="font-weight:bold">
                        Contact&nbsp;Person</label> </td>
                    
                </tr>
                <tr>
                    <td style="text-align:right;width:20%">
                      
                    Last Name
                          
                   &nbsp;
                        <asp:TextBox ID="txtbxLastName" MaxLength="100" Columns="30" CssClass="txtmixcaps" runat="server"></asp:TextBox>
                        <ajaxToolkit:AutoCompleteExtender ID="aceLastName" runat="server" TargetControlID="txtbxLastName"
                         DelimiterCharacters="," CompletionListCssClass="AutoExtender" CompletionListItemCssClass="AutoExtenderList"
                         CompletionListHighlightedItemCssClass="AutoExtenderHighlight" ShowOnlyCurrentWordInCompletionListItem="true"
                          MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="1" CompletionInterval="100"
                                    ServiceMethod="Getlastname"  FirstRowSelected="true">
                        </ajaxToolkit:AutoCompleteExtender>
                    </td>
                    <td style="text-align:left;width:80%">
                        &nbsp;&nbsp;
                         First Name
                           
                       
                        <asp:TextBox ID="txtbxFirstname" MaxLength="100" Columns="30" CssClass="txtmixcaps" runat="server"></asp:TextBox>
                        <ajaxToolkit:AutoCompleteExtender ID="acefirstname" runat="server" TargetControlID="txtbxFirstname"
                          DelimiterCharacters="," CompletionListCssClass="AutoExtender" CompletionListItemCssClass="AutoExtenderList"
                         CompletionListHighlightedItemCssClass="AutoExtenderHighlight" ShowOnlyCurrentWordInCompletionListItem="true"
                          MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="1" CompletionInterval="100"
                          ServiceMethod="Getpersonalname"  FirstRowSelected="true">
                        </ajaxToolkit:AutoCompleteExtender>
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel ID="pnlShow" runat="server" Visible="false">
            <table>
                <tr>
                    <td style="text-align:right">
                        <label style="text-align:right">
                        Show</label> </td>
                    <td>
                        <asp:TextBox ID="txtshowname" CssClass="txtmixcaps txtreq" runat="server" autocomplete="off" 
                                  MaxLength="200" ToolTip="Enter show name!"></asp:TextBox>
                        <ajaxToolkit:AutoCompleteExtender ID="aceshowname" runat="server" TargetControlID="txtshowname" DelimiterCharacters=","
                                CompletionListElementID="divacewidth" CompletionListCssClass="AutoExtender" CompletionListItemCssClass="AutoExtenderList"
                                CompletionListHighlightedItemCssClass="AutoExtenderHighlight"
                                MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="1" CompletionInterval="60"
                                ServiceMethod="Getshownames"  FirstRowSelected="true">
                        </ajaxToolkit:AutoCompleteExtender>
                    </td>
                    <td style="text-align:right">
                        &nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;<label>
                        Show Begin date</label> </td>
                    <td>
                        <asp:TextBox ID="txtshowbegindate" runat="server" CssClass="txtreq" onkeyup="highlight();"></asp:TextBox>
                        <ajaxToolkit:CalendarExtender ID="ceshowbegindate" runat="server" TargetControlID="txtshowbegindate">
                        </ajaxToolkit:CalendarExtender>
                       
                      
                    </td>
                </tr>
                <tr>
                    <td style="text-align:right">
                        <label>
                        Corporate Name</label> </td>
                    <td>
                        <asp:TextBox ID="txtcorpname" runat="server" CssClass="txtmixcaps txtreq" MaxLength="200" onkeyup="highlight();"></asp:TextBox>
                    </td>
                    <td>
                       &nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp; <label>
                        Company Manager</label> </td>
                    <td>
                        <asp:DropDownCheckBoxes ID="ddckComanyManager" runat="server"
                    AddJQueryReference="True" UseButtons="True" UseSelectAllNode="True" OnSelectedIndexChanged="ddckComanyManager_SelectedIndexChanged"
                   SelectBoxWidth="500" DropDownBoxBoxWidth="200"  DropDownBoxBoxHeight="130" style="top: 0px; left: 0px; width:85px; height: 10px;" >
                        </asp:DropDownCheckBoxes>
                        <asp:Label ID="lblCompanymanager" runat="server" Visible="false" Text="Label"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="text-align:right">Overhead nut

                        </td>
                    <td>
                        <asp:TextBox ID="txtoverheadnut" runat="server" ></asp:TextBox>
                     
                    </td>
                    
                    <td style="text-align:right">
                       &nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp; <label>
                        Variable Royalities</label> </td>
                    <td>
                        <asp:TextBox ID="txtvariableroyalities" MaxLength="20" runat="server"></asp:TextBox>
                       
                    </td>
                </tr>
                <tr>
                    <td>Wkly Operating Expense
                        </td>
                    <td>
                        <asp:TextBox ID="txtwklyopexp" runat="server" ></asp:TextBox>
                        
                    </td>
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel ID="pnlContact" runat="server" Visible="false">
            <table>
                <tr>
                    <td style="text-align:right">
                        <label style="text-align:right">
                       First Middle Name </label> </td>
                    <td>
                        <asp:TextBox ID="txtbxCFirstName" CssClass="txtmixcaps" runat="server" autocomplete="off" 
                                  MaxLength="200" ToolTip="Enter show name!"></asp:TextBox>
                         <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server" TargetControlID="txtlastname"
                           CompletionListElementID="divacewidth" CompletionListCssClass="AutoExtender" CompletionListItemCssClass="AutoExtenderList"
                            CompletionListHighlightedItemCssClass="AutoExtenderHighlight"
                            MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="1" CompletionInterval="100"
                            ServiceMethod="Getpersonalname"  FirstRowSelected="true">
                        </ajaxToolkit:AutoCompleteExtender>
                        
                    </td>
                    <td>
                         &nbsp; &nbsp;<asp:TextBox ID="txtbxCMiddleName" CssClass="txtmixcaps txtreq" runat="server" autocomplete="off" 
                                  MaxLength="200" ToolTip="Enter middle name!"></asp:TextBox>
                       

                    </td>
                    <td style="text-align:left">
                        &nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;<label>
                        Employee Type </label> </td>
                    <td>
                        <asp:DropDownList SkinID="ddlmedium" ID="ddlemployeetype" runat="server">
                        </asp:DropDownList>
                       
                      
                    </td>
                </tr>
                <tr>
                    <td style="text-align:left">
                        <label>
                        Last Name</label> </td>
                    <td>
                        <asp:TextBox ID="txtlastname" runat="server" CssClass="txtmixcaps" MaxLength="100"></asp:TextBox>
                        <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" TargetControlID="txtbxCFirstName"
                           CompletionListCssClass="AutoExtender" CompletionListItemCssClass="AutoExtenderList"
                            CompletionListHighlightedItemCssClass="AutoExtenderHighlight"
                            MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="1" CompletionInterval="100"
                            ServiceMethod="Getlastname"  FirstRowSelected="true">
                        </ajaxToolkit:AutoCompleteExtender>
                   


                    </td>
                    <td></td>
                    <td style="text-align:left">
                       &nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp; <label>
                        Date of Hire</label> </td>
                    <td>
                      <asp:TextBox ID="txtdateifhire" runat="server"></asp:TextBox>
                       
                       

                    </td>
                </tr>
                <tr>
                    <td style="text-align:left">Company

                        </td>
                    <td>
                          <asp:TextBox ID="txtcompany" CssClass="txtmixcaps" runat="server" MaxLength="50"></asp:TextBox>
                        <div id="divacewidth1">
                        </div>
                        <ajaxToolkit:AutoCompleteExtender ID="acecompany" runat="server" TargetControlID="txtcompany"
                            CompletionListCssClass="AutoExtender" CompletionListItemCssClass="AutoExtenderList"
                            CompletionListHighlightedItemCssClass="AutoExtenderHighlight"
                            MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="1" CompletionInterval="100"
                            ServiceMethod="Getcompanyname" FirstRowSelected="true">
                        </ajaxToolkit:AutoCompleteExtender>
                     
                    </td>
                    </td>
                    <td></td>
                    <td style="text-align:left">
                       &nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp; <label>
                           Termination Date
                     </label> </td>
                    <td>
                         <asp:TextBox ID="txttermanitationdate" runat="server"  AutoPostBack="true"></asp:TextBox>
                       

                       
                    </td>
                </tr>
                <tr>
                    <td style="text-align:left">
                        Title
                          </td>
                    <td>
                        <asp:DropDownList ID="ddltitle" runat="server" SkinID="ddlmedium">
                            <asp:ListItem>SELECT</asp:ListItem>
                        </asp:DropDownList>
                     
                    </td>
                    </td>
                    <td></td>
                    <td style="text-align:left">
                       &nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp; <label>
                           Employee Status
                     </label> </td>
                    <td>
                        <asp:DropDownList ID="ddlemployeestatus" runat="server" SkinID="ddlmedium">
                        </asp:DropDownList>
                       
                    </td>
                </tr>
                 <tr>
                    <td style="text-align:left">Show Name

                        </td>
                    <td>
                        <asp:TextBox ID="txtCShowName" runat="server" ></asp:TextBox>
                        <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="txtCShowName" DelimiterCharacters=","
                                 CompletionListCssClass="AutoExtender" CompletionListItemCssClass="AutoExtenderList"
                                CompletionListHighlightedItemCssClass="AutoExtenderHighlight"
                                MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="1" CompletionInterval="60"
                                ServiceMethod="Getshownames"  FirstRowSelected="true">
                        </ajaxToolkit:AutoCompleteExtender>
                     
                    </td>
                    </td>
                     <td></td>
                    <td style="text-align:left">
                       &nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp; <label>
                          Marital Status 
                     </label> </td>
                    <td>
                        <asp:TextBox ID="txtmaterialstatus" MaxLength="10" runat="server"></asp:TextBox>
                       
                    </td>
                </tr>
                <tr>
                    <td>

                    </td>
                    <td>

                    </td>
                    <td></td>
                     <td style="text-align:left">
                       &nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp; <label>
                          DOB 
                     </label> </td>
                    <td>
                        <asp:TextBox ID="txtdob" runat="server"></asp:TextBox>
                       
                    </td>
                </tr>
                <tr>

                    <td colspan="4">
                        <table>
                            <tr>
                                <td style="text-align:right">
                                    Residental
                                </td>
                                <td>
                                   &nbsp;
                                    &nbsp; ZipCode 
                                </td>
                                <td>
                                     <asp:TextBox ID="txtZipCode" runat="server"></asp:TextBox>

                                      <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender6" runat="server" TargetControlID="txtZipCode"
                                                 DelimiterCharacters="," CompletionListCssClass="AutoExtender" CompletionListItemCssClass="AutoExtenderList"
                        FirstRowSelected="true" MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="1"
                        CompletionInterval="100"
                        ServiceMethod="GetZipcode">
                                          </ajaxToolkit:AutoCompleteExtender>
                                </td>
                                <td>
                                    &nbsp; &nbsp;
                                    &nbsp; &nbsp; City 
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCity" runat="server"></asp:TextBox>
                                      <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender5" runat="server" TargetControlID="txtCity"
                              CompletionListHighlightedItemCssClass="AutoExtenderHighlight"
                          DelimiterCharacters="," CompletionListCssClass="AutoExtender" CompletionListItemCssClass="AutoExtenderList"
                        FirstRowSelected="true" MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="1"
                        CompletionInterval="100"
                        ServiceMethod="Getcityname">
                                                </ajaxToolkit:AutoCompleteExtender>
                               
                                </td>
                            </tr>
                            <tr>
                                <td></td>
                                <td>
                                   &nbsp; &nbsp;  State 
                                </td>
                                <td>
                                    <asp:TextBox ID="txtState" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    &nbsp; &nbsp;
                                    &nbsp; &nbsp; Country
                                </td>
                                <td>
                                <asp:TextBox ID="txtCCountry" runat="server"></asp:TextBox>
                                        <ajaxToolkit:AutoCompleteExtender ID="AutoCompleteExtender7" runat="server" TargetControlID="txtCCountry"
                              CompletionListHighlightedItemCssClass="AutoExtenderHighlight"
                          DelimiterCharacters="," CompletionListCssClass="AutoExtender" CompletionListItemCssClass="AutoExtenderList"
                        FirstRowSelected="true" MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="1"
                        CompletionInterval="100"
                        ServiceMethod="Getcountryname">
                                                </ajaxToolkit:AutoCompleteExtender>
                                </td>
                            </tr>

                        </table>

                    </td>
                   
                </tr>
            </table>
        </asp:Panel>
        </td>
       </tr>
        <tr>
            <td colspan="3">
                &nbsp;
            </td>
        </tr>
        <tr>
        <td valign="top" width="50%">
        <asp:Panel ID="pnlLastSearch" CssClass="CSSTableGenerator" runat ="server">
            <table style="width:100%">
                <tr>
                    <td colspan="3" style="text-align:left">
                         <label style="vertical-align:top;text-align:left">
                           Last&nbsp;Search 
                       </label> 
                    </td>
                </tr>
                   <tr>
                    <td style="width:40%">
                      
                   
                       
                        <asp:TextBox ID="txtLastSearch" Rows="3" TextMode="MultiLine" runat="server" Width="100%"></asp:TextBox>
                     
                        </td>
                       <td style="width:60%">
                       <asp:Button ID="btnLastSearch"  Style="vertical-align:top" CssClass="buttn" OnClick="btnLastSearch_Click" runat="server" Text=" Retrieve Last Search" />
                   </td>
                </tr>
             
            </table>
        </asp:Panel>   
            </td></tr></table>   

    </ContentTemplate>
</asp:UpdatePanel>

</asp:Content>
