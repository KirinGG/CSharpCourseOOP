using System;
using System.IO;

namespace FileConvert
{
    class FileCsvConvertToHtml
    {
        static void Main(string[] args)
        {
            if (args.Length != 2)
            {
                throw new ArgumentException("Необходимо указать параметры: [имя входного файла.csv] [имя выходного файла.html]");
            }

            string inputFilePath = args[0];
            string outFilePath = args[1];

            if (!File.Exists(inputFilePath))
            {
                throw new ArgumentException("Не найден входной файл.", nameof(inputFilePath));
            }

            using (StreamReader reader = new StreamReader(inputFilePath))
            {
                using (StreamWriter writer = new StreamWriter(outFilePath))
                {
                    string currentLine;
                    int quotesCount = 0;

                    writer.WriteLine(GetHtmlDocumentHeader());

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

                                writer.Write(GetHtmlTagTableDetail(i, currentLine));

                                if (i == currentLine.Length - 1)
                                {
                                    writer.Write(GetHtmlTagTableRow(false));
                                }
                            }
                            else if (currentLine[i] == '"' && i == 0)
                            {
                                writer.Write(GetHtmlTagTableRow(true));
                                writer.Write(GetHtmlTagTableDetail(i, currentLine));
                            }
                            else if (i == (currentLine.Length - 1) && (quotesCount % 2 != 0))
                            {
                                writer.Write(GetHtmlTagBreakRow());
                            }
                            else
                            {
                                writer.Write(ReplaceSpecialCharacters(currentLine[i]));
                            }
                        }
                    }

                    writer.WriteLine(GetHtmlDocumentFooter());
                }
            }
        }

        public static string ReplaceSpecialCharacters(char character)
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

        public static string GetHtmlDocumentHeader()
        {
            return string.Join(Environment.NewLine,
                "<!doctype html>",
                "<html>",
                "<head>",
                "<meta charset='utf-8'/>",
                "<title>Csv to Html</title>",
                "</head>",
                "<body>",
                "<table>");
        }

        public static string GetHtmlDocumentFooter()
        {
            return string.Join(Environment.NewLine,
                "</table>",
                "</body>",
                "</html>");
        }

        public static string GetHtmlTagTableDetail(int i, string currentLine)
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

        public static string GetHtmlTagTableRow(bool isOpenTag)
        {
            if (isOpenTag)
            {
                return $"<tr>{Environment.NewLine}";
            }

            return $"{Environment.NewLine}</tr>{Environment.NewLine}";
        }

        public static string GetHtmlTagBreakRow()
        {
            return "<br>";
        }
    }
}
