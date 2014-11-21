<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="History.aspx.cs" Inherits="Portal.GUI.EditoralOffice.MainOffce.Newslist.History" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %> 

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="chart" style="text-align:center">
          <%--  <asp:CHART id="Chart1" runat="server" Height="250px" Width="870px" ImageLocation="~/images/ChartImages/ChartPic_#SEQ(300,3)" ImageType="Png" BackColor="#D3DFF0" Palette="BrightPastel" BorderDashStyle="Solid" BackSecondaryColor="White" BackGradientStyle="TopBottom" BorderWidth="2" BorderColor="26, 59, 105">
			    <borderskin SkinStyle="Emboss"></borderskin>
			    <series >
				    <asp:Series  IsXValueIndexed="true" XValueType="String" Name="Series1" ChartType="Line" BorderColor="180, 26, 59, 105" YValueType="Int32" IsValueShownAsLabel="true"></asp:Series>
			    </series>
			    <chartareas>
				    <asp:ChartArea Name="ChartArea1" BorderColor="64, 64, 64, 64" BorderDashStyle="Solid" BackSecondaryColor="White" BackColor="64, 165, 191, 228" ShadowColor="Transparent" BackGradientStyle="TopBottom">
					    <%--<position Y="2" Height="94" Width="94" X="2"></position>
					    <axisy LineColor="64, 64, 64, 64">
						    <LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" />
						    <MajorGrid LineColor="64, 64, 64, 64" />
					    </axisy>
					   
				    </asp:ChartArea>
			    </chartareas>
		    </asp:CHART>--%>
            <asp:Image runat="server" ID="imgChart" />
        </div>
        <br />
        <div style="max-width:800px; display:none">
            <asp:GridView ID="grdList" runat="server" AutoGenerateColumns="False" 
                CellPadding="4" ForeColor="Black" GridLines="Vertical" Width="100%" 
                BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px">
                <RowStyle BackColor="#F7F7DE" />
                <Columns>
                    <asp:TemplateField HeaderText="Hoạt động">
                        <ItemTemplate>
                            <%#Eval("Comment_Title") %> <i>(<%#Eval("CreateDate") %>)</i>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <FooterStyle BackColor="#CCCC99" />
                <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                <AlternatingRowStyle BackColor="White" />
                
            </asp:GridView>
        </div>
    </form>
</body>
</html>
