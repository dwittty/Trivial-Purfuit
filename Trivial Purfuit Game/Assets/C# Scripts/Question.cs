using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Category
{
    RED, GREEN, BLUE, WHITE
}

public readonly struct Question
{

    public Category Category { get; }
    public string Prompt { get; }
    public string Correct { get; }
    public string Wrong1 { get; }
    public string Wrong2 { get; }
    public string Wrong3 { get; }

    public Question(Category category, string prompt, string correct, string wrong1, string wrong2, string wrong3)
    {
        Category = category;
        Prompt = prompt;
        Correct = correct;
        Wrong1 = wrong1;
        Wrong2 = wrong2;
        Wrong3 = wrong3;
    }
}
