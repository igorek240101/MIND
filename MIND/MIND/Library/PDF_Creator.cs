using iText.IO.Image;
using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Forms;
using iText.Forms.Fields;
using iText.Kernel.Pdf.Action;
using iText.Kernel.Pdf.Annot;
using iText.Kernel.Pdf.Canvas.Draw;
using iText.Layout;
using iText.Layout.Borders;
using iText.Layout.Element;
using iText.Layout.Properties;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Drawing;

namespace MIND.Library
{
    static class PDF_Creator
    {

        public static void Create(List<LinesText> value, Form1 parent, string str)
        {
            PdfWriter writer = new PdfWriter(str);
            PdfDocument pdf = new PdfDocument(writer);
            Document document = new Document(pdf);
            for (int i = 0; i < value.Count; i++)
            {
                if (value[i].GetType() == typeof(SimpleLines))
                {
                    SimpleLines simpleLines = value[i] as SimpleLines;
                    Paragraph header = new Paragraph();
                    int y = 5;
                    for (int j = 0; j < simpleLines.value.Controls.Count; j++)
                    {
                        if (y < simpleLines.value.Controls[j].Location.Y)
                        {
                            y = simpleLines.value.Controls[j].Location.Y;
                            header.Add("\r\n");
                        }
                        if (simpleLines.value.Controls[j].GetType() == typeof(SimpleInLineText.SimpleInLineTextControl))
                        {
                            SimpleInLineText.SimpleInLineTextControl control = simpleLines.value.Controls[j] as SimpleInLineText.SimpleInLineTextControl;
                            CreateText(control, header);
                        }
                        else
                        {
                            if (simpleLines.value.Controls[j].GetType() == typeof(Link.LinkControl))
                            {
                                Link.LinkControl control = simpleLines.value.Controls[j] as Link.LinkControl;
                                CreateLink(document, control, header, false);
                            }
                            else
                            {
                                if (simpleLines.value.Controls[j].GetType() == typeof(ImageText.ImageTextControl))
                                {
                                    CreateImage(document, simpleLines.value.Controls[j] as ImageText.ImageTextControl, false, ref header, false,true);
                                }
                                else
                                {
                                    if (simpleLines.value.Controls[j].GetType() == typeof(Label))
                                    {
                                        CreateCode(simpleLines.value.Controls[j] as Label, header);
                                    }
                                }
                            }
                        }
                    }
                    document.Add(header);
                }
                else
                {
                    if (value[i].GetType() == typeof(HeaderLines))
                    {
                        HeaderLines simpleLines = value[i] as HeaderLines;
                        Paragraph header = new Paragraph();
                        int y = 5;
                        for (int j = 0; j < simpleLines.value.Controls.Count; j++)
                        {
                            if (y < simpleLines.value.Controls[j].Location.Y)
                            {
                                y = simpleLines.value.Controls[j].Location.Y;
                                header.Add("\r\n");
                            }
                            if (simpleLines.value.Controls[j].GetType() == typeof(SimpleInLineText.SimpleInLineTextControl))
                            {
                                SimpleInLineText.SimpleInLineTextControl control = simpleLines.value.Controls[j] as SimpleInLineText.SimpleInLineTextControl;
                                CreateText(control, header);
                            }
                            else
                            {
                                if (simpleLines.value.Controls[j].GetType() == typeof(Link.LinkControl))
                                {
                                    Link.LinkControl control = simpleLines.value.Controls[j] as Link.LinkControl;
                                    CreateLink(document, control, header, false);
                                }
                                else
                                {
                                    if (simpleLines.value.Controls[j].GetType() == typeof(ImageText.ImageTextControl))
                                    {
                                        CreateImage(document, simpleLines.value.Controls[j] as ImageText.ImageTextControl, false, ref header, false, true);
                                    }
                                    else
                                    {
                                        if (simpleLines.value.Controls[j].GetType() == typeof(Label))
                                        {
                                            CreateCode(simpleLines.value.Controls[j] as Label, header);
                                        }
                                    }
                                }
                            }
                        }
                        document.Add(header);
                    }
                    else
                    {
                        if (value[i].GetType() == typeof(QuotationLines))
                        {
                            QuotationLines quotationLines = value[i] as QuotationLines;
                            Paragraph header = new Paragraph();
                            int y = 5;
                            header.SetBorderLeft(new SolidBorder(ColorConstants.GRAY, 4));
                            header.SetPadding(30);
                            for (int j = 0; j < quotationLines.value.Controls.Count; j++)
                            {
                                if (y < quotationLines.value.Controls[j].Location.Y)
                                {
                                    y = quotationLines.value.Controls[j].Location.Y;
                                    header.Add("\r\n");
                                }
                                if (quotationLines.value.Controls[j].GetType() == typeof(SimpleInLineText.SimpleInLineTextControl))
                                {
                                    SimpleInLineText.SimpleInLineTextControl control = quotationLines.value.Controls[j] as SimpleInLineText.SimpleInLineTextControl;
                                    CreateText(control, header);
                                }
                                else
                                {
                                    if (quotationLines.value.Controls[j].GetType() == typeof(Link.LinkControl))
                                    {
                                        Link.LinkControl control = quotationLines.value.Controls[j] as Link.LinkControl;
                                        CreateLink(document, control, header, true);
                                    }
                                    else
                                    {
                                        if (quotationLines.value.Controls[j].GetType() == typeof(ImageText.ImageTextControl))
                                        {
                                            CreateImage(document, quotationLines.value.Controls[j] as ImageText.ImageTextControl, false, ref header, true, j+1== quotationLines.value.Controls.Count);
                                        }
                                        else
                                        {
                                            if (quotationLines.value.Controls[j].GetType() == typeof(Label))
                                            {
                                                CreateCode(quotationLines.value.Controls[j] as Label, header);
                                            }
                                        }
                                    }
                                }
                            }
                            document.Add(header);
                        }
                        else
                        {
                            if (value[i].GetType() == typeof(ListLine))
                            {
                                ListLine listLine = value[i] as ListLine;
                                int n = 0;
                                List list = GetList(listLine.space, listLine.value as ListLine.ListLineControl, ref n);
                                document.Add(list);
                            }
                            else
                            {
                                if (value[i].GetType() == typeof(LineLines))
                                {
                                    LineSeparator ls = new LineSeparator(new SolidLine((value[i] as LineLines).w / 2));
                                    document.Add(ls);
                                    document.Add(new Paragraph());
                                }
                                else
                                {
                                    if (value[i].GetType() == typeof(LineCode))
                                    {
                                        LineCode lineCode = value[i] as LineCode;
                                        Paragraph header = new Paragraph();
                                        header.SetBackgroundColor(ColorConstants.LIGHT_GRAY);
                                        int y = 5;
                                        for (int j = 0; j < lineCode.value.Controls.Count; j += 2)
                                        {
                                            if (y < lineCode.value.Controls[j].Location.Y)
                                            {
                                                y = lineCode.value.Controls[j].Location.Y;
                                                header.Add("\r\n");
                                            }
                                            if (lineCode.value.Controls[j].GetType() == typeof(Label))
                                            {
                                                int count = 4 - (lineCode.value.Controls[j + 1] as Label).Text.Length;
                                                CreateCode(lineCode.value.Controls[j+1] as Label, header);
                                                string s = " ";
                                                for (int k = 0; k < count; k++) s += "  ";
                                                header.Add(s);
                                                CreateCode(lineCode.value.Controls[j] as Label, header);
                                            }
                                        }
                                        document.Add(header);
                                    }
                                    else
                                    {
                                        if(value[i].GetType() == typeof(ChekLine))
                                        {
                                            int y = 5;
                                            ChekLine chek = value[i] as ChekLine;
                                            CheckBox chrckbox = chek.value.Controls[0] as CheckBox;
                                            Paragraph header = new Paragraph();
                                            if (chrckbox.Checked) header.SetLineThrough();
                                            if (chrckbox.Checked) header.SetItalic();
                                            header.Add("•     ");

                                            for (int j = 1; j < chek.value.Controls.Count; j++)
                                            {
                                                if (y < chek.value.Controls[j].Location.Y)
                                                {
                                                    y = chek.value.Controls[j].Location.Y;
                                                    header.Add("\r\n");
                                                }
                                                if (chek.value.Controls[j].GetType() == typeof(SimpleInLineText.SimpleInLineTextControl))
                                                {
                                                    SimpleInLineText.SimpleInLineTextControl control = chek.value.Controls[j] as SimpleInLineText.SimpleInLineTextControl;
                                                    CreateText(control, header);
                                                }
                                                else
                                                {
                                                    if (chek.value.Controls[j].GetType() == typeof(Link.LinkControl))
                                                    {
                                                        Link.LinkControl control = chek.value.Controls[j] as Link.LinkControl;
                                                        CreateLink(document, control, header, false);
                                                    }
                                                    else
                                                    {
                                                        if (chek.value.Controls[j].GetType() == typeof(ImageText.ImageTextControl))
                                                        {
                                                            CreateImage(document, chek.value.Controls[j] as ImageText.ImageTextControl, false, ref header, false, true);
                                                        }
                                                        else
                                                        {
                                                            if (chek.value.Controls[j].GetType() == typeof(Label))
                                                            {
                                                                CreateCode(chek.value.Controls[j] as Label, header);
                                                            }
                                                        }
                                                    }
                                                }
                                            }

                                            document.Add(header);
                                        }
                                        else
                                        {
                                            if(value[i].GetType() == typeof(TableLines))
                                            {
                                                TableLines tablelines = value[i] as TableLines;
                                                Table table = new Table(tablelines.xc, false);
                                                for(int j = 0; j < tablelines.yc; j++)
                                                {
                                                    for (int k = 0; k < tablelines.xc; k++)
                                                    {
                                                        Cell cell = new Cell();
                                                        if (tablelines.loc[k] == null) cell.SetTextAlignment(TextAlignment.CENTER);
                                                        else if(tablelines.loc[k] == true) cell.SetTextAlignment(TextAlignment.RIGHT);
                                                        else cell.SetTextAlignment(TextAlignment.LEFT);
                                                        int y = 5;
                                                        Paragraph header = new Paragraph();
                                                        Style style = new Style();
                                                        SimpleLines simpleLines = tablelines.cell[j, k];
                                                        for (int a = 0; a < simpleLines.value.Controls.Count; a++)
                                                        {
                                                            if (y < simpleLines.value.Controls[a].Location.Y)
                                                            {
                                                                y = simpleLines.value.Controls[a].Location.Y;
                                                                header.Add("\r\n");
                                                            }
                                                            if (simpleLines.value.Controls[a].GetType() == typeof(SimpleInLineText.SimpleInLineTextControl))
                                                            {
                                                                SimpleInLineText.SimpleInLineTextControl control = simpleLines.value.Controls[a] as SimpleInLineText.SimpleInLineTextControl;
                                                                CreateText(control, header);
                                                            }
                                                            else
                                                            {
                                                                if (simpleLines.value.Controls[a].GetType() == typeof(Link.LinkControl))
                                                                {
                                                                    Link.LinkControl control = simpleLines.value.Controls[a] as Link.LinkControl;
                                                                    CreateLink(cell, control, header, false);
                                                                }
                                                                else
                                                                {
                                                                    if (simpleLines.value.Controls[a].GetType() == typeof(ImageText.ImageTextControl))
                                                                    {
                                                                        CreateImage(cell, simpleLines.value.Controls[a] as ImageText.ImageTextControl, false, ref header, false, true);
                                                                    }
                                                                    else
                                                                    {
                                                                        if (simpleLines.value.Controls[a].GetType() == typeof(Label))
                                                                        {
                                                                            CreateCode(simpleLines.value.Controls[a] as Label, header);
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                        cell.Add(header);
                                                        table.AddCell(cell);
                                                    }
                                                }
                                                document.Add(table);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                parent.toolStripProgressBar1.Value = (int)(((float)i / value.Count) * 100);
                parent.toolStripProgressBar1.Invalidate();
            }
            document.Close();
            parent.toolStripProgressBar1.Value = 0;
            MessageBox.Show("Экспорт в pdf завершен", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        private static void CreateText(SimpleInLineText.SimpleInLineTextControl control, Paragraph header)
        {
            for (int k = 0; k < control.Controls.Count; k++)
            {
                Style style = new Style();
                Label label = control.Controls[k] as Label;
                style.SetFont(PdfFontFactory.CreateFont("C:\\Windows\\Fonts\\times.ttf", "Cp1251", true));
                style.SetFontSize(label.Font.Size);
                if (label.Font.Bold) style.SetBold();
                if (label.Font.Italic) style.SetItalic();
                if (label.Font.Underline) style.SetUnderline();
                if (label.Font.Strikeout) style.SetLineThrough();
                header.Add(new Text(label.Text).AddStyle(style));
            }
        }

        private static void CreateLink(object document, Link.LinkControl control, Paragraph header, bool border)
        {
            for (int k = 0; k < control.Controls.Count; k++)
            {
                if (control.Controls[k].GetType() == typeof(LinkLabel))
                {
                    Style style = new Style();
                    LinkLabel label = control.Controls[k] as LinkLabel;
                    iText.Kernel.Geom.Rectangle rect = new iText.Kernel.Geom.Rectangle(0, 0);
                    PdfLinkAnnotation annotation = new PdfLinkAnnotation(rect);
                    PdfAction action = PdfAction.CreateURI(label.Links[0].LinkData.ToString());
                    annotation.SetAction(action);
                    annotation.SetContents(label.ContextMenuStrip.Text);
                    iText.Layout.Element.Link link = new iText.Layout.Element.Link(label.Text, annotation);
                    style.SetFont(PdfFontFactory.CreateFont("C:\\Windows\\Fonts\\times.ttf", "Cp1251", true));
                    style.SetFontSize(label.Font.Size);
                    if (label.Font.Bold) style.SetBold();
                    if (label.Font.Italic) style.SetItalic();
                    style.SetUnderline();
                    if (label.Font.Strikeout) style.SetLineThrough();
                    style.SetFontColor(ColorConstants.BLUE);
                    header.Add(link.AddStyle(style));
                    #region Анотация, которую я так и не смог сделать
                    /*
                    // Creating a PdfTextMarkupAnnotation object       
                    rect = new Rectangle(35, 785, 0, 0);
                    float[] floatArray = new float[] { 35, 785, 45, 785, 35, 795, 45, 795 };
                    PdfAnnotation Annotation = PdfTextMarkupAnnotation.CreateUnderline(rect, floatArray);

                    // Setting color to the annotation       
                    Annotation.SetColor(ColorConstants.BLUE);

                    // Setting title to the annotation       
                    Annotation.SetTitle(new PdfString(label.ContextMenuStrip.Text));

                    // Setting contents to the annotation       
                    Annotation.SetContents(" ");

                    // Creating a new Pdfpage
                    PdfPage pdfPage = pdf.AddNewPage();

                    // Adding annotation to a page in a PDF       
                    pdfPage.AddAnnotation(Annotation);
                    */
                    #endregion
                }
                else
                {
                    ImageText.ImageTextControl picture = (control.Controls[k] as ImageText.ImageTextControl);
                    CreateImage(document, picture, true, ref header, border, k+1==control.Controls.Count);
                }
            }
        }

        private static void CreateImage(object document, ImageText.ImageTextControl imgControl, bool isLinked, ref Paragraph header, bool border, bool isLast)
        {
            iText.Layout.Element.Image img = new iText.Layout.Element.Image(ImageDataFactory.Create((imgControl.Controls[0] as PictureBox).Image, System.Drawing.Color.White)).SetTextAlignment(TextAlignment.LEFT);
            if (isLinked) img.SetAction(PdfAction.CreateURI((imgControl.Controls[0] as PictureBox).Name));
            if (border) { img.SetBorder(new SolidBorder(ColorConstants.GRAY, 4)); img.SetHorizontalAlignment(iText.Layout.Properties.HorizontalAlignment.CENTER); }
            if (document.GetType() == typeof(Document))
            {
                (document as Document).Add(header);
                (document as Document).Add(img);
            }
            else
            {
                if (document.GetType() == typeof(Cell))
                {
                    (document as Cell).Add(header);
                    (document as Cell).Add(img);
                }
                else
                {
                    if (document.GetType() == typeof(ListItem))
                    {
                        (document as ListItem).Add(header);
                        (document as ListItem).Add(img);
                    }
                }
            }
            header = new Paragraph();
            header.SetTextAlignment(TextAlignment.CENTER);
            for (int i = 1; i < imgControl.Controls.Count; i++)
            {
                Style style = new Style();
                if (imgControl.Controls[i].GetType() == typeof(Label))
                {
                    Label label = imgControl.Controls[i] as Label;
                    style.SetFont(PdfFontFactory.CreateFont("C:\\Windows\\Fonts\\times.ttf", "Cp1251", true));
                    style.SetFontSize(label.Font.Size);
                    if (label.Font.Bold) style.SetBold();
                    if (label.Font.Italic) style.SetItalic();
                    if (label.Font.Underline) style.SetUnderline();
                    if (label.Font.Strikeout) style.SetLineThrough();
                    header.Add(new Text(label.Text).AddStyle(style));
                }
                else
                {
                    if (imgControl.Controls[i].GetType() == typeof(Link.LinkControl))
                    {
                        CreateLink(document, imgControl.Controls[i] as Link.LinkControl, header, border);
                    }
                }
            }
            if (document.GetType() == typeof(Document)) (document as Document).Add(header);
            else
            {
                if (document.GetType() == typeof(Cell)) (document as Cell).Add(header);
                else
                {
                    if (document.GetType() == typeof(ListItem)) (document as ListItem).Add(header);
                }
            }
            header = new Paragraph();
            if (border && !isLast)
            {
                header.SetBorderLeft(new SolidBorder(ColorConstants.GRAY, 4));
                header.SetPadding(30);
            }
        }


        private static void CreateCode(Label label, Paragraph header)
        {
            Style style = new Style();
            style.SetFont(PdfFontFactory.CreateFont("C:\\Windows\\Fonts\\consola.ttf", "Cp1251", true));
            style.SetFontSize(label.Font.Size);
            style.SetBackgroundColor(ColorConstants.LIGHT_GRAY);
            style.SetFontColor(ColorConstants.DARK_GRAY);
            header.Add(new Text(label.Text).AddStyle(style));
        }


        private static List GetList(int[] space, ListLine.ListLineControl value, ref int now)
        {
            List list = new List();
            list.SetListSymbol("       ");
            for (int i = 0; now < space.Length; now++, i++)
            {
                if ( i== 0 || space[now] == space[now - 1])
                {
                    ListItem item = new ListItem();
                    Paragraph header = new Paragraph();
                    header.Add((value.Controls[now * 2 + 1] as Label).Text);
                    SimpleLines.SimpleLinesControl simpleLines = value.Controls[now * 2] as SimpleLines.SimpleLinesControl;
                    int y = 5;
                    Style style = new Style();
                    for (int a = 0; a < simpleLines.Controls.Count; a++)
                    {
                        if (y < simpleLines.Controls[a].Location.Y)
                        {
                            y = simpleLines.Controls[a].Location.Y;
                            header.Add("\r\n");
                        }
                        if (simpleLines.Controls[a].GetType() == typeof(SimpleInLineText.SimpleInLineTextControl))
                        {
                            SimpleInLineText.SimpleInLineTextControl control = simpleLines.Controls[a] as SimpleInLineText.SimpleInLineTextControl;
                            CreateText(control, header);
                        }
                        else
                        {
                            if (simpleLines.Controls[a].GetType() == typeof(Link.LinkControl))
                            {
                                Link.LinkControl control = simpleLines.Controls[a] as Link.LinkControl;
                                CreateLink(item, control, header, false);
                            }
                            else
                            {
                                if (simpleLines.Controls[a].GetType() == typeof(ImageText.ImageTextControl))
                                {
                                    CreateImage(item, simpleLines.Controls[a] as ImageText.ImageTextControl, false, ref header, false, true);
                                }
                                else
                                {
                                    if (simpleLines.Controls[a].GetType() == typeof(Label))
                                    {
                                        CreateCode(simpleLines.Controls[a] as Label, header);
                                    }
                                }
                            }
                        }
                    }
                    item.Add(header);
                    list.Add(item);
                }
                else
                {
                    if (space[now] > space[now - 1])
                    {
                        int memory = now;
                        ListItem item = new ListItem();
                        item.Add(GetList(space, value, ref now));
                        if (space[now + 1] == space[memory-1]) i = -1;
                        list.Add(item);
                    }
                    else
                    {
                        now--;
                        return list;
                    }
                } 
            }
            return list;
            
        }

    }
}
