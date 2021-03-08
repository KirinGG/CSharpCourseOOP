using System;

namespace FileCsvConvertToHtml
{
    class HtmlDocument
    {
        private IDataProvider dataProvider;
        private IDataReceiver dataReceiver;

        public HtmlDocument(IDataProvider dataProvider, IDataReceiver dataReceiver)
        {
            this.dataProvider = dataProvider;
            this.dataReceiver = dataReceiver;
        }

        public HtmlDocument CreateHeader()
        {
            dataReceiver.Append(GetHeader());

            return this;
        }

        private string GetHeader()
        {
            return string.Join(Environment.NewLine,
                "<!DOCTYPE html>",
                "<html>",
                "<head>",
                "<meta charset=\"utf-8\"/>",
                "<title>Csv to Html</title>",
                "</head>",
                "<body>",
                "<table>");
        }

        public HtmlDocument CreateBody()
        {
            string currentLine;
            int quotesCount = 0;

            while ((currentLine = dataProvider.Next()) != null)
            {
                currentLine = currentLine.Replace("\"\"\"\"", "|");

                for (int i = 0; i < currentLine.Length; i++)
                {
                    if (currentLine[i] == '"')
                    {
                        quotesCount++;
                    }

                    if (((currentLine[i] == ',') || (i == currentLine.Length - 1) || (i == 0)) && (quotesCount % 2 == 0))
                    {
                        if (i == 0)
                        {
                            dataReceiver.Append(GetHtmlTagTableRow(true));
                        }

                        dataReceiver.Append(GetHtmlTagTableDetail(i, currentLine, quotesCount));

                        if (i == currentLine.Length - 1)
                        {
                            dataReceiver.Append(GetHtmlTagTableRow(false));
                        }
                    }
                    else if (currentLine[i] == '"' && i == 0)
                    {
                        dataReceiver.Append(GetHtmlTagTableRow(true));
                        dataReceiver.Append(GetHtmlTagTableDetail(i, currentLine, quotesCount));
                    }
                    else if (i == (currentLine.Length - 1) && (quotesCount % 2 != 0))
                    {
                        dataReceiver.Append(GetHtmlTagBreakRow());
                    }
                    else
                    {
                        dataReceiver.Append(ReplaceSpecialCharacters(currentLine[i]));
                    }
                }
            }

            return this;
        }

        public HtmlDocument CreateFooter()
        {
            dataReceiver.Append(GetFooter());

            return this;
        }

        private string GetFooter()
        {
            return string.Join(Environment.NewLine,
                       "</table>",
                       "</body>",
                       "</html>");
        }

        private string ReplaceSpecialCharacters(char character)
        {
            string result;

            switch (character)
            {
                case '&':
                    result = "&amp;";
                    break;
                case '<':
                    result = "&lt;";
                    break;
                case '>':
                    result = "&gt;";
                    break;
                case '|':
                    result = "&quot;";
                    break;
                default:
                    result = character.ToString();
                    break;
            }

            return result;
        }

        private string GetHtmlTagTableDetail(int i, string currentLine, int quoteCount)
        {
            if (i == 0)
            {
                if (currentLine[i] == ',')
                {
                    return "<td></td><td>";
                }

                return $"<td>{ReplaceSpecialCharacters(currentLine[i])}";
            }

            if (i == currentLine.Length - 1)
            {
                if (currentLine[i] == ',')
                {
                    return "</td><td></td>";
                }

                return $"{ReplaceSpecialCharacters(currentLine[i])}</td>";
            }

            return "</td><td>";
        }

        private string GetHtmlTagTableRow(bool isOpenTag)
        {
            if (isOpenTag)
            {
                return $"<tr>{Environment.NewLine}";
            }

            return $"{Environment.NewLine}</tr>{Environment.NewLine}";
        }

        private string GetHtmlTagBreakRow()
        {
            return "<br>";
        }

        public void Close()
        {
            dataProvider.Close();
            dataReceiver.Close();
        }
    }
}

