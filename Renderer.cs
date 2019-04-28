using System;
using System.Xml.Linq;

namespace Info.Obak.Sudoku
{
    public enum DifficulityLevel
    {
        Easy,
        Medium,
        Hard
    }

    public static class Renderer
    {
        public static string ToHtml(Sudoku sudoku, DifficulityLevel level)
        {
            var html = new XElement("html");
            var head = new XElement("head");
            var style = new XElement("style", css);
            var body = new XElement("body");
            var table = new XElement("table");
            html.Add(head);
            head.Add(style);
            html.Add(body);
            body.Add(table);

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
                    
                    rowX.Add(new XElement("td", value, 
                        new XAttribute("class", clazz),
                        new XAttribute("id", "cell_"+x+"_"+y)));
                }
            }

            return html.ToString();
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
