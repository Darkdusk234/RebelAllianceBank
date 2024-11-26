using System;

namespace RebelAllianceBank.utils;

public class SelectOneOrMore
{
    private List<string> _ColumnHeders;
    private List<string> _Body;
    private bool _isSelected = false;
    private int[] _SelectedOption = [];

    public SelectOneOrMore(string[] columnHeders, List<string> body)
    {
        _ColumnHeders = columnHeders.ToList();
        _Body = body;

        _ColumnHeders.Insert(0, "Vald");
    }

    public int[] Show()
    {
        Markdown.Heder(Enums.HeaderLevel.Header2, "Tryck på upp eller ner för att navigera. Välj med mellanslag och bekrefta med enter");
        (int Left, int Top) = Console.GetCursorPosition();

        while (!_isSelected)
        {
            for (int i = 0; i < _Body.Count; i++)
            {
                Console.SetCursorPosition(Left, Top + i);
                if (_SelectedOption.Contains(i))
                {
                    
                }
            }

            Markdown.Table(_ColumnHeders.ToArray(), _Body);
        }

        return _SelectedOption;
    }
}
