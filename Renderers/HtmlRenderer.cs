using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Info.Obak.Sudoku.Renderers
{

    public class HtmlRenderer : BaseRenderer, IRenderer
    {
        public string Render(IEnumerable<Sudoku> sudokus, DifficulityLevel level)
        {
            var html = new XElement("html");
            var head = new XElement("head",
                new XElement("link",
                    new XAttribute("rel", "stylesheet"),
                    new XAttribute("href", "https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/css/bootstrap.min.css")));            

            var style = new XElement("style", "");
            var body = new XElement("body");
            html.Add(head);
            head.Add(style);
            html.Add(body);
            var container = new XElement("container");
            body.Add(container);
            int idx = 0;
            XElement row = new XElement("div", new XAttribute("class", "row sudokuRow"));
            container.Add(row);
            int rowCount = -1;
            foreach (var sudoku in sudokus)
            {
                if (rowCount <0)
                {
                    rowCount = sudoku.RowCount;
                }
                if ((idx++ % 2)==0)
                {
                    //row = new XElement("div", new XAttribute("class", "row"));
                    //container.Add(row);
                }
                row.Add(ToHtmlPart(sudoku, level, "col sudokuCol"));
            }

            int minWidth = (rowCount * 300 / 9);
            int height = minWidth + 50;            
            var myCss = css
                .Replace("#.sudokuCol_min-width#", minWidth + "px")
                .Replace("#.sudokuCol-height#", height + "px");
            style.Value = myCss;
            return html.ToString();
        }

        public XElement ToHtmlPart(Sudoku sudoku, DifficulityLevel level, string mainClazz)
        {            
            var div = new XElement("div", new XAttribute("class", mainClazz));
            var table = new XElement("table");            
            var footer = new XElement("p", level.ToString() + " #" + sudoku.Seed);

            div.Add(
                table,
                footer);

            (var deck, var given) = GetDeck(sudoku, level);
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
                        // hidden
                        value = ' ';
                    }                    
                    rowX.Add(new XElement("td", value, 
                        new XAttribute("class", clazz),
                        new XAttribute("id", "cell_"+x+"_"+y)));
                }
            }            
            footer.Value += " "+ given + "/" + sudoku.CellCount;

            return div;
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
.sudokuCol {
    min-width: #.sudokuCol_min-width#;    
    height: #.sudokuCol-height#;    
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
