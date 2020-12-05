using iText.IO.Image;
using iText.IO.Font;
using iText.Kernel.Colors;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Action;
using iText.Kernel.Pdf.Annot;
using iText.Kernel.Font;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace MIND.Library
{
    static class PDF_Creator
    {

        public static void Create(List<LinesText> value)
        {
            PdfWriter writer = new PdfWriter(Directory.GetCurrentDirectory() + "\\demo.pdf");
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
                        Style style1 = new Style();
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
                                CreateLink(document, control, header);
                            }
                            else
                            {
                                if (simpleLines.value.Controls[j].GetType() == typeof(ImageText.ImageTextControl))
                                {
                                    CreateImage(document, simpleLines.value.Controls[j] as ImageText.ImageTextControl, false, ref header);
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
            }
            document.Close();
        }


        private static void CreateText(SimpleInLineText.SimpleInLineTextControl control, Paragraph header)
        {
            for (int k = 0; k < control.Controls.Count; k++)
            {
                Label label = control.Controls[k] as Label;
                Style style = new Style();
                style.SetFont(PdfFontFactory.CreateFont("C:\\Windows\\Fonts\\times.ttf", "Cp1251", true));
                style.SetFontSize(14.25f);
                if (label.Font.Bold) style.SetBold();
                if (label.Font.Italic) style.SetItalic();
                if (label.Font.Underline) style.SetUnderline();
                if (label.Font.Strikeout) style.SetLineThrough();
                header.Add(new Text(label.Text).AddStyle(style));
            }
        }

        private static void CreateLink(Document document, Link.LinkControl control, Paragraph header)
        {
            for (int k = 0; k < control.Controls.Count; k++)
            {
                if (control.Controls[k].GetType() == typeof(LinkLabel))
                {
                    LinkLabel label = control.Controls[k] as LinkLabel;
                    Rectangle rect = new Rectangle(0, 0);
                    PdfLinkAnnotation annotation = new PdfLinkAnnotation(rect);
                    PdfAction action = PdfAction.CreateURI(label.Links[0].LinkData.ToString());
                    annotation.SetAction(action);
                    annotation.SetContents(label.ContextMenuStrip.Text);
                    iText.Layout.Element.Link link = new iText.Layout.Element.Link(label.Text, annotation);
                    Style style = new Style();
                    style.SetFont(PdfFontFactory.CreateFont("C:\\Windows\\Fonts\\times.ttf", "Cp1251", true));
                    style.SetFontSize(14.25f);
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
                    CreateImage(document, picture, true, ref header);
                }
            }
        }

        private static void CreateImage(Document document, ImageText.ImageTextControl imgControl, bool isLinked, ref Paragraph header)
        {
            document.Add(header);
            Image img = new Image(ImageDataFactory.Create((imgControl.Controls[0] as PictureBox).Image, System.Drawing.Color.White)).SetTextAlignment(TextAlignment.LEFT);
            if(isLinked)img.SetAction(PdfAction.CreateURI((imgControl.Controls[0] as PictureBox).Name));
            document.Add(img);
            header = new Paragraph();
            for (int i = 1; i < imgControl.Controls.Count; i++)
            {
                if(imgControl.Controls[i].GetType() == typeof(Label))
                {
                    Label label = imgControl.Controls[i] as Label;
                    Style style = new Style();
                    style.SetFont(PdfFontFactory.CreateFont("C:\\Windows\\Fonts\\times.ttf", "Cp1251", true));
                    style.SetFontSize(14.25f);
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
                        CreateLink(document, imgControl.Controls[i] as Link.LinkControl, header);
                    }
                }
            }
        }


        private static void CreateCode(Label label, Paragraph header)
        {
            Style style = new Style();
            style.SetFont(PdfFontFactory.CreateFont("C:\\Windows\\Fonts\\consola.ttf", "Cp1251", true));
            style.SetFontSize(14.25f);
            style.SetBackgroundColor(ColorConstants.LIGHT_GRAY);
            style.SetFontColor(ColorConstants.DARK_GRAY);
            header.Add(new Text(label.Text).AddStyle(style));
        }

    }
}
