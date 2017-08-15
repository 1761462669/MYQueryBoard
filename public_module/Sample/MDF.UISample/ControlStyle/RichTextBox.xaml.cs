using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace MDF.UISample.ControlStyle
{
    public partial class RichTextBoxtEMP : UserControl
    {
        public RichTextBoxtEMP()
        {
            InitializeComponent();

            RichTextBox C;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string s= "<Section xml:space=\"preserve\" HasTrailingParagraphBreakOnPaste=\"False\" xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\"><Paragraph FontSize=\"11\" FontFamily=\"Portable User Interface\" Foreground=\"#FF000000\" FontWeight=\"Normal\" FontStyle=\"Normal\" FontStretch=\"Normal\" CharacterSpacing=\"0\" Typography.AnnotationAlternates=\"0\" Typography.EastAsianExpertForms=\"False\" Typography.EastAsianLanguage=\"Normal\" Typography.EastAsianWidths=\"Normal\" Typography.StandardLigatures=\"True\" Typography.ContextualLigatures=\"True\" Typography.DiscretionaryLigatures=\"False\" Typography.HistoricalLigatures=\"False\" Typography.StandardSwashes=\"0\" Typography.ContextualSwashes=\"0\" Typography.ContextualAlternates=\"True\" Typography.StylisticAlternates=\"0\" Typography.StylisticSet1=\"False\" Typography.StylisticSet2=\"False\" Typography.StylisticSet3=\"False\" Typography.StylisticSet4=\"False\" Typography.StylisticSet5=\"False\" Typography.StylisticSet6=\"False\" Typography.StylisticSet7=\"False\" Typography.StylisticSet8=\"False\" Typography.StylisticSet9=\"False\" Typography.StylisticSet10=\"False\" Typography.StylisticSet11=\"False\" Typography.StylisticSet12=\"False\" Typography.StylisticSet13=\"False\" Typography.StylisticSet14=\"False\" Typography.StylisticSet15=\"False\" Typography.StylisticSet16=\"False\" Typography.StylisticSet17=\"False\" Typography.StylisticSet18=\"False\" Typography.StylisticSet19=\"False\" Typography.StylisticSet20=\"False\" Typography.Capitals=\"Normal\" Typography.CapitalSpacing=\"False\" Typography.Kerning=\"True\" Typography.CaseSensitiveForms=\"False\" Typography.HistoricalForms=\"False\" Typography.Fraction=\"Normal\" Typography.NumeralStyle=\"Normal\" Typography.NumeralAlignment=\"Normal\" Typography.SlashedZero=\"False\" Typography.MathematicalGreek=\"False\" Typography.Variants=\"Normal\" TextOptions.TextHintingMode=\"Fixed\" TextOptions.TextFormattingMode=\"Ideal\" TextOptions.TextRenderingMode=\"Auto\" TextAlignment=\"Left\" LineHeight=\"0\" LineStackingStrategy=\"MaxHeight\"><Run>fffffffffffffffffffffffffff</Run></Paragraph></Section>";

            rbtMyRichTextBox.Xaml = s;
        }
    }
}
