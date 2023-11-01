using System.Text;
using Microsoft.Toolkit.Parsers.Markdown;
using Microsoft.Toolkit.Parsers.Markdown.Blocks;

var path = Path.Combine(Directory.GetCurrentDirectory(), "examples/example.md");
var md = File.ReadAllText(path);

var document = new MarkdownDocument();
document.Parse(md);

var formatter = new LatexFormatter();
var latex = new StringBuilder();

foreach (var element in document.Blocks)
{
  var l = element switch
  {
    HeaderBlock header => formatter.FormatSection(header),
    ListBlock list => formatter.FormatList(list),
    ParagraphBlock paragraph => formatter.FormatParagraph(paragraph),
    _ => string.Empty
  };

  latex.Append(l);
}

var latexPath = Path.Combine(Directory.GetCurrentDirectory(), "examples/example.tex");
File.WriteAllText(latexPath, latex.ToString());

