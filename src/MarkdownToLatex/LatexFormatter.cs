using System.Diagnostics;
using System.Text;
using Microsoft.Toolkit.Parsers.Markdown;
using Microsoft.Toolkit.Parsers.Markdown.Blocks;


public class LatexFormatter
{
  public string FormatParagraph(ParagraphBlock paragraph)
  {
    return paragraph.ToString() + "\n\n";
  }

  public string FormatList(ListBlock list)
  {
    var latexList = new StringBuilder();

    var (begin, end) = list.Style switch
    {
      ListStyle.Bulleted => ("\\begin(itemize)", "\n\\end(itemize)\n\n"),
      ListStyle.Numbered => ("\\begin(enumerate)", "\n\\end(enumerate)\n\n"),
      _ => throw new UnreachableException()
    };

    latexList.Append(begin);

    foreach (var item in list.Items)
    {
      latexList.Append($"\n\t\\item {item.Blocks.First()}");
    }

    latexList.Append(end);

    return latexList.ToString();
  }

  public string FormatSection(HeaderBlock header)
  {
    var trimmed = header.ToString().Trim();
    return header.HeaderLevel switch
    {
      1 => $"\\section{{{trimmed}}}\n\n",
      2 => $"\\subsection{{{trimmed}}}\n\n",
      3 => $"\\subsubsection{{{trimmed}}}\n\n",
      _ => throw new UnreachableException()
    };
  }
}