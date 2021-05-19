using System;
using System.IO;

namespace FileCsvConvertToHtml
{
    class Converter
    {
        private readonly StreamReader reader;
        private readonly StreamWriter writer;

        public Converter(StreamReader reader, StreamWriter writer)
        {
            this.reader = reader;
            this.writer = writer;
        }

        private void CreateHeader()
        {
            writer.Write(GetHeader());
        }

        public void ConvertCsvToHtml()
        {
            CreateHeader();
            CreateBody();
            CreateFooter();
        }

        private static string GetHeader()
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

        private void CreateBody()
        {
            string currentLine;
            int quotesCount = 0;
            char previousCharacter = ' ';

            while ((currentLine = reader.ReadLine()) != null)
            {
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
                            writer.Write(GetHtmlTagTableRow(true));
                        }

                        writer.Write(GetHtmlTagTableDetail(i, currentLine, quotesCount));

                        if (i == currentLine.Length - 1)
                        {
                            writer.Write(GetHtmlTagTableRow(false));
                        }
                    }
                    else if (currentLine[i] == '"' && i == 0)
                    {
                        writer.Write(GetHtmlTagTableRow(true));
                        writer.Write(GetHtmlTagTableDetail(i, currentLine, quotesCount));
                    }
                    else if (i == (currentLine.Length - 1) && (quotesCount % 2 != 0))
                    {
                        writer.Write(GetHtmlTagBreakRow());
                    }
                    else
                    {
                        bool isPrintableQuotation = (currentLine[i] == '"') && (previousCharacter == '"') && (quotesCount % 2 != 0);

                        if (currentLine[i] != '"' || isPrintableQuotation)
                        {
                            writer.Write(ReplaceSpecialCharacters(currentLine[i]));
                        }
                    }

                    previousCharacter = currentLine[i];
                }
            }
        }

        private void CreateFooter()
        {
            writer.WriteLine(GetFooter());
        }

        private static string GetFooter()
        {
            return string.Join(Environment.NewLine,
                       "</table>",
                       "</body>",
                       "</html>");
        }

        private static string ReplaceSpecialCharacters(char character)
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
                default:
                    result = character.ToString();
                    break;
            }

            return result;
        }

        private static string GetHtmlTagTableDetail(int i, string currentLine, int quoteCount)
        {
            if (i == 0)
            {
                if (currentLine[i] == ',')
                {
                    return "<td></td><td>";
                }

                if (currentLine[i] == '"')
                {
                    return "<td>";
                }

                return $"<td>{ReplaceSpecialCharacters(currentLine[i])}";
            }

            if (i == currentLine.Length - 1)
            {
                if (currentLine[i] == ',')
                {
                    return "</td><td></td>";
                }

                if (currentLine[i] == '"')
                {
                    return "</td>";
                }

                return $"{ReplaceSpecialCharacters(currentLine[i])}</td>";
            }

            return "</td><td>";
        }

        private static string GetHtmlTagTableRow(bool isOpenTag)
        {
            if (isOpenTag)
            {
                return $"<tr>{Environment.NewLine}";
            }

            return $"{Environment.NewLine}</tr>{Environment.NewLine}";
        }

        private static string GetHtmlTagBreakRow()
        {
            return "<br>";
        }
    }
}

