using System;
using System.Xml.Linq;

namespace Info.Obak.Sudoku
{
    public enum DifficulityLevel
    {
        Easy = 34,
        Medium = 31,
        Hard = 28
    }

    public static class Renderer
    {
        public static string ToHtml(XElement [] parts)
        {
            var html = new XElement("html");
            var head = new XElement("head");
            var style = new XElement("style", css);
            var body = new XElement("body");
            html.Add(head);
            head.Add(style);
            html.Add(body);
            body.Add(parts);
            return html.ToString();
        }

        public static XElement ToHtmlPart(Sudoku sudoku, DifficulityLevel level)
        {
            var div = new XElement("div");
            var table = new XElement("table");
            div.Add(table);
            var footer = new XElement("p", level.ToString() + " #" + sudoku.Seed);

            div.Add(
                table,
                footer);
            int hidden = 0;
            var deck = GetDeck(sudoku.CellCount, (int)level);
            for (int y=0; y<sudoku.RowCount; y++)
            {
                var row = sudoku.GetRow(y);
                var trclazz = "top1";
                if (y > 0 && (y % sudoku.Size) == 0)
                {
                    trclazz = "top2";
                }
                var rowX = new XElement("tr", new XAttribute("class", trclazz));
                table.Add(rowX);
                for (int x=0; x<row.Values.Count; x++)
                {
                    var clazz = "left1"; 
                    if (x>0 && (x%sudoku.Size)==0)
                    {
                        clazz = "left2";
                    }
                    var value = "0123456789ABCDEFGHIJKLMNOPQRST"[row.Values[x]];

                    int idx = y * sudoku.RowCount + x;

                    if (deck[idx] == 0)
                    {
                        hidden++;
                        value = ' ';
                    }
                    
                    rowX.Add(new XElement("td", value, 
                        new XAttribute("class", clazz),
                        new XAttribute("id", "cell_"+x+"_"+y)));
                }
            }
            var given = sudoku.CellCount - hidden;
            footer.Value += " "+ given + "/" + sudoku.CellCount;

            return div;
        }

        private static int[] GetDeck(int length, int ones)
        {
            Random random = new Random();
            var list = new int[length];
            for (int i=0; i<length; i++)
            {
                list[i] = (i < ones) ? 1 : 0;
            }
            for (int i=0; i<length; i++)
            {
                var idx = random.Next(i, length);
                var tmp = list[idx];
                list[idx] = list[i];
                list[i] = tmp;
            }
            return list;
        }

        private const string css = @"
table {
  border: 2px solid black;
  border-collapse:collapse;
}
.top1 {
  border-top: #DDDDEE solid 1px;
}
.left1 {
  border-left: #DDDDEE solid 1px;
}
.left2 {
  border-left: black solid 2px;
}
.top2 {
  border-top: black solid 2px;
}
td {
    width: 24pt;
    height: 25pt;
    vertical-align: middle;
    text-align: center;
    font-size: 18pt;
    font-family: times,serif;
}
";
    }
}
