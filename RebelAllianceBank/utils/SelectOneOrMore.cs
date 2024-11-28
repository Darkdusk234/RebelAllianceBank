
namespace RebelAllianceBank.utils;

public class SelectOneOrMore
{
    private List<string> _ColumnHeders;
    private List<string> _Body;
    private bool _isSelected = false;
    private List<int> _SelectedOption = [];
    private int _currentSelected = 0;

    public SelectOneOrMore(string[] columnHeders, List<string> body)
    {
        _ColumnHeders = columnHeders.ToList();
        _Body = body;

        _ColumnHeders.Insert(0, "Vald");

        List<string> tempBody = new List<string>();
        for (int i = 0; i < _Body.Count; i++)
        {
            if ((i % (_ColumnHeders.Count - 1)) == 0)
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

    public int[] Show(int maxAllowedSelected)
    {
        Console.Clear();
        (int Left, int Top) = Console.GetCursorPosition();


        var rows = _Body.Chunk(_ColumnHeders.Count).ToArray();
        int rowCount = rows.GetUpperBound(0);

        while (!_isSelected)
        {
            for (int i = 0; i <= rowCount; i++)
            {
                rows[i][0] = _SelectedOption.Contains(i) ? "[x]" : "[ ]";
            }
            _Body = rows.SelectMany(row => row).ToList();


            Console.SetCursorPosition(Left, Top);
            Markdown.Heder(Enums.HeaderLevel.Header2, "Tryck på upp eller ner för att navigera. Välj med mellanslag och bekrefta med enter");
            Markdown.Table(_ColumnHeders.ToArray(), _Body);
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
                    toggleInput(_SelectedOption, _currentSelected);
                    break;
                case ConsoleKey.Enter:
                    _isSelected = true;
                    break;
            }
        }


        return _SelectedOption.ToArray();
    }

    private List<int> toggleInput(List<int> AllSelected, int selectedIndex)
    {
        int index = AllSelected.FindIndex(selected => selected == selectedIndex);

        if (index != -1)
        {
            AllSelected.RemoveAt(index);
        }
        else
        {
            AllSelected.Add(selectedIndex);
        }

        return AllSelected;

    }
}