
using RebelAllianceBank.Enums;

namespace RebelAllianceBank.utils;

public class SelectOneOrMore
{
    private List<string> _ColumnHeader;
    private List<string> _Body;

    public SelectOneOrMore(string[] columnHeders, List<string> body)
    {
        _ColumnHeader = columnHeders.ToList();
        _Body = body;

        _ColumnHeader.Insert(0, "Vald");

        List<string> tempBody = new List<string>();
        for (int i = 0; i < _Body.Count; i++)
        {
            if ((i % (_ColumnHeader.Count - 1)) == 0)
            {
                tempBody.Add("[ ]");
                tempBody.Add(_Body[i]);
            }
            else
            {
                tempBody.Add(_Body[i]);
            }
        }

        _Body = tempBody;
    }

    public int[] Show(int maxAllowedSelected = 1)
    {
        bool _isSelected = false;
        List<int> _SelectedOption = [];
        int _currentSelected = 0;

        (int Left, int Top) = Console.GetCursorPosition();


        var rows = _Body.Chunk(_ColumnHeader.Count).ToArray();
        int rowCount = rows.GetUpperBound(0);

        while (!_isSelected)
        {
            for (int i = 0; i <= rowCount; i++)
            {
                rows[i][0] = _SelectedOption.Contains(i) ? "[x]" : "[ ]";
            }
            _Body = rows.SelectMany(row => row).ToList();


            Console.SetCursorPosition(Left, Top);
            Markdown.Header(HeaderLevel.Header2, "Tryck på upp eller ner för att navigera. Välj med mellanslag och bekräfta med enter");
            Markdown.Table(_ColumnHeader.ToArray(), _Body);
            Console.WriteLine($"\nDu är på rad {TextColor.Yellow}{_currentSelected + 1}{TextColor.NORMAL}");

            var Key = Console.ReadKey().Key;

            switch (Key)
            {
                case ConsoleKey.UpArrow:
                    _currentSelected = _currentSelected <= 0 ? rowCount : _currentSelected - 1;
                    break;
                case ConsoleKey.DownArrow:
                    _currentSelected = _currentSelected < rowCount ? _currentSelected + 1 : _currentSelected = 0;
                    break;
                case ConsoleKey.Spacebar:
                    toggleInput(_SelectedOption, _currentSelected, maxAllowedSelected);
                    break;
                case ConsoleKey.Enter:
                    _isSelected = true;
                    break;
            }
        }


        return _SelectedOption.ToArray();
    }

    private List<int> toggleInput(List<int> AllSelected, int selectedIndex, int maxAllowedSelected)
    {
        int index = AllSelected.FindIndex(selected => selected == selectedIndex);


        if (index != -1)
        {
            AllSelected.RemoveAt(index);
        }

        if (AllSelected.Count < maxAllowedSelected && index == -1)
        {
            AllSelected.Add(selectedIndex);
        }

        return AllSelected;

    }
}