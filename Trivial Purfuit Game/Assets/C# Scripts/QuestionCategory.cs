using System.Collections.Generic;
using UnityEngine;

public class QuestionCategory
{
    private static System.Random random = new System.Random();

    public Category Category { get; }
    private List<Question> Pool;
    private int Index;

    public QuestionCategory(Category category)
    {
        Category = category;
        Pool = new List<Question>();
        Index = 0;
    }

    // Initialize a question pool from a list of questions
    // Only questions in the list that match the given Category
    public QuestionCategory(Category category, List<Question> questions) :
        this(category)
    {
        foreach(Question q in questions)
        {
            AddQuestion(q);
        }

        Shuffle();
    }

    public void AddQuestion(Question question)
    {
        if(question.Category == Category)
        {
            Pool.Add(question);
        }
    }

    // Shuffle every question in the pool and reset to the first element
    public void Shuffle()
    {
        int size = Pool.Count;
        while (size > 1)
        {
            size--;
            int k = random.Next(size + 1);
            Question value = Pool[k];
            Pool[k] = Pool[size];
            Pool[size] = value;
        }

        Index = 0;
    }

    public Question Draw()
    {
        Question q = Pool[Index++];
        if(Index == Pool.Count)
        {
            Shuffle();
        }
        return q;
    }

}
